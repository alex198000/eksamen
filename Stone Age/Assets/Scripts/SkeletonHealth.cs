using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonHealth : MonoBehaviour
{
    public int hpSkelet;
    public int hpMaxSkelet = 40;
    [SerializeField] private GameEvent hungerEvent;
    [SerializeField] private GameEvent lifeEvent;
    [SerializeField] private GameEvent scoreEvent;
    [SerializeField] private GameEvent recordEvent;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private HungerManager hungerManager;
    [SerializeField] private GameObject skeletonWin;
    [SerializeField] private GameObject skeletonEffect;
    [SerializeField] private GameObject skeletonDeath;
    //void Start()
    //{
    //    hpSkelet = hpMaxSkelet;
    //}

    void OnEnable()
    {
        hpSkelet = hpMaxSkelet;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D col)
    {
        BulletScript bulletScript = col.gameObject.GetComponent<BulletScript>();
        LifeScript lifeScript = col.gameObject.GetComponent<LifeScript>();
        HealthScript healthScript = col.gameObject.GetComponent<HealthScript>();


        if (bulletScript != null)                         // столкновение с камнем
        {
            hpSkelet -= bulletScript.damageStone;
            if (hpSkelet > 0)
            {
               GameObject effectShot = Instantiate(skeletonEffect, transform.position, transform.rotation);
               Destroy(effectShot, 2f);
            }
            if (hpSkelet <= 0)
            {
                GameObject effectDeath = Instantiate(skeletonDeath, transform.position, transform.rotation);
                Destroy(effectDeath, 2f);
                gameObject.SetActive(false);
                hpSkelet = hpMaxSkelet;   //GetComponent<SkeletonMoove>().skeletonProperty.SkeletonHealth
                scoreManager.ScoreVal(100);
                scoreManager.RecordVal();
                recordEvent.Raise();
                scoreEvent.Raise();
            }
        }

        if (lifeScript != null)                         // столкновение с player
        {
            GameObject effectShot = Instantiate(skeletonWin, transform.position, transform.rotation);
            Destroy(effectShot, 2f);
            gameObject.SetActive(false);

            if (lifeScript.life > 1)                //используем поля healthScript
            {
                col.gameObject.transform.position = lifeScript.spawnPlayerCurent;

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
