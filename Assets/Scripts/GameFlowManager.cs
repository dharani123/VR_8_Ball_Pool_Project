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
        checkBallsTimer = 1f;
    }


    private void UpdateTurn() {
        playersTurn = !playersTurn;

        Debug.Log(playersTurn ? "Now is Players Turn" : "Now is computers turn");
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
}
