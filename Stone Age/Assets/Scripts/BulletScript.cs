using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject explosionShot;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameEvent scoreEvent;
    [SerializeField] private GameEvent recordEvent;

    public int damageStone = 1;
    private void OnEnable()
    {
        rb.AddForce(transform.right * speedBullet);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        SkeletonMoove skeletonMoove = col.gameObject.GetComponent<SkeletonMoove>();

        if (skeletonMoove != null)
        {
            gameObject.SetActive(false);
            GameObject effectShot = Instantiate(explosionShot, transform.position, transform.rotation);
            Destroy(effectShot, 2f);
        }
    }
}
