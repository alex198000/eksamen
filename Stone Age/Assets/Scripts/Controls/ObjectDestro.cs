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
            if (skeletonMoove != null)                     //???? ???? ??????
            {
                coll.gameObject.SetActive(false);
                _sceletonManager.SceletonMinus++;
                OnSceletonMinus?.Invoke();
            }
            if (bulletScript != null)                         // ??????????? ?????
            {
                coll.gameObject.SetActive(false);
            }

            if (lifeScript != null)                     //???? ???? ?????
            {
                if (lifeScript.Life > 1)                //?????????? ???? healthScript
                {
                    coll.gameObject.transform.position = lifeScript.SpawnPlayerCurent;

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