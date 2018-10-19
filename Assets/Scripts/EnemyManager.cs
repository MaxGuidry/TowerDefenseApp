using System;
using System.Collections.Generic;
using GlobalMethods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyClass
{
    public class EnemyManager : MonoBehaviour
    {
        private Dictionary<EnemyTypes, GameObject> _enemyDictionary;

        public List<Enemy> AllEnemies;

        [Header("Enemy Prefabs")] public GameObject BasicMeleePrefab;

        public GameObject BasicRangedPrefab;
        public GameObject BasicTankPrefab;
        [Header("Hub")] public GameObject Center;
        public float SpawnDistance;

        /// <summary>
        ///     Add each enemy type to a dictionary with the assigned prefab
        ///     Start each coroutine
        /// </summary>
        private void Awake()
        {
            _enemyDictionary = new Dictionary<EnemyTypes, GameObject>
            {
                {EnemyTypes.BasicMelee, BasicRangedPrefab},
                {EnemyTypes.BasicRanged, BasicRangedPrefab},
                {EnemyTypes.BasicTank, BasicTankPrefab}
            };

            StartCoroutine(Utils.RepeatAction(SpawnRandomEnemy, Random.Range(1, 2)));
            StartCoroutine(Utils.RepeatAction(BeginEnemyAction, 0.02f));
        }

        /// <summary>
        ///     Create each enemy with the passed in random stat
        ///     Add each enemy into the list
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="enemyDamage"></param>
        /// <param name="enemySpeed"></param>
        /// <param name="stoppingDistance"></param>
        /// <param name="enemyType"></param>
        /// <param name="enemyPrefab"></param>
        /// <param name="spawnPos"></param>
        /// <returns></returns>
        public GameObject SpawnEnemy(float maxHealth, float enemyDamage, float enemySpeed, float stoppingDistance,
            EnemyTypes enemyType,
            GameObject enemyPrefab, Vector3 spawnPos)
        {
            var enemyInstance = Instantiate(_enemyDictionary[enemyType], spawnPos, Quaternion.identity);
            enemyInstance.transform.SetParent(transform);

            var enemyClass = enemyInstance.GetComponent<Enemy>();
            enemyClass.InitEnemy(maxHealth, enemyDamage, enemySpeed, stoppingDistance, enemyType, spawnPos);

            AllEnemies.Add(enemyClass);
            return enemyInstance;
        }

        /// <summary>
        ///     Set each random stat for the enemy
        /// </summary>
        public void SpawnRandomEnemy()
        {
            var enumType = Utils.GetRandomEnum<EnemyTypes>();
            var spawnPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 0)) * SpawnDistance;
            Utils.SetEnemyStats(enumType);
            float maxHealth, enemyDamage, enemySpeed, stoppingDistance;
            GameObject prefab;

            switch (enumType)
            {
                case EnemyTypes.BasicMelee:
                {
                    prefab = BasicMeleePrefab;
                    maxHealth = Utils.BasicMelee.MaxHealth;
                    enemyDamage = Utils.BasicMelee.EnemyDamage;
                    enemySpeed = Utils.BasicMelee.EnemySpeed;
                    stoppingDistance = Utils.BasicMelee.StoppingDistance;
                    break;
                }
                case EnemyTypes.BasicRanged:
                {
                    prefab = BasicRangedPrefab;
                    maxHealth = Utils.BasicRanged.MaxHealth;
                    enemyDamage = Utils.BasicRanged.EnemyDamage;
                    enemySpeed = Utils.BasicRanged.EnemySpeed;
                    stoppingDistance = Utils.BasicRanged.StoppingDistance;
                    break;
                }
                case EnemyTypes.BasicTank:
                {
                    prefab = BasicTankPrefab;
                    maxHealth = Utils.BasicTank.MaxHealth;
                    enemyDamage = Utils.BasicTank.EnemyDamage;
                    enemySpeed = Utils.BasicTank.EnemySpeed;
                    stoppingDistance = Utils.BasicRanged.StoppingDistance;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SpawnEnemy(maxHealth, enemyDamage, enemySpeed, stoppingDistance, enumType, prefab, spawnPos);
        }

        /// <summary>
        ///     Using this action to make each enemy chase the target
        /// </summary>
        /// <returns></returns>
        private void BeginEnemyAction()
        {
            foreach (var enemy in AllEnemies)
            {
                if (enemy.CurrentState != EnemyState.Dead)
                {
                    enemy.ChaseTarget(Center.transform.position, Time.deltaTime);
                }
            }
        }

        /// <summary>
        ///     Stop all Cotoutines on this behaviour
        /// </summary>
        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}