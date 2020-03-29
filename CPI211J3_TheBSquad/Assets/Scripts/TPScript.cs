using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPScript : MonoBehaviour
{
    public Text promptText;

    private bool TPArea = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && TPArea == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<TPCount>().TPCounter++;
            GameObject.FindGameObjectWithTag("Player").GetComponent<TPCount>().UpdateTP();
            Destroy(gameObject);
            promptText.text = "";
        }
        else if (TPArea == true)
        {
            promptText.text = "Press E to pick up";
        }
        else
        {
            promptText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            TPArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            TPArea = false;
        }
    }

}
