using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Transform playerTransform;
    Transform targetTransform;
    float shootSpeed = 10.0f;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.Find("Player1").transform;
        targetTransform = GameObject.Find("Target").transform;

        transform.position = playerTransform.position;
        transform.forward = new Vector2(Camera.main.transform.position.x,Camera.main.transform.position.y);
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += transform.forward * shootSpeed * Time.deltaTime;

        if (transform.position.magnitude >= 30.0f)
        {
            Destroy(this.gameObject);
        }

        Debug.Log(this.transform.forward);

		
	}
}
