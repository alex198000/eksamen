using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Levels
{
    public class LifeBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _filLife;
        [SerializeField] private TextMeshProUGUI _lifeText;

        //public Slider Slider { get => _slider; set => _slider = value; }

        public void SetMaxLife(int life)
        {
            _slider.maxValue = life;
            _slider.value = life;

            //filLife.color = _gradient.Evaluate(1f);
            _lifeText.color = _gradient.Evaluate(1f);
        }
        public void SetLife(int life)
        {
            _slider.value = life;

            //filLife.color = _gradient.Evaluate(slider.normalizedValue);
            _lifeText.color = _gradient.Evaluate(_slider.normalizedValue);
        }
    }
}