using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private GameObject bonusInstr;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosionDelete;
    [SerializeField] private GameObject bonus;
    private int instrCount= 1;
    private void Awake()
    {
        
    }
    void OnTriggerEnter2D(Collider2D otherCol)                                //падают листики, появляется бонус и инструкция как его получить
    {
        if (otherCol.gameObject.tag == "Weapon")
        {
            GetComponent<Animation>().Play();
            anim.Play();

            explosionDelete = Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(bonus, transform.position, transform.rotation);
            
            Destroy(explosionDelete, 7f);
           StartCoroutine(InstrBonus());
            
        }
    }
    void OnTriggerExit2D(Collider2D otherCol)                             //подстраховка для дубины
    {
        if (otherCol.gameObject.tag == "Weapon")
        {
            otherCol.GetComponent<Collider2D>().enabled = false;

        }
    }
    IEnumerator InstrBonus()
    {
        
    while (instrCount > 0)
    {

            bonusInstr.SetActive(true);
            yield return new WaitForSeconds(4);
            bonusInstr.SetActive(false);
            instrCount--;
        }
    }
}
