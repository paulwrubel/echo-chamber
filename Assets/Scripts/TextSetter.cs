using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter : MonoBehaviour
{

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string s) {
        text.text = s;
    }

    public void SetTextFloat(float fs) {
        string s = fs.ToString();
        SetText(s);
    }

    public void SetTextFloatDecimal(float fs) {
        string s = (fs/10).ToString();
        SetText(s);
    }
}
