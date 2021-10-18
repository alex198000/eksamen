using System.Collections;
using UnityEngine;

namespace Levels
{
    public enum FruitAndMushrom { Muhomor, Water, Poganka, Mushroom, Fruit, SuperMushroom, DotSave1, DotSave2 };
    public class ContactScript : MonoBehaviour
    {
        [SerializeField] private HealthScript _healthScript;
        [SerializeField] private HungerManager _hungerManager;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private SceneDrive _sceneDrive;
        [SerializeField] private LifeScript _lifeScript;
        [SerializeField] private GameEvent _lifeEvent;
        [SerializeField] private GameEvent _recordEvent;
        [SerializeField] private GameObject _danger;
        [SerializeField] private GameObject _muhomorInstr;
        [SerializeField] private GameObject _dotInstr;
        [SerializeField] private GameObject _loseInstr;
        [SerializeField] private GameObject _winGame;
        [SerializeField] private GameObject _winExp;
        [SerializeField] private GameObject _eatFruit;
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
                if (_lifeScript.life > 1)
                {
                    GameObject dangerous = Instantiate(_danger, transform.position, transform.rotation);
                    Destroy(dangerous, 5f);
                    gameObject.transform.position = _lifeScript.spawnPlayerCurent;
                    StartCoroutine(InstructMuhomor());
                    _lifeScript.LifeDamage(1);
                    _hungerManager.LifeVal(1);
                    _lifeEvent.Raise();
                    _lifeScript.lifeBar.SetLife(_lifeScript.life);
                    _healthScript.hp = _healthScript.hpMax;
                    _healthScript.healthBar.SetHealth(_healthScript.hp);
                    _hungerManager.Hunger = _healthScript.hpMax;
                    _sceneDrive.UpdateScore();
                }
                else
                {
                    _lifeScript.LastLife();
                }
            }

            if (waterScript != null)                                //контакт с водой
            {
                fruitAndMushrom = FruitAndMushrom.Water;
                if (_lifeScript.life > 1)
                {
                    gameObject.transform.position = _lifeScript.spawnPlayerCurent;
                    StartCoroutine(InstrWeater());
                    _lifeScript.LifeDamage(1);
                    _hungerManager.LifeVal(1);
                    _lifeEvent.Raise();
                    _lifeScript.lifeBar.SetLife(_lifeScript.life);
                    _healthScript.hp = _healthScript.hpMax;
                    _healthScript.healthBar.SetHealth(_healthScript.hp);
                    _hungerManager.Hunger = _healthScript.hpMax;
                    _sceneDrive.UpdateScore();
                }
                else
                {
                    _lifeScript.LastLife();
                }
            }

            if (pogankaScript != null)                     //контакт с поганкой, ущерб
            {
                fruitAndMushrom = FruitAndMushrom.Poganka;
                _healthScript.TakeDamage(10);
                _healthScript.healthBar.SetHealth(_healthScript.hp);
                Destroy(otherCol.gameObject);
                GameObject dangerous = Instantiate(_danger, transform.position, transform.rotation);
                Destroy(dangerous, 5f);
            }

            if (mushroomScript != null)                 //контакт с полезным грибом
            {
                fruitAndMushrom = FruitAndMushrom.Mushroom;
                GameObject fru = Instantiate(_eatFruit, transform.position, transform.rotation);
                Destroy(fru, 5f);
                _healthScript.PlusDamage(10);
                _healthScript.healthBar.SetHealth(_healthScript.hp);
                _scoreManager.RecordVal();
                _recordEvent.Raise();
                Destroy(otherCol.gameObject);
                if (_healthScript.hp > _healthScript.hpMax)
                {
                    _healthScript.hpMax = _healthScript.hp;
                    _healthScript.healthBar.SetMaxHealth(_healthScript.hpMax);
                }
            }

            if (fruitScript != null)                  //контакт с фруктами
            {
                fruitAndMushrom = FruitAndMushrom.Fruit;
                GameObject fru = Instantiate(_eatFruit, transform.position, transform.rotation);
                Destroy(fru, 5f);
                _healthScript.PlusDamage(20);
                _healthScript.healthBar.SetHealth(_healthScript.hp);
                Destroy(otherCol.gameObject);
                _scoreManager.RecordVal();
                _recordEvent.Raise();

                if (_healthScript.hp > _healthScript.hpMax)
                {
                    _healthScript.hpMax = _healthScript.hp;
                    _healthScript.healthBar.SetMaxHealth(_healthScript.hpMax);
                }
            }

            if (superMushroomScript != null)                       // контакт с супер грибом для перехода к новому уровню
            {
                fruitAndMushrom = FruitAndMushrom.SuperMushroom;
                GameObject win = Instantiate(_winExp, transform.position, transform.rotation);
                Destroy(win, 5f);
                _winGame.SetActive(true);
                gameObject.SetActive(false);
                _lifeScript.spawnPlayer = new Vector3(-2, -2, 0);

                if (PlayerPrefs.GetInt("LevelSave") < _sceneDrive.unlockLevel)
                {
                    PlayerPrefs.SetInt("LevelSave", _sceneDrive.unlockLevel);
                }

                _healthScript.PlusDamage(100);
                _scoreManager.RecordVal();
                _recordEvent.Raise();
                _healthScript.healthBar.SetHealth(_healthScript.hp);
                Destroy(otherCol.gameObject);

                if (_healthScript.hp > _healthScript.hpMax)
                {
                    _healthScript.hpMax = _healthScript.hp;
                    _healthScript.healthBar.SetMaxHealth(_healthScript.hpMax);
                }
            }

            if (otherCol.gameObject.tag == "save1")                  //контакт с точкой сохранения1
            {
                fruitAndMushrom = FruitAndMushrom.DotSave1;
                if (otherCol.transform.position.x > _lifeScript.spawnPlayerCurent.x)                                                 //толькр если точка сохранения больше текущей по х
                {
                    StartCoroutine(DotContr());
                    _lifeScript.spawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
                    //skeletonSpawner.spawnPointPosition = new Vector3;
                }
            }
            if (otherCol.gameObject.tag == "save2")                  //контакт с точкой сохранения2
            {
                fruitAndMushrom = FruitAndMushrom.DotSave2;
                if (otherCol.transform.position.x > _lifeScript.spawnPlayerCurent.x)
                {
                    StartCoroutine(DotContr());
                    _lifeScript.spawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
                }
            }
        }
        IEnumerator InstrWeater()                        //запуск сообщения об утонувшем герое
        {
            if (_lifeScript.life >= 1)
            {
                _loseInstr.SetActive(true);
                yield return new WaitForSeconds(3);
                _loseInstr.SetActive(false);
            }
        }
        IEnumerator InstructMuhomor()                       //запуск сообщения об отравлении
        {
            if (_lifeScript.life >= 1)
            {
                _muhomorInstr.SetActive(true);
                yield return new WaitForSeconds(3);
                _muhomorInstr.SetActive(false);
            }
        }
        IEnumerator DotContr()                            //запуск сообщения о новой точке сохранения
        {
            _dotInstr.SetActive(true);
            yield return new WaitForSeconds(3);
            _dotInstr.SetActive(false);
        }
    }
}
