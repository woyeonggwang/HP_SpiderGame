using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderMove : MonoBehaviour
{
    private Animator anim;
    private Transform _butterfly;
    private float m_speed;
    private bool m_canmove;
    private bool isDeath;
    private float deathRotZ;
    
    private void Start()
    {
        if(_butterfly == null)
            _butterfly = ScriptManager.instance._butterfly;

        anim = transform.GetChild(0).GetComponent<Animator>();

        float randSize = Random.Range(0.4f, 0.85f);
        transform.localScale = new Vector3(randSize, randSize, randSize);

    }

    private void OnDisable()
    {
        ObjectReset();
        float randSize = Random.Range(0.4f, 0.85f);
        transform.localScale = new Vector3(randSize, randSize, randSize);
    }

    private void Update()
    {
        Rotation();

        if (!isDeath)
        {
            if (Vector3.Distance(transform.position, _butterfly.transform.position) < 0.9f)
            {
                // 거미
                anim.SetBool("attackStart", true);
                m_canmove = true;
            }
        }
        
        if (!m_canmove)
        {
            SpiderSpeed();
            transform.Translate(Vector3.right * m_speed * Time.deltaTime);
        }

        if (isDeath)
        {
            transform.rotation = Quaternion.Euler(90f, transform.rotation.y, deathRotZ);
            Vector3 dir = Vector3.zero;
            dir += Vector3.forward * 5f * Time.deltaTime;
            transform.Translate(dir);
        }
    }

    public void Death()
    {
        anim.SetBool("attackStart", false);
        ProjectManager.insance.SpiderPos(transform.localPosition);
        isDeath = true;
        deathRotZ = Random.Range(0, 360f);
        m_canmove = true;
        //anim.SetTrigger("killSpider");
        Invoke("Disable", 3f);
    }

    private void Disable()
    {
        isDeath = false;
        transform.gameObject.SetActive(false);
    }

    private void Rotation()
    {
        Vector3 dir = _butterfly.localPosition - transform.localPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ObjectReset()
    {
        m_canmove = false;
    }

    private void SpiderSpeed()
    {
        float score = ScoreGauge.instance.barPosition.position.x;
        if (score < 831)
        {
            m_speed = 0.2f;
            ScriptManager.instance._delay = 1.6f;
        }
        else if (score > 830 && score < 961)
        {
            m_speed = 0.4f;
            ScriptManager.instance._delay = 1.4f;
        }
        else if (score > 960 && score < 1091)
        {
            m_speed = 0.6f;
            ScriptManager.instance._delay = 1.2f;
        }
        else if (score > 1090)
        {
            m_speed = 0.8f;
            ScriptManager.instance._delay = 1.0f;
        }
    }
}
