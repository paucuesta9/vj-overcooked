using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
    public GameObject destino;
    public bool canpickup;

    public GameObject[] platos;
    public struct Utensilio
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
    GameObject Encimera, EncimeraAux, Pared;

    public Utensilio utensilio, utensilioAux;
    bool hasItem;
    bool hasEncimera, dejarExtintor;

    public GameObject menuSuperior;

    void Start()
    {
        canpickup = false;
        hasItem = false;
        hasEncimera = false;
        colorToPaint = new Color(0, 0, 1);
        dejarExtintor = false;
    }

    void Update()
    {
        // if (utensilio.Object != null) Debug.Log(utensilio.Object.name);
        // else Debug.Log("Se fue a la mierda");
        // if(canpickup) Debug.Log("CERTO");
        // else Debug.Log ("FALSO");
        if (hasEncimera && (Encimera.tag == "Fogon" || Encimera.tag == "Horno") && Input.GetKeyDown("o"))
        {
            Encimera.GetComponent<CocinarOlla>().finish();
            Encimera.GetComponent<CocinarSarten>().finish();
        }
        if (!hasItem && Input.GetKeyDown("n"))
        {
            instiantiateMeal(menuSuperior.GetComponent<AparicionPedidos>().getNextMeal());
        }
        else if (Encimera != null && Encimera.tag == "Caja" && ((!GlobalVariables.mouse && Input.GetKeyDown("e")) || (GlobalVariables.mouse && Input.GetMouseButtonDown(0))) && hasItem == false)
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
        else if (hasEncimera && Encimera.tag == "Horno" && hasItem && utensilio.Object.tag == "Plato" && ((!GlobalVariables.mouse && Input.GetKeyDown("e")) || (GlobalVariables.mouse && Input.GetMouseButtonDown(0))))
        {
            foreach (Transform hijo in Encimera.transform)
            {
                if (hijo.name != "Object")
                {
                    hijo.position = utensilio.Object.transform.position;
                    hijo.parent = utensilio.Object.transform;
                }
            }
            Encimera.GetComponent<CocinarHorno>().taken();
        }
        else if (((!GlobalVariables.mouse && Input.GetKeyDown("return")) || (GlobalVariables.mouse && Input.GetMouseButtonDown(1))))
        {
            if (utensilio.Object.transform.parent.gameObject.tag == "Fogon")
            {
                if (canpickup && utensilio.Object.name.Contains("Olla") && utensilio.Object.transform.GetChild(0).gameObject.activeSelf)
                {
                    utensilio.Object.transform.parent.gameObject.GetComponent<CocinarOlla>().active = true;
                }
                else if (canpickup && utensilio.Object.name.Contains("Sarten"))
                {
                    if (utensilio.Object.transform.childCount > 0)
                        utensilio.Object.transform.parent.gameObject.GetComponent<CocinarSarten>().active = true;
                }
            }
            else if (hasEncimera && Encimera.tag == "Horno")
            {
                if (Encimera.transform.childCount > 0)
                    Encimera.GetComponent<CocinarHorno>().active = true;
            }
        }
        else if (canpickup == true)
        {
            if (((!GlobalVariables.mouse && Input.GetKeyDown("e")) || (GlobalVariables.mouse && Input.GetMouseButtonDown(0))) && hasItem == false)
            {
                if (utensilio.Object.tag == "Extintor") utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                else if (utensilio.Object.transform.parent.tag == "Fogon") utensilio.Object.transform.parent.GetComponent<ControlFogones>().changeStateFire();
                if ((utensilio.Object.name.Contains("Olla") || utensilio.Object.name.Contains("Sarten")) && utensilio.Object.transform.parent.gameObject.tag == "Fogon")
                {
                    utensilio.Object.transform.parent.gameObject.GetComponent<CocinarOlla>().active = false;
                    utensilio.Object.transform.parent.gameObject.GetComponent<CocinarSarten>().active = false;
                }
                hasItem = true;
                if (utensilio.Object.tag == "Comida")
                {
                    GameObject parent = utensilio.Object.transform.parent.gameObject;
                    Debug.Log(parent.name);
                    if (parent.tag == "Plato")
                    {
                        Debug.Log("EEEEEE");
                        canpickup = true;
                        utensilio = new Utensilio(parent, new Color(0, 0, 1));
                        utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
                    }
                }
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
                    if (!utensilio.Object.GetComponent<EstadoPlato>().limpio) utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0.7f, 0.7f);
                    utensilio.Object.GetComponent<Collider>().enabled = true;
                }
            }
        }
        if (((!GlobalVariables.mouse && Input.GetKeyDown("q")) || (GlobalVariables.mouse && Input.GetMouseButtonDown(0))) && hasItem == true)
        {
            if (hasEncimera && Encimera.tag == "Basura")
            {
                foreach (Transform alimento in utensilio.Object.transform)
                {
                    Destroy(alimento.gameObject);
                }
                utensilio.Object.GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0.7f, 0.7f);
            }
            else if (hasEncimera && Encimera.tag != "Caja")
            {
                hasItem = false;
                if (hasEncimeraAnObject(Encimera))
                {
                    if (Encimera.tag == "Horno")
                    {
                        int numItems = Encimera.transform.childCount;
                        utensilio.Object.transform.parent = Encimera.transform;
                        utensilio.Object.transform.position = Encimera.transform.Find("Object").position + new Vector3(0.0f, 0.1f * numItems, 0.0f);
                    }
                    else if (Encimera.tag == "Fogon")
                    {
                        foreach (Transform hijo in Encimera.transform)
                        {
                            if ((hijo.name.Contains("Olla") && hijo.GetChild(0).gameObject.activeSelf) || hijo.name.Contains("Sarten"))
                            {
                                int numItems = hijo.childCount;
                                if (hijo.name.Contains("Olla")) numItems -= 2;
                                else numItems--;
                                utensilio.Object.transform.parent = hijo;
                                utensilio.Object.transform.position = hijo.Find("Object").position + new Vector3(0.0f, 0.1f * numItems, 0.0f);
                            }
                        }
                    }
                    else
                    {
                        foreach (Transform hijo in Encimera.transform)
                        {
                            if (hijo.tag == "Plato")
                            {
                                hijo.gameObject.GetComponent<EstadoPlato>().limpio = false;
                                int numItems = hijo.childCount;
                                if (utensilio.Object.name.Contains("Olla"))
                                {
                                    hasItem = true;
                                    foreach (Transform alimento in utensilio.Object.transform)
                                    {
                                        if (alimento.name != "Agua" && alimento.name != "Object")
                                        {
                                            alimento.parent = hijo;
                                            alimento.position = hijo.position + new Vector3(0.0f, 0.1f * numItems, 0.0f);
                                            hijo.gameObject.GetComponent<ComprobarPlato>().addIngredient(alimento.name);
                                        }
                                    }
                                    break;
                                }
                                else if (utensilio.Object.name.Contains("Sarten"))
                                {
                                    hasItem = true;
                                    foreach (Transform alimento in utensilio.Object.transform)
                                    {
                                        if (alimento.name != "Object")
                                        {
                                            alimento.parent = hijo;
                                            alimento.position = hijo.position + new Vector3(0.0f, 0.1f * numItems, 0.0f);
                                            hijo.gameObject.GetComponent<ComprobarPlato>().addIngredient(alimento.name);
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    utensilio.Object.transform.parent = hijo;
                                    utensilio.Object.transform.position = hijo.position + new Vector3(0.0f, 0.1f * numItems, 0.0f);
                                    hijo.gameObject.GetComponent<ComprobarPlato>().addIngredient(utensilio.Object.name);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    utensilio.Object.transform.parent = Encimera.transform;
                    utensilio.Object.transform.position = Encimera.transform.Find("Object").position;
                    if (Encimera.tag == "Fin")
                    {
                        Encimera.GetComponent<PlatoTerminado>().finished();
                    }
                }
                if (utensilio.Object.name.Contains("Pan"))
                {
                    foreach (Transform hijo in utensilio.Object.transform)
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                    }
                }
                else utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
                if (Encimera.tag == "Fogon")
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
            else if (dejarExtintor)
            {
                hasItem = false;
                utensilio.Object.transform.parent = Pared.transform;
                utensilio.Object.transform.position = Pared.transform.Find("Object").transform.position;
                utensilio.Object.transform.rotation = Pared.transform.Find("Object").transform.rotation;
            }
        }
        if (((!GlobalVariables.mouse && Input.GetKeyDown("space")) || (GlobalVariables.mouse && Input.GetMouseButtonDown(1))) && hasItem == true && utensilio.Object.tag == "Extintor")
        {
            if (!utensilio.Object.GetComponent<Extintor>().isActive()) utensilio.Object.GetComponent<Extintor>().activate();
            else utensilio.Object.GetComponent<Extintor>().stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Pica" && hasItem && utensilio.Object.name.Contains("Olla") && utensilio.Object.transform.GetChild(0).gameObject.activeSelf == false)
        {
            Debug.Log("ME PINTARON PAJARITOS");
            other.gameObject.GetComponent<MeshRenderer>().material.color = colorToPaint;
            other.gameObject.GetComponent<LlenarOlla>().active = true;
        }
        else if (tag == "Pica" && hasItem && utensilio.Object.tag == "Plato" && !utensilio.Object.GetComponent<EstadoPlato>().limpio)
        {
            other.gameObject.GetComponent<LavarPlato>().active = true;
            other.gameObject.GetComponent<LavarPlato>().plato = utensilio.Object;
        }
        else if (!hasItem && !canpickup)
        {
            if (tag == "Comida")
            {
                if (other.gameObject.transform.parent.tag != "Plato" && other.gameObject.transform.parent.tag != "Utensilio")
                {
                    canpickup = true;
                    utensilio = new Utensilio(other.gameObject, new Color(0, 0, 1));
                    if (utensilio.Object.name.Contains("Pan"))
                    {
                        foreach (Transform hijo in utensilio.Object.transform)
                        {
                            if (utensilio.Object.name.Contains("Pan"))
                            {
                                foreach (Transform hijo2 in hijo)
                                {
                                    hijo2.GetComponent<MeshRenderer>().material.color = colorToPaint;
                                }
                            }
                            else
                                hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                        }
                    }
                    else utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
                }
            }
            else if (tag == "Utensilio" || tag == "Plato")
            {
                canpickup = true;
                utensilio = new Utensilio(other.gameObject, new Color(0, 0, 1));
                utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
            }
            else if (tag == "Platos")
            {
                canpickup = true;
                utensilio = new Utensilio(other.gameObject.transform.GetChild(other.gameObject.transform.childCount - 1).gameObject, new Color(1, 1, 1));
                utensilio.Object.GetComponent<MeshRenderer>().material.color = colorToPaint;
            }
            else if (tag == "Extintor")
            {
                Debug.Log("SOY YOOOO LA QUE SIGUE AQUI");
                canpickup = true;
                utensilio = new Utensilio(other.gameObject, new Color(0, 0, 1));
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
        Debug.Log("HORNO");
        if (other.gameObject != null && utensilio.Object != null) Debug.Log(other.gameObject.name + utensilio.Object.name);
        if (tag == "Pica")
        {
            other.gameObject.GetComponent<LlenarOlla>().active = false;
            other.gameObject.GetComponent<LavarPlato>().active = false;
            other.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(156, 156, 156, 255);
        }
        else if (other.gameObject == utensilio.Object || ((tag == "Platos") && (other.gameObject == utensilio.Object.transform.parent.gameObject)))
        {
            Debug.Log("HOLIII");
            canpickup = false;
            if (tag == "Utensilio" || tag == "Plato" || tag == "Comida")
            {
                if (other.gameObject.name.Contains("Pan"))
                {
                    foreach (Transform hijo in other.gameObject.transform)
                    {
                        if (other.gameObject.name.Contains("Pan"))
                        {
                            foreach (Transform hijo2 in hijo)
                            {
                                hijo2.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            }
                        }
                        else
                            hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                    }
                }
                else if (tag == "Plato" && !other.gameObject.GetComponent<EstadoPlato>().limpio) other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0.7f, 0.7f);
                else
                {
                    Debug.Log("SUPERHOLII");
                    other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }
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
        if (hasItem && utensilio.Object.tag == "Extintor" && tag == "Pared")
        {
            dejarExtintor = true;
            Pared = other.gameObject;
        }
        else if ((tag == "Encimera" || tag == "Fogon" || tag == "Fin") && !hasEncimeraAnObject(other.gameObject) && hasEncimera == false)
        {
            if (tag != "Fogon" || utensilio.Object.tag != "Comida")
            {
                paintFornitures(other.gameObject);
            }

        }
        else if (tag == "Basura" && hasItem && utensilio.Object.tag == "Plato" && utensilio.Object.transform.childCount != 0)
        {
            paintFornitures(other.gameObject);
        }
        else if (tag == "Encimera" && hasEncimeraAnObject(other.gameObject))
        {
            foreach (Transform hijo in other.gameObject.transform)
            {
                if (hijo.tag == "Plato" && (hijo.childCount != 0 || hijo.gameObject.GetComponent<EstadoPlato>().limpio))
                {
                    paintFornitures(other.gameObject);
                    break;
                }
            }
        }
        else if (tag == "Fogon" && hasItem && utensilio.Object.tag == "Comida" && hasEncimera == false)
        {
            foreach (Transform hijo in other.transform)
            {
                if ((hijo.gameObject.name.Contains("Olla") && hijo.gameObject.transform.GetChild(0).gameObject.activeSelf) || hijo.gameObject.name.Contains("Sarten"))
                {
                    if ((hijo.gameObject.name.Contains("Olla") && !utensilio.Object.name.Contains("Carne") && !utensilio.Object.name.Contains("Cebolla") && !utensilio.Object.name.Contains("Pan") && utensilio.Object.name.Contains("_c")) ||
                        (hijo.gameObject.name.Contains("Sarten") && !utensilio.Object.name.Contains("Pan") && !utensilio.Object.name.Contains("Lechuga") && !utensilio.Object.name.Contains("Tomate") && (utensilio.Object.name.Contains("_c") || utensilio.Object.name.Contains("Carne"))))
                    {
                        hasEncimera = true;
                        Encimera = other.gameObject;
                        break;
                    }
                }
            }
        }
        else if (tag == "Horno" && hasEncimera == false)
        {
            if (hasItem && ((utensilio.Object.tag == "Comida" && utensilio.Object.name.Contains("_c")) || utensilio.Object.tag == "Plato"))
            {
                paintFornitures(other.gameObject);
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

        else if (tag == "Encimera" || tag == "Fogon" || tag == "Caja" || tag == "Fin")
        {
            if (hasItem && utensilio.Object.tag != "Comida")
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
        if (hasItem && utensilio.Object.tag == "Extintor" && tag == "Pared")
        {
            dejarExtintor = false;
        }
        else if ((tag == "Encimera" || tag == "Fogon" || tag == "Caja" || tag == "Horno" || tag == "Fin" || tag == "Basura") && other.gameObject == Encimera)
        {
            hasEncimera = false;
            Encimera = null;
            if (tag == "Encimera")
            {
                if (!hasEncimeraATabla(other.gameObject, 0))
                {
                    if (other.gameObject.name.Contains("Esquina")) other.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                    else
                    {
                        other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                        foreach (Transform hijo in other.gameObject.transform)
                        {
                            if (hijo.name.Contains("Pan"))
                            {
                                hijo.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                                hijo.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            }
                            else if (hijo.name != "encimera" && hijo.name != "Object" && hijo.name != "Box")
                            {
                                hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            }
                        }
                    }
                }
            }
            else if (tag == "Fogon")
            {
                foreach (Transform hijo in other.gameObject.transform)
                {
                    if (hijo.name != "Object" && hijo.tag != "Utensilio" && hijo.name != "Fuego" && !hijo.name.Contains("ProgressBar"))
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                    }
                }
            }
            else if (tag == "Horno")
            {
                Material[] materials = other.gameObject.GetComponent<MeshRenderer>().materials;
                foreach (Material material in materials)
                {
                    if (material.color == colorToPaint)
                    {
                        material.color = new Color(0, 0, 0);
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
            else if (tag == "Basura")
            {
                other.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(255, 200, 200, 200);
                other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color32(255, 200, 200, 200);
                other.gameObject.transform.GetChild(0).Rotate(60, 0, 0);
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
                foreach (Transform hijo in Encimera.transform)
                {
                    if (Encimera.name.Contains("Pan"))
                    {
                        hijo.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = colorToPaint;
                        hijo.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = colorToPaint;
                    }
                    else if (hijo.name != "encimera" && hijo.name != "Object" && hijo.name != "Box")
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                    }
                }
            }
            else if (tag == "Fogon")
                foreach (Transform hijo in Encimera.transform)
                {
                    if (hijo.name != "Object" && hijo.name != "Fuego")
                    {
                        hijo.GetComponent<MeshRenderer>().material.color = colorToPaint;
                    }
                }
            else if (tag == "Horno")
            {
                Material[] materials = mueble.GetComponent<MeshRenderer>().materials;
                foreach (Material material in materials)
                {
                    Debug.Log("CORRE CORRE CORRE CORZÓN");
                    if (material.color == new Color(0, 0, 0))
                    {
                        material.color = colorToPaint;
                        break;
                    }
                }

            }
            else if (tag == "Basura")
            {
                Encimera.GetComponent<MeshRenderer>().material.color = colorToPaint;
                Encimera.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = colorToPaint;
                Encimera.transform.GetChild(0).Rotate(-60, 0, 0);
            }
        }
    }

    public void llenarOlla()
    {
        utensilio.Object.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void instiantiateMeal(string meal)
    {
        if (meal != "-1")
        {
            hasItem = true;
            int num = 0;
            Int32.TryParse(meal, out num);
            utensilio.Object = (GameObject)Instantiate(platos[num], destino.transform.position, platos[num].transform.rotation);
            utensilio.Object.transform.SetParent(transform);
            utensilio.Object.name = meal;
        }
    }
}

