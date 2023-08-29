using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColourOnTurn : MonoBehaviour
{

    public Material Green;
    public Material Red;
    private bool currentTurn;


    // Start is called before the first frame update
    void Start()
    {
        currentTurn = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        bool playersTurn = GameFlowManager.Instance.playersTurn;
        if (playersTurn != currentTurn) {
            if (playersTurn)
            {
                this.GetComponent<Renderer>().material = Green;
                
            }
            else {
                this.GetComponent<Renderer>().material = Red;
            }

            currentTurn = playersTurn;
        }
    }
}
