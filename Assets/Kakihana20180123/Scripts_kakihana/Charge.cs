﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {

    /*エネルギー充電クラス*/

    PlayerController player; // 参照するプレイヤークラス
    GameObject playerObj; // 参照するプレイヤーオブジェクト

    public static bool chargeFlg = false; // チャージするか
	// Update is called once per frame
	void Update () {
        if (chargeFlg == true)
        {
            player.energy = 100;
            chargeFlg = false;
        }

	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Misdeed") // プレイヤーが接近した時にプレイヤー情報を取得
        {
            playerObj = col.gameObject;
            player = playerObj.GetComponent<PlayerController>();
        }
    }

    

}
