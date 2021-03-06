﻿using System;
using Global;
using UnityEngine;
using UnityEngine.Events;

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

        public EnemyState CurrentState;
        public EnemyTypes EnemyType;
        public float MaxHealth, EnemyDamage, EnemySpeed, StoppingDistance;
        public Vector3 SpawnPos;

        /// <summary>
        ///     Initialize this enemy with passed in values
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="enemyDamage"></param>
        /// <param name="enemySpeed"></param>
        /// <param name="stoppingDistance"></param>
        /// <param name="enemyType"></param>
        /// <param name="spawnPos"></param>
        public void InitEnemy(float maxHealth, float enemyDamage, float enemySpeed, float stoppingDistance,
            EnemyTypes enemyType,
            Vector3 spawnPos)
        {
            StoppingDistance = stoppingDistance;
            MaxHealth = maxHealth;
            EnemyDamage = enemyDamage;
            EnemyType = enemyType;
            SpawnPos = spawnPos;
            EnemySpeed = enemySpeed;
            _currentHealth = MaxHealth;
            StartCoroutine(Utils.RepeatActionForeverWithDelay(DestroyDeadEnemy, 0.02f));
        }

        /// <summary>
        ///     The current enemy is going to take the passed in damage
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                CurrentState = EnemyState.Dead;
                //TODO: TEMP...add a better way of deciding what enemies give whtat money
                Player.PlayerData.Money += 100;
                
            }
        }

        /// <summary>
        ///     Chase the target relative to the time
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="time"></param>
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

        /// <summary>
        ///     Set state to attacking and run animtion
        /// </summary>
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

        /// <summary>
        ///     Check if enemy is dead and destroy it
        /// </summary>
        /// <returns></returns>
        public void DestroyDeadEnemy()
        {
            var towers = FindObjectsOfType<Tower.TowerBehavior>() as Tower.TowerBehavior[];
            
            if (CurrentState == EnemyState.Dead)
            {
                foreach (var t in towers)
                    t.targets.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        /// <summary>
        ///     Stop all coroutines
        ///     Remove dead enemy from list
        /// </summary>
        private void OnDisable()
        {
            StopAllCoroutines();
            EnemyManager.Instance.AllEnemies.Remove(this);
        }
    }
}