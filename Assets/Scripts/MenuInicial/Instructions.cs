using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    int num = 0;
    public GameObject[] pag = new GameObject[9];
    public GameObject left_Arrow_Button;
    public GameObject right_Arrow_Button;

    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void left_Arrow_inst() {
        if (num == 14) right_Arrow_Button.SetActive(true);
        if (num > 0 ) {
            //Se desactiva la escena anterior
            pag[num].SetActive(false);
            foreach (Transform hijo in pag[num].transform) {
                hijo.gameObject.SetActive(false);
            }
            
            //Se activa la escena que se visualizará
            pag[--num].SetActive(true);
            foreach (Transform hijo in pag[num].transform) {
                hijo.gameObject.SetActive(true);
            }
        }
        if (num == 0) left_Arrow_Button.SetActive(false);
    }

    public void right_Arrow_inst() {
        if (num == 0) left_Arrow_Button.SetActive(true);
        if (num < 14 ) {
            //Se desactiva la escena anterior
            pag[num].SetActive(false);
            foreach (Transform hijo in pag[num].transform) {
                hijo.gameObject.SetActive(false);
            }
            //Se activa la escena que se visualizará
            pag[++num].SetActive(true);
            Debug.Log(num);
            foreach (Transform hijo in pag[num].transform) {
                hijo.gameObject.SetActive(true);
            }
        }
        if (num == 14) right_Arrow_Button.SetActive(false);
        
    }

}
