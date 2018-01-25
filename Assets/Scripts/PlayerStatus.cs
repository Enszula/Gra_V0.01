using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

    [SerializeField] private RectTransform healthBarRect;
    [SerializeField] private Text healthBarText;
    [SerializeField] private Image healthBarColor;
    GradientColorKey[] gck = new GradientColorKey[3];
    GradientAlphaKey[] gak = new GradientAlphaKey[2];
    Gradient g = new Gradient();
    void Start()
    {
        if(healthBarRect == null)
        {
            Debug.LogError("Brak obiektu healthBarRect");
        }

        if (healthBarText == null)
        {
            Debug.LogError("Brak obiektu healthBarText");
        }

        gck[0].color = Color.green;
        gck[0].time = 0.0f;
        gck[1].color = Color.yellow;
        gck[1].time = 0.5f;
        gck[2].color = Color.red;
        gck[2].time = 1.0f;
        gak[0].alpha = 1.0f;
        gak[0].time = 0.0f;
        gak[1].alpha = 1.0f;
        gak[1].time = 1.0f;
        g.SetKeys(gck, gak);
        Debug.Log(g.Evaluate(0.25f));
    }

    public void SetHealth(int _cur, int _max)//cur -- current
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthBarText.text = _cur + "/" + _max;
        healthBarColor.color = g.Evaluate(1 - _value);
        
    }
}
