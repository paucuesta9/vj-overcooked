using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float tiempo;
    public Text timer;
    // Start is called before the first frame update
    void Start()
    {
        timer.text = tiempo.ToString("00");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > 0) {
            timer.text = calcularTiempo();
        }

    }

    public string calcularTiempo() {

        tiempo -= Time.deltaTime;

        string minutos = Mathf.Floor(tiempo / 60).ToString("00");
        string segundos = Mathf.Floor(tiempo % 60).ToString("00");

        return minutos + ":" + segundos;
    }
}
