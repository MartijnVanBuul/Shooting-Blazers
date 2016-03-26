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

        //TESTING
        List<CaptureType> captureTypes = new List<CaptureType>();
        captureTypes.Add(CaptureType.blue);
        captureTypes.Add(CaptureType.red);
        captureTypes.Add(CaptureType.yellow);
        captureTypes.Add(CaptureType.blue);
        captureTypes.Add(CaptureType.red);
        captureTypes.Add(CaptureType.yellow);
        captureTypes.Add(CaptureType.blue);
        captureTypes.Add(CaptureType.red);
        captureTypes.Add(CaptureType.yellow);
        captureTypes.Add(CaptureType.blue);
        captureTypes.Add(CaptureType.red);
        captureTypes.Add(CaptureType.blue);
        captureTypes.Add(CaptureType.red);
        captureTypes.Add(CaptureType.yellow);
        captureTypes.Add(CaptureType.yellow);


        SetCircles(captureTypes);
	    StartCoroutine(test());
	    /////////////
	}

    IEnumerator test()
    {
        yield return new WaitForSeconds(2);
        PlacedCircle();
    }

    public void PlacedCircle()
    {
        AmountOfCircles.NextCaptureType();
    }

    public void SetCircles(List<CaptureType> captureTypes )
    {
        AmountOfCircles.SetCircles(captureTypes);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
