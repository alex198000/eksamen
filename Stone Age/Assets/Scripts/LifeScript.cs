using System.Collections;
using UnityEngine;

namespace Levels
{
    public class LifeScript : MonoBehaviour
    {
        [SerializeField] private LifeBar lifeBar;
        [SerializeField] private int life;
        [SerializeField] private int lifeMax = 11;
        [SerializeField] private Vector3 spawnPlayer;
        [SerializeField] private Vector3 spawnPlayerCurent;
        [SerializeField] private HungerManager _hungerManager;
        [SerializeField] private GameEvent _lifeEvent;
        [SerializeField] private SceneDrive _sceneDrive;
        [SerializeField] private HealthScript _healthScript;
        [SerializeField] private GameObject _loseGame;
        [SerializeField] private GameObject _defExp;
        [SerializeField] private GameObject _lifeInstr;

        public int LifeMax { get => lifeMax; set => lifeMax = value; }
        public int Life { get => life; set => life = value; }
        public LifeBar LifeBar { get => lifeBar; set => lifeBar = value; }
        public Vector3 SpawnPlayer { get => spawnPlayer; set => spawnPlayer = value; }
        public Vector3 SpawnPlayerCurent { get => spawnPlayerCurent; set => spawnPlayerCurent = value; }

        void Start()
        {
            life = lifeMax;
            lifeBar.SetMaxLife(lifeMax);
            _hungerManager.Life = lifeMax;
            spawnPlayer = gameObject.transform.position;     //начальная точка спавна
            spawnPlayerCurent = spawnPlayer;
            _sceneDrive.UpdateScore();
        }
        public void LastLife()                           // последняя жизнь
        {
            spawnPlayerCurent = spawnPlayer;
            LifeDamage(1);
            _hungerManager.LifeVal(1);
            gameObject.SetActive(false);
            _lifeEvent.Raise();
            lifeBar.SetLife(life);
            _healthScript.Hp = 0;
            _hungerManager.Hunger = _healthScript.Hp;
            _healthScript.HealthBar.SetHealth(_healthScript.Hp);
            GameObject def = Instantiate(_defExp, transform.position, transform.rotation);
            Destroy(def, 5f);
            _sceneDrive.UpdateScore();                             // обновляем юай здоровья
        }
        public void LifeDamage(int lifeDamage)                        //отнимание жизни
        {
            life -= lifeDamage;
            lifeBar.SetLife(life);
            if (life < 1)
            {
                _loseGame.SetActive(true);
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
                _lifeInstr.SetActive(true);
                yield return new WaitForSeconds(2f);
                _lifeInstr.SetActive(false);
            }
        }
    }
}
