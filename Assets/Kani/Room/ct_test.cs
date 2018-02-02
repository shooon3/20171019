using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.SceneManagement;

public class ct_test : MonoBehaviour {
	//変数等の定義　(padID_X ゲームパッドを選択し、保存させる)
	//(X_OK Aが押されたときに表示されるobjectの保存先)
	//(GameStartFlgX　Aキーが押されるとtrue、Bキーが押されるとfalseになる)
	public GamePad.Index padID_A, padID_B, padID_C, padID_D;
	public GameObject A_OK, B_OK, C_OK, D_OK;
	public bool GameStartFlgA, GameStartFlgB, GameStartFlgC, GameStartFlgD;
	GameObject Game;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//キー定義
		var Pad_A = GamePad.GetState(padID_A, false);
		var Pad_B = GamePad.GetState(padID_B, false);
		var Pad_C = GamePad.GetState(padID_C, false);
		var Pad_D = GamePad.GetState(padID_D, false);
		
		//Aキー入力でゲーム開始準備
		if (Pad_A.A && GameStartFlgA == false) { GameStartFlgA= true; GameObject.Instantiate(A_OK); }
		if (Pad_B.A && GameStartFlgB == false) { GameStartFlgB = true; GameObject.Instantiate(B_OK); }
		if (Pad_C.A && GameStartFlgC == false) { GameStartFlgC = true; GameObject.Instantiate(C_OK); }
		if (Pad_D.A && GameStartFlgD == false) { GameStartFlgD = true; GameObject.Instantiate(D_OK); }
		
		//Bキー入力でゲーム開始準備の取り消し objectの消去がわからなかったので、一度タグ検索をかけています
		if (Pad_A.B) { GameStartFlgA = false; Game = GameObject.FindGameObjectWithTag("P_A"); Destroy(Game); }
		if (Pad_B.B) { GameStartFlgB = false; Game = GameObject.FindGameObjectWithTag("P_B"); Destroy(Game); }
		if (Pad_C.B) { GameStartFlgC = false; Game = GameObject.FindGameObjectWithTag("P_C"); Destroy(Game); }
		if (Pad_D.B) { GameStartFlgD = false; Game = GameObject.FindGameObjectWithTag("P_D"); Destroy(Game); }

		//ゲームスタート　全てのプレイヤーが表示されている状態になるとゲームが始まります ※一定時間ステイさせる予定
		if (GameStartFlgA == true && GameStartFlgB == true && GameStartFlgC == true && GameStartFlgD == true)
		{
            SceneManager.LoadScene("battlestage");
			Debug.Log("gamestart");
		}
	}
}
