using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMusic : MonoBehaviour
{
    public AudioSource audio3;


    public void Mute (){
        Debug.Log("LA CUCARACHAAA");
        if (!GlobalVariables.mute) {
            Debug.Log("YA NO PUEDE CAMINAR");
            audio3.Pause();
            GlobalVariables.mute = true;
        }
        else {
            Debug.Log("PORQUE LE FALTA");
            audio3.UnPause();
            GlobalVariables.mute = false;
        }
    }
}