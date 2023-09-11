using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.XR.Interaction.Toolkit;
public class Hole : MonoBehaviour
{
    // This class handles the positioning of the potted balls under the table.

    public Transform SolidsPosition;
    public Transform StripesPosition;
    public AudioSource audioSource;
    public AudioClip pottingSound;

    private XRBaseController rightHandController;
    private XRBaseController leftHandController;
    // Start is called before the first frame update
    void Start()
    {
        GameObject rightHand = GameObject.Find("RightHand Controller");
        rightHandController = rightHand.GetComponent<XRBaseController>();

        GameObject leftHand = GameObject.Find("LeftHand Controller");
        leftHandController = leftHand.GetComponent<XRBaseController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("solid") ||
            other.CompareTag("stripe") ||
            other.CompareTag("black"))
        {
            audioSource.PlayOneShot(pottingSound);
            SendVibration();


            int ballNumber = int.Parse(other.gameObject.name.Substring(4));
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            // set the solid under the table at its position
            if (ballNumber <= 7) {
                other.gameObject.transform.position = SolidsPosition.position + (-SolidsPosition.transform.up) * 0.1f * (ballNumber - 1);
                if (GameMode.Instance.gameType == GameMode.GameType.Computer)
                {
                    GameFlowManager.Instance.SolidPotted();
                }
            }
            // set the stripe under the table at its position
            if (ballNumber >= 9)
            {
                other.gameObject.transform.position = StripesPosition.position + (-StripesPosition.transform.up) * 0.1f * (ballNumber - 9);
                if (GameMode.Instance.gameType == GameMode.GameType.Computer)
                {
                    GameFlowManager.Instance.StripePotted();
                }
            }

            if (ballNumber == 8) {
                if (GameMode.Instance.gameType == GameMode.GameType.Computer){

                    GameFlowManager.Instance.CheckWhoWins();
                }
            
            }

        }

        if (other.CompareTag("cue_ball")) {
            audioSource.PlayOneShot(pottingSound);
            SendVibration();


            GameObject.Find("cue_ball").GetComponent<ResetScript>().Reset();
            if (GameMode.Instance.gameType == GameMode.GameType.Multiplayer)
            {
                PhotonNetwork.RaiseEvent(2, null, RaiseEventOptions.Default, SendOptions.SendUnreliable);
            }
        }
    }

    private void SendVibration() {
        leftHandController.SendHapticImpulse(0.3f, 1f);
        rightHandController.SendHapticImpulse(0.3f, 1f);

    }
}
