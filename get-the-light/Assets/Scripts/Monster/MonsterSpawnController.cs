using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

#pragma warning disable CS0649

namespace Assets.Scripts.Monster
{
    public class MonsterSpawnController : MonoBehaviour
    {
        [Header("Monster List")]
        [SerializeField]
        private GameObject[] _monsters;
    
        [Space(3)]
        [SerializeField]
        [Header("Spawn Interval")]
        private float _waitingForNextSpawn;

        [Space(3)]

        [Header("X Spawn Range")]
        [SerializeField]
        private float _xMinimum;
        [SerializeField]
        private float _xMaximum;
        
        [Space(3)]

        [Header("Y Spawn Range")]
        [SerializeField]
        private float _yMinimum;
        [SerializeField]
        private float _yMaximum;

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void Awake()
        {
            StartCoroutine(SpawnMonsters());
        }

        private IEnumerator SpawnMonsters()
        {
            while (true)
            {
                Vector3 position = new(Random.Range(_xMinimum, _xMaximum), Random.Range(_yMinimum, _yMaximum), 1);
                Instantiate(_monsters[Random.Range(0, _monsters.Length)], position, Quaternion.identity);
                yield return new WaitForSeconds(_waitingForNextSpawn);
            }
        }
    }
}
