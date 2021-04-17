using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class moveChef : MonoBehaviour
{

    public float speed = 10.0f;
    public float rotationSpeed = 300.0f;

    private Rigidbody body;

    private Vector3 vectorPlayer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        /*
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, speed * Time.deltaTime);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, -speed * Time.deltaTime);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    GetComponent<Rigidbody>().AddForce(-speed * Time.deltaTime, 0.0f, 0.0f);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    GetComponent<Rigidbody>().AddForce(speed * Time.deltaTime, 0.0f, 0.0f);
                }*/
    //GetComponent<Rigidbody>().AddForce(Input.GetAxis("Vertical") * speed * Time.deltaTime, 0.0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);
    /*transform.Translate(0.0f, 0.0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);
    //if (Input.GetAxis("Vertical") >= 0.0f)
    //    transform.Rotate(0.0f, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0.0f);
    //else
    //    transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0.0f);
}
*/

    void Update()
    {
        vectorPlayer = new Vector3(Input.GetAxis("Horizontal") * 10f, body.velocity.y, Input.GetAxis("Vertical") * 10f);
        transform.LookAt(transform.position + new Vector3(vectorPlayer.x, 0, vectorPlayer.z));
    }

    void FixedUpdate()
    {
        body.velocity = vectorPlayer;
        //GetComponent<Rigidbody>().AddForce(flightspeed);
        /*
        var currentVelocity = GetComponent<Rigidbody>().velocity;

        if (currentVelocity.y <= 0f)
            return;

        currentVelocity.y = 0f;

        GetComponent<Rigidbody>().velocity = currentVelocity;
        */
    }
}
