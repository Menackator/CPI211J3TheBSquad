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
    public AudioClip HSSound;
    public AudioClip TPSound;
    private AudioSource playerSounds;
    public float timeLeft;
    public int totNumOfTP;
    public int curNumOfTP;
    public int curNumOfHS;
    private int preNumOfTP;
    private Text timeMessage;
    private Text TPMessage;
    private Text DangerMessage;
    private Text HSMessage;
    private Text promptText;
    private Image fill;
    private float preBottleSpeed;
    public Color minHSCol;
    public Color fullHSCol;
    public bool HSEnabled;
    public float timeOfHSEffect;
    public float HSLeft;
    public bool TPArea = false;
    public bool winState = false;
    private string message;

    public float distanceToBot;

    private void Awake()
    {
        playerSounds = GetComponent<AudioSource>();
        preNumOfTP = curNumOfTP;
        GameObject.FindGameObjectWithTag("HS_Slide").GetComponent<Slider>().maxValue = timeOfHSEffect;
        fill = GameObject.FindWithTag("PowerUpColor").GetComponent<Image>();
        DangerMessage = GameObject.Find("DangerMessage").GetComponent<Text>();
        promptText = GameObject.Find("PickUpMessage").GetComponent<Text>();
        timeMessage = GameObject.Find("Time").GetComponent<Text>();
        TPMessage = GameObject.Find("ToiletPaperPacks").GetComponent<Text>();
        HSMessage = GameObject.Find("HandSanitizer").GetComponent<Text>();
        preBottleSpeed = GameObject.Find("CoronaBottle").GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
    }

    private void Update()
    {
        distanceToBot = Vector3.Distance(transform.position, GameObject.Find("CoronaBottle").transform.position);
        if (distanceToBot < 7.5)
        {
            DangerMessage.text = "LOOK OUT";
        }
        else
        {
            DangerMessage.text = "";
        }

        // HS effect countdown
        if (HSEnabled == true && HSLeft > 0)
        {
            HSLeft -= Time.deltaTime;
            GameObject.FindGameObjectWithTag("HS_Slide").GetComponent<Slider>().value = HSLeft;
            fill.color = Color.Lerp(minHSCol,fullHSCol,HSLeft/timeOfHSEffect);
        }
        else
        {
            GameObject.Find("CoronaBottle").GetComponent<UnityEngine.AI.NavMeshAgent>().speed = preBottleSpeed;
            HSEnabled = false;
            fill.enabled = false;
        }

        // Updates message prompt
        if (TPArea == true)
        {
            promptText.text = "Press E to pick up";
        }
        else
        {
            promptText.text = "";
        }

        // Increases speed of bottle upon collection of TP
        if (preNumOfTP < curNumOfTP)
        {
            preNumOfTP = curNumOfTP;
            GameObject.Find("CoronaBottle").GetComponent<UnityEngine.AI.NavMeshAgent>().speed += 1.6f;
            preBottleSpeed = GameObject.Find("CoronaBottle").GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
        }

        // Updates number of HS collected
        message = "Hand Sanitizers: " + curNumOfHS;
        HSMessage.text = message;

        // Updates number of TP packs collected
        message = curNumOfTP.ToString() + "/" + totNumOfTP.ToString() + " Toilet Paper Rolls Picked Up";
        TPMessage.text = message;

        if (Input.GetKeyDown(KeyCode.R) && curNumOfHS > 0 && HSEnabled == false)
        {
            playerSounds.clip = HSSound;
            playerSounds.Play();
            curNumOfHS -= 1;
            HSLeft = timeOfHSEffect;
            fill.enabled = true;
            HSEnabled = true;
            preBottleSpeed = GameObject.Find("CoronaBottle").GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
            GameObject.Find("CoronaBottle").GetComponent<UnityEngine.AI.NavMeshAgent>().speed = preBottleSpeed/2.0f;
            //StartCoroutine(HSPowerUp());
        }

        // Updates time left
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        message = "Time Left: " + Mathf.Round(timeLeft).ToString();
        timeMessage.text = message;

        // Ends game if time runs out
        if (timeLeft < 0 && winState == false)
        {
            SceneManager.LoadScene("GameOver");
        }

        // Triggers the win state when player collects all TP packs
        if (curNumOfTP == totNumOfTP)
        {
            winState = true;
            promptText.fontSize  = 80;
            promptText.text = "You have defeated the Coronavir-bottle by getting enough toilet paper!"
                            + "\nCongratulations!";
            GameObject.Find("CoronaBottle").GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0;
        }
    }

    //IEnumerator HSPowerUp()
    //{
        
    //    yield return null;
    //}
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "TP")
        {
            TPArea = true;

            // Collects TP
            if (Input.GetKeyDown(KeyCode.E))
            {
                promptText.text = "";
                curNumOfTP++;
                TPArea = false;
                playerSounds.clip = TPSound;
                playerSounds.Play();
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "HS")
        {
            TPArea = true;

            // Collects HS
            if (Input.GetKeyDown(KeyCode.E))
            {
                promptText.text = "";
                curNumOfHS++;
                TPArea = false;
                playerSounds.clip = TPSound;
                playerSounds.Play();
                Destroy(other.gameObject);
            }
        }

        // Ends game if corona bottle touches player
        if(other.gameObject.CompareTag("Enemy") && winState == false)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "TP" || other.gameObject.tag == "HS")
        {
            TPArea = false;
        }
    }

}
