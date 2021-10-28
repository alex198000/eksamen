using System;
using UnityEngine;

namespace Levels
{
    public class EggScript : MonoBehaviour
    {
        public static event Action OnPlayerEggContact;
        private void OnCollisionEnter2D(Collision2D coll)
        {
            HealthScript healthScript = coll.gameObject.GetComponent<HealthScript>();

            if (healthScript != null)                                //контакт с плеером
            {
                OnPlayerEggContact?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}