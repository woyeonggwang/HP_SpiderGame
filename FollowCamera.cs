using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform _target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(_target.transform.position.x, _target.transform.position.y + 2f, _target.transform.position.z + 10f);
        transform.position = Vector3.Lerp(transform.position, targetPos, 3f);
        //transform.LookAt(_target);
    }
}
