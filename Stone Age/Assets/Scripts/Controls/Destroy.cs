using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private HungerManager hungerManager;
    [SerializeField] private GameEvent lifeEvent;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        SkeletonMoove skeletonMoove = coll.gameObject.GetComponent<SkeletonMoove>();
        LifeScript lifeScript = coll.gameObject.GetComponent<LifeScript>();
        HealthScript healthScript = coll.gameObject.GetComponent<HealthScript>();
        BulletScript bulletScript = coll.gameObject.GetComponent<BulletScript>();
        if (skeletonMoove != null)                     //если упал скелет
        {
            coll.gameObject.SetActive(false);
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
                hungerManager.LifeVal(1);
                lifeEvent.Raise();
                lifeScript.lifeBar.SetLife(lifeScript.life);
                healthScript.hp = healthScript.hpMax;
                healthScript.healthBar.SetHealth(healthScript.hp);
                hungerManager.Hunger = healthScript.hpMax;
               
            }
            else
            {
                lifeScript.LastLife();
            }
        }
    }
}
