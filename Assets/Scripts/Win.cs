using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public Text final_points;
    // Start is called before the first frame update
    void Start()
    {
        final_points.text = GlobalVariables.points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCredits() {
        SceneManager.LoadScene("Credits");
    }
}
