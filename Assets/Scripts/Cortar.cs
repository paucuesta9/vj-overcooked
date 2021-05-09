using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cortar : MonoBehaviour
{
    float progreso;
    public bool active, canCut;

    public GameObject progressBarModel;
    GameObject progresBar;
    float waitTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        progreso = 0;
        active = false;
        canCut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            foreach (Transform hijo in transform)
            {
                if (hijo.gameObject.tag == "Comida") {
                    if (!hijo.gameObject.name.Contains("cortado") && !hijo.gameObject.name.Contains("cortada")) canCut = true;
                    else canCut = false;
                }
            }
            if ((Input.GetKeyDown("c") || Input.GetKey("c")) && canCut) {       
                if (progreso == 0) {
                    progresBar = (GameObject)Instantiate(progressBarModel, transform.position + new Vector3(0, 2, 0), progressBarModel.transform.rotation);
                    progresBar.transform.SetParent(transform);
                }
                progreso += 1.0f/waitTime * Time.deltaTime;
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
            } else if (progreso != 0) {
                Destroy(progresBar);
                progreso = 0;
                progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
            }
        } else if (progreso != 0) {
            Destroy(progresBar);
            progreso = 0;
            progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = progreso;
        }
    }
}
