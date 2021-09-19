using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    [SerializeField] private string skeletonTag;
    [SerializeField] private Vector3 spawnPointPosition;
    [SerializeField] private Vector3 dist;
    [SerializeField] private float spawnRate;
    [SerializeField] private float waveRate;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform SkeletonManager;
    [SerializeField] private List<SkeletonProperty> skeletonProperties;
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private int waveCount;
    [SerializeField] private int skeletonCount;


    void Start()
    {
        
        StartCoroutine(SpawnSkeleton());
    }
    private void Update()
    {
        dist = player.transform.position;                                     //постоянно меняется точка спавна скелетов
    }

    void CreateSkeleton()
    {
        spawnPointPosition = new Vector3(dist.x +6, dist.y +8, dist.z);                                     //спавн на опр расстоянии по х у z
        GameObject skeleton = ObjectPooler.objectPooler.GetPooledObject(skeletonTag);
        if (skeleton != null)
        {
            
        

        skeleton.transform.position = spawnPointPosition;
       
        skeleton.SetActive(true);
        skeleton.transform.SetParent(SkeletonManager);

        int randomSheepPropertyIndex = Random.Range(0, skeletonProperties.Count);

        skeleton.GetComponent<SkeletonMoove>().SetPropertyToSkeleton(skeletonProperties[randomSheepPropertyIndex]);
    }
    }


    IEnumerator SpawnSkeleton()
    {
        yield return new WaitForSeconds(spawnRate);      //ожидание перед волной
        while (waveCount > 0)
        {
            for (int i = 0; i < skeletonCount; i++)
            {
                yield return new WaitForSeconds(spawnRate);
                CreateSkeleton();
            }

            //skeletonCount += 5; //увеличиваем число спавнящихся скелетов



            waveCount--;                                      //уменьшаем кол во волн
            yield return new WaitForSeconds(waveRate);        // ожидание перед сл волной

        }
    }
}
