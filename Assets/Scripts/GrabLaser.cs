using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabLaser : MonoBehaviour {

    public enum LaserState { Disabled, Untargeted, Targeted }

    public GameObject UntargetedLaser;
    public GameObject TargetedLaser;

    public LaserState state = LaserState.Disabled;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetState (LaserState ls) {
        switch (ls) {
            case LaserState.Disabled:
                UntargetedLaser.SetActive(false);
                TargetedLaser.SetActive(false);
                break;
            case LaserState.Untargeted:
                UntargetedLaser.SetActive(true);
                TargetedLaser.SetActive(false);
                break;
            case LaserState.Targeted:
                UntargetedLaser.SetActive(false);
                TargetedLaser.SetActive(true);
                break;
            default:
                UntargetedLaser.SetActive(false);
                TargetedLaser.SetActive(false);
                break;
        }
        state = ls; 
    }

    public GameObject GetActiveLaser() {
        GameObject result;
        switch (state) {
            case LaserState.Disabled:
                result = null;
                break;
            case LaserState.Untargeted:
                result = UntargetedLaser;
                break;
            case LaserState.Targeted:
                result = TargetedLaser;
                break;
            default:
                result = null;
                break;
        }
        return result;
    }
}
