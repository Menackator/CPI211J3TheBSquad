using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPCount : MonoBehaviour
{
    //this is attached to the player
    public int TPCounter;
    public Text counter;
    public int needed;

    private void Start()
    {
        TPCounter = 0;
        needed = 6;     //change at will
    }

    // Update is called once per frame
    public void UpdateTP()
    {
        counter.text = "Toilet Paper collected: " + TPCounter;
        //or you could do
        //counter.text = (needed - TPCounter) + "toilet paper left."; 
    }
}
