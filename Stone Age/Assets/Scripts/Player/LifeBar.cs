using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LifeBar : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image filLife;
    [SerializeField] private TextMeshProUGUI lifeText;
    public void SetMaxLife(int life)
    {
        slider.maxValue = life;
        slider.value = life;

        //filLife.color = gradient.Evaluate(1f);
        lifeText.color = gradient.Evaluate(1f);
    }
    public void SetLife(int life)
    {
        slider.value = life;

        //filLife.color = gradient.Evaluate(slider.normalizedValue);
        lifeText.color = gradient.Evaluate(slider.normalizedValue);
    }
}
