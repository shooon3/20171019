using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitCameraSetting : MonoBehaviour {
	//　カメラの分割方法
	public enum SplitCameraMode
	{
		horizontal,
		vertical,
		square
	};

	public SplitCameraMode mode;    //　カメラの分割方法

	//　プレイヤーを写すそれぞれのカメラ
	public Camera player1Camera;
	public Camera player2Camera;
	public Camera player3Camera;
	public Camera player4Camera;

	// Use this for initialization
	void Start()
	{
		//　２プレイヤー用の横分割
		if (mode == SplitCameraMode.horizontal)
		{
			//　カメラ３、４は非表示
			player3Camera.gameObject.SetActive(false);
			player4Camera.gameObject.SetActive(false);
			//　カメラのViewPortRectの変更
			player1Camera.rect = new Rect(0f, 0f, 0.5f, 1f);
			player2Camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);

			//　２プレイヤー用の縦分割
		}
		else if (mode == SplitCameraMode.vertical)
		{
			//　カメラ３、４は非表示
			player3Camera.gameObject.SetActive(false);
			player4Camera.gameObject.SetActive(false);
			//　カメラのViewPortRectの変更
			player1Camera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
			player2Camera.rect = new Rect(0f, 0f, 1f, 0.5f);

			//　４プレイヤー用の4分割
		}
		else if (mode == SplitCameraMode.square)
		{
			//　カメラのViewPortRectの変更
			player1Camera.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
			player2Camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
			player3Camera.rect = new Rect(0f, 0f, 0.5f, 0.5f);
			player4Camera.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);

		}
	}
}