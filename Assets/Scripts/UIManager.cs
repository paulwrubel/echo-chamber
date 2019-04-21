using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Transform ParentTransform;
    public OVRInput.Button menuToggleButton = OVRInput.Button.Four;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(ParentTransform, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(menuToggleButton)) {
            print("bing bong 1");
            GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
            GetComponent<CanvasScaler>().enabled = !GetComponent<CanvasScaler>().enabled;
        }
    }
}
