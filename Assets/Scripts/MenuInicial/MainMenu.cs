using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame () {
        GlobalVariables.scene = 1;
        SceneManager.LoadScene("Options");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void LoadLevels() {
        SceneManager.LoadScene("Levels");
    }

    public void LoadInstructions() {
        SceneManager.LoadScene("Instructions");
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }
}
