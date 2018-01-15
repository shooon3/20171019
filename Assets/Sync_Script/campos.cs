using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campos : manage
{

    public Transform camSync;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = camSync.position;
        transform.rotation = camSync.rotation;
	}
}
