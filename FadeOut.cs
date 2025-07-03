using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    /*[HideInInspector] public ScriptManager _fadeOut;

    public void AnimationEnd()
    {

        StartCoroutine(_fadeOut.FadeOut());
        
    }*/
    float time = 0;
    float restart = 10f;
    float touchDelay = 8f;
    public Image _fade;
    private void Awake()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= restart)
        {
            SceneManager.LoadScene("Intro");
            //StartCoroutine(Fadeout());
        }
        if (time >= touchDelay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                SceneManager.LoadScene("Intro");
                //StartCoroutine(Fadeout());
            }

        }
    }

    public IEnumerator Fadeout()
    {
        float colorA = 0;
        while (colorA < 1)
        {
            colorA += 0.5f * Time.deltaTime;
            _fade.color = new Color(0, 0, 0, colorA);
            yield return null;
        }
    }
}
