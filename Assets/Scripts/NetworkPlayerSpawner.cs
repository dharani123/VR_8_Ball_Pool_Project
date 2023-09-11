using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    // This class spawns the network player.
    private GameObject spawnedPlayerPrefab;
    private GameObject spawnedCuePrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        spawnedPlayerPrefab = PhotonNetwork.Instantiate("NetworkPlayerPrefab", transform.position, transform.rotation);
        spawnedCuePrefab = PhotonNetwork.Instantiate("NetworkCue", transform.position, transform.rotation);


    }


    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        PhotonNetwork.Destroy(spawnedCuePrefab);

        base.OnLeftRoom();
    }
}
