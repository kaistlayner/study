using System.Diagnostics.CodeAnalysis;
using UnityEngine;

#pragma warning disable CS0649

namespace Assets.Scripts.Monster
{
    public class MonsterMove : MonoBehaviour
    {
        [Header("Speed")]
        [SerializeField]
        private float _moveSpeed;

        private GameObject _house;
        private float THRESHOLD;

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void Awake()
        {
            _house = GameObject.FindWithTag("House");
            THRESHOLD = _house.GetComponent<RectTransform>().rect.height;
        } 

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void Update()
        {
            Vector3 delta = _house.transform.position - transform.position;
            transform.position += delta.normalized * _moveSpeed * Time.deltaTime;

            if(Vector3.Magnitude(delta) < THRESHOLD){
                _house.GetComponent<Home>().OnHomeAttacked(Time.deltaTime * 10);
            }
        }
    }
}
