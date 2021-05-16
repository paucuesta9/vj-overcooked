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
            }
            progreso += 1.0f / waitTime * Time.deltaTime;
            progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
            if (progreso < 0.75) progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().color = new Color32(252, 219, 3, 255);
            else if (progreso < 1) progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().color = new Color32(0, 255, 47, 255);
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
            if (hijo.gameObject.name != "Object" && hijo.gameObject.tag != "ProgressBar")
            {
                if (hijo.gameObject.name.Contains("Pan"))
                {
                    foreach (Transform trozoPan in hijo)
                    {
                        trozoPan.GetComponent<MeshRenderer>().material.color = new Color32(100, 10, 10, 255);
                    }
                }
                else hijo.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(10, 10, 10, 255);
            }
        }
    }
}
