using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private float score;
    public float timer = 20;

    public Text scoreDisplay;
    public Text timerDisplay;

	void Awake () {
        instance = this;
	}


    void Update () {
        timer -= Time.deltaTime;

        timerDisplay.text = timer.ToString();
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
}
