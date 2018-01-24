using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class controller : MonoBehaviour {

	//ゲームパッド番号の設定
	public GamePad.Index padID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//ゲームパッドの対応をさせる。
		var Pad = GamePad.GetState(padID, false);
		
		//キー入力をとる
		//ABXYキー
		if (Pad.A) { }
		if (Pad.B) { }
		if (Pad.X) { }
		if (Pad.Y) { }
		//十字キー
		if (Pad.Up) { }
		if (Pad.Down) { }
		if (Pad.Right) { }
		if (Pad.Left) { }
		//スティックの押し込み判定
		if (Pad.LeftStick) { }
		if (Pad.RightStick) { }
		//スティックの押し込み判定
		if (Pad.Start) { }
		if (Pad.Back) { }
		//LRの上の判定
		//※下の判定は不明
		if (Pad.RightShoulder) { }
		if (Pad.LeftShoulder) { }

		//左右スティックの方向をとる。
		//Vector2なのでlStick.xやrStick.yのように使用できる
		Vector2 lStick = Pad.LeftStickAxis;
		Vector2 rStick = Pad.rightStickAxis;
	}
}
