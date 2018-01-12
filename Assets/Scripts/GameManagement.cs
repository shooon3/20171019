using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {

    /*共有したいゲーム情報を管理するクラス*/

    public GameObject[] citizenPlayerInfo; // 市民のプレイヤー情報
    public GameObject[] terroPlayerInfo; // テロリストのプレイヤー情報

    public int[] playerId; // プレイヤーID
    public string[] playerName; // プレイヤー名
    public int[] playerHp; // プレイヤーHP
    public bool[] ruTerrorist; // テロリストかどうか※ruはare youの省略形

    public int citizenGroupMoney; // 市民側の所持金
    public int terroGroupMoney; // テロリスト側の所持金

    float time = 0.0f; // 経過時間のフレーム
    public int count = 0; // 経過時間
    const int timeLimit = 300; // 制限時間

    [SerializeField] private bool[] isStun; // 気絶しているか
    [SerializeField] private bool[] isDown; // 確保されたか（テロリスト）
    [SerializeField] private bool[] isRaid; // 襲撃されたか
    [SerializeField] private bool[] respawn; // 復活待機

	// Use this for initialization
	void Start () {
        terroPlayerInfo = GameObject.FindGameObjectsWithTag("Terrorist");
        citizenPlayerInfo = GameObject.FindGameObjectsWithTag("Citizen");
        playerId = new int[terroPlayerInfo.Length + citizenPlayerInfo.Length];
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (Time.frameCount % 60 == 0) // １秒毎にcountを足す
        {
            count++;
        }
        if (count >= timeLimit) // countが300（5分）超えるとゲーム終了
        {
            // ゲーム終了メソッド
        }

		
	}
}
