using UnityEngine;
using System.Collections;

public class CapturableObject : MonoBehaviour {

    public CaptureType StartCaptureType;
    private CaptureType actualCaptureType;
    private float score = 1f;

    /// <summary>
    /// Adding the score of this object to the total score.
    /// </summary>
	public void SetCaptureType(CaptureType )
    {
        GameManager.instance.AddScore(score);
    }
}
