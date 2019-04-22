using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        // check if in architects room
        if (transform.position.z > 9 && transform.position.y < -25) {
            if (transform.position.x < -8) {
                transform.Translate(16, 0, 0, Space.World);
            } else if (transform.position.x > 8) {
                transform.Translate(-16, 0, 0, Space.World);
            }
        }
    }
}
