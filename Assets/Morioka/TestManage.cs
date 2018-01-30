using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManage : Photon.MonoBehaviour
{

    public bool inRoom;
    PhotonPlayer[] photonPlayer;

    // Use this for initialization
    void Start()
    {
        inRoom = false;
        //サーバへ接続、ロビーへ入室
        PhotonNetwork.ConnectUsingSettings("v1.0");
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
    }

    //ルームの入室に失敗
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("On Created Room");
        //自分でルームを作成して入室
        PhotonNetwork.CreateRoom(null);
        inRoom = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            PhotonNetwork.Instantiate("Cube", transform.position, Quaternion.identity, 0);
    }

    void OnReceivedRoomListUpdate()
    {
        //プレイヤーオブジェクトをResourcesからロード
        photonPlayer = PhotonNetwork.playerList;
        Debug.Log(photonPlayer.Length);
    }
}
