using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashYourHands : MonoBehaviour
{

    public float washMeter = 100;

    private float originalAmount;

    public float soapAmount = 20;

    private float difference;

    public Object soap;

    private bool danger;

    // Start is called before the first frame update
    void Start()
    {
        originalAmount = washMeter;
        difference = washMeter - soapAmount;
        danger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (washMeter > 0)
        {
            washMeter -= Time.deltaTime;
            danger = false;
        }
        else
        {
            washMeter = 0;
            danger = true;
        }
        //Debug.Log(washMeter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (soap)
        {
            if (washMeter >= difference)
            {
                washMeter = originalAmount;
            }
            else
            {
                washMeter += soapAmount;
            }
            other.gameObject.SetActive(false);
        }
    }
}
