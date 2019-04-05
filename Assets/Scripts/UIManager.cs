using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Transform ParentTransform;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(ParentTransform, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three)) {
            GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
            GetComponent<CanvasScaler>().enabled = !GetComponent<CanvasScaler>().enabled;
        }
    }
}
