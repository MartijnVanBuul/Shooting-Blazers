using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CapturableObjectManager : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;

    public Material RegularMaterial;
    public Material CaptureMaterial;

    private Vector3 circlePosition;
    private bool isCircleStarted;
    private float circleRadius = 1;
    private float circleIncrease = 0.05f;
    private List<GameObject> capturableObjects = new List<GameObject>();

	// Use this for initialization
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            isCircleStarted = true;
            circlePosition = StartCircle();
            circleRadius = 1;
            capturableObjects.Clear();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCircleStarted = false;
            capturableObjects = StopCircle();
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
            Collider[] captureColliders = Physics.OverlapSphere(circlePosition, circleRadius);
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
        if (circlePosition != Vector3.zero)
        {
            Collider[] captureColliders = Physics.OverlapSphere(circlePosition, circleRadius);
            foreach (Collider captureCollider in captureColliders)
                if(captureCollider.tag == "CaptureObject")
                    captureCollider.GetComponent<MeshRenderer>().material = CaptureMaterial;
        }

        circleRadius += circleIncrease;
    }
}
