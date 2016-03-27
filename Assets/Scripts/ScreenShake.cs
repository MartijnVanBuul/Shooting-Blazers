using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	public float Intensity = 0.1f;
	public float MaximumDeviation = 1.0f;
	public bool debug = false;

	float shakeTimeLeft = 0.0f;
	Vector3 shakeCummulative = new Vector3();

	// Use this for initialization
	void Start () 
	{
		DoScreenShakeReset(); 
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
			shakeTimeLeft = 0.0f;

			if (shakeCummulative.x > Intensity)
				cameraAdd.x = -Intensity;
			else if (shakeCummulative.x < -Intensity)
				cameraAdd.x = Intensity;
			else
				cameraAdd.x = -shakeCummulative.x;

			if (shakeCummulative.z > Intensity)
				cameraAdd.z = -Intensity;
			else if (shakeCummulative.z < -Intensity)
				cameraAdd.z = Intensity;
			else
				cameraAdd.z = -shakeCummulative.z;
		}

		// Process position change
		shakeCummulative += cameraAdd;
		Camera.main.transform.position += cameraAdd;

		if (debug)
			Debug.Log("Time: " + shakeTimeLeft + " Shake: (" + cameraAdd.x + ", " + cameraAdd.z + "):  (" + shakeCummulative.x + ", " + shakeCummulative.z + ")");
	}


	public void DoScreenShake(float duration)
	{
		shakeTimeLeft = duration;

		if (debug) 
			Debug.Log("Shake for " + duration + " seconds.");
	}

	public void DoScreenShakeReset()
	{
		shakeTimeLeft = 0;
		Camera.main.transform.position -= shakeCummulative;
		shakeCummulative = Vector3.zero;

		if (debug)
			Debug.Log("Shake reset");
	}
}
