using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manage : Photon.MonoBehaviour
{

    public bool inRoom;
    GameObject player;
    public Vector3 InstancePos = new Vector3(0, 5, 0);
    public Transform room;

    // Use this for initialization
    void Start ()
    {
        inRoom = false;
        //サーバへ接続、ロビーへ入室
        PhotonNetwork.ConnectUsingSettings("v1.0");
        //プレイヤーオブジェクトをResourcesからロード
        player = Resources.Load("Player") as GameObject;
	}

    //ロビーに入室した
    void OnJoinedLobby()
    {
        Debug.Log("On Joined Lobby");
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

    void Update()
    {
        if (inRoom)
        {
            PhotonNetwork.Instantiate("Player", room.position + InstancePos, Quaternion.identity, 0);
            inRoom = false;
        }
    }

    public void OnClickJoin()
    {
        //ルームへ接続する
        PhotonNetwork.JoinRandomRoom();
    }
}
