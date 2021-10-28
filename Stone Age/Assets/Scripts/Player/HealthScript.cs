using System.Collections;
using UnityEngine;

namespace Levels
{
    public class HealthScript : MonoBehaviour
    {        
        [SerializeField] private int _hp;
        [SerializeField] private int _hpMax = 40;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private HungerManager _hungerManager;
        [SerializeField] private SceneDrive _sceneDrive;
        [SerializeField] private LifeScript _lifeScript;
        [SerializeField] private GameEvent _scoreEvent;
        [SerializeField] private GameEvent _recordEvent;
        [SerializeField] private GameEvent _hungerEvent;
        [SerializeField] private GameEvent _lifeEvent;
        [SerializeField] private GameObject _overGame;

        public int HpMax { get => _hpMax; set => _hpMax = value; }
        public int Hp { get => _hp; set => _hp = value; }
        public HealthBar HealthBar { get => _healthBar; set => _healthBar = value; }

        private void Start()
        {
            _hp = _hpMax;
            _healthBar.SetMaxHealth(_hpMax);
            _hungerManager.Hunger = _hpMax;
            StartCoroutine(HealthBay());
        }
        public void TakeDamage(int damage)                       //отнимаем здоровье от ядов
        {
            _hp -= damage;
            _healthBar.SetHealth(_hp);
            if (_hp > _hpMax)
            {
                _hpMax = _hp;
                _healthBar.SetMaxHealth(_hpMax);
            }

            _hungerManager.HungerVal(damage);
            _hungerEvent.Raise();
            if (_hp <= 0)
            {
                if (_lifeScript.Life > 1)
                {
                    _lifeScript.LifeDamage(1);
                    _hungerManager.LifeVal(1);
                    StartCoroutine(_lifeScript.InstructLife());

                    gameObject.transform.position = _lifeScript.SpawnPlayerCurent;    //спавн в сохраненную точку
                    _lifeEvent.Raise();
                    _lifeScript.LifeBar.SetLife(_lifeScript.Life);
                    _hp = _hpMax;
                    _healthBar.SetHealth(_hp);
                    _hungerManager.Hunger = _hpMax;
                    _sceneDrive.UpdateScore();
                }
                else
                {
                    _lifeScript.LastLife();                                        // все обнуляется
                }
            }
        }
        public void PlusDamage(int plusHp, int plusScore)                       //увеличение шкалы голода пери сьедании полезного
        {
            _hp += plusHp;
            _healthBar.SetHealth(_hp);
            _scoreManager.ScoreVal(plusScore);
            _scoreEvent.Raise();
            _hungerManager.HungerPlus(plusHp);
            _hungerEvent.Raise();
            if (_hp > _hpMax)
            {
                _hpMax = _hp;
                _healthBar.SetMaxHealth(_hpMax);
            }
        }
        IEnumerator HealthBay()                   // уменьшение шкалы голода
        {
            while (_hp > 0)
            {
                TakeDamage(5);
                yield return new WaitForSeconds(2f);
            }
            yield return null;                              // выход из корутины
        }
    }
}
