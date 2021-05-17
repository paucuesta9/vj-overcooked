using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class AparicionPedidos : MonoBehaviour
{
    public GameObject[] platos;
    public List<int> tipoPlato;
    float time;
    int i;
    GameObject[] cartelesPedidos = new GameObject[8];

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicion_anterior;
        if (tipoPlato.Count != 0) {
            time += Time.deltaTime;

            // Aparece un nuevo pedido cada minuto
            if (Mathf.Floor(time / 60) == 1) { 
                time = 0;

                // Se escoje aleatoriamente un plato correspondiente al nivel
                System.Random random = new System.Random();
                int indexNum = random.Next(tipoPlato.Max() + 1);
                int plato = tipoPlato[indexNum];
                tipoPlato.RemoveAt(indexNum);

                // Se guarda la posición donde aparecerá el pedido
                if (i > 0) {
                    posicion_anterior = cartelesPedidos[i-1].transform.position + new Vector3(1.55f, 0, 0);
                }
                else posicion_anterior = new Vector3(-17.0610004f,13.8830004f,33.4840012f);

                // Se instancia el pedido
                cartelesPedidos[i] = (GameObject)Instantiate(platos[plato], posicion_anterior, platos[plato].transform.rotation);
                cartelesPedidos[i].transform.Rotate(62.0f, 0f, 0f, Space.Self);
                cartelesPedidos[i].transform.localScale = new Vector3(0.008f, 0.008f, 1f);
                cartelesPedidos[i].transform.SetParent(transform);

                i++;
            }
        }
        
    }

    // eliminar cartel
}
