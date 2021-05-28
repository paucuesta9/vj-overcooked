using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprobarPlato : MonoBehaviour
{

    string[][] platos = new string[7][];
    string[] ingredientes = new string[6];
    string[] ingredientesValidos;
    int numIngredientes;
    public GameObject[] platosFinalizados;

    GameObject platoTeminado;

    // Start is called before the first frame update
    void Start()
    {
        numIngredientes = 0;
        ingredientesValidos = new string[] { "lechuga_c", "tomate_c", "patata_o", "tomate_o", "pimiento_o", "queso_o", "Pan_c", "carne_s", "carne_queso_s", "carne_queso_cebolla_s", "pizza_m", "pizza_c" };
        platos[0] = new string[] { "lechuga_c", "lechuga_c", "lechuga_c", "tomate_c", "tomate_c" }; // Ensalada de lechuga y tomate
        platos[1] = new string[] { "ensalada_patatas_o" }; // Ensalada de patatas
        platos[2] = new string[] { "Pan_c", "carne_queso_s" }; // Cheeseburguer
        platos[3] = new string[] { "carne_queso_cebolla_s", "tomate_c", "lechuga_c", "Pan_c" }; // Hamburguesa completa
        platos[4] = new string[] { "carne_s", "patata_o", "tomate_c" }; // Carne al plato
        platos[5] = new string[] { "pizza_m_h" }; // Pizza margarita
        platos[6] = new string[] { "pizza_c_h" }; // Pizza completa
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addIngredient(string name)
    {
        bool valido = false;
        foreach (string ingrediente in ingredientesValidos)
        {
            if (name == ingrediente)
            {
                valido = true;
                break;
            }
        }
        Debug.Log("Es valido? " + valido);
        if (valido)
        {
            if (numIngredientes < 6)
            {
                ingredientes[numIngredientes] = name;
                ++numIngredientes;
                int plato = comprobarPlato();
                Debug.Log("Plato es: " + plato);
                if (plato != -1)
                {
                    foreach (Transform ingrediente in transform)
                    {
                        Destroy(ingrediente.gameObject);
                    }
                    platoTeminado = (GameObject)Instantiate(platosFinalizados[plato], transform.position, platosFinalizados[plato].transform.rotation);
                    platoTeminado.transform.SetParent(transform);
                    platoTeminado.name = plato.ToString();
                }
            }
        }
    }

    int comprobarPlato()
    {
        for (int i = 0; i < 7; ++i)
        {
            bool valido = true;
            for (int k = 0; k < platos[i].Length; ++k)
            {
                bool found = false;
                for (int j = 0; j < numIngredientes; ++j)
                {
                    string ingrediente = ingredientes[j];
                    if (ingrediente == platos[i][k]) found = true;
                }
                //if (i == 2) Debug.Log("Alimento " + ingrediente + " es encontrado? " + found);
                if (!found)
                {
                    valido = false;
                    break;
                }
            }
            if (valido) return i;
        }
        return -1;
    }
}
