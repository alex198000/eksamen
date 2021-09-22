using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class MyPlayerMovement : MonoBehaviour
{
    [Header("Player Property")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpForce;
    [SerializeField] private GameObject animObject;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private float currentPlayerSpeed;
    private bool groundCheck;    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    
    }
    private void Update()
    {
        //MooveBut();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(currentPlayerSpeed * Time.deltaTime, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(currentPlayerSpeed));

        //nextFire -= Time.deltaTime;
    }

    public void RightMove()
    {
        currentPlayerSpeed = playerSpeed;
        //transform.Rotate(0f, 0f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void LeftMove()
    {
        currentPlayerSpeed = -playerSpeed;
        // transform.Rotate(0f, 180f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

   public void StopMove()
    {
        currentPlayerSpeed = 0f;
    }
    public void Jump()
    {
        if (groundCheck)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, playerJumpForce);
            groundCheck = false;
        }
    }

    //public void Dubin()
    //{
    //    StartCoroutine(DobinCollider());
    //}

    //public void Fire()
    //{


    //    if (nextFire < 0)
    //    {
    //        animator.SetTrigger("Fire");
    //        GameObject bullet = ObjectPooler.objectPooler.GetPooledObject(bulletTag);
    //        if (bullet != null)
    //        {
    //            bullet.transform.position = spawnPoint.position;
    //            bullet.transform.rotation = spawnPoint.rotation;
    //            bullet.SetActive(true);            
    //            bullet.transform.SetParent(bulletManager);
            
    //        nextFire = fireRate;
    //        }
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        groundCheck = true;
    }

    //public void MooveBut()
    //{
    //    if (Input.GetButton("Jump"))
    //    {
    //        Jump();
    //    }
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        LeftMove();
    //    }
    //    if (Input.GetKeyUp(KeyCode.A))
    //    {
    //        StopMove();
    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        RightMove();
    //    }
    //    if (Input.GetKeyUp(KeyCode.D))
    //    {
    //        StopMove();
    //    }
    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        Dubin();
    //    }
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        Fire();
    //    }
    //}
    //IEnumerator DobinCollider()                            //запуск сообщения о потери жизни
    //{
    //    animator.SetTrigger("Atak");
    //    col.enabled = true;
    //    yield return new WaitForSeconds(1);
    //    col.enabled = false;
    //}
}





