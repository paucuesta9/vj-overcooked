using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class AparicionPedidos : MonoBehaviour
{
    public Image plato1;
    public Image plato2;
    public Image plato3;
    public Image plato4;
    public Image plato5;
    public Image plato6;
    public Image plato7;

    public int numPlatos;
    public List<int> tipoPlato;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        time += Time.deltaTime;
        if (Mathf.Floor(time / 10) == 1) {
            time = 0;
            System.Random random = new System.Random();
            int indexNum = random.Next(tipoPlato.Max() + 1);
            int plato = tipoPlato[indexNum];
            tipoPlato.RemoveAt(indexNum);
            Debug.Log(plato);
            // progresBar = (GameObject)Instantiate(progressBarModel, transform.position + new Vector3(0, 2, 0), progressBarModel.transform.rotation);
            // progresBar.transform.SetParent(transform);
        }
        */
    }
}
