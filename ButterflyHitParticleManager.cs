using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyHitParticleManager : MonoBehaviour
{
    public GameObject prefab;
    public Transform target;
    public int max;
    public float cooltime;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Start()
    {
        if (max == 0) max = 100;
        if (cooltime == 0) cooltime = 3f;

        for(int i = 0; i < max; i++)
        {
            GameObject temp = Instantiate(prefab, target.position, Quaternion.identity);
            temp.SetActive(false);
            temp.transform.parent = transform;
            particles.Add(temp.GetComponent<ParticleSystem>());
        }
    }

    public void Play()
    {
        for(int i = 0; i < particles.Count; i++)
        {
            if (!particles[i].gameObject.activeInHierarchy)
            {
                particles[i].gameObject.SetActive(true);
                particles[i].Play();
                StartCoroutine(Cooltime(particles[i].gameObject));
                break;
            }
        }
    }

    private IEnumerator Cooltime(GameObject particle)
    {
        yield return new WaitForSeconds(cooltime);
        particle.gameObject.SetActive(false);
    }

}
