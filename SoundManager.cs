using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource _effect;
    public AudioSource _ButterflyBG;
    public AudioClip[] _clip;

    public void _ButterflyBGPlay()
    {
        _ButterflyBG.Play();
    }
    public void Play(int index)
    {
        _effect.PlayOneShot(_clip[index]);
    }
}
