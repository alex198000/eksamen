using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "SkeletonProperty", menuName = "ScriptableObjects/NewSkeletonProperty")]
    public class SkeletonProperty : ScriptableObject
    {

        [SerializeField] private float _skeletonSpeed;
        [SerializeField] private Vector3 _skeletonScale;
        [SerializeField] private Material _skeletonColor;
        //[SerializeField] private Quaternion _skeletonRotate;      //поворот спрайта
        [SerializeField] private Sprite _skeletonSprite;
        [SerializeField] private int _skeletonHealth;
        [SerializeField] private Vector2 _skeletonColl;

        public float SpeedSkeleton
        {
            get
            {
                return _skeletonSpeed;
            }

        }
        public Vector3 ScaleSkeleton
        {
            get
            {
                return _skeletonScale;
            }

        }
        public Material SkeletonColor
        {
            get
            {
                return _skeletonColor;
            }

        }

        public Sprite SkeletonSprite
        {
            get
            {
                return _skeletonSprite;
            }

        }

        public int SkeletonHealth
        {
            get
            {
                return _skeletonHealth;
            }

        }

        public Vector2 SkeletonColl
        {
            get
            {
                return _skeletonColl;
            }
        }
        //public Quaternion SkeletonRotate
        //{
        //    get
        //    {
        //        return _skeletonRotate;
        //    }
        //}

    }
}

