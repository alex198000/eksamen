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
            if (_lifeScript.Life > 1)
            {
                GameObject dangerous = Instantiate(_effect, transform.position, transform.rotation);
                Destroy(dangerous, 5f);
                _player.transform.position = _lifeScript.SpawnPlayerCurent;                
                StartCoroutine(InstructMuhomor());               
                _lifeScript.LifeDamage(1);
                _hungerManager.LifeVal(1);
                _lifeEvent.Raise();
                _lifeScript.LifeBar.SetLife(_lifeScript.Life);
                _healthScript.Hp = _healthScript.HpMax;
                _healthScript.HealthBar.SetHealth(_healthScript.Hp);
                _hungerManager.Hunger = _healthScript.HpMax;
                //gameObject.SetActive(false);        //при активации корутина не отрыбытывает до конца
                _sceneDrive.UpdateScore();
            }
            else
            {
                _lifeScript.LastLife();
            }
        }

        IEnumerator InstructMuhomor()                       //запуск сообщения об отравлении
        {
            if (_lifeScript.Life >= 1)
            {
                _muhomorInstr.SetActive(true);
                yield return new WaitForSeconds(2f);
                _muhomorInstr.SetActive(false);
            }
        }
    }
}