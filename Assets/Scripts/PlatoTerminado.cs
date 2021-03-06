using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatoTerminado : MonoBehaviour
{

    public GameObject menuSuperior;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void finished()
    {
        foreach (Transform hijo in transform)
        {
            if (hijo.name != "Object")
            {
                foreach (Transform plato in hijo)
                {
                    if (plato.tag != "Plato")
                    {
                        menuSuperior.GetComponent<AparicionPedidos>().finished(plato.gameObject.name);
                        Destroy(plato.gameObject);
                    }
                }
                hijo.GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0.7f, 0.7f);
                hijo.GetComponent<EstadoPlato>().limpio = false;
            }
        }
    }
}
