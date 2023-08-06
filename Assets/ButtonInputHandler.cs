using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class ButtonInputHandler : MonoBehaviourPun
{

    public InputActionProperty buttonA;
    public InputActionProperty buttonB;
    public InputActionProperty buttonY;
    public PhotonView balls;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == 1) {
            ResetAllBalls();
        }

        if (obj.Code == 2)
        {
            ResetCueBall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonA.action.ReadValue<float>() == 1) {
            ResetCueBall();

            if (GameMode.Instance.gameType == GameMode.GameType.Multiplayer)
            {
                PhotonNetwork.RaiseEvent(2, null, RaiseEventOptions.Default, SendOptions.SendUnreliable);
            }
        }

        if (buttonB.action.ReadValue<float>() == 1)
        {
            ResetAllBalls();
            if (GameMode.Instance.gameType == GameMode.GameType.Multiplayer) {

                PhotonNetwork.RaiseEvent(1, null, RaiseEventOptions.Default, SendOptions.SendUnreliable);
            }
        }

        if (buttonY.action.ReadValue<float>() == 1)
        {
            ResetCueStick();
        }

    }

    private void ResetCueBall() {
        if (GameMode.Instance.gameType == GameMode.GameType.Multiplayer)
        {
            balls.RequestOwnership();
        }

        GameObject.Find("cue_ball").GetComponent<ResetScript>().Reset();
    }

    private void ResetAllBalls() {
        if (GameMode.Instance.gameType == GameMode.GameType.Multiplayer)
        {
            balls.RequestOwnership();
        }

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
