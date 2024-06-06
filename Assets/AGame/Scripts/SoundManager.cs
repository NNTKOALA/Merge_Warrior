using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource buyCharacterAudioSource;
    public AudioSource fightAudioSource;

    public void PlayBuyCharacterSound()
    {
        buyCharacterAudioSource.Play();
    }

    public void PlayFightSound()
    {
        fightAudioSource.Play();
    }
}
