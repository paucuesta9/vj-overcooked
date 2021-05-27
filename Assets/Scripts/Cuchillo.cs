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
    void Start()
    {
        cut = false;
        initialPos = transform.position;
        pos = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cut)
        {
            pos += 10.0f * Time.deltaTime;
            if (typeCuchillo == 1)
            {
                double posz = Math.Sin(pos) / 4;
                Debug.Log(posz);
                transform.position = new Vector3(transform.position.x, transform.position.y, (float)posz + initialPos.z);
            }
            else
            {
                double posy = Math.Sin(pos) / 20;
                Debug.Log(posy);
                transform.position = new Vector3(transform.position.x, (float)posy + transform.position.y, initialPos.z);
            }

        }
        else if (initialPos != transform.position) transform.position = initialPos;
    }
}
