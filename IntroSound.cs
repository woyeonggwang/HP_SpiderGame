using UnityEngine;

public class IntroSound : MonoBehaviour
{
    [HideInInspector]public SoundManager m_sound;
    public AudioSource _introBgm;
    private int m_increase;
    private void Start()
    {
        m_sound = GetComponent<SoundManager>();
        _introBgm.Play();
        m_increase = 1;
    }

    private void Update()
    {
        _introBgm.volume += m_increase * AudioListener.volume *0.1f* Time.deltaTime;
        if (AudioListener.volume <= 0.01f)
        {
            m_increase = -1;
        }
        if (AudioListener.volume >= 1.0f)
        {
            m_increase = 1;
        }
    }

    private void OnApplicationQuit()
    {
        _introBgm.volume -= m_increase * AudioListener.volume * 0.1f * Time.deltaTime;
        if (AudioListener.volume <= 0.01f)
        {
            m_increase = -1;
        }
        if (AudioListener.volume >= 1.0f)
        {
            m_increase = 1;
        }
    }
}
