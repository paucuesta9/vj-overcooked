using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class moveChef : MonoBehaviour
{

    public float speed = 10.0f;
    public float rotationSpeed = 300.0f;
    public GameObject menuSuperior;

    private Rigidbody body;

    private Vector3 vectorPlayer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (menuSuperior.GetComponent<Timer>().getTime() > 0) {
            vectorPlayer = new Vector3(Input.GetAxis("Horizontal") * 10f, -1.0f, Input.GetAxis("Vertical") * 10f);
            transform.LookAt(transform.position + new Vector3(vectorPlayer.x, 0, vectorPlayer.z));
        }
    }

    void FixedUpdate()
    {
        body.velocity = vectorPlayer;
    }
}
