using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "SceletonManager", menuName = "ScriptableObjects/NewSceletonManager")]
    public class SceletonManager : ScriptableObject
    {
        [SerializeField] private int _sceletonPlus;
        [SerializeField] private int _sceletonMinus;

        public int SceletonPlus { get { return _sceletonPlus; } set { _sceletonPlus = value; } }
        public int SceletonMinus { get { return _sceletonMinus; } set { _sceletonMinus = value; } }

        private void OnEnable()
        {
            SceletonPlus = 0;
            SceletonMinus = 0;
        }
    }
}


