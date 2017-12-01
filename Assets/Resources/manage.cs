using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manage : MonoBehaviour {

    private bool KeyLock;

    // Use this for initialization
    void Start ()
    {
        KeyLock = false;
        //サーバへ接続、ロビーへ入室
        PhotonNetwork.ConnectUsingSettings(null);
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

    void FixedUpdate ()
    {
        transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);
    }

}
