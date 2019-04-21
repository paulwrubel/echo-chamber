using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGrabber : MonoBehaviour {

    public GameObject laser;
    public Transform anchor;
    public OVRInput.Button grabButton;
    public float range = 8;
    public LayerMask grabLayer = 0;

    private GrabLaser grabLaser;

    // Start is called before the first frame update
    void Start() {
        grabLaser = laser.GetComponent<GrabLaser>();
        grabLaser.SetState(GrabLaser.LaserState.Disabled);
        laser.transform.SetParent(anchor, false);
    }

    // Update is called once per frame
    void Update() {
        
        if (OVRInput.Get(grabButton)){
            Vector3 start = grabLaser.transform.position;
            Vector3 forward = grabLaser.transform.forward;

            Vector3 end = start + (forward * range);

            RaycastHit hit;
            if (Physics.Raycast(start, forward, out hit, range, grabLayer) && (hit.transform.gameObject.GetComponent<OVRGrabbable>() != null)) {
                grabLaser.SetState(GrabLaser.LaserState.Targeted);
            } else {
                grabLaser.SetState(GrabLaser.LaserState.Untargeted);
            }

            LineRenderer lineRenderer = grabLaser.GetActiveLaser().GetComponent<LineRenderer>();

            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

        } else {
            grabLaser.SetState(GrabLaser.LaserState.Disabled);
        }

    }
}
