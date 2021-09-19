using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonProperty", menuName = "ScriptableObjects/NewSkeletonProperty")]
public class SkeletonProperty : ScriptableObject
{

    [SerializeField] private float skeletonSpeed;
    [SerializeField] private Vector3 skeletonScale;
    [SerializeField] private Material skeletonColor;
    //[SerializeField] private Quaternion skeletonRotate;      //поворот спрайта



    public float SpeedSkeleton
    {
        get
        {
           return skeletonSpeed;
        }
        
    }
    public Vector3 ScaleSkeleton
    {
        get
        {
            return  skeletonScale;
        }
       
    }
    public Material SkeletonColor
    {
        get
        {
            return skeletonColor;
        }

    }
    //public Quaternion SkeletonRotate
    //{
    //    get
    //    {
    //        return skeletonRotate;
    //    }
    //}

}


