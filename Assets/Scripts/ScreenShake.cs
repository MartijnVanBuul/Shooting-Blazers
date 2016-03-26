using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	public float Intensity = 0.1f;
	public float MaximumDeviation = 1.0f;

	float shakeTimeLeft = 0.0f;
	Vector3 shakeCummulative = new Vector3();

	// Use this for initialization
	void Start () {
		//ScreenShake(5.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 cameraAdd = new Vector3();

		if (shakeTimeLeft > 0.0f)
		{
			shakeTimeLeft -= Time.deltaTime;
			cameraAdd.Set(Random.Range(-Intensity, Intensity), 0, Random.Range(-Intensity, Intensity));

			if (shakeCummulative.x + cameraAdd.x > MaximumDeviation) // X to high
				cameraAdd.x = MaximumDeviation - shakeCummulative.x;

			else if (shakeCummulative.x + cameraAdd.x < -MaximumDeviation) // X to low
				cameraAdd.x = -MaximumDeviation - shakeCummulative.x;

			if (shakeCummulative.z + cameraAdd.z > MaximumDeviation) // Z to high
				cameraAdd.z = MaximumDeviation - shakeCummulative.z;

			else if (shakeCummulative.z + cameraAdd.z < -MaximumDeviation) // Z to low
				cameraAdd.z = -MaximumDeviation - shakeCummulative.z;
		}
		else // Return to 0
		{
			if (shakeCummulative.x > Intensity)
				cameraAdd.x = -Intensity;
			else if (shakeCummulative.x < -Intensity)
				cameraAdd.x = Intensity;
			else
				cameraAdd.x = shakeCummulative.x;

			if (shakeCummulative.z > Intensity)
				cameraAdd.z = -Intensity;
			else if (shakeCummulative.z < -Intensity)
				cameraAdd.z = Intensity;
			else
				cameraAdd.z = shakeCummulative.z;
		}

		// Process position change
		shakeCummulative += cameraAdd;
		Camera.main.transform.position += cameraAdd;
	}

	/*
	public void ScreenShake(float duration)
	{
		Debug.Log("Shake for " + duration / 60.0f + " seconds.");
		shakeTimeLeft = duration;
	}

	public void ScreenShakeReset()
	{
		Debug.Log("Shake reset");
		shakeTimeLeft = 0;
		Camera.main.transform.position -= shakeCummulative;
		shakeCummulative = Vector3.zero;
	}*/
}
