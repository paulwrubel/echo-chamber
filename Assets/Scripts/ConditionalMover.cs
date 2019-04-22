using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalMover : MonoBehaviour
{
    public GameObject[] activators;
    public Vector3 deltaPosition;
    public float moveTime;
    private bool hasMoved;
    private bool isMoving;
    private int counter = 0;
    private int totalMoveFrames;
    // Start is called before the first frame update

    // Update is called once per frame

    void Start() {
        totalMoveFrames = (int)(moveTime / Time.fixedDeltaTime) + 1;
        print(totalMoveFrames);
    }
    void Update()
    {
        if (!isMoving && !hasMoved && SumActivations() == activators.Length && counter < 180) {
            counter++;
        }
        if (!isMoving && !hasMoved && SumActivations() == activators.Length && counter >= 180) {
            isMoving = true;
        }
        if (SumActivations() != activators.Length) {
            counter = 0;
        }
        print(counter);
    }

    void FixedUpdate() {
        if (isMoving) {
            Vector3 deltaPositionOverTime = (deltaPosition * Time.deltaTime) / moveTime;
            transform.position += deltaPositionOverTime;
            totalMoveFrames--;
            if (totalMoveFrames == 0) {
                isMoving = false;
                hasMoved = true;
            }
        }
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
