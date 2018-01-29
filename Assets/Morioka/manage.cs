using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manage : Photon.MonoBehaviour
{

    public bool inRoom;

    public Transform[] spawnPos = new Transform[2];
    public Vector3 InstancePos = new Vector3(0, 5, 0);
    public Transform room;
    int count = 0;
    PhotonPlayer[] photonPlayer;

    // Use this for initialization
    void Start ()
    {
        inRoom = false;
        //サーバへ接続、ロビーへ入室
        PhotonNetwork.ConnectUsingSettings("v1.0");
        //プレイヤーオブジェクトをResourcesからロード
        photonPlayer = PhotonNetwork.playerList;
    }

    //ロビーに入室した
    void OnJoinedLobby()
    {
        Debug.Log("On Joined Lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    //ルームへ入室した
    void OnJoinedRoom()
    {
        //入室完了を出力し、キーロック解除
        Debug.Log("On Joined Room");
        inRoom = true;
        InstantiatePlayerObject();
    }

    //ルームの入室に失敗
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("On Created Room");
        //自分でルームを作成して入室
        PhotonNetwork.CreateRoom(null);
        inRoom = true;
        InstantiatePlayerObject();
    }

    void InstantiatePlayerObject()
    {
        PhotonNetwork.Instantiate("Player", room.position + InstancePos, Quaternion.identity, 0);
    }
}
