using System;
using UnityEngine;

namespace EnemyClass
{
    public enum EnemyState
    {
        Chasing,
        Attacking,
        Dead
    }

    [Serializable]
    public class Enemy : MonoBehaviour
    {
        private float _currentHealth;
        private EnemyManager _enemyManager;

        public EnemyState CurrentState;
        public EnemyTypes EnemyType;
        public float MaxHealth, EnemyDamage, EnemySpeed, StoppingDistance;
        public Vector3 SpawnPos;

        public void InitEnemy(float maxHealth, float enemyDamage, float enemySpeed, float stoppingDistance, EnemyTypes enemyType,
            Vector3 spawnPos)
        {
            StoppingDistance = stoppingDistance;
            MaxHealth = maxHealth;
            EnemyDamage = enemyDamage;
            EnemyType = enemyType;
            SpawnPos = spawnPos;
            EnemySpeed = enemySpeed;
            _currentHealth = MaxHealth;
            _enemyManager = FindObjectOfType<EnemyManager>();
        }

        private void Update()
        {
            if (CurrentState == EnemyState.Dead)
            {
                _enemyManager.AllEnemies.Remove(this);
                Destroy(gameObject);
            }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = _currentHealth - damage > 0 ? Utils.UpdateHealth(_currentHealth - damage) : Utils.UpdateHealth(0);

            if (_currentHealth == 0)
            {
                CurrentState = EnemyState.Dead;
            }
        }

        public void ChaseTarget(Vector3 targetPos, float time)
        {
            if (CurrentState != EnemyState.Dead)
            {
                if (Vector3.Distance(targetPos, transform.position) > StoppingDistance)
                {
                    CurrentState = EnemyState.Chasing;
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, EnemySpeed * time);
                }
                else
                {
                    AttackTarget();
                    //TODO: Temporary
                    TakeDamage(MaxHealth);
                }
            }
        }

        public void AttackTarget()
        {
            CurrentState = EnemyState.Attacking;
            switch (EnemyType)
            {
                case EnemyTypes.BasicMelee:
                    break;
                case EnemyTypes.BasicRanged:
                    break;
                case EnemyTypes.BasicTank:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}