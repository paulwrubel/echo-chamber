using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Transform ParentTransform;
<<<<<<< HEAD
    public OVRInput.Button menuToggleButton = OVRInput.Button.Four;
=======
>>>>>>> 59cad8d86aeb6280c08cc696598f6ef6891bebed

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        transform.SetParent(ParentTransform, false);
=======
        transform.SetParent(ParentTransform, true);
>>>>>>> 59cad8d86aeb6280c08cc696598f6ef6891bebed
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (OVRInput.GetDown(menuToggleButton)) {
            print("bing bong 1");
=======
        if (OVRInput.GetDown(OVRInput.Button.Three)) {
>>>>>>> 59cad8d86aeb6280c08cc696598f6ef6891bebed
            GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
            GetComponent<CanvasScaler>().enabled = !GetComponent<CanvasScaler>().enabled;
        }
    }
}
