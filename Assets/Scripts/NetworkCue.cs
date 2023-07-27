using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkCue : MonoBehaviour
{

    private Transform stickTransform;
    private PhotonView photonView;
    public Material transparant;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        stickTransform = GameObject.Find("Cue").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) {

            this.gameObject.GetComponent<MeshRenderer>().material = transparant;

            this.transform.position = stickTransform.position;
            this.transform.rotation = stickTransform.rotation;
        }
    }
}
