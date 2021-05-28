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
    public AudioSource audio;
    public AudioSource audio2;
    

    // Start is called before the first frame update
    void Start()
    {
        time = 30;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tipoPlato.Count != 0 && i < 5) {
            time += Time.deltaTime;

            // Aparece un nuevo pedido cada minuto
            if (Mathf.Floor(time / 30) == 1) { 
                time = 0;
                if (!GlobalVariables.mute) audio.Play();
                // Se escoje aleatoriamente un plato correspondiente al nivel
                System.Random random = new System.Random();
                int indexNum = random.Next(tipoPlato.Count);
                Debug.Log(indexNum);
                int plato = tipoPlato[indexNum];
                tipoPlato.RemoveAt(indexNum);
                Debug.Log(plato);
                // Se instancia el pedido
                cartelesPedidos[i] = (GameObject)Instantiate(platos[plato]);
                cartelesPedidos[i].transform.SetParent(transform);
                cartelesPedidos[i].transform.rotation = cartelesPedidos[i].transform.parent.rotation;
                cartelesPedidos[i].transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                // Se guarda la posición donde aparecerá el pedido
                if (i > 0) {
                    cartelesPedidos[i].transform.position = cartelesPedidos[i-1].transform.position + new Vector3(1.55f, 0, 0);
                }
                else {
                    Vector3 pos = new Vector3(-810f, 450f, 0f);
                     cartelesPedidos[i].transform.localPosition = pos;
                }
                cartelesPedidos[i].name = plato.ToString();
                
                i++;
            }
        } 
    }

    public void finished(string num) {
        audio2.Play();
        bool destroyed = false;
        for (int j = 0; j < i; ++j) {
            if (destroyed) {
                cartelesPedidos[j].transform.position -= new Vector3(1.55f, 0, 0);
                cartelesPedidos[j-1] = cartelesPedidos[j];
            }
            else if (num == cartelesPedidos[j].name) {
                platosAcabados++;
                Destroy(cartelesPedidos[j]);
                destroyed = true;
                GetComponent<Puntuacion>().calcularPoint(10);
                if (platosAcabados == total) {
                    GetComponent<Puntuacion>().calcularPoint(25);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
        if (destroyed) --i;
        else GetComponent<Puntuacion>().calcularPoint(-15);
    }

    public string getNextMeal() {
        if (i > 0) return cartelesPedidos[0].name;
        else return "-1";
    }
}
