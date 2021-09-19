using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoove : MonoBehaviour
{
    
    [SerializeField] private Vector3 diraction;
    private Rigidbody rb;
    private MeshRenderer mr;
    [SerializeField] private GameObject skeletonEffect;
    [SerializeField] private GameObject skeletonWin;
    [SerializeField] private SkeletonProperty skeletonProperty;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameEvent hungerEvent;
    [SerializeField] private GameEvent lifeEvent;
    [SerializeField] private GameEvent scoreEvent;
    [SerializeField] private GameEvent recordEvent;


    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        transform.Translate(diraction * skeletonProperty.SpeedSkeleton * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        BulletScript bulletScript = col.gameObject.GetComponent<BulletScript>();
        HealthScript healthScript = col.gameObject.GetComponent<HealthScript>();
        

        if (bulletScript != null)                         // столкновение с камнем
        {
            gameObject.SetActive(false);
            GameObject effectShot = Instantiate(skeletonEffect, transform.position, transform.rotation);
            Destroy(effectShot, 2f);



        }
       


        if (healthScript != null)                         // столкновение с player
        {

            GameObject effectShot = Instantiate(skeletonWin, transform.position, transform.rotation);
            Destroy(effectShot, 2f);
            gameObject.SetActive(false);

            if (healthScript.life > 1)                //используем поля healthScript
            {
                col.gameObject.transform.position = healthScript.spawnPlayerCurent;

                healthScript.LifeDamage(1);
                scoreManager.LifeVal(1);
                lifeEvent.Raise();
                healthScript.lifeBar.SetLife(healthScript.life);
                healthScript.hp = healthScript.hpMax;
                healthScript.healthBar.SetHealth(healthScript.hp);
                scoreManager.Hunger = healthScript.hpMax;
                //skeletonRec.GetComponent<SceneDrive>().UpdateScore();
            }
            else
            {
                healthScript.LastLife();
            }
        }
    }

    public void SetPropertyToSkeleton(SkeletonProperty skeletonProperty)
    {
        this.skeletonProperty = skeletonProperty;
        transform.localScale = new Vector3(skeletonProperty.ScaleSkeleton.x, skeletonProperty.ScaleSkeleton.y, skeletonProperty.ScaleSkeleton.z);
        //mr.material = skeletonProperty.SkeletonColor;

        //transform.rotation = new Quaternion(skeletonProperty.SkeletonRotate.x, skeletonProperty.SkeletonRotate.y,skeletonProperty.SkeletonRotate.z, skeletonProperty.SkeletonRotate.w);
    }
}
