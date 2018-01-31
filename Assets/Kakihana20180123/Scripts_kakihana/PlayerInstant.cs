using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstant : Photon.PunBehaviour {

    public GameObject playerPre;

	// Use this for initialization
	void Start () {
        if (!PhotonNetwork.connected)
        {
            return;
        }
        GameObject player = PhotonNetwork.Instantiate(
            this.playerPre.name,
            new Vector3(0.0f, 0.0f, 0.0f),
            Quaternion.identity,
            0
            );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
