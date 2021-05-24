using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public static Mai music;

    // void Awake(){
    //     if (music == null){

    //         music = this;
    //         DontDestroyOnLoad(this);

    //     } else {
    //         Destroy(this);
    //     }
    // }

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
