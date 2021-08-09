using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource scoreaudio;

    private void OnMouseUpAsButton()
    {
        scoreaudio.Play();
        ScoringSystem.theScore = ScoringSystem.theScore + 50;
        Destroy(gameObject);
    }
}
