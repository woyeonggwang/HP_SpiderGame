using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteSoundController : MonoBehaviour
{
    public AudioSource _bite;
    public AudioSource _jump;
   
    public void Bite()
    {
        _bite.Play();
    }
    public void Jump()
    {
        _jump.Play();
    }
}
