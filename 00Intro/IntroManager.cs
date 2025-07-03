using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public static IntroManager instance;
    private IntroSpiderKing m_introSpiderKing;
    public Image _fade;
    public Animator[] _anim;
    public GameObject title;
    private bool m_canAnimPlay;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_introSpiderKing = GetComponent<IntroSpiderKing>();
        _fade.color = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(Fade("out"));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_canAnimPlay)
            {
                m_canAnimPlay = true;
                _anim[0].SetTrigger("startCamera");
                _anim[1].SetTrigger("startMove");
                _anim[2].SetTrigger("startFly");
                _anim[3].SetTrigger("flyTrigger");
                _anim[4].SetTrigger("gameStart");
                _anim[5].SetTrigger("gameStart");
                title.SetActive(false);
            }
        }
    }

    public IEnumerator Fade(string inout)
    {
        float colorA = 0;
        float speed = 1f;
        if (inout == "in")
        {
            colorA = 0;
            while (colorA < 1f)
            {
                colorA += speed * Time.deltaTime;
                _fade.color = new Color(0f, 0f, 0f, colorA);
                yield return null;
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else if (inout == "out")
        {
            yield return new WaitForSeconds(1.0f);
            colorA = 1;
            while (colorA > 0f)
            {
                colorA -= speed * Time.deltaTime;
                _fade.color = new Color(0f, 0f, 0f, colorA);
                yield return null;
            }
        }
    }

    public void SpiderKingShow()
    {
        m_introSpiderKing.Show();
    }
}
