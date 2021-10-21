using System.Collections;
using UnityEngine;

namespace Levels
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float fireRate;
        [SerializeField] private float nextFire;
        [SerializeField] private Transform bulletManager;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D col;
        [SerializeField] private string bulletTag;
        private void FixedUpdate()
        {
            nextFire -= Time.deltaTime;
        }
        public void Dubin()
        {
            StartCoroutine(DobinCollider());
        }
        public void Fire()
        {
            if (nextFire < 0)
            {
                animator.SetTrigger("Fire");
                GameObject bullet = ObjectPooler.objectPooler.GetPooledObject(bulletTag);
                if (bullet != null)
                {
                    bullet.transform.position = spawnPoint.position;
                    bullet.transform.rotation = spawnPoint.rotation;
                    bullet.SetActive(true);
                    bullet.transform.SetParent(bulletManager);

                    nextFire = fireRate;
                }
            }
        }
        IEnumerator DobinCollider()
        {
            animator.SetTrigger("Atak");
            col.enabled = true;
            yield return new WaitForSeconds(1);
            col.enabled = false;
        }
    }
}