using UnityEngine;

namespace Levels
{
    public abstract class BaseEat : MonoBehaviour
    {
        [SerializeField] protected int bonus;
        [SerializeField] protected GameObject effect;
    }
}