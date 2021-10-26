using System.Collections;
using UnityEngine;

namespace Levels
{
    public class MuhomorScript : BaseContact
    {
        [SerializeField] private GameObject _muhomorInstr;
        [SerializeField] private GameObject _player;
        public override void Contact()
        {
            if (_lifeScript.life > 1)
            {
                GameObject dangerous = Instantiate(_effect, transform.position, transform.rotation);
                Destroy(dangerous, 5f);
                _player.transform.position = _lifeScript.spawnPlayerCurent;
                StartCoroutine(InstructMuhomor());
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

        IEnumerator InstructMuhomor()                       //запуск сообщения об отравлении
        {
            if (_lifeScript.life >= 1)
            {
                _muhomorInstr.SetActive(true);
                yield return new WaitForSeconds(3);
                _muhomorInstr.SetActive(false);
            }
        }
    }
}