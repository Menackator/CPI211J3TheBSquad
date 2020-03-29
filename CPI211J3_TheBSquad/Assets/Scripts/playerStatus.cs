using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple class that handles the player's health
/// </summary>
public class playerStatus : MonoBehaviour
{
    public float timeLeft;
    public int totNumOfTP;
    public int curNumOfTP;
    private Text timeMessage;
    private Text TPMessage;
    public bool winState = false;
    private string message;

    private void Awake()
    {
        timeMessage = GameObject.Find("Time").GetComponent<Text>();
        TPMessage = GameObject.Find("ToiletPaperPacks").GetComponent<Text>();
    }

    private void Update()
    {
        // Updates number of TP packs collected
        message = curNumOfTP.ToString() + "/" + totNumOfTP.ToString() + "Toilet Paper Packs Picked Up";
        TPMessage.text = message;

        // Updates time left
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        message = "Time Left: " + Mathf.Round(timeLeft).ToString();
        timeMessage.text = message;

        // Ends game if time runs out
        if(timeLeft < 0 && winState == false)
        {
            SceneManager.LoadScene("GameOver");
        }

        // Triggers the win state when player collects all TP packs
        if(curNumOfTP == totNumOfTP)
        {
            winState = true;
            GameObject.Find("CoronaBottle").GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Ends game if corona bottle touches player
        if(collision.gameObject.CompareTag("Enemy") && winState == false)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
