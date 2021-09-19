using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp;
    public int hpMax = 40;
    public HealthBar healthBar;
    public LifeBar lifeBar;
    public int life;
    public int lifeMax = 11;
    public Vector3 spawnPlayer;
    public Vector3 spawnPlayerCurent;
    [SerializeField] private ScoreManager scoreManager;
    //[SerializeField] private SkeletonSpawner skeletonSpawner;
    [SerializeField] private SceneDrive sceneDrive;
    [SerializeField] private GameEvent scoreEvent;
    [SerializeField] private GameEvent recordEvent;
    [SerializeField] private GameEvent hungerEvent;
    [SerializeField] private GameEvent lifeEvent;    
    [SerializeField] private GameObject loseGame;
    [SerializeField] private GameObject overGame;
    [SerializeField] private GameObject winGame;
    [SerializeField] private GameObject loseInstr;
    [SerializeField] private GameObject muhomorInstr;
    [SerializeField] private GameObject lifeInstr;
    [SerializeField] private GameObject dotInstr;
    [SerializeField] private GameObject danger;
    [SerializeField] private GameObject eatFruit;

    private void Start()
    {
        
        spawnPlayer = gameObject.transform.position;     //начальная точка спавна
        spawnPlayerCurent = spawnPlayer;
        hp = hpMax;
        healthBar.SetMaxHealth(hpMax);
        scoreManager.Hunger = hpMax;
        StartCoroutine(HealthBay());
        life = lifeMax;
        lifeBar.SetMaxLife(lifeMax);
        scoreManager.Life = lifeMax;
        sceneDrive.UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D otherCol)
    {
        if (otherCol.gameObject.tag == "Muhomor")                                     //контакт с мухомором, минус жизнь
        {
            if (life > 1)
            {
                GameObject dangerous = Instantiate(danger, transform.position, transform.rotation);
                Destroy(dangerous, 5f);
                gameObject.transform.position = spawnPlayerCurent;
                StartCoroutine(InstructMuhomor());
                LifeDamage(1);
                scoreManager.LifeVal(1);
                lifeEvent.Raise();                
                lifeBar.SetLife(life);
                hp = hpMax;
                healthBar.SetHealth(hp);
                scoreManager.Hunger = hpMax;
                sceneDrive.UpdateScore();
            }
            else
            {              
                LastLife();
            }
        }

        if (otherCol.gameObject.tag == "Water")                                //контакт с водой
        {
            if (life > 1)
            {
                gameObject.transform.position = spawnPlayerCurent;
                StartCoroutine(InstrWeater());
                LifeDamage(1);
                scoreManager.LifeVal(1);
                lifeEvent.Raise();
                lifeBar.SetLife(life);
                hp = hpMax;
                healthBar.SetHealth(hp);
                scoreManager.Hunger = hpMax;
                sceneDrive.UpdateScore();
            }
            else
            {                
                LastLife();
            }
        }

        if (otherCol.gameObject.tag == "poganka")                     //контакт с поганкой, ущерб
        {
            TakeDamage(10);
            healthBar.SetHealth(hp);
            Destroy(otherCol.gameObject);
            GameObject dangerous = Instantiate(danger, transform.position, transform.rotation); 
            Destroy(dangerous, 5f);
        }

        if (otherCol.gameObject.tag == "Mushroom")                 //контакт с полезным грибом
        {
            GameObject fru = Instantiate(eatFruit, transform.position, transform.rotation);
            Destroy(fru, 5f);
            PlusDamage(10);
            healthBar.SetHealth(hp);
            scoreManager.RecordVal();
            recordEvent.Raise();          
            Destroy(otherCol.gameObject);
            if (hp > hpMax)
            {
                hpMax = hp;
                healthBar.SetMaxHealth(hpMax);
            }
        }

        if (otherCol.gameObject.tag == "fruit")                  //контакт с фруктами
        {
            GameObject fru = Instantiate(eatFruit, transform.position, transform.rotation);
            Destroy(fru, 5f);
            PlusDamage(20);
            healthBar.SetHealth(hp);
            Destroy(otherCol.gameObject);
            scoreManager.RecordVal();
            recordEvent.Raise();
           
            if (hp > hpMax)
            {
                hpMax = hp;
                healthBar.SetMaxHealth(hpMax);
            }
        }

        if (otherCol.gameObject.tag == "superMushroom")                       // контакт с супер грибом для перехода к новому уровню
        {
            GameObject fru = Instantiate(eatFruit, transform.position, transform.rotation);
            Destroy(fru, 5f);
            winGame.SetActive(true);
            gameObject.SetActive(false);
            spawnPlayer = new Vector3(-2, -2, 0);
            
              if (PlayerPrefs.GetInt("LevelSave") < sceneDrive.unlockLevel)
               {
                PlayerPrefs.SetInt("LevelSave", sceneDrive.unlockLevel);
               }

            PlusDamage(100);
            scoreManager.RecordVal();
            recordEvent.Raise();          
            healthBar.SetHealth(hp);
            Destroy(otherCol.gameObject);
            
            if (hp > hpMax)
              {
                hpMax = hp;
                healthBar.SetMaxHealth(hpMax);
              }
        }
        
        if (otherCol.gameObject.tag == "save1")                  //контакт с точкой сохранения1
        {
            if (otherCol.transform.position.x > spawnPlayerCurent.x)                                                 //толькр если точка сохранения больше текущей по х
            {
                StartCoroutine(DotContr());
                spawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
                //skeletonSpawner.spawnPointPosition = new Vector3;
            }
        }
        if (otherCol.gameObject.tag == "save2")                  //контакт с точкой сохранения2
        {
            if (otherCol.transform.position.x > spawnPlayerCurent.x)
            {
                StartCoroutine(DotContr());
                spawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
            }
        }
    }
    public void LastLife()                           // последняя жизнь
    {
        spawnPlayerCurent = spawnPlayer;
        LifeDamage(1);
        scoreManager.LifeVal(1);
        gameObject.SetActive(false);
        lifeEvent.Raise();
        lifeBar.SetLife(life);
        hp = 0;
        scoreManager.Hunger = hp;
        healthBar.SetHealth(hp);
        sceneDrive.UpdateScore();                             // обновляем юай здоровья
    }

    public void TakeDamage(int damage)                       //отнимание здоровья от ядов
    {
        hp -= damage;
        healthBar.SetHealth(hp);
        if (hp > hpMax)
        {
            hpMax = hp;
            healthBar.SetMaxHealth(hpMax);
        }

        scoreManager.HungerVal(damage);
        hungerEvent.Raise();
        if (hp <= 0)
        { 
            if(life > 1)
            {            
            LifeDamage(1);
            scoreManager.LifeVal(1);
            StartCoroutine(InstructLife());

            gameObject.transform.position = spawnPlayerCurent;    //спавн в сохраненную точку
            lifeEvent.Raise();
            lifeBar.SetLife(life);
            hp = hpMax;
            healthBar.SetHealth(hp);
            scoreManager.Hunger = hpMax;
            sceneDrive.UpdateScore();
            }
            else
            {                
                LastLife();                                        // все обнуляется
            }
        }
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
    private void PlusDamage(int plus)                       //увеличение шкалы голода пери сьедании полезного
    {
        hp += plus;
        healthBar.SetHealth(hp);
        scoreManager.ScoreVal(plus);
        scoreEvent.Raise();
        scoreManager.HungerPlus(plus);
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
    IEnumerator InstrWeater()                        //запуск сообщения об утонувшем герое
    {
        if (life >= 1)
        {
            loseInstr.SetActive(true);
            yield return new WaitForSeconds(3);
            loseInstr.SetActive(false);
        }
    }

    IEnumerator InstructMuhomor()                       //запуск сообщения об отравлении
    {
        if (life >= 1)
        {
            muhomorInstr.SetActive(true);
            yield return new WaitForSeconds(3);
            muhomorInstr.SetActive(false);
        }
    }
    IEnumerator InstructLife()                            //запуск сообщения о потери жизни
    {
        if (life >= 1)
        {
            lifeInstr.SetActive(true);
            yield return new WaitForSeconds(3);
            lifeInstr.SetActive(false);
        }
    }
    IEnumerator DotContr()                            //запуск сообщения о потери жизни
    {
        dotInstr.SetActive(true);
        yield return new WaitForSeconds(3);
        dotInstr.SetActive(false);        
    }
}
