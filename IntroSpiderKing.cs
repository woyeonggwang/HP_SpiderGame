using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSpiderKing : MonoBehaviour
{
    public Transform _spiderKing;
    public Transform _spiderKingTargetPos;
    private Animator m_spiderKingAnim;

    private void Start()
    {
        m_spiderKingAnim = _spiderKing.transform.GetChild(0).GetComponent<Animator>();
    }

    public void Show()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        m_spiderKingAnim.SetTrigger("walk");
        while (Vector3.Distance(_spiderKing.position, _spiderKingTargetPos.position) > 0.1f)
        {
            _spiderKing.transform.position = Vector3.MoveTowards(_spiderKing.position, _spiderKingTargetPos.position, 20f * Time.deltaTime);
            yield return null;
        }
        m_spiderKingAnim.SetTrigger("idle");
    }

}
