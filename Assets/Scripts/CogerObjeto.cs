using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
    public GameObject destino; //reference to your hands/the position where you want your object to go
    bool canpickup; //a bool to see if you can or cant pick up the item

    struct Utensilio
    {
        public GameObject Object;
        public Color color;

        public Utensilio(GameObject Object, Color color)
        {
            this.Object = Object;
            this.color = color;
        }
    }

    Color colorToPaint;

    GameObject Encimera;

    Utensilio utensilio;
    bool hasItem;
    bool hasEncimera;
    void Start()
    {
        canpickup = false;
        hasItem = false;
        hasEncimera = false;
        colorToPaint = new Color(0, 0, 1);
    }

    void Update()
    {
        if (canpickup == true)
        {
            if (Input.GetKeyDown("e") && hasItem == false)
            {
                hasItem = true;
                utensilio.Object.transform.position = destino.transform.position;
                utensilio.Object.transform.parent = destino.transform;
                utensilio.Object.GetComponent<MeshRenderer>().material.color = utensilio.color;
            }
        }
        if (Input.GetKeyDown("q") && hasItem == true)
        {
            if (hasEncimera)
            {
                utensilio.Object.transform.parent = Encimera.transform;
                utensilio.Object.transform.position = Encimera.transform.Find("Object").position;
                if (Encimera.tag == "Encimera")
                    Encimera.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                else if (Encimera.tag == "Fogon")
                    foreach (Transform hijo in Encimera.transform)
                    {
                        if (hijo.name != "Object")
                            hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                    }
                hasItem = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (!hasItem && !canpickup)
        {
            if (tag == "Utensilio")
            {
                canpickup = true;
                utensilio = new Utensilio(other.gameObject, other.gameObject.GetComponent<MeshRenderer>().material.color);
                utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
            }
            else if (tag == "Platos")
            {
                canpickup = true;
                utensilio = new Utensilio(other.gameObject.transform.GetChild(other.gameObject.transform.childCount - 1).gameObject, new Color(1, 1, 1));
                utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canpickup = false;
        utensilio.Object.GetComponent<MeshRenderer>().material.color = utensilio.color;
    }

    private void OnCollisionEnter(Collision other)
    {
        var tag = other.gameObject.tag;
        if ((tag == "Encimera" || tag == "Fogon") && !hasEncimeraAnObject(other.gameObject) && hasEncimera == false)
        {
            hasEncimera = true;
            Encimera = other.gameObject;
            if (hasItem == true)
            {
                if (tag == "Encimera")
                    Encimera.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = colorToPaint;
                else if (tag == "Fogon")
                    foreach (Transform hijo in Encimera.transform)
                    {
                        if (hijo.name != "Object")
                            hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                    }
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Encimera" || tag == "Fogon")
        {
            hasEncimera = false;
            Encimera = null;
            if (tag == "Encimera")
                other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
            else if (tag == "Fogon")
                foreach (Transform hijo in other.gameObject.transform)
                {
                    if (hijo.name != "Object" && hijo.tag != "Utensilio")
                        hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }
        }
    }

    private bool hasEncimeraAnObject(GameObject encimera)
    {
        foreach (Transform hijo in encimera.transform)
        {
            if (hijo.tag == "Utensilio" || hijo.tag == "Platos" || hijo.tag == "Plato" || hijo.tag == "Alimento")
            {
                return true;
            }
        }
        return false;
    }
}
