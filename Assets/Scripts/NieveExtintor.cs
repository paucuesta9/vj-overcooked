using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NieveExtintor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Fire") {
            other.gameObject.GetComponent<Fuego>().destroyObject();
        }
    }
}
