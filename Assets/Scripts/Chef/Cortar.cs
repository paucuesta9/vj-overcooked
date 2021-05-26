using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cortar : MonoBehaviour
{
    float progreso;
    public bool active, canCut;
    public int typeCuchillo;

    public GameObject progressBarModel;
    GameObject progresBar;
    GameObject newAlimento;
    float waitTime = 5.0f;

    public GameObject jugador;
    AudioSource audio;

    public GameObject panCortado, tomateCortado, lechugaCortada, quesoCortado, cebollaCortada;

    // Start is called before the first frame update
    void Start()
    {
        progreso = 0;
        active = false;
        canCut = false;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            foreach (Transform hijo in transform)
            {
                if (hijo.gameObject.tag == "Comida")
                {
                    if (!hijo.gameObject.name.Contains("_"))
                    {
                        if (typeCuchillo == 0)
                        {
                            if (!hijo.gameObject.name.Contains("Pan") && !hijo.gameObject.name.Contains("Pizza")) canCut = true;
                            else canCut = false;
                        }
                        else if (typeCuchillo == 1)
                        {
                            if (hijo.gameObject.name.Contains("Pan")) canCut = true;
                            else canCut = false;
                        }
                    }
                    else canCut = false;
                    break;
                }
            }
            if (canCut && ((!GlobalVariables.mouse && (Input.GetKeyDown("c") || Input.GetKey("c"))) || (GlobalVariables.mouse && (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)))))
            {
                if (progreso == 0)
                {
                    if (!GlobalVariables.mute ) audio.Play();
                    progresBar = (GameObject)Instantiate(progressBarModel, transform.position + new Vector3(0, 2, 0), progressBarModel.transform.rotation);
                    progresBar.transform.SetParent(transform);
                }
                if (!GlobalVariables.mute && !audio.isPlaying) audio.Play();
                else if (GlobalVariables.mute) audio.Stop();
                progreso += 1.0f / waitTime * Time.deltaTime;
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
                if (progreso >= 1) cortado();
            }
            else if (progreso != 0)
            {
                Destroy(progresBar);
                progreso = 0;
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
                audio.Stop();
            }
        }
        else if (progreso != 0)
        {
            Destroy(progresBar);
            progreso = 0;
            audio.Stop();
        }
    }

    private void cortado()
    {
        audio.Stop();
        Destroy(progresBar);
        progreso = 0;
        foreach (Transform hijo in transform)
        {
            if (hijo.gameObject.tag == "Comida")
            {
                canCut = false;
                Vector3 pos = hijo.gameObject.transform.position;
                string nombre = hijo.name;
                Destroy(hijo.gameObject);
                jugador.GetComponent<CogerObjeto>().canpickup = false;
                if (nombre.Contains("Pan"))
                {
                    newAlimento = (GameObject)Instantiate(panCortado, pos, panCortado.transform.rotation);
                    newAlimento.transform.SetParent(transform);
                    newAlimento.name = "pan_c";
                }
                if (nombre.Contains("Queso"))
                {
                    newAlimento = (GameObject)Instantiate(quesoCortado, pos, quesoCortado.transform.rotation);
                    newAlimento.transform.SetParent(transform);
                    newAlimento.name = "queso_c";
                }
                if (nombre.Contains("Tomate"))
                {
                    newAlimento = (GameObject)Instantiate(tomateCortado, pos, tomateCortado.transform.rotation);
                    newAlimento.transform.SetParent(transform);
                    newAlimento.name = "tomate_c";
                }
                if (nombre.Contains("Lechuga"))
                {
                    newAlimento = (GameObject)Instantiate(lechugaCortada, pos, lechugaCortada.transform.rotation);
                    newAlimento.transform.SetParent(transform);
                    newAlimento.name = "lechuga_c";
                }
                if (nombre.Contains("Cebolla"))
                {
                    newAlimento = (GameObject)Instantiate(cebollaCortada, pos, cebollaCortada.transform.rotation);
                    newAlimento.transform.SetParent(transform);
                    newAlimento.name = "cebolla_c";
                }
                if (nombre.Contains("Pimiento"))
                {
                    //TODO: Cambiar modelo a pimiento cortado
                    newAlimento = (GameObject)Instantiate(cebollaCortada, pos, cebollaCortada.transform.rotation);
                    newAlimento.transform.SetParent(transform);
                    newAlimento.name = "pimiento_c";
                }
                break;
            }
        }
    }
}
