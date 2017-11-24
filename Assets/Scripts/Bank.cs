using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour {

    BankManager bankmanager;
    GameObject bankmanagerObj;

    public string thisBankId;
    private int haveMoney;
    public static bool raidFlg = false;

	// Use this for initialization
	void Start () {
        bankmanagerObj = GameObject.Find("Status");
        bankmanager = bankmanagerObj.GetComponent<BankManager>();
        bankmanager.SetMoneyID(int.Parse(thisBankId));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
