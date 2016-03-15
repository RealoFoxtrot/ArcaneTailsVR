using UnityEngine;
using System.Collections;

public class CrosshairOptions : MonoBehaviour {

    public WorldCrosshair nonAdaptiveCrosshair;
    public WorldCrosshair adaptiveCrosshair;
    public GameObject topView;

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.P))
            ToggleAdaptiveScaling();
        if (Input.GetKeyUp(KeyCode.O))
            ToggleStereoTweak();
        if (Input.GetKeyUp(KeyCode.I))
            ToggleTopView();
	}

    private void ToggleTopView()
    {
        if (topView.activeSelf == false)
            topView.SetActive(true);
        else
            topView.SetActive(false);
    }

    private void ToggleStereoTweak()
    {
        if (nonAdaptiveCrosshair.stereoscopicScaleTweak == false)
        {
            nonAdaptiveCrosshair.stereoscopicScaleTweak = true;
            adaptiveCrosshair.stereoscopicScaleTweak = true;
        }
        else
        {
            nonAdaptiveCrosshair.stereoscopicScaleTweak = false;
            adaptiveCrosshair.stereoscopicScaleTweak = false;
        }
    }

    /// <summary>
    /// Adaptive scaling can't be toggled at runtime, so here there are two different crosshairs that are toggled on or off instead
    /// </summary>
    private void ToggleAdaptiveScaling()
    {
        if (nonAdaptiveCrosshair.gameObject.activeSelf == true)
        {
            nonAdaptiveCrosshair.gameObject.SetActive(false);
            adaptiveCrosshair.gameObject.SetActive(true);
        }
        else
        {
            nonAdaptiveCrosshair.gameObject.SetActive(true);
            adaptiveCrosshair.gameObject.SetActive(false);
        }
    }
}
