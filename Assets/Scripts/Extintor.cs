using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Extintor : MonoBehaviour
{

    public GameObject progressBarModel;
    GameObject progresBar;

    float waitTime = 15.0f;

    bool active;
    float value;

    public GameObject nieveCarbonicaModel;
    GameObject nieveCarbonica;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        value = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            value -= 1.0f / waitTime * Time.deltaTime;
            progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = value;
            nieveCarbonica = (GameObject)Instantiate(nieveCarbonicaModel, transform.position + new Vector3(0,0, -0.5f), transform.rotation);
        }
    }

    public void activate() {
        active = true;
        progresBar = (GameObject)Instantiate(progressBarModel, transform.position + new Vector3(0, 2, 0), progressBarModel.transform.rotation);
        progresBar.transform.SetParent(transform);
        progresBar.transform.Find("ProgressBar").gameObject.GetComponent<Image>().fillAmount = value;
    }

    public void stop() {
        active = false;
        Destroy(progresBar);
    }

    public bool isActive() {
        return active;
    }
}
