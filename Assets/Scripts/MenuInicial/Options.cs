using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void MouseClick() {
        GlobalVariables.mouse = true;
        string nivel = "Nivel" + GlobalVariables.scene.ToString();
        SceneManager.LoadScene(nivel);
    }

    public void KeyboardClick() {
        GlobalVariables.mouse = false;
        string nivel = "Nivel" + GlobalVariables.scene.ToString();
        SceneManager.LoadScene(nivel);
    }
}
