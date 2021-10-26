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
        public abstract void Contact();
        
    }
}