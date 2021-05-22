using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour
{
    public Text punt;

    void Start()
    {
        punt.text = GlobalVariables.points.ToString("0");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void calcularPoint(int point) {
        GlobalVariables.points += point;
        punt.text = GlobalVariables.points.ToString();
    }
}