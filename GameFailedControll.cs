using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFailedControll : MonoBehaviour
{
    public Image _timeGage;
    public Image _fade;
    public Text _timer;
    public AudioSource _narration;

    private SoundManager m_sound;

    float time = 0;
    float restart = 14f;

    private void Start()
    {
        StartCoroutine(FadeIn());
        m_sound = GetComponent<SoundManager>();
        m_sound.Play(3);
        _narration.Play();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time < 1)
        {
            StartCoroutine(FadeIn());
        }
        if (time > restart)
        {
            StartCoroutine(FadeOut());
        }else if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeOut());
        }

    }

    // Update is called once per frame
    public IEnumerator FadeIn()
    {
        float colorA = 1f;
        while (colorA > 0)
        {
            colorA -= 0.5f * Time.deltaTime;
            _fade.color = new Color(0, 0, 0, colorA);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float colorA = 0;
        while (colorA < 1)
        {
            colorA += 0.5f * Time.deltaTime;
            _fade.color = new Color(0, 0, 0, colorA);
            if (colorA >0.999999999999999999999999999999999f)
            {
                SceneManager.LoadScene(0);
            }
            yield return null;
        }

    }
    public SoundManager Sound()
    {
        return m_sound;
    }
}
