using System.Collections;
using UnityEngine;

namespace Levels
{
    //public enum FruitAndMushrom { Muhomor, Water, Poganka, Mushroom, Fruit, SuperMushroom, DotSave1, DotSave2 };
    public class ContactScript : MonoBehaviour
    {      
        [SerializeField] private LifeScript _lifeScript;      
        [SerializeField] private GameObject _dotInstr;  
        [SerializeField] private GameObject _winExp;
      
        void OnTriggerEnter2D(Collider2D otherCol)
        {
           BaseContact contact = otherCol.gameObject.GetComponent<BaseContact>();

                if (contact != null)
                {
                    contact.Contact();
                    Destroy(otherCol.gameObject);
                }            


            if (otherCol.gameObject.tag == "save1")                  //������� � ������ ����������1
            {
                if (otherCol.transform.position.x > _lifeScript.spawnPlayerCurent.x)                                                 //������ ���� ����� ���������� ������ ������� �� �
                {
                    StartCoroutine(DotContr());
                    _lifeScript.spawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
                }
            }

            if (otherCol.gameObject.tag == "save2")                  //������� � ������ ����������2
            {
                if (otherCol.transform.position.x > _lifeScript.spawnPlayerCurent.x)
                {
                    StartCoroutine(DotContr());
                    _lifeScript.spawnPlayerCurent = new Vector3(otherCol.transform.position.x, otherCol.transform.position.y, 0);
                }
            }
        }
              
        IEnumerator DotContr()                            //������ ��������� � ����� ����� ����������
        {
            _dotInstr.SetActive(true);
            yield return new WaitForSeconds(3);
            _dotInstr.SetActive(false);
        }
    }
}
