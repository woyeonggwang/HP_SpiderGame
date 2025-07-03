using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDecrease : MonoBehaviour
{
    public void Attack()
    {
        if (ScoreGauge.instance.barPosition.position.x > 700)
        {
            ScoreGauge.instance.barPosition.transform.Translate(new Vector3(-2.6f, 0, 0));
            ScriptManager.instance.butterflyHitParticleManager.Play();
            ScriptManager.instance.Sound().Play(1);
        }
    }
}