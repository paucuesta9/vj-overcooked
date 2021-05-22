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

    // Start is called before the first frame update
    void Start()
    {
        timer.text = tiempo.ToString("00");
        timesUP = transform.Find("tabla").gameObject;
        exit = transform.Find("Exit").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > 0) {
            timer.text = calcularTiempo();
        }
        else {
            timesUP.SetActive(true);
            exit.SetActive(true);
            exit.transform.GetChild(0).gameObject.SetActive(true);
            tiempo = 0;
            timer.text = "00:00";
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
