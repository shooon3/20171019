using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {

    public GameObject[] citizenPlayerInfo; // 市民のプレイヤー情報
    public GameObject[] terroPlayerInfo; // テロリストのプレイヤー情報

    public int[] playerId;
    public string[] playerName;
    public int[] playerHp;

    public int citizenGroupMoney;
    public int terroGroupMoney;

    float time = 0.0f;
    public int count = 0;
    const int timeLimit = 300;

    [SerializeField] private bool[] isStun;
    [SerializeField] private bool[] isDown;
    [SerializeField] private bool[] isRaid;
    [SerializeField] private bool[] respawn;

	// Use this for initialization
	void Start () {
        terroPlayerInfo = GameObject.FindGameObjectsWithTag("Terrorist");
        citizenPlayerInfo = GameObject.FindGameObjectsWithTag("Citizen");
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (Time.frameCount % 60 == 0)
        {
            count++;
        }
        if (count >= timeLimit)
        {
            // ゲーム終了
        }

		
	}
}
