using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{    
    private void Fade()
    {
        StartCoroutine(IntroManager.instance.Fade("in"));
    }

    private void SpiderKingShow()
    {
        IntroManager.instance.SpiderKingShow();
    }
}
