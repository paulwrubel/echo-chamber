using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIK : MonoBehaviour {

    // public

    public Transform[] points;
    public float[] constraintAngles;
    public Transform target1;
    public Transform origin;
    public bool matchOrientation = false;
    public bool isConstrained = false;
    public bool checkRange = false;
    public int loopBound = 10;
    public float epsilon = 0.001f;

    // private

    private float[] lengths;
    private Vector3 target2;

    // Start is called before the first frame update
    void Start() {
        lengths = new float[points.Length-1];
        for (int i = 0; i < points.Length - 1; i++) {
            lengths[i] = (points[i].position - points[i+1].position).magnitude;
        }
    }

    // Update is called once per frame
    void Update() {
        target2 = target1.position + target1.forward * lengths[lengths.Length-1];
        Solve();
    }

    void Solve() {
        if (checkRange && !IsInRange()) {
            Vector3 unitToTarget = (target1.position - origin.position).normalized;
            for (int i = 0; i < points.Length - 1; i++) {
                points[i+1].position = points[i].position + unitToTarget * lengths[i];
            }
        } else {
            int loopCount = 0;
            while(!IsWithinEpsilon() && loopCount < loopBound) {
                Backward();
                Forward();
                points[points.Length-1].LookAt(points[points.Length-2]);
                loopCount++;
            }
            if (loopCount >= loopBound && matchOrientation == true) {
                matchOrientation = false;
                loopCount = 0;
                while(!IsWithinEpsilon() && loopCount < loopBound) {
                    Backward();
                    Forward();
                    points[points.Length-1].LookAt(points[points.Length-2]);
                    loopCount++;
                }
                matchOrientation = true;
            }
        }
    }

    void Backward() {
        points[points.Length-1].position = target1.position;
        int startingJointIndex = matchOrientation ? points.Length - 3 : points.Length - 2;
        if (matchOrientation) {
            points[points.Length-2].position = target2;
        }
        float angle;
        for (int i = startingJointIndex; i >= 0; i--) {
            Transform jointToMove = points[i];
            Transform previousJoint = points[i+1];
            Vector3 unitToNextPoint = (jointToMove.position - previousJoint.position).normalized;
            jointToMove.position = previousJoint.position + unitToNextPoint * lengths[i];
            // fix rotation
            jointToMove.LookAt(previousJoint);
            // check constraints
            if (isConstrained) {
                if (i != points.Length - 2) {
                    angle = Quaternion.Angle(jointToMove.rotation, previousJoint.rotation);
                    if (angle > constraintAngles[i+1]) {
                        jointToMove.rotation = Quaternion.Lerp(jointToMove.rotation, previousJoint.rotation, 1 - (constraintAngles[i+1] / angle));
                    }
                    jointToMove.position = previousJoint.position - jointToMove.forward * lengths[i];
                }
            }
        }
        // if (isConstrained) {
        //     Quaternion upRotation = Quaternion.LookRotation(Vector3.up, -Vector3.forward);
        //     angle = Quaternion.Angle(upRotation, points[0].rotation);
        //     if (angle > constraintAngles[0]) {
        //         points[0].rotation = Quaternion.Lerp(upRotation, points[0].rotation, 1 - (constraintAngles[0] / angle));
        //     }
        //     points[0].position = points[1].position - points[0].forward * lengths[0];
        // }
        
    }

    void Forward() {
        points[0].position = origin.position;
        float angle;
        for (int i = 1; i < points.Length; i++) {
            Transform jointToMove = points[i];
            Transform previousJoint = points[i-1];
            Vector3 unitToNextPoint = (jointToMove.position - previousJoint.position).normalized;
            jointToMove.position = previousJoint.position + unitToNextPoint * lengths[i-1];
            // fix rotation
            previousJoint.LookAt(jointToMove);
            // check constraints
            if (isConstrained) {
                if (i != 1) {
                    Transform twoJointsPrevious = points[i-2];
                    angle = Quaternion.Angle(twoJointsPrevious.rotation, previousJoint.rotation);
                    if (angle > constraintAngles[i-1]) {
                        previousJoint.rotation = Quaternion.Lerp(twoJointsPrevious.rotation, previousJoint.rotation, constraintAngles[i-1] / angle);
                    }
                    jointToMove.position = previousJoint.position + previousJoint.forward * lengths[i-1];
                } else {
                    Transform originJoint = points[0];
                    Transform nextJoint = points[1];
                    Quaternion upRotation = Quaternion.LookRotation(Vector3.up, originJoint.up);
                    angle = Quaternion.Angle(upRotation, originJoint.rotation);
                    if (angle > constraintAngles[0]) {
                        originJoint.rotation = Quaternion.Lerp(originJoint.rotation, upRotation, 1 - (constraintAngles[0] / angle));
                    }
                    nextJoint.position = originJoint.position + originJoint.forward * lengths[0];
                }
            }
        }
    }

    void OrientJoints() {
        if (points.Length > 1) {
            for (int i = 0; i < points.Length - 1; i++) {
            }
        }
    }

    void Constrain() {
        Quaternion upRotation = Quaternion.LookRotation(Vector3.up, -Vector3.forward);
        float angle = Quaternion.Angle(upRotation, points[0].rotation);
        if (angle > constraintAngles[0]) {
            points[0].rotation = Quaternion.Lerp(upRotation, points[0].rotation, constraintAngles[0] / angle);
        }

        for (int i = 1; i < points.Length - 1; i++) {
            angle = Quaternion.Angle(points[i-1].rotation, points[i].rotation);
            if (angle > constraintAngles[i]) {
                points[i].rotation = Quaternion.Lerp(points[i-1].rotation, points[i].rotation, constraintAngles[i] / angle);
            }
            points[i].position = points[i-1].position + points[i-1].forward * lengths[i-1];
        }
        points[points.Length-1].position = points[points.Length-2].position + points[points.Length-2].forward * lengths[points.Length-2];
    }

    bool IsInRange() {
        float lengthOfArm = 0;
        foreach(float l in lengths) lengthOfArm += l;
        float distToTarget = (points[0].position - target1.position).magnitude;
        return lengthOfArm > distToTarget;
    }

    bool IsWithinEpsilon() {
        float distFromTarget1 = (points[points.Length-1].position - target1.position).magnitude;
        float distFromTarget2 = (points[points.Length-2].position - target2).magnitude;
        if (matchOrientation) {
            return distFromTarget1 < epsilon && distFromTarget2 < epsilon;
        } else {
            return distFromTarget1 < epsilon;
        }
    }
}
