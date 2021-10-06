using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitAndMushrom { Muhomor, Water, Poganka, Mushroom, Fruit, SuperMushroom, DotSave1, DotSave2 };
public class ContactScript : MonoBehaviour
{
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private HungerManager hungerManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private SceneDrive sceneDrive;
    [SerializeField] private LifeScript lifeScript;
    [SerializeField] private GameEvent lifeEvent;
    [SerializeField] private GameEvent recordEvent;
    [SerializeField] private GameObject danger;
    [SerializeField] private GameObject muhomorInstr;
    [SerializeField] private GameObject dotInstr;
    [SerializeField] private GameObject loseInstr;
    [SerializeField] private GameObject winGame;
    [SerializeField] private GameObject winExp;
    [SerializeField] private GameObject eatFruit;
    private FruitAndMushrom fruitAndMushrom = FruitAndMushrom.SuperMushroom;
    
    void OnTriggerEnter2D(Collider2D otherCol)
    {
        FruitScript fruitScript = otherCol.gameObject.GetComponent<FruitScript>();
        MushroomScript mushroomScript = otherCol.gameObject.GetComponent<MushroomScript>();
        WaterScript waterScript = otherCol.gameObject.GetComponent<WaterScript>();
        MuhomorScript muhomorScript = otherCol.gameObject.GetComponent<MuhomorScript>();
        PogankaScript pogankaScript = otherCol.gameObject.GetComponent<PogankaScript>();
        SuperMushroomScript superMushroomScript = otherCol.gameObject.GetComponent<SuperMushroomScript>();

        if (muhomorScript != null)                                     //контакт с мухомором, минус жизнь
        {
            fruitAndMushrom = FruitAndMushrom.Muhomor;
            if (lifeScript.life > 1)
            {
                GameObject dangerous = Instantiate(danger, transform.position, transform.rotation);
                Destroy(dangerous, 5f);
                gameObject.transform.position = lifeScript.spawnPlayerCurent;
                StartCoroutine(InstructMuhomor());
                lifeScript.LifeDamage(1);
                hungerManager.LifeVal(1);
                lifeEvent.Raise();
                lifeScript.lifeBar.SetLife(lifeScript.life);
                healthScript.hp = healthScript.hpMax;
                healthScript.healthBar.SetHealth(healthScript.hp);
                hungerManager.Hunger = healthScript.hpMax;
                sceneDrive.UpdateScore();
            }
            else
            {
                lifeScript.LastLife();
            }
        }

        if (waterScript != null)                                //контакт с водой
        {
            fruitAndMushrom = FruitAndMushrom.Water;
            if (lifeScript.life > 1)
            {
                gameObject.transform.position = lifeScript.spawnPlayerCurent;
                StartCoroutine(InstrWeater());
                lifeScript.LifeDamage(1);
                hungerManager.LifeVal(1);
                lifeEvent.Raise();
                lifeScript.lifeBar.SetLife(lifeScript.life);
                healthScript.hp = healthScript.hpMax;
                healthScript.healthBar.SetHealth(healthScript.hp);
                hungerManager.Hunger = healthScript.hpMax;
                sceneDrive.UpdateScore();
            }
            else
            {
                lifeScript.LastLife();
            }
        }

        if (pogankaScript != null)                     //контакт с поганкой, ущерб
        {
            fruitAndMushrom = FruitAndMushrom.Poganka;
            healthScript.TakeDamage(10);
            healthScript.healthBar.SetHealth(healthScript.hp);
            Destroy(otherCol.gameObject);
            GameObject dangerous = Instantiate(danger, transform.position, transform.rotation);
            Destroy(dangerous, 5f);
        }

        if (mushroomScript != null)                 //контакт с полезным грибом
        {
            fruitAndMushrom = FruitAndMushrom.Mushroom;
            GameObject fru = Instantiate(eatFruit, transform.position, transform.rotation);
            Destroy(fru, 5f);
            healthScript.PlusDamage(10);
            healthScript.healthBar.SetHealth(healthScript.hp);
            scoreManager.RecordVal();
            recordEvent.Raise();
            Destroy(otherCol.gameObject);
            if (healthScript.hp > healthScript.hpMax)
            {
                healthScript.hpMax = healthScript.hp;
                healthScript.healthBar.SetMaxHealth(healthScript.hpMax);
            }
        }

        if (fruitScript != null)                  //контакт с фруктами
        {
            fruitAndMushrom = FruitAndMushrom.Fruit;
            GameObject fru = Instantiate(eatFruit, transform.position, transform.rotation);
            Destroy(fru, 5f);
            healthScript.PlusDamage(20);
            healthScript.healthBar.SetHealth(healthScript.hp);
            Destroy(otherCol.gameObject);
            scoreManager.RecordVal();
            recordEvent.Raise();

            if (healthScript.hp > healthScript.hpMax)
            {
                healthScript.hpMax = healthScript.hp;
                healthScript.healthBar.SetMaxHealth(healthScript.hpMax);
            }
        }

        if (superMushroomScript != null)                       // контакт с супер грибом для перехода к новому уровню
        {
            fruitAndMushrom = FruitAndMushrom.SuperMushroom;
            GameObject win = Instantiate(winExp, transform.position, transform.rotation);
            Destroy(win, 5f);
            winGame.SetActive(true);
            gameObject.SetActive(false);
            lifeScript.spawnPlayer = new Vector3(-2, -2, 0);

            if (PlayerPrefs.GetInt("LevelSave") < sceneDrive.unlockLevel)
            {
                PlayerPrefs.SetInt("LevelSave", sceneDrive.unlockLevel);
            }

            healthScript.PlusDamage(100);
            scoreManager.RecordVal();
            recordEvent.Raise();
            healthScript.healthBar.SetHealth(healthScript.hp);
            Destroy(otherCol.gameObject);

            if (healthScript.hp > healthScript.hpMax)
            {
                healthScript.hpMax = healthScript.hp;
                healthScript.healthBar.SetMaxHealth(healthScript.hpMax);
            }
        }

        if (otherCol.gameObject.tag == "save1")                  //контакт с точкой сохранения1
        {
            fruitAndMushrom = FruitAndMushrom.DotSave1;
            if (otherCol.transform.position.x > lifeScript.spawnPlayerCurent.x)                                                 //толькр если точка сохранения больше текущей по х
            {
                StartCoroutine(DotContr());
                lifeScript.spawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
                //skeletonSpawner.spawnPointPosition = new Vector3;
            }
        }
        if (otherCol.gameObject.tag == "save2")                  //контакт с точкой сохранения2
        {
            fruitAndMushrom = FruitAndMushrom.DotSave2;
            if (otherCol.transform.position.x > lifeScript.spawnPlayerCurent.x)
            {
                StartCoroutine(DotContr());
                lifeScript.spawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
            }
        }
    }
    IEnumerator InstrWeater()                        //запуск сообщения об утонувшем герое
    {
        if (lifeScript.life >= 1)
        {
            loseInstr.SetActive(true);
            yield return new WaitForSeconds(3);
            loseInstr.SetActive(false);
        }
    }
    IEnumerator InstructMuhomor()                       //запуск сообщения об отравлении
    {
        if (lifeScript.life >= 1)
        {
            muhomorInstr.SetActive(true);
            yield return new WaitForSeconds(3);
            muhomorInstr.SetActive(false);
        }
    }
    IEnumerator DotContr()                            //запуск сообщения о новой точке сохранения
    {
        dotInstr.SetActive(true);
        yield return new WaitForSeconds(3);
        dotInstr.SetActive(false);
    }
}
