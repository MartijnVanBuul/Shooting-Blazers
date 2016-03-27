using UnityEngine;
using System.Collections;

public class CapturableObject : MonoBehaviour {

    public CaptureType GoalCaptureType;
    private CaptureType currentCaptureType = CaptureType.none;

    private Animator animPlayer;

    public float correctScore;
    public float neutralScore;
    public float failScore;

    private MeshRenderer meshRenderer;
    private Color originalColor;

	void Start(){
		animPlayer = GetComponentInChildren<Animator>();

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalColor = meshRenderer.material.color;
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
    public float Score()
    {
        if (GoalCaptureType == currentCaptureType)
            return correctScore;
        else if ((GoalCaptureType == CaptureType.red && (currentCaptureType == CaptureType.purple || currentCaptureType == CaptureType.orange)) ||
                (GoalCaptureType == CaptureType.blue && (currentCaptureType == CaptureType.purple || currentCaptureType == CaptureType.green)) ||
                (GoalCaptureType == CaptureType.yellow && (currentCaptureType == CaptureType.green || currentCaptureType == CaptureType.orange)))
            return neutralScore;
        else
            return failScore;
    }


	public float testAnimation()
	{
		Debug.Log("Hallo");
		animPlayer.SetTrigger("showBubble");
        AnimatorStateInfo info = animPlayer.GetCurrentAnimatorStateInfo(0);
        return info.length;
	}

    public void SetCaptureGoal(CaptureType type)
    {
        GoalCaptureType = type;

        if (meshRenderer == null)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (GoalCaptureType == CaptureType.red)
            meshRenderer.material.color = Color.red;
        else if (GoalCaptureType == CaptureType.blue)
            meshRenderer.material.color = Color.blue;
        else if (GoalCaptureType == CaptureType.yellow)
            meshRenderer.material.color = Color.yellow;
        else if (GoalCaptureType == CaptureType.purple)
            meshRenderer.material.color = Color.magenta;
        else if (GoalCaptureType == CaptureType.green)
            meshRenderer.material.color = Color.green;
        else if (GoalCaptureType == CaptureType.orange)
            meshRenderer.material.color = new Color(1, 0.5f, 0);
        else
            meshRenderer.material.color = Color.white;

        originalColor = meshRenderer.material.color;
    }

    /// <summary>
    /// Setting or removing the highlight from a tile.
    /// </summary>
    /// <param name="highlight">True if it will be highlighted, false if it will be original color.</param>
    public void SetHighlightColor(bool highlight)
    {
            if (highlight)
                meshRenderer.material.color = ChangeColorBrightness(originalColor, 3f);
            else
                meshRenderer.material.color = originalColor;
    }

    /// <summary>
    /// Creates color with corrected brightness.
    /// </summary>
    /// <param name="color">Color to correct.</param>
    /// <param name="correctionFactor">The brightness correction factor. This will be a multiplier.
    /// <returns>Corrected Color.</returns>
    public static Color ChangeColorBrightness(Color color, float correctionFactor)
    {
        float red = color.r;
        float green = color.g;
        float blue = color.b;

        red *= correctionFactor;
        green *= correctionFactor;
        blue *= correctionFactor;

        return new Color(red, green, blue, color.a);
    }
}
