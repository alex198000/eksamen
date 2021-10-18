using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class SkeletonSpawner : MonoBehaviour
    {
        [SerializeField] private string skeletonTag;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform SkeletonManager;
        [SerializeField] private List<SkeletonProperty> skeletonProperties;
        [SerializeField] private HealthScript healthScript;
        [SerializeField] private int waveCount;
        [SerializeField] private int skeletonCount;
        [SerializeField] private float spawnRate;
        [SerializeField] private float waveRate;
        [SerializeField] private Vector3 spawnPointPosition;
        [SerializeField] private Vector3 dist;


        void Start()
        {
            StartCoroutine(SpawnSkeleton());
        }
        private void Update()
        {
            dist = player.transform.position;                                     //��������� �������� ����� ������ ��������
        }

        void CreateSkeleton()
        {
            spawnPointPosition = new Vector3(dist.x + 6, dist.y + 8, dist.z);                                     //����� �� ��� ���������� �� � � z
            GameObject skeleton = ObjectPooler.objectPooler.GetPooledObject(skeletonTag);
            if (skeleton != null)
            {
                skeleton.transform.position = spawnPointPosition;

                skeleton.SetActive(true);
                skeleton.transform.SetParent(SkeletonManager);

                int randomSkeletonPropertyIndex = Random.Range(0, skeletonProperties.Count);

                skeleton.GetComponent<SkeletonMoove>().SetPropertyToSkeleton(skeletonProperties[randomSkeletonPropertyIndex]);
            }
        }


        IEnumerator SpawnSkeleton()
        {
            yield return new WaitForSeconds(spawnRate);      //�������� ����� ������
            while (waveCount > 0)
            {
                for (int i = 0; i < skeletonCount; i++)
                {
                    yield return new WaitForSeconds(spawnRate);
                    CreateSkeleton();
                }

                skeletonCount += 2; //����������� ����� ����������� ��������



                waveCount--;                                      //��������� ��� �� ����
                yield return new WaitForSeconds(waveRate);        // �������� ����� �� ������

            }
        }
    }
}
