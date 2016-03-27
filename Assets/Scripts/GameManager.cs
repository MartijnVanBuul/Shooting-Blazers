using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private float score;
    private float timer;
    private float roundTime = 20;
    
    private List<CapturableObject> capturableObjects = new List<CapturableObject>();
    private List<CaptureType> captureTypes = new List<CaptureType>();

    public Text scoreDisplay;
    public Text timerDisplay;
    public Slider happyMeter;
    public GameObject DisplayCanvas;
    public GUIManager GuiManager;
    public int amountArea = 3;

    public Sprite[] sprites;

	void Awake () {
        instance = this;
	}

    void Start()
    {
        GenerateCaptureAreas();
        GenerateCaptureObjects();

        timer = roundTime;
    }

    void Update () {
        timer -= Time.deltaTime;

        timerDisplay.text = timer.ToString();
	}

    public void PlacedCircle()
    {
        GuiManager.PlacedCircle();
    }

    /// <summary>
    /// Adding the score to the total score.
    /// </summary>
    /// <param name="captureScore">The score of the captureObject.</param>
    public void AddScore(float captureScore)
    {
        score += captureScore;

        scoreDisplay.text = score.ToString();
    }

    /// <summary>
    /// Method for generating capture areas.
    /// </summary>
    public void GenerateCaptureAreas()
    {
        for (int i = 0; i < amountArea; i++)
            captureTypes.Add((CaptureType)Random.Range(1, 3));

        GetComponent<CapturableObjectManager>().SetCaptureTypes(captureTypes);
        GuiManager.SetCircles(captureTypes);
    }

    /// <summary>
    /// Method for generating capture objects.
    /// </summary>
	public void GenerateCaptureObjects()
	{
        GetComponent<LevelGenerator>().GenerateLevel();

    }


    /// <summary>
    /// When all circles are used up NextRound() is called.
    /// The score will be calculated and new capturable objects will respawn for the next round.
    /// Also new CaptureAreas will be defined.
    /// If game over? return te main menu.
    /// </summary>
    public void NextRound()
    {
		//Adding score for each capturableobject in the game
		foreach (CapturableObject capturableObject in capturableObjects)
		{
			float score = capturableObject.Score();
            capturableObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            
            if (score > 0)
                capturableObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            else if (score == 0)
                capturableObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            else
                capturableObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];

            happyMeter.value += score;
            Destroy(capturableObject.gameObject, capturableObject.testAnimation());
		}

		//Clearing the all lists
		capturableObjects.Clear ();
        captureTypes.Clear();

		//Generating new values for the next round:
		GenerateCaptureAreas ();
		Invoke("GenerateCaptureObjects", 2);

        foreach (Transform child in DisplayCanvas.transform)
            if (child.name.Contains("Circle"))
                Destroy(child.gameObject);

        //Switch the day
        GuiManager.SwitchDay();

        timer = roundTime;
    }

    public void AddObject(CapturableObject captureObject)
    {
        capturableObjects.Add(captureObject);
    }
}
