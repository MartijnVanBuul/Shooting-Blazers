using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

	public int numberOfRed		= 1;
	public int numberOfGreen	= 1;
	public int numberOfBlue		= 1;

	void spriteAdd(string filename, float x, float y, float z)
	{
		GameObject go = new GameObject();
		go.transform.position.Set(x, y, z);

		SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
		renderer.sprite = Resources.Load("Sprites/" + filename, typeof(Sprite)) as Sprite;
		
		//Debug.Log("Added sprite: " + filename);
	}

	// Use this for initialization
	void Start()
	{
		spriteAdd("Circle", -5, 0, 0);
		spriteAdd("Circle", 0, 5, 0);
		spriteAdd("Circle", 5, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
