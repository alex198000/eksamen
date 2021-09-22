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
        HealthScript healthScript = coll.gameObject.GetComponent<HealthScript>();
        BulletScript bulletScript = coll.gameObject.GetComponent<BulletScript>();
        if (skeletonMoove != null)                     //���� ���� ������
        {
            coll.gameObject.SetActive(false);
        }
        if (bulletScript != null)                         // ����������� �����
        {
            coll.gameObject.SetActive(false);
        }

        if (healthScript != null)                     //���� ���� �����
        {


            if (healthScript.life > 1)                //���������� ���� healthScript
            {
                coll.gameObject.transform.position = healthScript.spawnPlayerCurent;

                healthScript.LifeDamage(1);
                hungerManager.LifeVal(1);
                lifeEvent.Raise();
                healthScript.lifeBar.SetLife(healthScript.life);
                healthScript.hp = healthScript.hpMax;
                healthScript.healthBar.SetHealth(healthScript.hp);
                hungerManager.Hunger = healthScript.hpMax;
               
            }
            else
            {
                healthScript.LastLife();
            }
        }
    }
}
