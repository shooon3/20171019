using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour {

    BankManager bankmanager;
    GameObject bankmanagerObj;
    public GameObject thisBankObj;

    public string thisBankId;
    [SerializeField] private int haveMoney;
    public bool raidFlg = false;

	// Use this for initialization
	void Start () {
        bankmanagerObj = GameObject.Find("Status");
        bankmanager = bankmanagerObj.GetComponent<BankManager>();
        haveMoney = bankmanager.PostMoney(int.Parse(thisBankId));
        thisBankObj = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (raidFlg == true)
        {
            
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Terrorist" && raidFlg == false)
        {
            bankmanager.RaidCheck(int.Parse(thisBankId));
        }

    }
}
