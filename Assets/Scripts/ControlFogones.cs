using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFogones : MonoBehaviour
{
    // Start is called before the first frame update

    float time;
    void Start()
    {
        time = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0)
        {
            time += 1.0f / 3.0f * Time.deltaTime;
            if (time >= 1)
            {
                transform.Find("Fuego").gameObject.SetActive(false);
                time = -1;
            }
        }
    }

    public void changeStateFire()
    {
        if (transform.Find("Fuego").gameObject.activeSelf)
        {
            Debug.Log("EXPLOTA EXPLOTA");
            time = 0;
        }
        else
        {
            Debug.Log("EXPLOTA EN MI CORAZON");
            transform.Find("Fuego").gameObject.SetActive(true);
        }
    }
}
