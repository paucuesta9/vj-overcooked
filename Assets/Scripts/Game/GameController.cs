using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
            SceneManager.LoadScene("Nivel1");
        if (Input.GetKeyDown("2"))
            SceneManager.LoadScene("Nivel2");
        if (Input.GetKeyDown("3"))
            SceneManager.LoadScene("Nivel3");
        if (Input.GetKeyDown("4"))
            SceneManager.LoadScene("Nivel4");
        if (Input.GetKeyDown("5"))
            SceneManager.LoadScene("Nivel5");
        if (Input.GetKeyDown("b")) GlobalVariables.canBurn = !GlobalVariables.canBurn;
    }
}
