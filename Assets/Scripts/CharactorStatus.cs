using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorStatus : MonoBehaviour
{
    /* キャラクターの基本ステータスのデータを格納しているクラス */

    // 市民（プレイヤー）の基本ステータス
    private int playerHp = 100; // HP
    const int playerMaxHp = 100; // 最大HP
    private int playerAtk = 1; // 攻撃力
    private float playerSpeed = 1.0f; // 移動速度

    // テロリスト（プレイヤー）の基本ステータス
    private int terroristHp = 200;
    const int terroristMaxHp = 200;
    private int terroristAtk = 50;
    private float terroristSpeed = 1.0f;

    // 市民（NPC）の基本ステータス
    private int citizenHp = 100;
    const int citizenMaxHp = 100;
    private int citizenAtk = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*市民（プレイヤー）データ送信メソッド*/
    public int postplayerHp() // HPを送る
    {
        return playerHp;
    }
    public int postplayerMaxHp() // 最大HPを送る
    {
        return playerMaxHp;
    }
    public int postplayerAtk() // 攻撃力を送る
    {
        return playerAtk;
    }
    public float postplayerSpeed() // 移動速度を送る
    {
        return playerSpeed;
    }

    /*テロリスト（プレイヤー）データ送信メソッド*/
    public int postterroristHp()
    {
        return terroristHp;
    }
    public int postterroristMaxHp()
    {
        return terroristMaxHp;
    }
    public int postterroristAtk()
    {
        return terroristAtk;
    }
    public float postterroristSpeed()
    {
        return terroristSpeed;
    }
}
