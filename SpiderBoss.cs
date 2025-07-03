using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : MonoBehaviour
{
    public Animator _spiderBoss;
    public AudioSource jump;
    private void Start()
    {
        _spiderBoss.speed = 0;
    }

    private void SpiderJump()
    {
        _spiderBoss.speed = 1;
        jump.Play();
    }
}
