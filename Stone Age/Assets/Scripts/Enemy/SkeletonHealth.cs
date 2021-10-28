using System;
using UnityEngine;

namespace Levels
{
    public class SkeletonHealth : MonoBehaviour
    {
        [SerializeField] private int _hpSkelet;
        [SerializeField] private int _hpMaxSkelet = 40;
        [SerializeField] private GameEvent _hungerEvent;
        [SerializeField] private GameEvent _lifeEvent;
        [SerializeField] private GameEvent _scoreEvent;
        [SerializeField] private GameEvent _recordEvent;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private SceletonManager _sceletonManager;
        [SerializeField] private HungerManager _hungerManager;
        [SerializeField] private GameObject _skeletonWin;
        [SerializeField] private GameObject _skeletonEffect;
        [SerializeField] private GameObject _skeletonDeath;        

        public int HpSkelet { get => _hpSkelet; set => _hpSkelet = value; }
        public int HpMaxSkelet { get => _hpMaxSkelet; set => _hpMaxSkelet = value; }

        public static event Action OnSceletonPlus;

        void OnEnable()
        {
            _hpSkelet = _hpMaxSkelet;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            BulletScript bulletScript = col.gameObject.GetComponent<BulletScript>();
            LifeScript lifeScript = col.gameObject.GetComponent<LifeScript>();
            HealthScript healthScript = col.gameObject.GetComponent<HealthScript>();

            if (bulletScript != null)                         // столкновение с камнем
            {
                _hpSkelet -= bulletScript.damageStone;
                if (_hpSkelet > 0)
                {
                    GameObject effectShot = Instantiate(_skeletonEffect, transform.position, transform.rotation);
                    Destroy(effectShot, 2f);
                }
                if (_hpSkelet <= 0)
                {
                    GameObject effectDeath = Instantiate(_skeletonDeath, transform.position, transform.rotation);
                    Destroy(effectDeath, 2f);
                    gameObject.SetActive(false);
                    _hpSkelet = _hpMaxSkelet;   //GetComponent<SkeletonMoove>().skeletonProperty.SkeletonHealth
                    _scoreManager.ScoreVal(100);
                    _scoreManager.RecordVal();
                    _recordEvent.Raise();
                    _scoreEvent.Raise();
                    _sceletonManager.SceletonPlus++;
                    OnSceletonPlus?.Invoke();
                }
            }

            if (lifeScript != null)                         // столкновение с player
            {
                GameObject effectShot = Instantiate(_skeletonWin, transform.position, transform.rotation);
                Destroy(effectShot, 2f);
                gameObject.SetActive(false);

                if (lifeScript.Life > 1)                //используем поля healthScript
                {
                    col.gameObject.transform.position = lifeScript.SpawnPlayerCurent;

                    lifeScript.LifeDamage(1);
                    _hungerManager.LifeVal(1);
                    _lifeEvent.Raise();
                    lifeScript.LifeBar.SetLife(lifeScript.Life);
                    healthScript.Hp = healthScript.HpMax;
                    healthScript.HealthBar.SetHealth(healthScript.Hp);
                    _hungerManager.Hunger = healthScript.HpMax;
                }
                else
                {
                    lifeScript.LastLife();
                }
            }
        }
    }
}
