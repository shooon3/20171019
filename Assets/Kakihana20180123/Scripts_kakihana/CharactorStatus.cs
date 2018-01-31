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
    string playerTag; // タグ

    // プレイヤー（misdeed）の基本ステータス
    private int misdeedHp = 200;
    const int misdeedMaxHp = 200;
    private int misdeedAtk = 50;
    private float misdeedSpeed = 7.0f;
    private int misdeedMoney = 0;

    // プレイヤー（guardian）の基本ステータス
    private int guardianHp = 100;
    const int guardianMaxHp = 100;
    private int guardianAtk = 1;
    private float guardianSpeed = 7.0f; 
    private int guardianMoney = 3000;

    public int postPlayerHp(int charType)
    {
        switch (charType)
        {
            case 1:
                playerHp = guardianHp;
                break;
            case 2:
                playerHp = misdeedHp;
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
                playerMaxHp = guardianMaxHp;
                break;
            case 2:
                playerMaxHp = misdeedMaxHp;
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
                playerAtk = guardianAtk;
                break;
            case 2:
                playerAtk = misdeedAtk;
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
                playerSpeed = guardianSpeed;
                break;
            case 2:
                playerSpeed = misdeedSpeed;
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
                playerMoney = guardianMoney;
                break;
            case 2:
                playerMoney = misdeedMoney;
                break;
            default:
                playerMoney = 1000;
                break;
        }
        return playerMoney;
    }

    public string postPlayertTag(int charType)
    {
        switch (charType)
        {
            case 1:
                playerTag = "Guardian";
                break;
            case 2:
                playerTag = "Misdeed";
                break;
            default:
                playerTag = "Guardian";
                break;
        }
        return playerTag;
    }
}
