using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderKingBite : MonoBehaviour
{
    public AudioSource _bite;

    public void Bite()
    {
        _bite.Play();
    }
}
