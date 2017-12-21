
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    int MyId;

	// Use this for initialization
	void Start ()
    {
        MyId = Numbering(transform.tag);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	}

    int Numbering(string objTag)
    {
        return ((GameObject.FindGameObjectsWithTag(objTag)).Length) + 1;        
    }
}
