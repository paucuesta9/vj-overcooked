using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CocinarSarten : MonoBehaviour
{
    float progreso;
    public bool active;

    public GameObject progressBarModel;
    GameObject progresBar;

    public GameObject fireModel;
    GameObject fire;
    bool hayFuego;

    public GameObject chesseBurguer;
    public GameObject onionChesseBurguer;
    GameObject newMeal;
    float waitTime = 15.0f;


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
                            if (ingrediente.gameObject.name.Contains("Carne")) ++num;
                            if (ingrediente.gameObject.name == "cebolla_c") ++num;
                            if (ingrediente.gameObject.name == "queso_c") ++num;
                        }
                        if (num >= 2)
                        {
                            foreach (Transform ingrediente in hijo)
                            {
                                if (ingrediente.gameObject.name != "Object")
                                {
                                    Destroy(ingrediente.gameObject);
                                }
                            }
                            if (num == 3)
                            {
                                newMeal = (GameObject)Instantiate(onionChesseBurguer, hijo.Find("Object").position, onionChesseBurguer.transform.rotation);
                                newMeal.transform.SetParent(hijo);
                                newMeal.name = "carne_queso_cebolla";
                            }
                            else if (num == 2)
                            {
                                newMeal = (GameObject)Instantiate(chesseBurguer, hijo.Find("Object").position, chesseBurguer.transform.rotation);
                                newMeal.transform.SetParent(hijo);
                                newMeal.name = "carne_queso";
                            }
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
                            if (!ingrediente.name.Contains("arne"))
                                ingrediente.name.Replace("_c", "_s");
                            else if (ingrediente.name.Contains("Carne"))
                                ingrediente.name = "carne_s";
                            else if (ingrediente.name == "carne_queso")
                                ingrediente.name = "carne_queso_s";
                            else if (ingrediente.name == "carne_queso_cebolla")
                                ingrediente.name = "carne_queso_cebolla_s";
                        }
                    }
                }
            }
            if (progreso >= 1 && GlobalVariables.canBurn)
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
                foreach (Transform alimento in hijo)
                {
                    if (alimento.name != "Object")
                    {
                        alimento.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(154, 57, 21, 255);
                        alimento.name.Replace("_s", "_q");
                    }
                }
            }
                if (!hayFuego) {
                    fire = (GameObject)Instantiate(fireModel, hijo.position + new Vector3(0,0.5f,-0.5f), fireModel.transform.rotation);
                    hayFuego = true;
                }
        }
         
    }

    public void finish() {
        Destroy(progresBar);
       foreach (Transform hijo in transform)
        {
            if (hijo.tag == "Utensilio")
            {
                foreach (Transform ingrediente in hijo)
                {   
                    if (!ingrediente.name.Contains("arne"))
                        ingrediente.name.Replace("_c", "_s");
                    else if (ingrediente.name.Contains("Carne"))
                        ingrediente.name = "carne_s";
                    else if (ingrediente.name == "carne_queso")
                        ingrediente.name = "carne_queso_s";
                    else if (ingrediente.name == "carne_queso_cebolla")
                        ingrediente.name = "carne_queso_cebolla_s";
                }
            }
        }
        active = false;
    }
}
