using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raid : MonoBehaviour {

    PlayerController player;
    GUIManager guimanager;
    GameManagement gm;


    public GameObject[] beRaidObj;
    public GameObject guiObj;
    public GameObject masterObj;
    public GameObject raidPlayer;
    public int raidCount = 0;

    public float reRaidTime = 0.0f;
    public float reRaidTimeLimit = 20.0f;

    bool isRaid = false;
    bool atakked = false;


	// Use this for initialization
	void Start () {
        guiObj = GameObject.Find("Status");
        masterObj = GameObject.Find("Master");
        gm = masterObj.GetComponent<GameManagement>();
        guimanager = masterObj.GetComponent<GUIManager>();
        //player = GameObject.FindGameObjectWithTag("Misdeed").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isRaid == true)
        {
            reRaidTime += Time.deltaTime;
            if (reRaidTime > reRaidTimeLimit)
            {
                isRaid = false;
            }
        }
	}

    public void MisdeedRaid()
    {
        if (isRaid == false)
        {
            raidCount++;
            isRaid = true;
            reRaidTime = 0.0f;
        }
    }

    void OnTriggerEnter(Collider col)
    {
    }
}
