using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirection : MonoBehaviour {

	public Transform player;
	public Transform centerEye;
	public float constantRotationDegrees = 0;
	public float cyclicRotationDegrees = 0;
	public float cycleSpeed = 0.1f;
	public float leftFactor = 1.0f;
	public float rightFactor = 1.0f;
	private int counter = 0;
	private float oldYRotation = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		counter++;
	}

	public void SetLeftRotationFactor(float lf) {
		leftFactor = lf / 10;
	}

	public void SetRightRotationFactor(float rf) {
		rightFactor = rf / 10;
	}

	public void SetSpin(float spin) {
		constantRotationDegrees = spin;
	}

	void FixedUpdate() {

		transform.RotateAround(player.position, Vector3.up, cyclicRotationDegrees * Mathf.Sin(counter*(cycleSpeed/100)) * Time.fixedDeltaTime);
		transform.RotateAround(player.position, Vector3.up, constantRotationDegrees * Time.fixedDeltaTime);
		
		//print(centerEye.localRotation.eulerAngles.y);
		float difference = centerEye.localRotation.eulerAngles.y - oldYRotation;
		if (difference > 90) {
			difference = centerEye.localRotation.eulerAngles.y - (oldYRotation+360);
		} else if (difference < -90) {
			difference = centerEye.localRotation.eulerAngles.y - (oldYRotation-360);
		}
		if (difference < 0) {
			transform.RotateAround(player.position, Vector3.up, -difference + (difference * leftFactor));	
		} else {
			transform.RotateAround(player.position, Vector3.up, -difference + (difference * rightFactor));	
		}

	}

	void LateUpdate() {
		oldYRotation = centerEye.localRotation.eulerAngles.y;
	}
}
