using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptManager : MonoBehaviour
{
    public static ScriptManager instance;

    [Header("나비가 공격당할때 파티클")]
    public ButterflyHitParticleManager butterflyHitParticleManager;

    [Header("거미, 거미스폰위치, 부모")]
    public GameObject _spider;
    public Transform[] _spiderSpawnPos;
    public Transform _objectPoolBox;

    [Header("애니메이션")]
    public Animator _spiderBoss;
    public Animator _butterfly_end;
    public Animator _completeFly;
    public Animator _fadeOut;
    public Animator _startGame;
    public Animator _startKing;
    public Animator _cameraShake;

    [Header("오디오")]
    public AudioSource[] _narration;

    [Header("텍스트")]
    public Text _timer;
    public Text _scoreText;

    [Header("이미지")]
    public Image _fade;
    public Image _GameGuide00;
    public Image _GameGuide01;
    public Image _bg;
    public Image _gamestart;
    public Image _titleBind;

    [Header("게이지")]
    public Image _gaugebar;
    public Sprite[] _gaugebarSpr;

    [Header("기타")]
    public GameObject _result;
    public Transform _butterfly;
    public Material[] _spiderMat;
    public GameObject _spiderWebEffect;
    public GameObject _gameruleParticle;
    public Sprite[] _spiderwebList;
    public GameObject[] _ruleParticle;
    [HideInInspector] public float _delay;
    [HideInInspector] public float m_sec;


    private SoundManager m_sound;

    private List<GameObject> m_spiderList = new List<GameObject>();

    private bool end;
    private float m_time;
    private float m_min;
    private int hp = 6;
    private int touchCount = 0; //웹터치소리 ui사라질때는 안 들리게
    private int m_score = 0;
    private bool m_fade;
    private bool m_gamestart;


    public AudioSource _mainBgm;
    //private int m_hitStack=0;
    private int m_increase;

    private bool m_gameend;
    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        m_sec = 60;
        end = false;

        ObjectPool();

        StartCoroutine(FadeIn());

        m_sound = GetComponent<SoundManager>();
        _mainBgm.Play();
        _narration[0].Play();
        m_increase = 1;

    }

    private void Update()
    {
        if (_mainBgm.volume < 0.5f)
        {
            _mainBgm.volume += m_increase * AudioListener.volume * 0.1f * Time.deltaTime;
        }

        if (AudioListener.volume <= 0.01f)
        {
            m_increase = -1;
        }

        if (AudioListener.volume >= 1.0f)
        {
            m_increase = 1;
        }

        if (m_gameend)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (m_fade)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_gamestart = true;
                _GameGuide00.gameObject.SetActive(false);
                _GameGuide01.gameObject.SetActive(false);
                _bg.gameObject.SetActive(false);
                _gamestart.gameObject.SetActive(false);
                _titleBind.gameObject.SetActive(false);
                _startGame.SetTrigger("startGame");
                _startKing.SetTrigger("gameStart");
                _gameruleParticle.SetActive(false);
                _ruleParticle[0].SetActive(false);
                _ruleParticle[1].SetActive(false);
                touchCount += 1;

            }
        }

        if (m_gamestart)
        {
            if (ScoreGauge.instance.barPosition.position.x > 0)
            {
                if (_gaugebar.sprite != _gaugebarSpr[0]) _gaugebar.sprite = _gaugebarSpr[0];
            }
            else
            {
                if (_gaugebar.sprite != _gaugebarSpr[1]) _gaugebar.sprite = _gaugebarSpr[1];
            }

            if (ScoreGauge.instance.barPosition.position.x > 960)
            {
                    ScoreGauge.instance.barPosition.transform.Translate(new Vector3(-6f, 0, 0) * Time.deltaTime);
                    //_score -= 0.3f;
            }
            else
            {
                if (ScoreGauge.instance.barPosition.position.x > 700)
                {
                    ScoreGauge.instance.barPosition.transform.Translate(new Vector3(-4f, 0, 0) * Time.deltaTime);
                    //_score -= 0.09f;
                }
            }

            Timer();

            m_time += Time.deltaTime;

            if (m_time > _delay)
            {
                _delay = 3f;
                SpawnSpider();
                m_time = 0;

            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hit;
                RaycastHit hitWeb;
                hit = Physics.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.Length > 0)
                {
                    for(int i = 0; i < hit.Length; i++)
                    {
                        
                        if (hit[i].collider.CompareTag("Spider"))
                        {
                            if (ScoreGauge.instance.barPosition.position.x < 1211)
                            {
                                ScoreGauge.instance.barPosition.transform.Translate(new Vector3(20.4f, 0, 0));
                            }

                            hit[i].collider.GetComponent<SpiderMove>().Death();
                            m_sound.Play(0);
                            m_score += 1;
                            if (m_score == 8) _narration[5].Play();
                            else if (m_score == 16) _narration[2].Play();
                        }

                        if (hit[i].collider.CompareTag("Web"))
                        {
                            ProjectManager.insance.WebPos(hit[i].point);
                            
                        }

                    }
                }
                if (Physics.Raycast(ray,out hitWeb))
                {
                    if (hitWeb.collider.CompareTag("Web"))
                    {
                        if (touchCount>1)
                        {

                            Sound().Play(3);
                        }
                    }
                }
            }
#else
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        RaycastHit[] hit;
                        RaycastHit hitWeb;
                        hit = Physics.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
                        if (hit.Length > 0)
                        {
                            for (int j = 0; j < hit.Length; j++)
                            {
                                if (hit[j].collider.CompareTag("Spider"))
                                {
                                    if (ScoreGauge.instance.barPosition.position.x < 1211)
                                    {
                                        ScoreGauge.instance.barPosition.transform.Translate(new Vector3(20.4f, 0, 0));
                                    }

                                    hit[j].collider.GetComponent<SpiderMove>().Death();
                                    m_sound.Play(0);
                                    m_score += 1;
                                    if (m_score == 8) _narration[5].Play();
                                    else if (m_score == 16) _narration[2].Play();
                                }

                                if (hit[j].collider.CompareTag("Web"))
                                {
                                    ProjectManager.insance.WebPos(hit[j].point);
                                }
                            }
                        }
                        if (Physics.Raycast(ray,out hitWeb))
                        {
                            if (hitWeb.collider.CompareTag("Web"))
                            {
                                if (touchCount>1)
                                {

                                    Sound().Play(3);
                                }
                            }
                        }
                    }
                }
            }
#endif
        }
    }

    private void ObjectPool()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject temp = Instantiate(_spider, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
            temp.gameObject.SetActive(false);
            temp.transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = _spiderMat[Random.Range(0, _spiderMat.Length)];
            temp.transform.parent = _objectPoolBox.transform;
            m_spiderList.Add(temp);
        }
    }

    private IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1.5f);
        _result.gameObject.SetActive(true);
        StartCoroutine(FadeIn2());

        _mainBgm.volume -= m_increase * AudioListener.volume * Time.deltaTime;
    }

    private void SpawnSpider()
    {
        for (int i = 0; i < m_spiderList.Count; i++)
        {
            if (!m_spiderList[i].activeInHierarchy)
            {
                m_spiderList[i].transform.position = _spiderSpawnPos[Random.Range(0, _spiderSpawnPos.Length)].transform.position;

                m_spiderList[i].SetActive(true);
                break;
            }
        }
    }

    public IEnumerator FadeOut()
    {
        float colorA = 0;
        while (colorA < 1)
        {
            colorA += 0.5f * Time.deltaTime;
            _fade.color = new Color(0, 0, 0, colorA);
            yield return null;
        }

    }

    public IEnumerator FadeOut2()
    {
        float colorA = 0;
        while (colorA < 1)
        {
            colorA += 0.5f * Time.deltaTime;
            _fade.color = new Color(0, 0, 0, colorA);
            yield return null;
            if (colorA > 0.89f)
            {
                _mainBgm.volume -= m_increase * AudioListener.volume * Time.deltaTime;
                SceneManager.LoadScene(2);
            }
        }

    }

    public IEnumerator FadeIn()
    {
        float complete = 0;
        float completeTime = 3f;
        float colorA = 1f;
        while (colorA > 0)
        {
            colorA -= 0.5f * Time.deltaTime;
            _fade.color = new Color(0, 0, 0, colorA);
            if (colorA < 0.2)
            {
                complete += Time.deltaTime;

                if (complete > completeTime)
                {
                    SceneManager.LoadScene(0);

                }
            }
            yield return null;
        }

    }

    public IEnumerator FadeIn2()
    {
        float complete = 0;
        float completeTime = 3f;

        complete += Time.deltaTime;
        if (complete > completeTime)
        {
            end = true;
        }
        yield return null;
        m_gameend = true;

    }

    public void SetFade()
    {
        m_fade = true;
    }

    public void Timer()
    {
        m_sec -= Time.deltaTime;
        Thread();
        string timerstr = string.Format("{0:N0}", m_sec);
        _timer.text = timerstr;
        ProjectManager.insance._time = timerstr;
    }

    public SoundManager Sound()
    {
        return m_sound;
    }

    public void Thread()
    {
        if (m_sec > 45 && m_sec < 50)
        {
            _spiderBoss.SetTrigger("angry");
        }
        else if (m_sec < 40 && m_sec > 35)
        {
            _spiderBoss.SetTrigger("angry1");
        }
        else if (m_sec < 30 && m_sec > 25)
        {
            _spiderBoss.SetTrigger("angry2");
        }
        else if (m_sec < 20 && m_sec > 15)
        {
            _spiderBoss.SetTrigger("angry3");
            _narration[4].Play();
        }
        else if (m_sec < 10)
        {
            _spiderBoss.SetTrigger("lastThreat");
        }
        if (m_sec < 0.2)
        {
            if (ScoreGauge.instance.barPosition.position.x > 960)
            {
                _narration[6].Play();
                StartCoroutine(GameEnd());
                _completeFly.SetTrigger("startFly");
                _objectPoolBox.gameObject.SetActive(false);
                _butterfly_end.SetTrigger("startFly");
                m_gamestart = false;
                _spiderWebEffect.SetActive(false);
                _mainBgm.Stop();
                _spiderBoss.SetTrigger("win");
                if (end)
                {
                    SceneManager.LoadScene(0);
                }

            }
            else if (ScoreGauge.instance.barPosition.position.x < 961)
            {
                _objectPoolBox.gameObject.SetActive(false);
                m_gamestart = false;
                _fadeOut.SetTrigger("gameEnd");
                StartCoroutine(FadeOut2());
                // ProjectManager.insance._timeGaugeFillAmount = _scoreGauge.fillAmount;
                _butterfly_end.SetTrigger("gameFailed");
            }
        }

    }

    public void Hit()
    {
        butterflyHitParticleManager.Play();
    }
}
