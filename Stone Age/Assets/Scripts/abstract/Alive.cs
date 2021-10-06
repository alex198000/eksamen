using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Alive : MonoBehaviour
{
    [SerializeField] protected int Health;
    public abstract void Die();
}
