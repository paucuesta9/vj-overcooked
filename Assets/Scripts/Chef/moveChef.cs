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

    private Rigidbody body;

    public Vector3 vectorPlayer;
    public float lastPosX, lastPosY, posX, posY;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        lastPosX = Input.mousePosition.x;
        lastPosY = Input.mousePosition.y;
        posX = posY = 0;

    }

    void Update()
    {
        if (menuSuperior.GetComponent<Timer>().getTime() > 0) {
            if (GlobalVariables.mouse) {
                //if (Input.mousePosition.x - lastPosX != 0) posX = (Input.mousePosition.x - lastPosX) * 2.0f;
                //if (Input.mousePosition.y - lastPosY != 0) posY = (Input.mousePosition.y - lastPosY) * 2.0f;
                //lastPosX = Input.mousePosition.x;
                //lastPosY = Input.mousePosition.y;
                Vector3 dir = Input.mousePosition - camara.WorldToScreenPoint(transform.position);
                vectorPlayer = new Vector3(dir.x, -1.0f, dir.y);
            }
            else {
                vectorPlayer = new Vector3(Input.GetAxis("Horizontal") * 10f, -1.0f, Input.GetAxis("Vertical") * 10f);
                transform.LookAt(transform.position + new Vector3(vectorPlayer.x, 0, vectorPlayer.z));
                
            }
        }
    }

    void FixedUpdate()
    {
        body.velocity = vectorPlayer;
    }
}
