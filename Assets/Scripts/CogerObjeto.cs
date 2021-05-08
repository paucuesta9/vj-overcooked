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

    GameObject obj;
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
        if (Encimera != null && Encimera.tag == "Caja" && Input.GetKeyDown("e") && hasItem == false) {
            hasItem = true;
            Debug.Log("DALE A TU CUERPO");
            utensilio.Object = (GameObject)Instantiate(Encimera.GetComponent<PropiedadCaja>().alimento, destino.transform.position, Encimera.GetComponent<PropiedadCaja>().alimento.transform.rotation);
            utensilio.Object.transform.parent = destino.transform;
            utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(1,1,1);
        }
        else if (canpickup == true)
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
                hasItem = false;
                utensilio.Object.transform.parent = Encimera.transform;
                utensilio.Object.transform.position = Encimera.transform.Find("Object").position;
                if (utensilio.Object.name == "Pan") {
                    foreach (Transform hijo in utensilio.Object.transform)
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                    }
                }
                else utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
                if (Encimera.tag == "Encimera")
                    Encimera.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                else if (Encimera.tag == "Fogon")
                    foreach (Transform hijo in Encimera.transform)
                    {
                        if (hijo.name != "Object" && hijo.name != "Fuego")
                            hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                    }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (!hasItem && !canpickup)
        {
            if (tag == "Utensilio" || tag == "Plato" || tag == "Comida")
            {
                canpickup = true;
                utensilio = new Utensilio(other.gameObject, new Color(0,0,1));
                if (utensilio.Object.name == "Pan") {
                    foreach (Transform hijo in utensilio.Object.transform)
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                    }
                }
                else utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
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
        if (other.gameObject == utensilio.Object || ((tag == "Platos") && (other.gameObject == utensilio.Object.transform.parent.gameObject))) {
            canpickup = false;
            if (tag == "Utensilio" || tag == "Plato" || tag == "Comida") {
                if (other.gameObject.name == "Pan") {
                    foreach (Transform hijo in other.gameObject.transform)
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = new Color(1,1,1);
                    }
                }
                else other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1,1,1);
            }
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
            if (tag != "Fogon" || utensilio.Object.tag != "Comida")
                paintFornitures(other.gameObject);
        }
        else if (tag == "Caja" && hasEncimera == false) {
            hasEncimera = true;
            Encimera = other.gameObject;
            foreach (Transform hijo in Encimera.transform)
            {
                hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
            }
        }
        else if (tag == "Encimera" || tag == "Fogon" || tag == "Caja") {
            if (tag != "Fogon" || utensilio.Object.tag != "Comida")
                EncimeraAux = other.gameObject;
        }        
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Exit " + other.gameObject.name);
        var tag = other.gameObject.tag;
        if ((tag == "Encimera" || tag == "Fogon" || tag == "Caja") && other.gameObject == Encimera)
        {
            hasEncimera = false;
            Encimera = null;
            if (tag == "Encimera")
                other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
            else if (tag == "Fogon") {
                foreach (Transform hijo in other.gameObject.transform)
                {
                    if (hijo.name != "Object" && hijo.tag != "Utensilio" && hijo.name != "Fuego")
                        hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }
            }
            else if (tag == "Caja") {
                foreach (Transform hijo in other.gameObject.transform)
                {
                    hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }
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
            if (hijo.tag == "Utensilio" || hijo.tag == "Platos" || hijo.tag == "Plato" || hijo.tag == "Comida")
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
                    if (hijo.name != "Object" && hijo.name != "Fuego")
                        hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                }
        }
    }
}
    
