using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuego : MonoBehaviour
{

    bool destroy;

    float tiempo;


    // Start is called before the first frame update
    void Start()
    {
        destroy = false;
        tiempo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            tiempo += 1.0f * 0.5 * Time.deltaTime;
            if (tiempo > 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void destroyObject()
    {
        destroy = true;
    }
}
