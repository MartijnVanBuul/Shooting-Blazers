using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
    public AmountOfCircles AmountOfCircles;

	// Use this for initialization
	void Start () {
        if (AmountOfCircles == null)
        {
            Debug.LogWarning("There is no reference to the AmountOfCircles script");
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
