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
    public Camera camara;
    public GameObject humo;

    private Rigidbody body;

    public Vector3 vectorPlayer, lastVector;
    public float lastPosX, lastPosY, posX, posY;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        vectorPlayer = lastVector = new Vector3(1, -1.0f, 1);
    }

    void Update()
    {
        if (menuSuperior.GetComponent<Timer>().getTime() > 0)
        {
            if (GlobalVariables.mouse)
            {
                Vector3 dir = Input.mousePosition - camara.WorldToScreenPoint(transform.position);
                vectorPlayer = new Vector3(dir.x, -1.0f, dir.y);
            }
            else
            {
                vectorPlayer = new Vector3(Input.GetAxis("Horizontal") * 10f, -1.0f, Input.GetAxis("Vertical") * 10f);
                transform.LookAt(transform.position + new Vector3(vectorPlayer.x, 0, vectorPlayer.z));
            }
        }
        if (vectorPlayer != lastVector)
        {
            Instantiate(humo, transform.position + new Vector3(-0.1f, 0, 0), transform.rotation);
        }
    }

    void FixedUpdate()
    {
        body.velocity = vectorPlayer;
    }
}
