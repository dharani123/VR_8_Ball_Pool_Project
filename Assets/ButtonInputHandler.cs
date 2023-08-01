using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonInputHandler : MonoBehaviour
{

    public InputActionProperty buttonA;
    public InputActionProperty buttonB;
    public InputActionProperty buttonY;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonA.action.ReadValue<float>() == 1) {
            ResetCueBall();
        }

        if (buttonB.action.ReadValue<float>() == 1)
        {
            ResetAllBalls();
        }

        if (buttonY.action.ReadValue<float>() == 1)
        {
            ResetCueStick();
        }

    }

    private void ResetCueBall() {
        GameObject.Find("cue_ball").GetComponent<ResetScript>().Reset();
    }

    private void ResetAllBalls() { 
        GameObject[] solids = GameObject.FindGameObjectsWithTag("solid");
        foreach (GameObject solid in solids) {
            solid.GetComponent<ResetScript>().Reset();
        }

        GameObject[] stripes = GameObject.FindGameObjectsWithTag("stripe");
        foreach (GameObject stripe in stripes)
        {
            stripe.GetComponent<ResetScript>().Reset();
        }

        GameObject black = GameObject.FindGameObjectWithTag("black");
        black.GetComponent<ResetScript>().Reset();

        ResetCueBall();
    }


    private void ResetCueStick() {

        GameObject.Find("Cue").GetComponent<ResetScript>().Reset();
    }
}
