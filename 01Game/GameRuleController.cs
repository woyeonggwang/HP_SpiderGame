using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleController : MonoBehaviour
{
    public AudioSource _narration;
    public GameObject _particle;

    public void GameStart()
    {
        ScriptManager.instance.SetFade();
        _narration.Play();
        _particle.SetActive(true);
    }

    public void ParticleActive()
    {
        _particle.SetActive(false);
    }
}
