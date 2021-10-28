using System.Collections;
using UnityEngine;

namespace Levels
{
    public class ContactScript : MonoBehaviour
    {      
        [SerializeField] private LifeScript _lifeScript;      
        [SerializeField] private GameObject _dotInstr;  
        [SerializeField] private GameObject _winExp;
        [SerializeField] protected GameObject _effectEat;
        [SerializeField] protected ScoreManager _scoreManager;
        [SerializeField] protected HealthScript _healthScript;
        [SerializeField] protected GameEvent _recordEvent;

        private void OnEnable()
        {
            EggScript.OnPlayerEggContact += ContactEgg;
        }

        private void OnDisable()
        {
            EggScript.OnPlayerEggContact -= ContactEgg;
        }
        void OnTriggerEnter2D(Collider2D otherCol)
        {
           BaseContact contact = otherCol.gameObject.GetComponent<BaseContact>();

                if (contact != null)
                {
                    contact.Contact();
                    //Destroy(otherCol.gameObject);
                }            


            if (otherCol.gameObject.tag == "save1")                  //контакт с точкой сохранения1
            {
                if (otherCol.transform.position.x > _lifeScript.SpawnPlayerCurent.x)                                                 //толькр если точка сохранения больше текущей по х
                {
                    StartCoroutine(DotContr());
                    _lifeScript.SpawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
                }
            }

            if (otherCol.gameObject.tag == "save2")                  //контакт с точкой сохранения2
            {
                if (otherCol.transform.position.x > _lifeScript.SpawnPlayerCurent.x)
                {
                    StartCoroutine(DotContr());
                    _lifeScript.SpawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
                }
            }
        }

        private void ContactEgg()
        {
            GameObject eggEffect = Instantiate(_effectEat, transform.position, transform.rotation);
            Destroy(eggEffect, 5f);
            _healthScript.PlusDamage(10, 20);
            _healthScript.HealthBar.SetHealth(_healthScript.Hp);
            _scoreManager.RecordVal();
            _recordEvent.Raise();
          
            if (_healthScript.Hp > _healthScript.HpMax)
            {
                _healthScript.HpMax = _healthScript.Hp;
                _healthScript.HealthBar.SetMaxHealth(_healthScript.HpMax);
            }
        }
              
        IEnumerator DotContr()                            //запуск сообщения о новой точке сохранения
        {
            _dotInstr.SetActive(true);
            yield return new WaitForSeconds(2f);
            _dotInstr.SetActive(false);
        }
    }
}
