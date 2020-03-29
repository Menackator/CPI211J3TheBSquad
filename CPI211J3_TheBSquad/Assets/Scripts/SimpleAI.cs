using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for the AI to follow the player.
/// </summary>
public class simpleAI : MonoBehaviour
{
    public NavMeshAgent NavAgent
    {
        get
        {
            return GetComponent<NavMeshAgent>();
        }
    }
    public Transform currentTarget;
    public float redirectDelay;



    private void Start()
    {
        StartCoroutine(RedirectRoutine());
    }

    /// <summary>
    /// Placeholder in case we want it to do something on collision.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        //Empty
    }


    /// <summary>
    /// Continously adjusts the AI's destination with a delay.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RedirectRoutine()
    {
        while(true)
        {
            NavAgent.SetDestination(currentTarget.position);

            yield return new WaitForSeconds(redirectDelay);
        }
    }
}
