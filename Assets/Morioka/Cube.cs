
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 vector = new Vector3(1, 1, 1) * Random.Range(-1, 1);
        transform.Translate(vector);
	}
}
