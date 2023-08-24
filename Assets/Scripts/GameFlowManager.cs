using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    #region Singelton

    public static GameFlowManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion


    public bool playersTurn = true;
    private bool ballsMoving = false;
    private float checkBallsTimer = 1f;


    public int totalStripesPotted = 0;
    public int totalSolidsPotted = 0;
    public int StripesPottedThisTurn = 0;
    public int SolidsPottedThisTurn = 0;


    public List<Transform> holes;

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        checkBallsTimer -= Time.deltaTime;

        if(checkBallsTimer < 0)
        {
            checkBallsTimer = 0;
        }



        if (ballsMoving && checkBallsTimer == 0)
        {
            if (checkAllBallsStopped()) {
                ballsMoving = false;
                Debug.Log("Update Turn");
                UpdateTurn();
            }
        }
    }


    public void onPlayerHitCue() {
        Debug.Log("Player hit the ball");
        ballsMoving = true;
        ResetBallsPotted();
        checkBallsTimer = 1f;
    }


    private void UpdateTurn() {

        if (playersTurn && SolidsPottedThisTurn > 0) {
            playersTurn = true;
        }
        else if (!playersTurn && StripesPottedThisTurn > 0)
        {
            playersTurn = false;
        }
        else
        {
            playersTurn = !playersTurn;
        }

        Debug.Log(playersTurn ? "Now is Players Turn" : "Now is computers turn");

        if (!playersTurn) {
            ComputerHitTheCue();
        
        }
    }

    private void ComputerHitTheCue()
    {
        GameObject[] stripes = GameObject.FindGameObjectsWithTag("stripe");
        GameObject white = GameObject.Find("cue_ball");

        List<GameObject> stripesThatCanBeHit = new List<GameObject>();

        foreach (GameObject stripe in stripes) {
            Vector3 direction = (stripe.transform.position - white.transform.position).normalized;
            Vector3 directionPerpendicular = Vector3.Cross(direction, new Vector3(0, 1, 0));

            float distance = Vector3.Magnitude(stripe.transform.position - white.transform.position);

            //Debug.DrawLine(white.transform.position, white.transform.position + distance * direction + directionPerpendicular*0.03f, Color.red, 10f);
            //Debug.DrawLine(white.transform.position, white.transform.position + distance * direction - directionPerpendicular*0.03f, Color.red, 10f);


            bool isValid = true;
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(white.transform.position, (distance * direction + directionPerpendicular * 0.03f).normalized, out hit, Mathf.Infinity))
            {
                //Debug.DrawRay(white.transform.position, (distance * direction + directionPerpendicular * 0.03f).normalized * hit.distance, Color.blue, 40f);
                if (!(UnityEngine.Object.ReferenceEquals(hit.collider.gameObject, stripe))) {
                    isValid = false;
                }
            }

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(white.transform.position, (distance * direction - directionPerpendicular * 0.03f).normalized, out hit, Mathf.Infinity))
            {
                //Debug.DrawRay(white.transform.position, (distance * direction - directionPerpendicular * 0.03f).normalized * hit.distance, Color.blue, 40f);
                if (!(UnityEngine.Object.ReferenceEquals(hit.collider.gameObject, stripe)))
                {
                    isValid = false;
                }
            }

            if (isValid) {
                stripesThatCanBeHit.Add(stripe);
            }
        }

        if (stripesThatCanBeHit.Count == 0) {
            white.GetComponent<Rigidbody>().AddForce((new Vector3(UnityEngine.Random.Range(-1f,1f), 0, UnityEngine.Random.Range(-1f, 1f))) * 10, ForceMode.Impulse);

            ballsMoving = true;
            ResetBallsPotted();
            checkBallsTimer = 1f;

            return;
        }

        GameObject aboutToHitStripe = stripesThatCanBeHit[0];

        float closestDistance = Mathf.Infinity;
        Vector3 closestHole = Vector3.zero;

        foreach (Transform hole in holes) {

            float dist = Vector3.Distance(hole.position, aboutToHitStripe.transform.position);
            if (dist < closestDistance) {
                closestDistance = dist;
                closestHole = new Vector3(hole.position.x, aboutToHitStripe.transform.position.y, hole.position.z) ;
            }

        }

        Vector3 holeDirection = (closestHole - aboutToHitStripe.transform.position).normalized;

        Vector3 hitPoint = aboutToHitStripe.transform.position - 0.06f * holeDirection;

        Vector3 forceDirection = hitPoint - white.transform.position;
        white.GetComponent<Rigidbody>().AddForce((new Vector3(forceDirection.x, 0, forceDirection.z)) * 10, ForceMode.Impulse);

        ballsMoving = true;
        ResetBallsPotted();
        checkBallsTimer = 1f;

    }

    private bool checkAllBallsStopped() {
        GameObject[] solids = GameObject.FindGameObjectsWithTag("solid");
        foreach (GameObject solid in solids)
        {
            if (solid.GetComponent<Rigidbody>().velocity.magnitude > 0) {
                return false;
            }
        }

        GameObject[] stripes = GameObject.FindGameObjectsWithTag("stripe");
        foreach (GameObject stripe in stripes)
        {
            if (stripe.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                return false;
            }
        }

        GameObject black = GameObject.FindGameObjectWithTag("black");

        if (black.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            return false;
        }

        GameObject white = GameObject.Find("cue_ball");

        if (white.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            return false;
        }


        return true;
    }

    public void SolidPotted() {

        totalSolidsPotted++;
        SolidsPottedThisTurn++;
    
    }


    public void StripePotted() {
        totalSolidsPotted++;
        StripesPottedThisTurn++;
    }

    private void ResetBallsPotted() {
        SolidsPottedThisTurn = 0;
        StripesPottedThisTurn = 0;
    }
}
