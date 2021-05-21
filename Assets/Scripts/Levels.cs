using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    int scene = 1;
    public GameObject[] levels = new GameObject[5];

    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void left_Arrow() {
        //Se desactiva la escena anterior
        levels[scene].SetActive(false);
        levels[scene].transform.GetChild(0).gameObject.SetActive(false);

        //Se activa la escena que se visualizará
        levels[--scene].SetActive(true);
        levels[scene].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void right_Arrow() {
        //Se desactiva la escena anterior
        levels[scene].SetActive(false);
        levels[scene].transform.GetChild(0).gameObject.SetActive(false);

        //Se activa la escena que se visualizará
        levels[++scene].SetActive(true);
        levels[scene].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void level() {
        string nivel = "Nivel" + scene.ToString();
        SceneManager.LoadScene(nivel);
    }
}
