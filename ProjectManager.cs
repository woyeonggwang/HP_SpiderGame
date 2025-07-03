using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager insance;
    public GameObject _spiderTouch;
    private List<ParticleSystem> m_particleList = new List<ParticleSystem>();
    private List<ParticleSystem> m_fail = new List<ParticleSystem>();
    [HideInInspector] public float _timeGaugeFillAmount;
    [HideInInspector] public string _time;

    private void Awake()
    {
        if(insance == null)
        {
            insance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
#endif
        ParticleObjectpool();
    }

    private void ParticleObjectpool()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject temp = Instantiate(_spiderTouch, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
            temp.gameObject.SetActive(false);
            m_particleList.Add(temp.GetComponent<ParticleSystem>());

        }
    }

    public void SpiderPos(Vector3 pos)
    {
        for (int i = 0; i < m_particleList.Count; i++)
        {
            if (!m_particleList[i].gameObject.activeInHierarchy)
            {
                m_particleList[i].transform.position = pos;
                m_particleList[i].gameObject.SetActive(true);
                if (i > 0 && m_particleList[i-1].gameObject.activeInHierarchy)
                {
                    m_particleList[i - 1].gameObject.SetActive(false);
                }
                break;
            }
        }
    }
    public void WebPos(Vector3 pos)
    {
        for (int i = 0; i < m_fail.Count; i++)
        {
            if (!m_fail[i].gameObject.activeInHierarchy)
            {
                m_fail[i].transform.position = pos;
                m_fail[i].gameObject.SetActive(true);
                if (i > 0 && m_fail[i - 1].gameObject.activeInHierarchy)
                {
                    m_fail[i - 1].gameObject.SetActive(false);
                }
                
                break;
            }
        }
    }
}
