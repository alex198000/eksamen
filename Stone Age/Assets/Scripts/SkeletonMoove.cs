using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoove : MonoBehaviour
{
    
    [SerializeField] private Vector3 diraction;
    private Rigidbody rb;
    private MeshRenderer mr;
    [SerializeField] private SpriteRenderer spriteSkeleton;
    [SerializeField] private SkeletonHealth skeletonHealth;
    [SerializeField] private BoxCollider2D colider;   
    [SerializeField] private SkeletonProperty skeletonProperty;  
    

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        colider = GetComponent<BoxCollider2D>();
        spriteSkeleton = GetComponent<SpriteRenderer>();
        skeletonHealth = GetComponent<SkeletonHealth>();
        
    }

    void Update()
    {
        transform.Translate(diraction * skeletonProperty.SpeedSkeleton * Time.deltaTime);
    }
    
    

    

        public void SetPropertyToSkeleton(SkeletonProperty skeletonProperty)
    {
        this.skeletonProperty = skeletonProperty;
        transform.localScale = new Vector3(skeletonProperty.ScaleSkeleton.x, skeletonProperty.ScaleSkeleton.y, skeletonProperty.ScaleSkeleton.z);
        skeletonHealth.hpMaxSkelet = skeletonProperty.SkeletonHealth;
        spriteSkeleton.sprite = skeletonProperty.SkeletonSprite;
        colider.size = skeletonProperty.SkeletonColl;
        //mr.material = skeletonProperty.SkeletonColor;

        //transform.rotation = new Quaternion(skeletonProperty.SkeletonRotate.x, skeletonProperty.SkeletonRotate.y,skeletonProperty.SkeletonRotate.z, skeletonProperty.SkeletonRotate.w);
    }
}
