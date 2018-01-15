
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    int MyId;
    public Transform campos;

	// Use this for initialization
	void Start ()
    {
        MyId = Numbering(transform.tag);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Input.GetAxis("Horizontal") * 0.1f, 0, Input.GetAxis("Vertical") * 0.1f);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        campos.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

        Camera.main.transform.position = campos.position;
        Camera.main.transform.rotation = campos.rotation;
    }

    int Numbering(string objTag)
    {
        return ((GameObject.FindGameObjectsWithTag(objTag)).Length) + 1;
    }
}
