using System.Collections;
using UnityEngine;

namespace Levels
{
    public class HealthScript : MonoBehaviour
    {
        public int hp;
        public int hpMax = 40;
        public HealthBar healthBar;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private HungerManager _hungerManager;
        [SerializeField] private SceneDrive _sceneDrive;
        [SerializeField] private LifeScript _lifeScript;
        [SerializeField] private GameEvent _scoreEvent;
        [SerializeField] private GameEvent _recordEvent;
        [SerializeField] private GameEvent _hungerEvent;
        [SerializeField] private GameEvent _lifeEvent;
        [SerializeField] private GameObject _overGame;
        private void Start()
        {
            hp = hpMax;
            healthBar.SetMaxHealth(hpMax);
            _hungerManager.Hunger = hpMax;
            StartCoroutine(HealthBay());
        }
        public void TakeDamage(int damage)                       //отнимаем здоровье от ядов
        {
            hp -= damage;
            healthBar.SetHealth(hp);
            if (hp > hpMax)
            {
                hpMax = hp;
                healthBar.SetMaxHealth(hpMax);
            }

            _hungerManager.HungerVal(damage);
            _hungerEvent.Raise();
            if (hp <= 0)
            {
                if (_lifeScript.life > 1)
                {
                    _lifeScript.LifeDamage(1);
                    _hungerManager.LifeVal(1);
                    StartCoroutine(_lifeScript.InstructLife());

                    gameObject.transform.position = _lifeScript.spawnPlayerCurent;    //спавн в сохраненную точку
                    _lifeEvent.Raise();
                    _lifeScript.lifeBar.SetLife(_lifeScript.life);
                    hp = hpMax;
                    healthBar.SetHealth(hp);
                    _hungerManager.Hunger = hpMax;
                    _sceneDrive.UpdateScore();
                }
                else
                {
                    _lifeScript.LastLife();                                        // все обнуляется
                }
            }
        }
        public void PlusDamage(int plus)                       //увеличение шкалы голода пери сьедании полезного
        {
            hp += plus;
            healthBar.SetHealth(hp);
            _scoreManager.ScoreVal(plus);
            _scoreEvent.Raise();
            _hungerManager.HungerPlus(plus);
            _hungerEvent.Raise();
            if (hp > hpMax)
            {
                hpMax = hp;
                healthBar.SetMaxHealth(hpMax);
            }
        }
        IEnumerator HealthBay()                   // уменьшение шкалы голода
        {
            while (hp > 0)
            {
                TakeDamage(5);
                yield return new WaitForSeconds(2);
            }
            yield return null;                              // выход из корутины
        }
    }
}
