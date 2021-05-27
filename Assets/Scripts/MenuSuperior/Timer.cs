using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float tiempo;
    public Text timer;

    GameObject timesUP;
    public GameObject timeUP;

    GameObject exit;
    public GameObject exitButton;

    bool first;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = tiempo.ToString("00");
        exit = transform.Find("Exit").gameObject;
        first = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > 0) {
            timer.text = calcularTiempo();
        }
        else {
            if (first) {
                timer.text = "00:00";
                Debug.Log("Y YO NACÍ");
                timesUP = (GameObject)Instantiate(timeUP);
                timesUP.transform.SetParent(transform);
                timesUP.transform.rotation = timesUP.transform.parent.rotation;
                timesUP.transform.localScale = new Vector3(1f, 1f, 1f);
                Vector3 pos = new Vector3(0f, 400f, 0f);
                timesUP.transform.localPosition = pos;
                exit.SetActive(true);
                exit.transform.GetChild(0).gameObject.SetActive(true);
                tiempo = 0;
                first = false;
            }
        }
  
    }

    public string calcularTiempo() {

        tiempo -= Time.deltaTime;

        string minutos = Mathf.Floor(tiempo / 60).ToString("00");
        string segundos = Mathf.Floor(tiempo % 60).ToString("00");

        return minutos + ":" + segundos;
    }

    public void Exit() {
        SceneManager.LoadScene("Menu");
    }

    public float getTime() {
        return tiempo;
    }
}
