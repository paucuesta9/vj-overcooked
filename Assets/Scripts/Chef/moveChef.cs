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
    Vector3 initialPosmirada;

    public GameObject mirada;
    double pos;
    bool growing;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        vectorPlayer = lastVector = new Vector3(1, -1.0f, 1);
        initialPosmirada = mirada.transform.position;
        pos = 0.0;
        growing = true;
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
        if (vectorPlayer != new Vector3(0.0f, -1.0f, 0.0f))
        {
            if (growing)
            {
                pos += 1.0 / 2.0 * Time.deltaTime;
                if (pos > 0.2)
                {
                    pos = 0.2f;
                    growing = false;
                }
            }
            else
            {
                pos -= 1.0 / 2 * Time.deltaTime;
                if (pos < -0.2)
                {
                    pos = -0.2f;
                    growing = true;
                }
            }
            double posx = Math.Sin(pos) / 6;
            mirada.transform.localPosition = new Vector3((float)posx, 0, 0.0397f);
            Instantiate(humo, transform.position + new Vector3(-0.1f, 0, 0), transform.rotation);
        }
        else
        {
            mirada.transform.localPosition = new Vector3(0, 0, 0.0397f);
            pos = 0.0;
        }
    }

    void FixedUpdate()
    {
        body.velocity = vectorPlayer;
    }
}
