using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum CaptureType
{
    none,
    red,
    blue,
    yellow,
    orange,
    purple,
    green,
    white
}

public class CapturableObjectManager : MonoBehaviour
{

    public GameObject CircleImage;
    public GameObject DisplayCanvas;

    private Ray ray;
    private RaycastHit hit;

    public Vector3 circlePosition;
    public bool isCircleStarted;
	public float circleRadiusMin = 0.1f;
	public float circleRadiusMax = 0.9f;
	public float circleTimeDuration = 2.0f;
    public float alpha = 0.5f;

	private float circleTimeNow = 0.0f;
	private float circleRadius = 0.0f;
	
    private int circleCount;
    private List<CaptureType> circleCaptureTypes = new List<CaptureType>();

    private GameObject currentCircle;
    private Material currentCircleMaterial;

    // Use this for initialization
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && circleCount < circleCaptureTypes.Count)
		{
			circleTimeNow = (circleTimeNow + Time.deltaTime) % circleTimeDuration;
			circleRadius = (circleRadiusMin + circleRadiusMax) * 0.5f * (1 + Mathf.Sin(2 * Mathf.PI * circleTimeNow));

            isCircleStarted = true;
            circlePosition = StartCircle();
<<<<<<< HEAD
            circleRadius = startRadius;
            capturableObjects.Clear();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCircleStarted = false;
            capturableObjects = StopCircle();
            foreach (GameObject capture in capturableObjects)
            {
                capture.GetComponent<CapturableObject>().SetCaptureType(circleCaptureType);
                capture.GetComponent<CapturableObject>().Score();
                capture.GetComponent<CapturableObject>().testAnimation();
            }
=======

            currentCircle = (GameObject)Instantiate(CircleImage, circlePosition, Quaternion.identity);
            currentCircle.transform.SetParent(DisplayCanvas.transform, true);
            currentCircleMaterial = new Material(currentCircle.GetComponent<Image>().material);


            if (circleCaptureTypes[circleCount] == CaptureType.red)
                currentCircleMaterial.color = new Color(1, 0, 0, alpha);
            else if (circleCaptureTypes[circleCount] == CaptureType.blue)
                currentCircleMaterial.color = new Color(0, 0, 1, alpha);
            else if (circleCaptureTypes[circleCount] == CaptureType.yellow)
                currentCircleMaterial.color = new Color(1, 1, 0, alpha);
            else if (circleCaptureTypes[circleCount] == CaptureType.purple)
                currentCircleMaterial.color = new Color(1, 0, 1, alpha);
            else if (circleCaptureTypes[circleCount] == CaptureType.green)
                currentCircleMaterial.color = new Color(0, 1, 0, alpha);
            else if (circleCaptureTypes[circleCount] == CaptureType.orange)
                currentCircleMaterial.color = new Color(1, 0.5f, 0, alpha);
            else
                currentCircleMaterial.color = new Color(1, 1, 1, alpha);

            currentCircle.GetComponent<Image>().material = currentCircleMaterial;
            currentCircle.GetComponent<RectTransform>().position = Input.mousePosition;
>>>>>>> origin/master
        }
        else if (Input.GetMouseButtonUp(0) && circleCount < circleCaptureTypes.Count)
            StopCircle();

        if (isCircleStarted)
            DrawCircle();
    }

    /// <summary>
    /// Method for starting the circle;
    /// </summary>
    /// <returns>The start position of the circle.</returns>
    private Vector3 StartCircle()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {

            return hit.point;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// Method that returns all the objects that are caught in the circle.
    /// </summary>
    /// <returns>The list of capturable gameobjects.</returns>
    private void StopCircle()
    {
        if (circlePosition != Vector3.zero)
        {
            Collider[] captureColliders = Physics.OverlapSphere(new Vector3(circlePosition.x, 0, circlePosition.z), circleRadius);
            foreach (Collider captureCollider in captureColliders)
                if (captureCollider.tag == "CaptureObject")
                    captureCollider.GetComponent<CapturableObject>().SetCaptureType(circleCaptureTypes[circleCount]);
        }

        circleCount++;
        isCircleStarted = false;

        if (circleCount == circleCaptureTypes.Count)
            GameManager.instance.NextRound();
    }

    /// <summary>
    /// Method for drawing the circle.
    /// </summary>
    private void DrawCircle()
    {
        currentCircle.GetComponent<RectTransform>().localScale = new Vector3(circleRadius / 2, circleRadius / 2, 0);
    }


    /// <summary>
    /// Setting the CaptureType of the circle.
    /// </summary>
    public void SetCaptureTypes(List<CaptureType> circleCaptureType)
    {
        this.circleCaptureTypes = circleCaptureType;
    }

    
}
