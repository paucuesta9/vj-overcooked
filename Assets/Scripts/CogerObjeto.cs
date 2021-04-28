using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
    public GameObject destino; //reference to your hands/the position where you want your object to go
    bool canpickup; //a bool to see if you can or cant pick up the item
    GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
    GameObject Encimera;
    bool hasItem; // a bool to see if you have an item in your hand
    bool hasEncimera;
    // Start is called before the first frame update
    void Start()
    {
        canpickup = false;    //setting both to false
        hasItem = false;
        hasEncimera = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canpickup == true) // if you enter thecollider of the objecct
        {
            if (Input.GetKeyDown("e") && hasItem == false)  // can be e or any key
            {
                hasItem = true;
                ObjectIwantToPickUp.transform.position = destino.transform.position; // sets the position of the object to your hand position
                ObjectIwantToPickUp.transform.parent = destino.transform; //makes the object become a child of the parent so that it moves with the hands
                ObjectIwantToPickUp.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
            }
        }
        if (Input.GetKeyDown("q") && hasItem == true) // if you have an item and get the key to remove the object, again can be any key
        {
            Debug.Log("Suelta");
            if (hasEncimera)
            {
                Debug.Log("Suelta en encimers");
                ObjectIwantToPickUp.transform.parent = Encimera.transform;
                ObjectIwantToPickUp.transform.position = Encimera.transform.GetChild(1).position; // make the object no be a child of the hands
                Encimera.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                hasItem = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        Debug.Log("Enter in collision");
        if (other.gameObject.tag == "Utensilio" && !hasItem) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            Debug.Log("Enter in utensilio");
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
            ObjectIwantToPickUp.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Sale de colisión");
        canpickup = false;
        ObjectIwantToPickUp.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
    }

    private void OnCollisionEnter(Collision other) // to see when the player enters the collider
    {
        Debug.Log("Enter in collision");
        if (other.gameObject.tag == "Encimera" && !hasEncimeraAnObject(other.gameObject) && hasEncimera == false)
        {
            hasEncimera = true;
            Encimera = other.gameObject;
            if (hasItem == true)
            {
                Encimera.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1);
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Sale de colisión");
        // canpickup = false; //when you leave the collider set the canpickup bool to false
        if (other.gameObject.tag == "Encimera")
        {
            hasEncimera = false;
            Encimera = null;
            other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
        }
    }

    private bool hasEncimeraAnObject(GameObject encimera)
    {
        foreach (Transform hijo in encimera.transform)
        {
            if (hijo.tag == "Utensilio")
            {
                return true;
            }
        }
        return false;
    }
}
