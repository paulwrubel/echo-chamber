using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirection : MonoBehaviour {

	public Transform player;
	public Transform centerEye;
	public GameObject[] activators;
	public float constantRotationDegrees = 0;
	public float leftFactor = 1.0f;
	public float rightFactor = 1.0f;
	public float deltaConstantRotationDegrees = 0;
	public float deltaLeftFactor = 0;
	public float deltaRightFactor = 0;
	private float oldYRotation = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int sum = SumActivations();
		if (sum != activators.Length) {
			constantRotationDegrees = sum * deltaConstantRotationDegrees;
			leftFactor = 1 + (sum * deltaLeftFactor);
			rightFactor = 1 + (sum * deltaRightFactor);
		}
	}

	void FixedUpdate() {

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

	public void SetLeftRotationFactor(float lf) {
		leftFactor = lf / 10;
	}

	public void SetRightRotationFactor(float rf) {
		rightFactor = rf / 10;
	}

	public void SetSpin(float spin) {
		constantRotationDegrees = spin;
	}

	private int SumActivations() {
        int count = 0;
        foreach(GameObject a in activators) {
            if (a.GetComponent<BoxTrigger>().activated) {
                count++;
            }
        }
        return count;
    }
}
