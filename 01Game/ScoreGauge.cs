using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGauge : MonoBehaviour
{
    public static ScoreGauge instance;
    public RectTransform barPosition;
    public Image bar;
    public Image text;
    public Sprite[] bg;
    [HideInInspector] public int score;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        score = 0;
    }

    public void Update()
    {
        barPosition.position = bar.rectTransform.position;
        for(int i = 0; i<bg.Length; i++)
        {
            if (barPosition.position.x > 960)
            {
                text.GetComponent<Image>().sprite = bg[0];
            }
            else
            {
                text.GetComponent<Image>().sprite = bg[1];
            }
        }
    }
}
