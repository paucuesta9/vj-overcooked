using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class moveChef : MonoBehaviour
{

    public float speed = 10.0f;
    public float rotationSpeed = 300.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, 0.0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        if (Input.GetAxis("Vertical") >= 0.0f)
            transform.Rotate(0.0f, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0.0f);
        else
            transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0.0f);
    }

    void OnCollisionEnter(Collision collision)
    {

    }
}
