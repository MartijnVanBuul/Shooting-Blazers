using UnityEngine;
using System.Collections;

public class CapturableObject : MonoBehaviour {

    public CaptureType GoalCaptureType;
    private CaptureType currentCaptureType = CaptureType.none;

    public float correctScore;
    public float neutralScore;
    public float failScore;

    void Start()
    {
        if(GoalCaptureType == CaptureType.red)
            GetComponent<MeshRenderer>().material.color = Color.red;
        else if(GoalCaptureType == CaptureType.blue)
            GetComponent<MeshRenderer>().material.color = Color.blue;
        else if (GoalCaptureType == CaptureType.yellow)
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        else if (GoalCaptureType == CaptureType.purple)
            GetComponent<MeshRenderer>().material.color = Color.magenta;
        else if (GoalCaptureType == CaptureType.green)
            GetComponent<MeshRenderer>().material.color = Color.green;
        else if (GoalCaptureType == CaptureType.orange)
            GetComponent<MeshRenderer>().material.color = new Color(1, 0.5f, 0);
        else 
            GetComponent<MeshRenderer>().material.color = Color.white;

    }

    /// <summary>
    /// Setting the CaptureType of the object.
    /// </summary>
	public void SetCaptureType(CaptureType circleCaptureType)
    {
        if(currentCaptureType == CaptureType.none)
            currentCaptureType = circleCaptureType;
        else if(currentCaptureType == CaptureType.red)
        {
            if (circleCaptureType == CaptureType.blue)
                currentCaptureType = CaptureType.purple;
            else if(circleCaptureType == CaptureType.yellow)
                currentCaptureType = CaptureType.orange;
        }
        else if (currentCaptureType == CaptureType.blue)
        {
            if (circleCaptureType == CaptureType.red)
                currentCaptureType = CaptureType.purple;
            else if (circleCaptureType == CaptureType.yellow)
                currentCaptureType = CaptureType.green;
        }
        else if (currentCaptureType == CaptureType.red)
        {
            if (circleCaptureType == CaptureType.yellow)
                currentCaptureType = CaptureType.green;
            else if (circleCaptureType == CaptureType.yellow)
                currentCaptureType = CaptureType.orange;
        }
    }

    /// <summary>
    /// Adding the score to the GameManager.
    /// </summary>
    public void Score()
    {
        if (GoalCaptureType == currentCaptureType)
            GameManager.instance.AddScore(correctScore);
        else if((GoalCaptureType == CaptureType.red && (currentCaptureType == CaptureType.purple || currentCaptureType == CaptureType.orange)) || 
                (GoalCaptureType == CaptureType.blue && (currentCaptureType == CaptureType.purple || currentCaptureType == CaptureType.green)) ||
                (GoalCaptureType == CaptureType.yellow && (currentCaptureType == CaptureType.green || currentCaptureType == CaptureType.orange)))
            GameManager.instance.AddScore(neutralScore);
        else
            GameManager.instance.AddScore(failScore);
    }
}
