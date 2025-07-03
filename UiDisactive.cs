using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDisactive : MonoBehaviour
{
    public Image _GameGuide00, _GameGuide01, _bg, _title;
    float time = 0;
    float delay = 2f;
    // Update is called once per frame
    private void Update()
    {
        time += Time.deltaTime;
        
        if (time > delay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Disactivate();
                
            }

        }
    }
    private void Disactivate()
    {
        if (_GameGuide00.gameObject)
        {
        _GameGuide00.gameObject.SetActive(false);
        }
        if (_GameGuide01.gameObject)
        {
        _GameGuide01.gameObject.SetActive(false);
        }
        if (_bg.gameObject)
        {
            _bg.gameObject.SetActive(false);
        }
        if (_title.gameObject)
        {
        _title.gameObject.SetActive(false);
        }
    }
}
