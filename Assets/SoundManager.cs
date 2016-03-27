using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Sounds
{
    Placement, NewRound, Happy
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip Placement;
    public List<AudioClip> NewRoundSound = new List<AudioClip>(); 
    public AudioClip Happy;
    public AudioSource Primary;
    public AudioSource Secondary;

    void Awake()
    {
        Instance = this;
    }

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
                case Sounds.Placement:
                Primary.Stop();
                Primary.clip = Placement;
                Primary.Play();
                break;
                case Sounds.NewRound:
                Secondary.Stop();
                Secondary.clip = NewRoundSound[Random.Range(0, NewRoundSound.Count)];
                Secondary.Play();
                break;
                case Sounds.Happy:
                Secondary.Stop();
                Secondary.clip = Happy;
                Secondary.Play();
                break;
                default:
                break; 
        }
        
    }
}
