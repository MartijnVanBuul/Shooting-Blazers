using UnityEngine;
using System.Collections;

public class ObjectMoodController : MonoBehaviour {

    public enum MoodState
    {
        neutral,
        happy,
        angry
    }

    public GameObject[] neutralState;
    public GameObject[] happyState;
    public GameObject[] angryState;

    Animator objectAC;

    public MoodState mood;

    void Awake()
    {
        objectAC = GetComponent<Animator>();
        SetState(MoodState.neutral);
    }

    //Set the moodState
    public void SetState(MoodState _mood)
    {
        objectAC.SetInteger("mood", (int)_mood);

        //Neutral
        if(_mood == MoodState.neutral)
        {
            SetMood(happyState, false);
            SetMood(angryState, false);
            SetMood(neutralState, true);
        }

        //Happy
        if (_mood == MoodState.happy)
        {
            SetMood(happyState, true);
            SetMood(angryState, false);
            SetMood(neutralState, false);
        }

        //Angry
        if (_mood == MoodState.angry)
        {
            SetMood(happyState, false);
            SetMood(angryState, true);
            SetMood(neutralState, false);
        }
    }

    //Set mood Method
    void SetMood(GameObject[] _moodCollection, bool _onOff)
    {
        foreach (var item in _moodCollection)
        {
            item.SetActive(_onOff);
        }
    }
}
