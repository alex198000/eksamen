using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp;
    public int hpMax = 40;
    public HealthBar healthBar;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private HungerManager hungerManager;
    [SerializeField] private SceneDrive sceneDrive;
    [SerializeField] private LifeScript lifeScript;
    [SerializeField] private GameEvent scoreEvent;
    [SerializeField] private GameEvent recordEvent;
    [SerializeField] private GameEvent hungerEvent;
    [SerializeField] private GameEvent lifeEvent;    
    [SerializeField] private GameObject overGame;
    private void Start()
    {  
        hp = hpMax;
        healthBar.SetMaxHealth(hpMax);
        hungerManager.Hunger = hpMax;
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

        hungerManager.HungerVal(damage);
        hungerEvent.Raise();
        if (hp <= 0)
        { 
            if(lifeScript.life > 1)
            {            
            lifeScript.LifeDamage(1);
            hungerManager.LifeVal(1);
            StartCoroutine(lifeScript.InstructLife());

            gameObject.transform.position = lifeScript.spawnPlayerCurent;    //спавн в сохраненную точку
            lifeEvent.Raise();
            lifeScript.lifeBar.SetLife(lifeScript.life);
            hp = hpMax;
            healthBar.SetHealth(hp);
            hungerManager.Hunger = hpMax;
            sceneDrive.UpdateScore();
            }
            else
            {                
                lifeScript.LastLife();                                        // все обнуляется
            }
        }
    }
     public void PlusDamage(int plus)                       //увеличение шкалы голода пери сьедании полезного
    {
        hp += plus;
        healthBar.SetHealth(hp);
        scoreManager.ScoreVal(plus);
        scoreEvent.Raise();
        hungerManager.HungerPlus(plus);
        hungerEvent.Raise();
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
