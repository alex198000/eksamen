using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    [SerializeField] Vector3 rotateDirection;
    [Range(-1000f, 1000f)] [SerializeField] float rotateSpeed = 1.5f;
   
    void Update()
    {
        transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);
    }
}
