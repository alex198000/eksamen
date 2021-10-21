using UnityEngine;

namespace Levels
{
    public class SkeletonMoove : MonoBehaviour
    {
        private Rigidbody _rb;
        private MeshRenderer _mr;
        [SerializeField] private SpriteRenderer _spriteSkeleton;
        [SerializeField] private SkeletonHealth _skeletonHealth;
        [SerializeField] private BoxCollider2D _colider;
        [SerializeField] private SkeletonProperty _skeletonProperty;
        [SerializeField] private Vector3 _diraction;


        private void Awake()
        {
            _mr = GetComponent<MeshRenderer>();
            _rb = GetComponent<Rigidbody>();
            _colider = GetComponent<BoxCollider2D>();
            _spriteSkeleton = GetComponent<SpriteRenderer>();
            _skeletonHealth = GetComponent<SkeletonHealth>();
        }

        void Update()
        {
            transform.Translate(_diraction * _skeletonProperty.SpeedSkeleton * Time.deltaTime);
        }

        public void SetPropertyToSkeleton(SkeletonProperty skeletonProperty)
        {
            this._skeletonProperty = skeletonProperty;
            transform.localScale = new Vector3(skeletonProperty.ScaleSkeleton.x, skeletonProperty.ScaleSkeleton.y, skeletonProperty.ScaleSkeleton.z);
            _skeletonHealth.HpMaxSkelet = skeletonProperty.SkeletonHealth;
            _skeletonHealth.HpSkelet = _skeletonHealth.HpMaxSkelet;
            _spriteSkeleton.sprite = skeletonProperty.SkeletonSprite;
            _colider.size = skeletonProperty.SkeletonColl;
            //_mr.material = _skeletonProperty.SkeletonColor;

            //transform.rotation = new Quaternion(_skeletonProperty.SkeletonRotate.x, _skeletonProperty.SkeletonRotate.y, _skeletonProperty.SkeletonRotate.z, _skeletonProperty.SkeletonRotate.w);
        }
    }
}
