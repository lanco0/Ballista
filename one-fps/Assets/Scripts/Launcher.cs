using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting "); // The correct method is Debug.Log, not Debug.log
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() // The correct method name is OnConnectedToMaster
    {
        Debug.Log("Connected"); // The correct method is Debug.Log, not Debug.log
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() // The correct method name is OnJoinedLobby
    {
        Debug.Log("Joined Lobby"); // The correct method is Debug.Log, not Debug.log
    }

    // Update is called once per frame
    void Update()
    {
        // You can add update logic here if needed
    }
}
