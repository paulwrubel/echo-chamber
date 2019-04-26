using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour
{

    public float leftPosition = 6.5f;
    public Material leftMaterial;
    public float rightPosition = -6.5f;
    public Material rightMaterial;
    public GameObject centerWall;
    public bool isLeft = true;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (isLeft) {
            transform.localPosition = Vector3.zero;
            transform.Translate(leftPosition, 0, 0);
            print(transform.position.x);
            centerWall.GetComponent<MeshRenderer>().material = leftMaterial;
        } else {
            transform.localPosition = Vector3.zero;
            transform.Translate(rightPosition, 0, 0);
            print(transform.position.x);
            centerWall.GetComponent<MeshRenderer>().material = rightMaterial;
        }
    }
}
