
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private int MyId;
    public Transform campos;
    private PhotonView mPhotonView;

    // Use this for initialization
    void Start()
    {
        mPhotonView = GetComponent<PhotonView>();

        //自分のオブジェクトが生成された際、オーナーIDを利用して名前をPhotonNetworkにプッシュする
        if (mPhotonView.isMine)
            PhotonNetwork.playerName = "Player" + mPhotonView.ownerId;
    }

    // Update is called once per frame
    void Update()
    {
        //自分の所有していないオブジェクトは操作しない
        if (!mPhotonView.isMine)
            return;

        //移動
        transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);
        //カメラ回転（上下は上限下限を設けていないのでぐるぐるしちゃう）
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        campos.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

        //MainCameraをカメラポジションに動かす
        Camera.main.transform.position = campos.position;
        Camera.main.transform.rotation = campos.rotation;
    }
}
