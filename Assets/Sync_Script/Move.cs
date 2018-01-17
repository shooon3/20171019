
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private int MyId;
    public Transform campos;
    private PhotonView mPhotonView;
    PhotonPlayer[] playerInfo;

	// Use this for initialization
	void Start ()
    {
        mPhotonView = GetComponent<PhotonView>();
        playerInfo = PhotonNetwork.playerList;
        MyId = Numbering(transform.tag);
        Debug.Log(MyId);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mPhotonView.isMine)
        {
            transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
            campos.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

            Camera.main.transform.position = campos.position;
            Camera.main.transform.rotation = campos.rotation;
        }
    }

    int Numbering(string objTag)
    {
        return playerInfo[playerInfo.Length - 1].ID;
    }
}
