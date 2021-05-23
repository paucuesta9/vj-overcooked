using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LlenarOlla : MonoBehaviour
{

    float progreso;
    public bool active;

    public GameObject progressBarModel;
    GameObject progresBar;

    float waitTime = 5.0f;

    public GameObject jugador;

    void Start()
    {
        progreso = 0;
        active = false;
    }

    void Update()
    {
        if (active)
        {
            if (((!GlobalVariables.mouse && (Input.GetKeyDown("g") || Input.GetKey("g"))) || (GlobalVariables.mouse && (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)))))
            {
                if (progreso == 0)
                {
                    progresBar = (GameObject)Instantiate(progressBarModel, transform.position + new Vector3(0, 2, 0), progressBarModel.transform.rotation);
                    progresBar.transform.SetParent(transform);
                }
                progreso += 1.0f / waitTime * Time.deltaTime;
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
                if (progreso >= 1)
                {
                    jugador.GetComponent<CogerObjeto>().llenarOlla();
                    Destroy(progresBar);
                    progreso = 0;
                    active = false;
                }
            }
            else if (progreso != 0)
            {
                Destroy(progresBar);
                progreso = 0;
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
            }
        }
        else if (progreso != 0)
        {
            Destroy(progresBar);
            progreso = 0;
        }
    }
}
