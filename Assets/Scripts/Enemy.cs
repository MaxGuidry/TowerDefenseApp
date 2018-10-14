using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyClass
{
    [Serializable]
    public class Enemy : MonoBehaviour
    {
        public EnemyTypes EnemyType;
        public float MaxHealth, EnemyDamage, EnemySpeed;
        public Vector3 SpawnPos;

        private float _currentHealth;
        private EnemyManager enemyManager;

        public void InitEnemy(float maxHealth, float enemyDamage, float enemySpeed, EnemyTypes enemyType, Vector3 spawnPos)
        {
            MaxHealth = maxHealth;
            EnemyDamage = enemyDamage;
            EnemyType = enemyType;
            SpawnPos = spawnPos;
            EnemySpeed = enemySpeed;
            _currentHealth = MaxHealth;
            enemyManager = FindObjectOfType<EnemyManager>();
        }

        public void TakeDamage(float damage)
        {
            var hp = _currentHealth - damage;
            _currentHealth = hp > 0 ? UpdateHealth(hp) : UpdateHealth(0);

            if (_currentHealth == 0)
            {
                enemyManager.AllEnemies.Remove(this);
                Destroy(gameObject);
            }
        }

        private float UpdateHealth(float newHealth)
        {
            return newHealth;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Hub")
            {
                TakeDamage(MaxHealth);
            }
        }
    }
}
