using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CocinarOlla : MonoBehaviour
{
    float progreso;
    public bool active;

    public GameObject fireModel;
    GameObject fire;
    public GameObject progressBarModel;
    GameObject progresBar;
    public GameObject ensaladaPatatasModel;
    GameObject newMeal;
    float waitTime = 15.0f;
    bool hayFuego;
    

    public GameObject jugador;

    void Start()
    {
        progreso = 0;
        active = false;
        hayFuego = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (progreso == 0)
            {
                hayFuego = false;
                progresBar = (GameObject)Instantiate(progressBarModel, transform.position + new Vector3(0, 2, 0), progressBarModel.transform.rotation);
                progresBar.transform.SetParent(transform);
                foreach (Transform hijo in transform)
                {
                    if (hijo.gameObject.tag == "Utensilio")
                    {
                        int num = 0;
                        foreach (Transform ingrediente in hijo)
                        {
                            if (ingrediente.gameObject.name == "patata_c") ++num;
                            if (ingrediente.gameObject.name == "tomate_c") ++num;
                            if (ingrediente.gameObject.name == "queso_c") ++num;
                            if (ingrediente.gameObject.name == "pimiento_c") ++num;
                        }
                        if (num == 5)
                        {
                            foreach (Transform ingrediente in hijo)
                            {
                                if (ingrediente.gameObject.name != "Object" && ingrediente.gameObject.name != "Agua")
                                {
                                    Destroy(ingrediente.gameObject);
                                }
                            }
                            newMeal = (GameObject)Instantiate(ensaladaPatatasModel, hijo.Find("Object").position, ensaladaPatatasModel.transform.rotation);
                            newMeal.transform.SetParent(hijo);
                            newMeal.name = "ensalada_patatas";
                        }
                    }
                }
            }
            progreso += 1.0f / waitTime * Time.deltaTime;
            progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
            if (progreso < 0.75) progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().color = new Color32(252, 219, 3, 255);
            else if (progreso < 1)
            {
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().color = new Color32(0, 255, 47, 255);
                foreach (Transform hijo in transform)
                {
                    if (hijo.tag == "Utensilio")
                    {
                        foreach (Transform ingrediente in hijo)
                        {
                            if (ingrediente.name != "Agua" && ingrediente.name != "Object")
                            {
                                if (ingrediente.name == "ensalada_patatas")
                                    ingrediente.name = "ensalada_patatas_o";
                                else ingrediente.name.Replace("_c", "_o");
                            }
                        }
                    }
                }
            }
            if (progreso >= 1)
            {
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                quemado();
            }
        }
        else if (progreso != 0)
        {
            Destroy(progresBar);
            progreso = 0;
        }
    }

    private void quemado()
    {
        foreach (Transform hijo in transform)
        {
            if (hijo.gameObject.tag == "Utensilio")
            {
                hijo.Find("Agua").gameObject.GetComponent<MeshRenderer>().material.color = new Color32(154, 57, 21, 255);
                foreach (Transform alimento in hijo)
                {
                    if (alimento.name != "Object")
                    {
                        alimento.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(154, 57, 21, 255);
                        alimento.name.Replace("_o", "_q");
                    }
                }
                if (!hayFuego) {
                    fire = (GameObject)Instantiate(fireModel, hijo.position, fireModel.transform.rotation);
                    hayFuego = true;
                }
            }
        }
        
    }
}
