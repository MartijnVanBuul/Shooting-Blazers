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

    private Vector3 circlePosition;
    private bool isCircleStarted;
    private float startRadius = 0.1f;
    private float circleRadius;
    private float circleIncrease = 0.05f;
    private List<GameObject> capturableObjects = new List<GameObject>();

    private CaptureType circleCaptureType = CaptureType.red;

    private GameObject currentCircle;

    // Use this for initialization
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCircleStarted = true;
            circlePosition = StartCircle();
            circleRadius = startRadius;
            capturableObjects.Clear();

            currentCircle = (GameObject)Instantiate(CircleImage, circlePosition, Quaternion.identity);
            currentCircle.transform.SetParent(DisplayCanvas.transform, true);
            currentCircle.GetComponent<Image>().material.color = new Color(1, 0, 0, 0.5f);
            currentCircle.GetComponent<RectTransform>().position = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCircleStarted = false;
            capturableObjects = StopCircle();
            foreach (GameObject capture in capturableObjects)
            {
                capture.GetComponent<CapturableObject>().SetCaptureType(circleCaptureType);
                capture.GetComponent<CapturableObject>().Score();
            }
        }

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
    private List<GameObject> StopCircle()
    {
        if (circlePosition != Vector3.zero)
        {
            Collider[] captureColliders = Physics.OverlapSphere(new Vector3(circlePosition.x, 0, circlePosition.z), circleRadius);
            foreach (Collider captureCollider in captureColliders)
                if (captureCollider.tag == "CaptureObject")
                    capturableObjects.Add(captureCollider.gameObject);
        }

        return capturableObjects;
    }

    /// <summary>
    /// Method for drawing the circle.
    /// </summary>
    private void DrawCircle()
    {
        circleRadius += circleIncrease;
        currentCircle.GetComponent<RectTransform>().localScale = new Vector3(circleRadius / 2, circleRadius / 2, 0);
    }


    /// <summary>
    /// Setting the CaptureType of the circle.
    /// </summary>
    public void SetCaptureType(CaptureType circleCaptureType)
    {
        this.circleCaptureType = circleCaptureType;
    }
}
