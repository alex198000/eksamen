using System.Collections;
using UnityEngine;

namespace Levels
{
    public class WaterScript : BaseContact
    {
        [SerializeField] private GameObject _loseInstr;
        [SerializeField] private GameObject _player;
        public override void Contact()
        {
            if (_lifeScript.life > 1)
            {
                _player.transform.position = _lifeScript.spawnPlayerCurent;
                StartCoroutine(InstrWeater());
                _lifeScript.LifeDamage(1);
                _hungerManager.LifeVal(1);
                _lifeEvent.Raise();
                _lifeScript.lifeBar.SetLife(_lifeScript.life);
                _healthScript.Hp = _healthScript.HpMax;
                _healthScript.healthBar.SetHealth(_healthScript.Hp);
                _hungerManager.Hunger = _healthScript.HpMax;
                _sceneDrive.UpdateScore();
            }
            else
            {
                _lifeScript.LastLife();
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
    }
}
