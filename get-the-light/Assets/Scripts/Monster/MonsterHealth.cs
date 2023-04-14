using System.Diagnostics.CodeAnalysis;
using UnityEngine;

#pragma warning disable CS0649

namespace Assets.Scripts.Monster
{
    public class MonsterHealth : MonoBehaviour
    {
        [SerializeField]
        private GameObject _xp;
        
        [SerializeField]
        private float _maxHealth;
        
        private float _health;

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void Awake()
        {
            _health = _maxHealth;
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        private void DeathRattle()
        {
            CreateXp();
        }
    
        private void CreateXp()
        {
            Instantiate(_xp, transform.position, Quaternion.identity);
        }

        public void UpdateHealth(float change)
        {
            _health += change;
            if(_health <= 0){
                CreateXp();
                Death();
            } 
        }

        // reduce health for home energy test
        private void Update(){
            UpdateHealth(-24 * Time.deltaTime);
        }
    }
}
