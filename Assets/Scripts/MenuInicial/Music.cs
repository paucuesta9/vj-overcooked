using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    private static Music instance = null;
    public static Music Instance {
         get { 
             return instance; }
     }

     void Awake()
     {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
             instance = this;
        }
     }

     void Update() {
        if ("Menu" == SceneManager.GetActiveScene().name || "Levels" == SceneManager.GetActiveScene().name 
            || "Instructions" == SceneManager.GetActiveScene().name || "Options" == SceneManager.GetActiveScene().name) {
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

     }
}
