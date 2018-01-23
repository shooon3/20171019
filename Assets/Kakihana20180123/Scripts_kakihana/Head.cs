using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

    public Camera camera;
    public Curve headMotion = new Curve();
    public LerpController lerpcontroller = new LerpController();
    public RigidBodyFPSController rbfpscontroller;
    public float strideIntaerval;
    [Range(0.0f, 1.0f)] public float RunStrideLength;
    private bool previousGrounded;
    private Vector3 originalCameraPotiton;

	// Use this for initialization
	void Start () {
        headMotion.Setup(camera, strideIntaerval);
        originalCameraPotiton = camera.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newCameraPos;
        if (rbfpscontroller.Velocity.magnitude > 0 && rbfpscontroller.Grounded)
        {
            camera.transform.localPosition = headMotion.DoHead(rbfpscontroller.Velocity.magnitude * (rbfpscontroller.Running ? RunStrideLength : 1f));
            newCameraPos = camera.transform.localPosition;
            newCameraPos.y = camera.transform.localPosition.y;
        }
        else
        {
            newCameraPos = camera.transform.localPosition;
            newCameraPos.y = camera.transform.localPosition.y;
        }
        camera.transform.localPosition = newCameraPos;
        if (previousGrounded && rbfpscontroller.Grounded)
        {
            StartCoroutine(lerpcontroller.Cycle());
        }

        previousGrounded = rbfpscontroller.Grounded;
	}
}
