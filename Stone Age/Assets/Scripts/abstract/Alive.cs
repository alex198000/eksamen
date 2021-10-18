using UnityEngine;

namespace Levels
{
    public abstract class Alive : MonoBehaviour
    {
        [SerializeField] protected int _health;
        public abstract void Die();
    }
}