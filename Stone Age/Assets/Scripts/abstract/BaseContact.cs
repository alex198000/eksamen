using UnityEngine;

namespace Levels
{
    public abstract class BaseContact : MonoBehaviour
    {
        [SerializeField] protected int _bonusScore;
        [SerializeField] protected int _bonusHp;
        [SerializeField] protected GameObject _effect;       
        [SerializeField] protected ScoreManager _scoreManager;        
        [SerializeField] protected HungerManager _hungerManager;       
        [SerializeField] protected SceneDrive _sceneDrive;
        [SerializeField] protected HealthScript _healthScript;
        [SerializeField] protected LifeScript _lifeScript;
        [SerializeField] protected GameEvent _recordEvent;
        [SerializeField] protected GameEvent _lifeEvent;
        //public abstract void Contact();
        public virtual void Contact()
        {
            GameObject fru = Instantiate(_effect, transform.position, transform.rotation);
            Destroy(fru, 5f);
            _healthScript.PlusDamage(_bonusHp, _bonusScore);
            _healthScript.HealthBar.SetHealth(_healthScript.Hp);
            _scoreManager.RecordVal();
            _recordEvent.Raise();
            gameObject.SetActive(false);
            if (_healthScript.Hp > _healthScript.HpMax)
            {
                _healthScript.HpMax = _healthScript.Hp;
                _healthScript.HealthBar.SetMaxHealth(_healthScript.HpMax);
            }
        }
        
    }
}