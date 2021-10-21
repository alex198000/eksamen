using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class SkeletonSpawner : MonoBehaviour
    {
        [SerializeField] private string _skeletonTag;
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _SkeletonManager;
        [SerializeField] private List<SkeletonProperty> _skeletonProperties;
        [SerializeField] private HealthScript _healthScript;
        [SerializeField] private int _waveCount;
        [SerializeField] private int _skeletonCount;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _waveRate;
        [SerializeField] private Vector3 _spawnPointPosition;
        [SerializeField] private Vector3 _dist;


        void Start()
        {
            StartCoroutine(SpawnSkeleton());
        }
        private void Update()
        {
            _dist = _player.transform.position;                                     //постоянно меняется точка спавна скелетов
        }

        void CreateSkeleton()
        {
            _spawnPointPosition = new Vector3(_dist.x + 6, _dist.y + 8, _dist.z);                                     //спавн на опр расстоянии по х у z
            GameObject skeleton = ObjectPooler.objectPooler.GetPooledObject(_skeletonTag);
            if (skeleton != null)
            {
                skeleton.transform.position = _spawnPointPosition;

                skeleton.SetActive(true);
                skeleton.transform.SetParent(_SkeletonManager);

                int randomSkeletonPropertyIndex = Random.Range(0, _skeletonProperties.Count);

                skeleton.GetComponent<SkeletonMoove>().SetPropertyToSkeleton(_skeletonProperties[randomSkeletonPropertyIndex]);
            }
        }

        IEnumerator SpawnSkeleton()
        {
            yield return new WaitForSeconds(_spawnRate);      //ожидание перед волной
            while (_waveCount > 0)
            {
                for (int i = 0; i < _skeletonCount; i++)
                {
                    yield return new WaitForSeconds(_spawnRate);
                    CreateSkeleton();
                }

                _skeletonCount += 2; //увеличиваем число спавнящихся скелетов

                _waveCount--;                                      //уменьшаем кол во волн
                yield return new WaitForSeconds(_waveRate);        // ожидание перед сл волной

            }
        }
    }
}
