using UnityEngine;

namespace Levels
{
    public class WoodScript : MonoBehaviour
    {
        [SerializeField] private Animation _anim;
        //[SerializeField] private GameObject _bonusInstr;
        [SerializeField] private GameObject _explosion;
        [SerializeField] private GameObject _explosionDelete;
        [SerializeField] private GameObject _bonus;
        //private int _instrCount = 1;
       
        void OnTriggerEnter2D(Collider2D otherCol)                                //падают листики, появляется бонус 
        {
            if (otherCol.gameObject.tag == "Weapon")
            {
                GetComponent<Animation>().Play();
                _anim.Play();

                _explosionDelete = Instantiate(_explosion, transform.position, transform.rotation);
                Instantiate(_bonus, transform.position, transform.rotation);

                Destroy(_explosionDelete, 7f);
                //StartCoroutine(InstrBonus());

            }
        }
        void OnTriggerExit2D(Collider2D otherCol)                             //подстраховка для дубины
        {
            if (otherCol.gameObject.tag == "Weapon")
            {
                otherCol.GetComponent<Collider2D>().enabled = false;

            }
        }
        //IEnumerator InstrBonus()
        //{

        //    while (_instrCount > 0)
        //    {

        //        _bonusInstr.SetActive(true);
        //        yield return new WaitForSeconds(4);
        //        _bonusInstr.SetActive(false);
        //        _instrCount--;
        //    }
        //}
    }
}