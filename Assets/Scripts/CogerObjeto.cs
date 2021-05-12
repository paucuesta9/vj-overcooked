using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
    public GameObject destino;
    public bool canpickup;

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
        if (Encimera != null && Encimera.tag == "Caja" && Input.GetKeyDown("e") && hasItem == false)
        {
            hasItem = true;
            Debug.Log("DALE A TU CUERPO");
            utensilio.Object = (GameObject)Instantiate(Encimera.GetComponent<PropiedadCaja>().alimento, destino.transform.position, Encimera.GetComponent<PropiedadCaja>().alimento.transform.rotation);
            utensilio.Object.transform.parent = destino.transform;
            if (utensilio.Object.name.Contains("Pan"))
            {
                foreach (Transform hijo in utensilio.Object.transform)
                {
                    hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }
            }
            else utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
        }
        else if (canpickup == true)
        {
            if (Input.GetKeyDown("e") && hasItem == false)
            {
                Debug.Log(utensilio.Object.transform.parent.name + " es tu padre");
                if (utensilio.Object.transform.parent.tag == "Fogon") utensilio.Object.transform.parent.GetComponent<ControlFogones>().changeStateFire();
                hasItem = true;
                utensilio.Object.transform.position = destino.transform.position;
                utensilio.Object.transform.parent = destino.transform;
                if (utensilio.Object.name.Contains("Pan"))
                {
                    foreach (Transform hijo in utensilio.Object.transform)
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                    }
                }
                else utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                if (utensilio.Object.tag == "Plato")
                {
                    utensilio.Object.GetComponent<Collider>().enabled = true;
                }
            }
        }
        if (Input.GetKeyDown("q") && hasItem == true)
        {
            if (hasEncimera)
            {
                hasItem = false;
                if (hasEncimeraAnObject(Encimera))
                {
                    foreach (Transform hijo in Encimera.transform)
                    {
                        if (hijo.tag == "Plato")
                        {
                            int numItems = hijo.childCount;
                            utensilio.Object.transform.parent = hijo;
                            utensilio.Object.transform.position = hijo.position + new Vector3(0.0f, 0.1f * numItems, 0.0f);
                            hijo.gameObject.GetComponent<ComprobarPlato>().addIngredient(utensilio.Object.name);
                            break;
                        }
                    }
                }
                else
                {
                    utensilio.Object.transform.parent = Encimera.transform;
                    utensilio.Object.transform.position = Encimera.transform.Find("Object").position;

                }
                if (utensilio.Object.name.Contains("Pan"))
                {
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
                        {
                            hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            break;
                        }
                    }
                if (utensilio.Object.transform.parent.tag == "Fogon") Encimera.GetComponent<ControlFogones>().changeStateFire();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Pica" && utensilio.Object != null && utensilio.Object.name.Contains("olla") && utensilio.Object.transform.GetChild(0).gameObject.activeSelf == false)
        {
            Debug.Log("ME PINTARON PAJARITOS");
            other.gameObject.GetComponent<LlenarOlla>().active = true;
        }
        else if (!hasItem && !canpickup)
        {
            if (tag == "Utensilio" || tag == "Plato" || tag == "Comida")
            {
                canpickup = true;
                utensilio = new Utensilio(other.gameObject, new Color(0, 0, 1));
                if (utensilio.Object.name.Contains("Pan"))
                {
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
        else if (!hasItem)
        {
            utensilioAux = new Utensilio(other.gameObject, new Color(1, 1, 1));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Pica") other.gameObject.GetComponent<LlenarOlla>().active = false;
        else if (other.gameObject == utensilio.Object || ((tag == "Platos") && (other.gameObject == utensilio.Object.transform.parent.gameObject)))
        {
            canpickup = false;
            if (tag == "Utensilio" || tag == "Plato" || tag == "Comida")
            {
                if (other.gameObject.name.Contains("Pan"))
                {
                    foreach (Transform hijo in other.gameObject.transform)
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                    }
                }
                else other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
            }
            else if (tag == "Platos")
            {
                utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
            }
        }
        if (other.gameObject == utensilioAux.Object)
        {
            utensilioAux.Object = null;
        }
        if (utensilioAux.Object != null && tag != "Pica")
        {
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
            {
                paintFornitures(other.gameObject);
            }

        }
        else if (tag == "Encimera" && utensilio.Object != null && utensilio.Object.tag == "Comida" && hasEncimeraAnObject(other.gameObject))
        {
            foreach (Transform hijo in other.gameObject.transform)
            {
                if (hijo.tag == "Plato")
                {
                    paintFornitures(other.gameObject);
                    break;
                }
            }
        }
        else if (tag == "Caja" && hasEncimera == false)
        {
            if (!hasItem)
            {
                hasEncimera = true;
                Encimera = other.gameObject;
                foreach (Transform hijo in Encimera.transform)
                {
                    hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                }
            }
        }
        else if (tag == "Encimera" || tag == "Fogon" || tag == "Caja")
        {
            if (tag != "Fogon" || utensilio.Object.tag != "Comida")
                EncimeraAux = other.gameObject;
        }
        if (tag == "Encimera" && hasEncimeraATabla(other.gameObject, 2))
        {
            other.gameObject.GetComponent<Cortar>().active = true;
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
            {
                if (!hasEncimeraATabla(other.gameObject, 0))
                    other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
            }
            else if (tag == "Fogon")
            {
                foreach (Transform hijo in other.gameObject.transform)
                {
                    if (hijo.name != "Object" && hijo.tag != "Utensilio" && hijo.name != "Fuego")
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                        break;
                    }
                }
            }
            else if (tag == "Caja")
            {
                foreach (Transform hijo in other.gameObject.transform)
                {
                    hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }
            }
            if (EncimeraAux != null && !hasEncimeraAnObject(EncimeraAux))
            {
                paintFornitures(EncimeraAux);
                EncimeraAux = null;
            }
        }
        if (other.gameObject == EncimeraAux)
        {
            EncimeraAux = null;
        }
        if (tag == "Encimera" && hasEncimeraATabla(other.gameObject, 0))
        {
            other.gameObject.GetComponent<Cortar>().active = false;
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

    private bool hasEncimeraATabla(GameObject encimera, int paint)
    {
        foreach (Transform hijo in encimera.transform)
        {
            if (hijo.tag == "Tabla")
            {
                if (paint == 1) hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                else if (paint == 0) hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                return true;
            }
        }
        return false;
    }

    private void paintFornitures(GameObject mueble)
    {
        hasEncimera = true;
        Encimera = mueble;
        var tag = Encimera.tag;
        if (hasItem == true)
        {
            if (tag == "Encimera")
            {
                if (!hasEncimeraATabla(mueble, 1))
                    Encimera.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = colorToPaint;
            }
            else if (tag == "Fogon")
                foreach (Transform hijo in Encimera.transform)
                {
                    if (hijo.name != "Object" && hijo.name != "Fuego")
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                        break;
                    }
                }
        }
    }

    public void llenarOlla()
    {
        utensilio.Object.transform.GetChild(0).gameObject.SetActive(true);
    }
}

