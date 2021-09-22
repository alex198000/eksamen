using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public LifeBar lifeBar;
    public int life;
    public int lifeMax = 11;
    public Vector3 spawnPlayer;
    public Vector3 spawnPlayerCurent;
    [SerializeField] private HungerManager hungerManager;
    [SerializeField] private GameEvent lifeEvent;
    [SerializeField] private SceneDrive sceneDrive;
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private GameObject loseGame;
    [SerializeField] private GameObject defExp;
    [SerializeField] private GameObject lifeInstr;
    
    void Start()
    {
        life = lifeMax;
        lifeBar.SetMaxLife(lifeMax);
        hungerManager.Life = lifeMax;
        spawnPlayer = gameObject.transform.position;     //начальная точка спавна
        spawnPlayerCurent = spawnPlayer;
        sceneDrive.UpdateScore();
    }
    public void LastLife()                           // последняя жизнь
    {
        spawnPlayerCurent = spawnPlayer;
        LifeDamage(1);
        hungerManager.LifeVal(1);
        gameObject.SetActive(false);
        lifeEvent.Raise();
        lifeBar.SetLife(life);
        healthScript.hp = 0;
        hungerManager.Hunger = healthScript.hp;
        healthScript.healthBar.SetHealth(healthScript.hp);
        GameObject def = Instantiate(defExp, transform.position, transform.rotation);
        Destroy(def, 5f);
        sceneDrive.UpdateScore();                             // обновляем юай здоровья
    }
    public void LifeDamage(int lifeDamage)                        //отнимание жизни
    {
        life -= lifeDamage;
        lifeBar.SetLife(life);
        if (life < 1)
        {
            loseGame.SetActive(true);
        }

        if (life > lifeMax)
        {
            lifeMax = life;
            lifeBar.SetMaxLife(lifeMax);
        }
    }
    public IEnumerator InstructLife()                            //запуск сообщения о потери жизни
    {
        if (life >= 1)
        {
            lifeInstr.SetActive(true);
            yield return new WaitForSeconds(3);
            lifeInstr.SetActive(false);
        }
    }
}
