using UnityEngine;
using System.Collections;
using System.Linq;

public class LevelGenerator : MonoBehaviour 
{
	public GameObject[] GameObj;
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
            int objectNumber = Random.Range(0, 100);
            if (objectNumber < 40)
            {
                GameObject go = (GameObject)Instantiate(GameObj[0], new Vector3(x, y, z), Quaternion.identity);
                go.GetComponent<CapturableObject>().SetCaptureGoal(CaptureType.red);
                GameManager.instance.AddObject(go.GetComponent<CapturableObject>());
            }
            else if (objectNumber < 80)
            {
                GameObject go = (GameObject)Instantiate(GameObj[1], new Vector3(x, y, z), Quaternion.identity);
                go.GetComponent<CapturableObject>().SetCaptureGoal(CaptureType.blue);
                GameManager.instance.AddObject(go.GetComponent<CapturableObject>());
            }
            else
            {
                GameObject go = (GameObject)Instantiate(GameObj[2], new Vector3(x, y, z), Quaternion.identity);
                go.GetComponent<CapturableObject>().SetCaptureGoal(CaptureType.purple);
                GameManager.instance.AddObject(go.GetComponent<CapturableObject>());
            }
        }
	}
}




