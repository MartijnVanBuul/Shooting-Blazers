﻿using UnityEngine;
using System.Collections;
using System.Linq;
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

    public Material blueReligionNormal;
    public Material blueReligionOutlined;
    public Material redReligionNormal;
    public Material redReligionOutlined;

	public float circleRadiusMin = 0.8f;
	public float circleRadiusMax = 3.0f;
	public float circleTimeDuration = 1.5f;
    public float circleAlpha = 0.5f;

    public Light DirectionalLight;

	private GameObject currentCircle;
	private Material currentCircleMaterial;
	private Ray ray;
	private RaycastHit hit;
	private List<CaptureType> circleCaptureTypes = new List<CaptureType>();
	private Vector3 circlePosition;
	private bool isCircleStarted;
	private int circleCount;

	private float circleTimeNow = 0.0f;
	private float circleRadius = 0.0f;


	// Use this for initialization
	void Update()
	{
		circleTimeNow = (circleTimeNow + Time.deltaTime) % circleTimeDuration;

		if (Input.GetMouseButtonDown(0) && circleCount < circleCaptureTypes.Count && DirectionalLight.intensity >= 1)
		{
            GetComponentInChildren<ScreenShake>().DoScreenShake(0.3f);
			isCircleStarted = true;
			circlePosition = StartCircle();

			currentCircle = (GameObject)Instantiate(CircleImage, circlePosition, Quaternion.identity);
			currentCircle.transform.SetParent(DisplayCanvas.transform, true);
			currentCircleMaterial = new Material(currentCircle.GetComponent<Image>().material);


			if (circleCaptureTypes[circleCount] == CaptureType.red)
				currentCircleMaterial.color = new Color(1, 0, 0, circleAlpha);
			else if (circleCaptureTypes[circleCount] == CaptureType.blue)
				currentCircleMaterial.color = new Color(0, 0, 1, circleAlpha);
			else if (circleCaptureTypes[circleCount] == CaptureType.yellow)
				currentCircleMaterial.color = new Color(1, 1, 0, circleAlpha);
			else if (circleCaptureTypes[circleCount] == CaptureType.purple)
				currentCircleMaterial.color = new Color(1, 0, 1, circleAlpha);
			else if (circleCaptureTypes[circleCount] == CaptureType.green)
				currentCircleMaterial.color = new Color(0, 1, 0, circleAlpha);
			else if (circleCaptureTypes[circleCount] == CaptureType.orange)
				currentCircleMaterial.color = new Color(1, 0.5f, 0, circleAlpha);
			else
				currentCircleMaterial.color = new Color(1, 1, 1, circleAlpha);

			currentCircle.GetComponent<Image>().material = currentCircleMaterial;
			currentCircle.GetComponent<RectTransform>().position = Input.mousePosition;

            SoundManager.Instance.PlaySound(Sounds.Placement);
        }
		else if (!Input.GetMouseButton(0) && isCircleStarted && circleCount < circleCaptureTypes.Count)
		{
            StopCircle();
            SoundManager.Instance.PlaySound(Sounds.Placement);
		}
	}

    void LateUpdate()
    {
        if (isCircleStarted)
            DrawCircle();
    }

	/// <summary>
	/// Method for starting the circle;
	/// </summary>
	/// <returns>The start position of the circle.</returns>
	private Vector3 StartCircle()
	{
		circleTimeNow = circleTimeDuration * 3.5f / 4.0f;

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
			Collider[] captureColliders = Physics.OverlapSphere(new Vector3(circlePosition.x, 0, circlePosition.z), circleRadius / 2);
			foreach (Collider captureCollider in captureColliders)
				if (captureCollider.tag == "CaptureObject")
					captureCollider.GetComponent<CapturableObject>().SetCaptureType(circleCaptureTypes[circleCount]);
		}

		circleCount++;
		isCircleStarted = false;

        GameManager.instance.PlacedCircle();

        if (circleCount == circleCaptureTypes.Count)
        {
            GameManager.instance.NextRound();
            circleCount = 0;
        }
        }

    /// <summary>
    /// Method for drawing the circle.
    /// </summary>
    private void DrawCircle()
	{
		circleRadius = circleRadiusMin + (circleRadiusMax - circleRadiusMin) * 0.5f * (1 + Mathf.Sin(2 * Mathf.PI * circleTimeNow / circleTimeDuration));

		currentCircle.GetComponent<RectTransform>().localScale = new Vector3(circleRadius / 1.4f, circleRadius / 1.4f, 0);
        
        foreach(Collider collider in Physics.OverlapSphere(new Vector3(circlePosition.x, 0, circlePosition.z), circleRadius / 2).Where(c => c.tag == "CaptureObject"))
        {
            collider.GetComponentInChildren<CapturableObject>().SetHighlight(true);
        }
	}


	/// <summary>
	/// Setting the CaptureType of the circle.
	/// </summary>
	public void SetCaptureTypes(List<CaptureType> circleCaptureType)
	{
		this.circleCaptureTypes = circleCaptureType;
	}
}
