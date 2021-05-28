using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CocinarHorno : MonoBehaviour
{
    float progreso;
    public bool active;

    public GameObject progressBarModel;
    GameObject progresBar;

    public GameObject pizzaMargarita;
    public GameObject pizzaPimiento;
    GameObject newMeal;
    float waitTime = 20.0f;

    public GameObject jugador;

    void Start()
    {
        progreso = 0;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (progreso == 0)
            {
                progresBar = (GameObject)Instantiate(progressBarModel, transform.position + new Vector3(0, 2, -1), progressBarModel.transform.rotation);
                progresBar.transform.SetParent(transform);
                int num = 0;
                foreach (Transform ingrediente in transform)
                {
                    if (ingrediente.gameObject.name == "Pan_c") ++num;
                    if (ingrediente.gameObject.name == "tomate_c") ++num;
                    if (ingrediente.gameObject.name == "queso_c") ++num;
                    if (ingrediente.gameObject.name == "pimiento_c") ++num;
                    if (ingrediente.gameObject.name == "cebolla_c") ++num;
                }
                if (num == 3)
                {
                    foreach (Transform ingrediente in transform)
                    {
                        if (ingrediente.gameObject.name != "Object" && ingrediente.gameObject.tag != "ProgressBar")
                        {
                            Destroy(ingrediente.gameObject);
                        }
                    }
                    newMeal = (GameObject)Instantiate(pizzaMargarita, transform.Find("Object").position, pizzaMargarita.transform.rotation);
                    newMeal.transform.SetParent(transform);
                    newMeal.name = "pizza_m";
                }
                else if (num == 5)
                {
                    foreach (Transform ingrediente in transform)
                    {
                        if (ingrediente.gameObject.name != "Object" && ingrediente.gameObject.tag != "ProgressBar")
                        {
                            Destroy(ingrediente.gameObject);
                        }
                    }
                    newMeal = (GameObject)Instantiate(pizzaPimiento, transform.Find("Object").position, pizzaPimiento.transform.rotation);
                    newMeal.transform.SetParent(transform);
                    newMeal.name = "pizza_c";
                }
            }
            progreso += 1.0f / waitTime * Time.deltaTime;
            progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
            if (progreso < 0.75) progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().color = new Color32(252, 219, 3, 255);
            else if (progreso < 1)
            {
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().color = new Color32(0, 255, 47, 255);
                foreach (Transform ingrediente in transform)
                {
                    if (ingrediente.name != "Object")
                    {
                        if (ingrediente.name == "pizza_m")
                            ingrediente.name = "pizza_m_h";
                        else if (ingrediente.name == "pizza_c")
                            ingrediente.name = "pizza_c_h";
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
        foreach (Transform alimento in transform)
        {
            if (alimento.name != "Object" && alimento.tag != "ProgressBar")
            {
                if (alimento.name.Contains("Pan")) {
                    foreach (Transform hijo in alimento.transform)
                    {
                        hijo.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(154, 57, 21, 255);
                    }
                }
                else {
                    alimento.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(154, 57, 21, 255);
                    alimento.name.Replace("_h", "_q");
                }
            }
        }
    }

    public void taken()
    {
        Destroy(progresBar);
        progreso = 0;
        active = false;
    }
}
