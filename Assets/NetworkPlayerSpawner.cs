using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{

    private GameObject spawnedPlayerPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        spawnedPlayerPrefab = PhotonNetwork.Instantiate("NetworkPlayerPrefab", transform.position, transform.rotation);
    
    }


    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        
        base.OnLeftRoom();
    }
}
