using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cuchillo : MonoBehaviour
{
    public int typeCuchillo;
    public bool cut;
    double pos;
    Vector3 initialPos;
    Quaternion initialRot;
    void Start()
    {
        cut = false;
        initialPos = transform.position;
        initialRot = transform.rotation;
        pos = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cut)
        {
            if (pos == 0.0f)
            {
                if (typeCuchillo == 1)
                    transform.rotation = GameObject.Find("Pug").transform.rotation;
                else
                {
                    Quaternion rotationPug = GameObject.Find("Pug").transform.rotation;
                    transform.localEulerAngles = new Vector3(270.0f, 0.0f, 90.0f);
                }
            }
            pos += 20.0f * Time.deltaTime;
            if (typeCuchillo == 1)
            {
                double posz = Math.Sin(pos) / 4;

                transform.position = new Vector3(transform.position.x, transform.position.y, (float)posz + initialPos.z);
            }
            else
            {
                double posy = Math.Sin(pos) / 20;
                transform.position = new Vector3(transform.position.x, (float)posy + transform.position.y, initialPos.z + 0.5f);
            }

        }
        else if (initialPos != transform.position)
        {
            transform.position = initialPos;
            transform.rotation = initialRot;
            pos = 0.0f;
        }
    }
}
