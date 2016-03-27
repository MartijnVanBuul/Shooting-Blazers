using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
    public AmountOfCircles AmountOfCircles;

    public Canvas DisplayCanvas;
    public Canvas ResourcesCanvas;
    public Light DirectionalLight;
    public Clock Clock;

    public float SwitchSensivitie = 1.5f;

	// Use this for initialization
	void Start () {
        if (AmountOfCircles == null)
        {
            Debug.LogWarning("There is no reference to the AmountOfCircles script");
        }

	    if (DisplayCanvas == null)
	    {
	        Debug.LogWarning("The is not a reference to the displaycanvas");
	    }

	    if (ResourcesCanvas == null)
	    {
	        Debug.LogWarning("The is not a reference to the resourcescanvas");
	    }
	}

    public void PlacedCircle()
    {
        AmountOfCircles.NextCaptureType();
    }

    public void SetCircles(List<CaptureType> captureTypes )
    {
        AmountOfCircles.SetCircles(captureTypes);
        Debug.Log(captureTypes.Count + " have been added to the GUIManager");
    }

    private bool _isSwitchingOff = false;
    private bool _isSwitchingOn = false;
    private float _lighting = 1f;
    public void SwitchDay()
    {
        _isSwitchingOff = true;
    }

    void FixedUpdate()
    {
        if (_isSwitchingOff)
        {
            _lighting -= Time.fixedDeltaTime/SwitchSensivitie;
            DirectionalLight.intensity = _lighting;
            Clock.hour += 1;

            if (_lighting <= 0)
            {
                _isSwitchingOff = false;
                _isSwitchingOn = true;
            }
        }

        else if (_isSwitchingOn)
        {
            _lighting += Time.fixedDeltaTime/SwitchSensivitie;
            DirectionalLight.intensity = _lighting;
            Clock.hour += 1;

            if (_lighting >= 1)
            {
                _isSwitchingOn = false;
            }
        }
    }

}
