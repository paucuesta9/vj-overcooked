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

    GameObject Encimera, EncimeraAux;

    Utensilio utensilio, utensilioAux;
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
                utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(1,1,1);
                if (utensilio.Object.tag == "Plato") {
                    utensilio.Object.GetComponent<Collider>().enabled = true;
                }
            }
        }
        if (Input.GetKeyDown("q") && hasItem == true)
        {
            if (hasEncimera)
            {
                utensilio.Object.transform.parent = Encimera.transform;
                utensilio.Object.transform.position = Encimera.transform.Find("Object").position;
                utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
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
            if (tag == "Utensilio" || tag == "Plato")
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
        else if (!hasItem) {
            utensilioAux = new Utensilio(other.gameObject, new Color(1,1,1));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var tag = other.gameObject.tag;
        Debug.Log("CUANDO TU VAS, YO VUELVO DE AHÍ " + other.gameObject.name);
        if (other.gameObject == utensilio.Object || ((tag == "Platos") && (other.gameObject == utensilio.Object.transform.parent.gameObject))) {
            canpickup = false;
            if (tag == "Utensilio" || tag == "Plato") other.GetComponent<MeshRenderer>().material.color = new Color(1,1,1);
            else if (tag == "Platos") {
                utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(1,1,1);
            }
        }
        if (other.gameObject == utensilioAux.Object) {
            utensilioAux.Object = null;
        }
        if (utensilioAux.Object != null) {
            OnTriggerEnter(utensilioAux.Object.GetComponent<Collider>());
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Enter " + other.gameObject.name);
        var tag = other.gameObject.tag;
        if ((tag == "Encimera" || tag == "Fogon") && !hasEncimeraAnObject(other.gameObject) && hasEncimera == false)
        {
            paintFornitures(other.gameObject);
        }
        else if (tag == "Encimera" || tag == "Fogon")EncimeraAux = other.gameObject;
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Exit " + other.gameObject.name);
        var tag = other.gameObject.tag;
        if ((tag == "Encimera" || tag == "Fogon") && other.gameObject == Encimera)
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
            if (EncimeraAux != null && !hasEncimeraAnObject(EncimeraAux)) {
                paintFornitures(EncimeraAux);
                EncimeraAux = null;
            }
        }
        if (other.gameObject == EncimeraAux) {
            EncimeraAux = null;
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

    private void paintFornitures(GameObject mueble) {
        hasEncimera = true;
        Encimera = mueble;
        var tag = Encimera.tag;
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
    
