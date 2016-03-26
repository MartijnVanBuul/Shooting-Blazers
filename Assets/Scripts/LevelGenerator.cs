using UnityEngine;
using System.Collections;
using System.Linq;

public class LevelGenerator : MonoBehaviour 
{
	public GameObject GameObj;
	public int Timeout = 10000;
	public float radius = 1.0f;
	public int CountMin = 8;
	public int CountMax = 12;
	public float xMin = -5.0f;
	public float xMax = 5.0f;
	public float yMin = 1.0f;
	public float yMax = 1.0f;
	public float zMin = -10.0f;
	public float zMax = 10.0f;

	public void GenerateLevel()
	{
		float x, y, z;
		int Count = Random.Range(CountMin, CountMax);
		int timeoutLeft = Timeout;

		for (int i = 0; i < Count; i++)
		{
			while(true)
			{
				if (--timeoutLeft > 0)
				{
					x = Random.Range(xMin, xMax);
					y = Random.Range(yMin, yMax);
					z = Random.Range(zMin, zMax);

					if (Physics.OverlapSphere(new Vector3(x, y, z), radius).Where(c => c.tag != "Floor").Count() == 0)
						break;
				}
				else
				{
					Debug.Log("Error: timeout!");
					return;
				}
			}
			GameObject go = (GameObject)Instantiate(GameObj, new Vector3(x, y, z), Quaternion.identity);
            go.GetComponent<CapturableObject>().SetCaptureGoal((CaptureType)Random.Range(1, 3));
            GameManager.instance.AddObject(go.GetComponent<CapturableObject>());
		}
	}
}




