using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicWin : MonoBehaviour
{
    private static musicWin instance = null;
    public static musicWin Instance {
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
        if ("WIN" == SceneManager.GetActiveScene().name || "Credits" == SceneManager.GetActiveScene().name ) {
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

     }
}
