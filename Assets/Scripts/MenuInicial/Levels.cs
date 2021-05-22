using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    int scene = 0;
    public GameObject[] levels = new GameObject[5];
    public GameObject left_Arrow_Button;
    public GameObject right_Arrow_Button;

    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void left_Arrow() {
        
        if (scene == 4) right_Arrow_Button.SetActive(true);
        if (scene > 0 ) {
            //Se desactiva la escena anterior
            levels[scene].SetActive(false);
            levels[scene].transform.GetChild(0).gameObject.SetActive(false);

            //Se activa la escena que se visualizará
            levels[--scene].SetActive(true);
            levels[scene].transform.GetChild(0).gameObject.SetActive(true);
        } 
        if (scene == 0) left_Arrow_Button.SetActive(false);
    }

    public void right_Arrow() {
        Debug.Log(scene);
        if (scene == 0) left_Arrow_Button.SetActive(true);
        if (scene < 4 ) {
            //Se desactiva la escena anterior
            levels[scene].SetActive(false);
            levels[scene].transform.GetChild(0).gameObject.SetActive(false);

            //Se activa la escena que se visualizará
            levels[++scene].SetActive(true);
            levels[scene].transform.GetChild(0).gameObject.SetActive(true);
        }
        if (scene == 4) right_Arrow_Button.SetActive(false);
        
    }

    public void level() {
        scene++;
        string nivel = "Nivel" + scene.ToString();
        SceneManager.LoadScene(nivel);
    }
}
