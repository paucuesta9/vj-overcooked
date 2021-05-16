using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprobarPlato : MonoBehaviour
{

    Dictionary<string, int> Platos = new Dictionary<string, int>();
    string ingredientesPlato;

    // Start is called before the first frame update
    void Start()
    {
        // Carne --> C
        // Tomate --> T
        // Cebolla --> CE
        // Lechuga --> L
        // Pan --> P
        // Pimientos --> PI
        // Queso --> Q
        // Pan + Carne + Cebolla cocinado + Queso --> H
        // Sopa de lechuga --> S
        // Patatas --> PA
        // Pizza1 --> PZ1
        // Pizza2 --> PZ2
        Platos["TTQQ"] = 1;
        Platos["LLTPIQ"] = 2;
        Platos["PC"] = 3;
        Platos["LTH"] = 4;
        Platos["CPA"] = 5;
        Platos["PZ1"] = 6;
        Platos["PZ2"] = 7;
        Platos["H"] = 8;
        Platos["S"] = 9;
        ingredientesPlato = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addIngredient(string name)
    {
        Debug.Log(name);
        if (name.Contains("carne")) ingredientesPlato += "C";
        if (name.Contains("cortado") || name.Contains("cortada"))
        {
            Debug.Log("VAMOS JOSE");
            if (name.Contains("Tomate")) ingredientesPlato += "T";
            if (name.Contains("Cebolla")) ingredientesPlato += "CE";
            if (name.Contains("Lechuga")) ingredientesPlato += "L";
            if (name.Contains("Pan")) ingredientesPlato += "P";
            if (name.Contains("Pimiento")) ingredientesPlato += "PI";
            if (name.Contains("Queso")) ingredientesPlato += "Q";
            if (name.Contains("Iham")) ingredientesPlato += "H";
            if (name.Contains("Patata")) ingredientesPlato += "PA";
            if (name.Contains("Pizza1")) ingredientesPlato += "PZ1";
            if (name.Contains("Pizza2")) ingredientesPlato += "PZ2";
            if (name.Contains("Sopa")) ingredientesPlato += "S";
        }
        int value = 0;
        if (Platos.TryGetValue(ingredientesPlato, out value))
        {
            platoTerminado();
        }
        Debug.Log(ingredientesPlato);
    }

    void platoTerminado()
    {
        Debug.Log("Plato Terminado");
    }
}
