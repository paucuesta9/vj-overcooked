using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
