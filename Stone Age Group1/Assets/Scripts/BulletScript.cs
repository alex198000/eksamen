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

    private void Start()
    {
        rb.velocity = transform.right * speedBullet * Time.deltaTime;
    }
    //private void FixedUpdate()                                                        //если не корректно работает из старта
    //{
    //    rb.velocity = transform.right * speedBullet * Time.fixedDeltaTime;
    //}

    private void OnCollisionEnter2D(Collision2D col)
    {
        SkeletonMoove skeletonMoove = col.gameObject.GetComponent<SkeletonMoove>();

        if (skeletonMoove != null)
        {
            gameObject.SetActive(false);
            GameObject effectShot = Instantiate(explosionShot, transform.position, transform.rotation);
            Destroy(effectShot, 2f);
            col.gameObject.SetActive(false);
            scoreManager.ScoreVal(100);
            scoreManager.RecordVal();
            recordEvent.Raise();
            scoreEvent.Raise();

        }
    }
}
