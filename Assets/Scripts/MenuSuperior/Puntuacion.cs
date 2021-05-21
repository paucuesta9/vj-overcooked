using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour
{
    public static Puntuacion inst;
    public int puntuacion;
    public Text punt;
    // Start is called before the first frame update
    
    // void Awake() {
    //     if (Puntuacion.inst == null) {
    //         Puntuacion.inst = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else {
    //         Destroy(gameObject);
    //     }
    // }

    void Start()
    {
        punt.text = puntuacion.ToString("0");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void calcularPoint(int point) {
        puntuacion += point;
        punt.text = puntuacion.ToString();
    }
}