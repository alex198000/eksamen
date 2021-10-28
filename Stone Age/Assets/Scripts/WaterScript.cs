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
            if (_lifeScript.Life > 1)
            {
                _player.transform.position = _lifeScript.SpawnPlayerCurent;
                StartCoroutine(InstrWeater());
                _lifeScript.LifeDamage(1);
                _hungerManager.LifeVal(1);
                _lifeEvent.Raise();
                _lifeScript.LifeBar.SetLife(_lifeScript.Life);
                _healthScript.Hp = _healthScript.HpMax;
                _healthScript.HealthBar.SetHealth(_healthScript.Hp);
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
            if (_lifeScript.Life >= 1)
            {
                _loseInstr.SetActive(true);
                yield return new WaitForSeconds(2f);
                _loseInstr.SetActive(false);
            }
        }
    }
}
