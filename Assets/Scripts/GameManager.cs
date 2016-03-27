using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int roundNumber = 1;

    private float score;
    private float timer;
    private float roundTime = 20;
    
    private List<CapturableObject> capturableObjects = new List<CapturableObject>();
    private List<CaptureType> captureTypes = new List<CaptureType>();

    public Text scoreDisplay;
    public Slider happyMeter;
    public GameObject DisplayCanvas;
    public GUIManager GuiManager;
    public int amountArea = 3;

    public int minCircles = 3;
    public int maxCircles = 4;

    public Sprite[] sprites;
    public Clock Clock;

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
        if (roundNumber == 1)
        {
            int amountCircles = Random.Range(minCircles, maxCircles + 1);
            for (int i = 0; i < amountCircles; i++)
                captureTypes.Add(CaptureType.red);
        }
        else
        {
            int amountCircles = Random.Range(minCircles, maxCircles + 1);
            for (int i = 0; i < amountCircles - 1; i++)
                captureTypes.Add((CaptureType)Random.Range(1, 3));

            if (!captureTypes.Contains(CaptureType.red))
                captureTypes.Add(CaptureType.red);
            else if (!captureTypes.Contains(CaptureType.blue))
                captureTypes.Add(CaptureType.blue);
            else
                captureTypes.Add((CaptureType)Random.Range(1, 3));
        }
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
        Invoke("GenerateCaptureAreas", 1.5f);
        Invoke("GenerateCaptureObjects", 1.5f);

        foreach (Transform child in DisplayCanvas.transform)
            if (child.name.Contains("Circle"))
                Destroy(child.gameObject);

        //Switch the day
        GuiManager.SwitchDay();
        SoundManager.Instance.PlaySound(Sounds.NewRound);

        timer = roundTime;

        roundNumber++;
    }

    public void AddObject(CapturableObject captureObject)
    {
        capturableObjects.Add(captureObject);
    }
}
