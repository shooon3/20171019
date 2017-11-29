using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorStatus : MonoBehaviour
{
    /* キャラクターの基本ステータスのデータを格納しているクラス */
    /* 市民とテロリストと分けてキャラクター情報を送信していたが
       プレイヤークラスの変数playerTypeを元にこのクラスで
       ステータスを振り分けるように変更
    */

    // プレイヤーステータスの初期化
    private int playerHp = 0; // HP
    int playerMaxHp = 0; // 最大HP
    private int playerAtk = 0; // 攻撃力
    private float playerSpeed = 0.0f; // 移動速度
    private int playerMoney = 0; // 所持金

    // プレイヤー（テロリスト）の基本ステータス
    private int terroristHp = 200;
    const int terroristMaxHp = 200;
    private int terroristAtk = 50;
    private float terroristSpeed = 7.0f;
    private int terroristMoney = 0;

    // プレイヤー（市民）の基本ステータス
    private int citizenHp = 100;
    const int citizenMaxHp = 100;
    private int citizenAtk = 1;
    private float citizenSpeed = 7.0f; 
    private int citizenMoney = 3000;

    public int postPlayerHp(int charType)
    {
        switch (charType)
        {
            case 1:
                playerHp = citizenHp;
                break;
            case 2:
                playerHp = terroristHp;
                break;
            default:
                playerHp = 10;
                break;
        }
        return playerHp;
    }

    public int postPlayerMaxHp(int charType)
    {
        switch (charType)
        {
            case 1:
                playerMaxHp = citizenMaxHp;
                break;
            case 2:
                playerMaxHp = terroristMaxHp;
                break;
            default:
                playerMaxHp = 10;
                break;
        }
        return playerMaxHp;
    }
    public int postPlayerAtk(int charType)
    {
        switch (charType)
        {
            case 1:
                playerAtk = citizenAtk;
                break;
            case 2:
                playerAtk = terroristAtk;
                break;
            default:
                playerAtk = 2;
                break;
        }
        return playerAtk;
    }

    public float postPlayerSpeed(int charType)
    {
        switch (charType)
        {
            case 1:
                playerSpeed = citizenSpeed;
                break;
            case 2:
                playerSpeed = terroristSpeed;
                break;
            default:
                playerSpeed = 5.0f;
                break;
        }
        return playerSpeed;
    }

    public int postPlayerMoney(int charType)
    {
        switch (charType)
        {
            case 1:
                playerMoney = citizenMoney;
                break;
            case 2:
                playerMoney = terroristMoney;
                break;
            default:
                playerMoney = 1000;
                break;
        }
        return playerMoney;
    }
}
