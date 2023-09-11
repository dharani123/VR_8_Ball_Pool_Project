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

    // This class handles the flow of the game like updating turn, AI, tracking potted balls and checking win condition.

    public bool playersTurn = true;
    private bool ballsMoving = false;
    private float checkBallsTimer = 1f;


    public int totalStripesPotted = 0;
    public int totalSolidsPotted = 0;
    public int StripesPottedThisTurn = 0;
    public int SolidsPottedThisTurn = 0;

    public GameObject youWinText;
    public GameObject computerWinsText;


    public List<Transform> holes;


    public AudioSource audioSource;
    public AudioClip yourTurn;
    public AudioClip computerTurn;
    public AudioClip win;

    void Start()
    {

    }

    public void ResetGame()
    {
        playersTurn = true;
        ballsMoving = false;
        checkBallsTimer = 1f;
        totalStripesPotted = 0;
        totalSolidsPotted = 0;
        StripesPottedThisTurn = 0;
        SolidsPottedThisTurn = 0;

    }

    // Update is called once per frame
    void Update()
    {
        checkBallsTimer -= Time.deltaTime;

        if (checkBallsTimer < 0)
        {
            checkBallsTimer = 0;
        }


        // If balls started moving, after 1 second checks when all balls stop and update turn 
        if (ballsMoving && checkBallsTimer == 0)
        {
            if (checkAllBallsStopped())
            {
                ballsMoving = false;
                Debug.Log("Update Turn");
                UpdateTurn();
            }
        }
    }


    public void onPlayerHitCue()
    {
        Debug.Log("Player hit the ball");
        ballsMoving = true;
        ResetBallsPotted();
        checkBallsTimer = 1f;
    }


    private void UpdateTurn()
    {
        // if players turn and potted solids retain turn
        if (playersTurn && SolidsPottedThisTurn > 0)
        {
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
        Debug.Log(totalStripesPotted);
        if (playersTurn)
        {
            audioSource.PlayOneShot(yourTurn);
        }
        else
        {
            audioSource.PlayOneShot(computerTurn);
        }

        if (!playersTurn)
        {
            ComputerHitTheCue();

        }
    }

    private void ComputerHitTheCue()
    {
        GameObject[] stripes = GameObject.FindGameObjectsWithTag("stripe");
        GameObject white = GameObject.Find("cue_ball");

        List<GameObject> stripesThatCanBeHit = new List<GameObject>();

        // if all the strips are potted hit the black ball
        if (totalStripesPotted == 7)
        {
            HitBlackBall(white);
            return;
        }

        foreach (GameObject stripe in stripes)
        {
            Vector3 direction = (stripe.transform.position - white.transform.position).normalized;
            Vector3 directionPerpendicular = Vector3.Cross(direction, new Vector3(0, 1, 0));

            float distance = Vector3.Magnitude(stripe.transform.position - white.transform.position);

            //Debug.DrawLine(white.transform.position, white.transform.position + distance * direction + directionPerpendicular*0.03f, Color.red, 10f);
            //Debug.DrawLine(white.transform.position, white.transform.position + distance * direction - directionPerpendicular*0.03f, Color.red, 10f);

            bool isValid = true;
            RaycastHit hit;
            // Draw Raycast from white ball to the left edge of the stripe
            if (Physics.Raycast(white.transform.position, (distance * direction + directionPerpendicular * 0.03f).normalized, out hit, Mathf.Infinity))
            {
                //Debug.DrawRay(white.transform.position, (distance * direction + directionPerpendicular * 0.03f).normalized * hit.distance, Color.blue, 40f);
                if (!(UnityEngine.Object.ReferenceEquals(hit.collider.gameObject, stripe)))
                {
                    isValid = false;
                }
            }

            // Draw Raycast from white ball to the right edge of the stripe
            if (Physics.Raycast(white.transform.position, (distance * direction - directionPerpendicular * 0.03f).normalized, out hit, Mathf.Infinity))
            {
                //Debug.DrawRay(white.transform.position, (distance * direction - directionPerpendicular * 0.03f).normalized * hit.distance, Color.blue, 40f);
                if (!(UnityEngine.Object.ReferenceEquals(hit.collider.gameObject, stripe)))
                {
                    isValid = false;
                }
            }
            // if there are no balls between them then make the stipe as valid to hit.
            if (isValid)
            {
                stripesThatCanBeHit.Add(stripe);
            }
        }

        // if no stipes can be hit apply random force.
        if (stripesThatCanBeHit.Count == 0)
        {
            white.GetComponent<Rigidbody>().AddForce((new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f))) * 10, ForceMode.Impulse);

            ballsMoving = true;
            ResetBallsPotted();
            checkBallsTimer = 1f;

            return;
        }


        GameObject aboutToHitStripe = stripesThatCanBeHit[0];

        float closestDistance = Mathf.Infinity;
        Vector3 closestHole = Vector3.zero;

        // get the closet hole
        foreach (Transform hole in holes)
        {

            float dist = Vector3.Distance(hole.position, aboutToHitStripe.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestHole = new Vector3(hole.position.x, aboutToHitStripe.transform.position.y, hole.position.z);
            }

        }

        // target direction of the stripe.
        Vector3 holeDirection = (closestHole - aboutToHitStripe.transform.position).normalized;

        // target point of the white ball.
        Vector3 hitPoint = aboutToHitStripe.transform.position - 0.06f * holeDirection;

        Vector3 forceDirection = hitPoint - white.transform.position;
        white.GetComponent<Rigidbody>().AddForce((new Vector3(forceDirection.x, 0, forceDirection.z)) * 10, ForceMode.Impulse);

        ballsMoving = true;
        ResetBallsPotted();
        checkBallsTimer = 1f;

    }

    private void HitBlackBall(GameObject white)
    {
        // similar logic of hitting the stripe applied on black
        GameObject aboutToHitBlack = GameObject.FindGameObjectWithTag("black");

        float closestDist = Mathf.Infinity;
        Vector3 closeHole = Vector3.zero;

        foreach (Transform hole in holes)
        {

            float dist = Vector3.Distance(hole.position, aboutToHitBlack.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closeHole = new Vector3(hole.position.x, aboutToHitBlack.transform.position.y, hole.position.z);
            }

        }

        Vector3 holeDirec = (closeHole - aboutToHitBlack.transform.position).normalized;

        Vector3 hittingPoint = aboutToHitBlack.transform.position - 0.06f * holeDirec;

        Vector3 forceDir = hittingPoint - white.transform.position;
        white.GetComponent<Rigidbody>().AddForce((new Vector3(forceDir.x, 0, forceDir.z)) * 10, ForceMode.Impulse);

        ballsMoving = true;
        ResetBallsPotted();
        checkBallsTimer = 1f;
    }

    private bool checkAllBallsStopped()
    {
        GameObject[] solids = GameObject.FindGameObjectsWithTag("solid");
        foreach (GameObject solid in solids)
        {
            if (solid.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
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

    public void SolidPotted()
    {

        totalSolidsPotted++;
        SolidsPottedThisTurn++;

    }


    public void StripePotted()
    {
        totalStripesPotted++;
        StripesPottedThisTurn++;
    }

    private void ResetBallsPotted()
    {
        SolidsPottedThisTurn = 0;
        StripesPottedThisTurn = 0;
    }


    public void CheckWhoWins()
    {
        audioSource.PlayOneShot(win);
        if (playersTurn)
        {
            if (totalSolidsPotted == 7)
            {
                Instantiate(youWinText);
            }
            else
            {
                Instantiate(computerWinsText);
            }
        }
        else {
            if (totalStripesPotted == 7)
            {
                Instantiate(computerWinsText);
            }
            else {
                Instantiate(youWinText);
            }
        
        }
    }
}