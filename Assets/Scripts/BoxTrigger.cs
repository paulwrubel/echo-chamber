using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{

    public GameObject[] objects;
    public Material activatedMaterial;
    public Material deactivatedMaterial;
    
    public bool activated = false;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.tag.Equals("Ball")) {
            activated = true;
        }
    }

    void OnTriggerExit(Collider otherCollider) {
        if (otherCollider.tag.Equals("Ball")) {
            activated = false;
        }
    }

    public void Update() {
        if (activated) {
            foreach(GameObject o in objects) {
                o.GetComponent<MeshRenderer>().material = activatedMaterial;
            }
        } else {
            foreach(GameObject o in objects) {
                o.GetComponent<MeshRenderer>().material = deactivatedMaterial;
            }
        }
    }
}
