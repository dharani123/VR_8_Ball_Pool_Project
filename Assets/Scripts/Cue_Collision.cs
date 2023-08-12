using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
public class Cue_Collision : MonoBehaviour
{

    private Vector3 prevPosition;
    private Vector3 currPosition;
    private Vector3 prevVelocity;
    private Vector3 currVelocity;
    private Vector3 accelaration;

    public PhotonView balls;

    public AudioSource audioSource;
    public AudioClip cueHit;
    private XRBaseController rightHandController;



    // Start is called before the first frame update
    void Start()
    {
        GameObject rightHand = GameObject.Find("RightHand Controller");
        rightHandController = rightHand.GetComponent<XRBaseController>();

        prevPosition = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currPosition = this.transform.position;
        
        currVelocity = (currPosition - prevPosition) / Time.deltaTime;
        accelaration = (currVelocity - prevVelocity) / Time.deltaTime;


        prevPosition = currPosition;
        prevVelocity = currVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("cue_ball"))
        {

            Rigidbody cueRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 accelarationAlongPlane = new Vector3(accelaration.x, 0, accelaration.z);

            float magnitude = accelarationAlongPlane.magnitude;
            Vector3 direction = -this.transform.forward;


            if (GameMode.Instance.gameType == GameMode.GameType.Computer) {
                if (GameFlowManager.Instance.playersTurn)
                {
                    GameFlowManager.Instance.onPlayerHitCue();
                }
                else {
                    return;
                }
            }

            if (GameMode.Instance.gameType == GameMode.GameType.Multiplayer)
            {
                balls.RequestOwnership();
            }

            cueRB.AddForce((new Vector3(direction.x, 0, direction.z)) * magnitude / 5, ForceMode.Impulse);
            audioSource.PlayOneShot(cueHit);
            rightHandController.SendHapticImpulse(1, 0.1f);
        }
    }
}
