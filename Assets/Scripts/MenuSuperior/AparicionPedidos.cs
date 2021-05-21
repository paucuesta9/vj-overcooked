using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class AparicionPedidos : MonoBehaviour
{
    public GameObject[] platos;
    public List<int> tipoPlato;
    public int total;
    float time;
    int i;
    int platosAcabados = 0;
    GameObject[] cartelesPedidos = new GameObject[8];

    // Start is called before the first frame update
    void Start()
    {
        time = 60;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicion_anterior;
        if (tipoPlato.Count != 0 && i < 6) {
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
                cartelesPedidos[i].name = plato.ToString();

                i++;
            }
        } 
    }

    public void finished(int num) {
        int destroyed = 0;
        for (int j = 0; j < i; ++j) {
            if (destroyed == 1) {
                cartelesPedidos[j].transform.position -= new Vector3(1.55f, 0, 0);
                cartelesPedidos[j-1] = cartelesPedidos[j];
            }
            if (num.ToString() == cartelesPedidos[j].name) {
                platosAcabados++;
                Destroy(cartelesPedidos[j]);
                destroyed = 1;
                GetComponent<Puntuacion>().calcularPoint(10);
                if (platosAcabados == total) {
                    GetComponent<Puntuacion>().calcularPoint(25);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
         --i;
    }
}
