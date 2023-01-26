using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsScript : MonoBehaviour
{

    public void SetFullScreen(bool toggle) {
        Debug.Log("Full");
        Screen.fullScreen = toggle;
    }

}
