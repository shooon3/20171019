using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manage : MonoBehaviour {

    private bool KeyLock;
    GameObject player;
    public Vector3 InstancePos = new Vector3(0, 5, 0);

    // Use this for initialization
    void Start ()
    {
        KeyLock = false;
        //サーバへ接続、ロビーへ入室
        PhotonNetwork.ConnectUsingSettings(null);
        //プレイヤーオブジェクトをResourcesからロード
        player = Resources.Load("Player") as GameObject;
	}

    //ロビーに入室した
    void OnJoinedLobby()
    {
        //どこかのルームへ接続する
        PhotonNetwork.JoinRandomRoom();
    }

    //ルームへ入室した
    void OnJoinedRoom()
    {
        //入室完了を出力し、キーロック解除
        Debug.Log("ルームへ入室しました");
        KeyLock = true;
    }

    //ルームの入室に失敗
    void OnPhotonRandomJoinFailed()
    {
        //自分でルームを作成して入室
        PhotonNetwork.CreateRoom(null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.Instantiate("Player", InstancePos, Quaternion.identity, 0);
    }

    void FixedUpdate()
    {

    }

}
