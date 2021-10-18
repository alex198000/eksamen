using System;
using UnityEngine;

namespace Levels
{
    public class ObjectDestro : MonoBehaviour
    {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private SceletonManager _sceletonManager;
        [SerializeField] private HungerManager _hungerManager;
        [SerializeField] private GameEvent _lifeEvent;

        public static Action OnSceletonMinus;

        private void OnTriggerEnter2D(Collider2D coll)
        {
            SkeletonMoove skeletonMoove = coll.gameObject.GetComponent<SkeletonMoove>();
            LifeScript lifeScript = coll.gameObject.GetComponent<LifeScript>();
            HealthScript healthScript = coll.gameObject.GetComponent<HealthScript>();
            BulletScript bulletScript = coll.gameObject.GetComponent<BulletScript>();
            if (skeletonMoove != null)                     //если упал скелет
            {
                coll.gameObject.SetActive(false);
                _sceletonManager.SceletonMinus++;
                OnSceletonMinus?.Invoke();
            }
            if (bulletScript != null)                         // деактивация камня
            {
                coll.gameObject.SetActive(false);
            }

            if (lifeScript != null)                     //если упал игрок
            {
                if (lifeScript.life > 1)                //используем поля healthScript
                {
                    coll.gameObject.transform.position = lifeScript.spawnPlayerCurent;

                    lifeScript.LifeDamage(1);
                    _hungerManager.LifeVal(1);
                    _lifeEvent.Raise();
                    lifeScript.lifeBar.SetLife(lifeScript.life);
                    healthScript.hp = healthScript.hpMax;
                    healthScript.healthBar.SetHealth(healthScript.hp);
                    _hungerManager.Hunger = healthScript.hpMax;

                }
                else
                {
                    lifeScript.LastLife();
                }
            }
        }
    }
}