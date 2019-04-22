using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCopy : MonoBehaviour
{
    public GameObject subject;
    public Vector3 translation;
    public int copyCount;
    // Start is called before the first frame update
    void Start()
    {
        GameObject root = subject;
        for (int i = 0; i < copyCount; i++) {
            GameObject copy = GameObject.Instantiate(root);
            copy.name = subject.name;
            Transform firstChild = copy.transform.GetChild(0);
            if (firstChild.gameObject.name.Equals(subject.name)) {
                GameObject.Destroy(firstChild.gameObject);
            }
            copy.transform.SetParent(root.transform);
            copy.transform.SetAsFirstSibling();
            copy.transform.localPosition = Vector3.zero;
            copy.transform.Translate(translation.x, translation.y, translation.z);
            root = copy;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
