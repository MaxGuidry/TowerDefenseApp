using System;
using System.Collections.Generic;
using GlobalMethods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyClass
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;
        private Dictionary<EnemyTypes, GameObject> _enemyDictionary;
        public List<Enemy> AllEnemies;

        [Header("Enemy Prefabs")]
        public GameObject BasicMeleePrefab;
        public GameObject BasicRangedPrefab;
        public GameObject BasicTankPrefab;

        [Header("Hub")]
        public GameObject Center;

        public float SpawnDistance;
        public static Stats BasicMelee, BasicRanged, BasicTank;
        /// <summary>
        ///     Add each enemy type to a dictionary with the assigned prefab
        ///     Start each coroutine
        /// </summary>
        private void Awake()
        {
            Instance = this;
            _enemyDictionary = new Dictionary<EnemyTypes, GameObject>
            {
                {EnemyTypes.BasicMelee, BasicMeleePrefab},
                {EnemyTypes.BasicRanged, BasicRangedPrefab},
                {EnemyTypes.BasicTank, BasicTankPrefab}
            };

            StartCoroutine(Utils.RepeatActionForeverWithDelay(SpawnRandomEnemy, Random.Range(1, 2)));
            StartCoroutine(Utils.RepeatActionForeverWithDelay(BeginEnemyAction, 0.02f));
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
            SetEnemyStats(enumType);
            float maxHealth, enemyDamage, enemySpeed, stoppingDistance;
            GameObject prefab;

            switch (enumType)
            {
                case EnemyTypes.BasicMelee:
                    {
                        prefab = BasicMeleePrefab;
                        maxHealth = BasicMelee.MaxHealth;
                        enemyDamage = BasicMelee.EnemyDamage;
                        enemySpeed = BasicMelee.EnemySpeed;
                        stoppingDistance = BasicMelee.StoppingDistance;
                        break;
                    }
                case EnemyTypes.BasicRanged:
                    {
                        prefab = BasicRangedPrefab;
                        maxHealth = BasicRanged.MaxHealth;
                        enemyDamage = BasicRanged.EnemyDamage;
                        enemySpeed = BasicRanged.EnemySpeed;
                        stoppingDistance = BasicRanged.StoppingDistance;
                        break;
                    }
                case EnemyTypes.BasicTank:
                    {
                        prefab = BasicTankPrefab;
                        maxHealth = BasicTank.MaxHealth;
                        enemyDamage = BasicTank.EnemyDamage;
                        enemySpeed = BasicTank.EnemySpeed;
                        stoppingDistance = BasicTank.StoppingDistance;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SpawnEnemy(maxHealth, enemyDamage, enemySpeed, stoppingDistance, enumType, prefab, spawnPos);
        }

        /// <summary>
        ///     Set each enemy stats here
        /// </summary>
        /// <param name="type"></param>
        public static void SetEnemyStats(EnemyTypes type)
        {
            switch (type)
            {
                case EnemyTypes.BasicMelee:
                    {
                        BasicMelee.MaxHealth = Random.Range(100, 150);
                        BasicMelee.EnemyDamage = Random.Range(10, 20);
                        BasicMelee.EnemySpeed = Random.Range(5, 8);
                        BasicMelee.StoppingDistance = 1f;
                        break;
                    }
                case EnemyTypes.BasicRanged:
                    {
                        BasicRanged.MaxHealth = Random.Range(75, 100);
                        BasicRanged.EnemyDamage = Random.Range(25, 30);
                        BasicRanged.EnemySpeed = Random.Range(5, 8);
                        BasicRanged.StoppingDistance = 7f;
                        break;
                    }
                case EnemyTypes.BasicTank:
                    {
                        BasicTank.MaxHealth = Random.Range(200, 250);
                        BasicTank.EnemyDamage = Random.Range(5, 20);
                        BasicTank.EnemySpeed = Random.Range(2, 3);
                        BasicTank.StoppingDistance = 1f;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
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

    public struct Stats
    {
        public float MaxHealth, EnemyDamage, EnemySpeed, StoppingDistance;
    }
}