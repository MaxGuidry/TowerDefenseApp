using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyClass
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Hub")] public GameObject Center;
        public float SpawnDistance;
        [Header("Enemy Prefabs")] public GameObject BasicMeleePrefab;
        public GameObject BasicRangedPrefab;
        public GameObject BasicTankPrefab;

        public List<Enemy> AllEnemies;
        private Dictionary<EnemyTypes, GameObject> _enemyDictionary;

        private void Start()
        {
            _enemyDictionary = new Dictionary<EnemyTypes, GameObject>
            {
                {EnemyTypes.BasicMelee, BasicRangedPrefab},
                {EnemyTypes.BasicRanged, BasicRangedPrefab},
                {EnemyTypes.BasicTank, BasicTankPrefab}
            };

            StartCoroutine(Utils.RepeatAction(SpawnRandomEnemy, Random.Range(1, 2)));
        }

        private void Update()
        {
            foreach (var enemy in AllEnemies)
            {
                if (enemy.CurrentState != EnemyState.Dead)
                {
                    enemy.ChaseTarget(Center.transform.position, Time.deltaTime);
                }
            }
        }

        public GameObject SpawnEnemy(float maxHealth, float enemyDamage, float enemySpeed, float stoppingDistance, EnemyTypes enemyType,
            GameObject enemyPrefab, Vector3 spawnPos)
        {
            var enemyInstance = Instantiate(_enemyDictionary[enemyType], spawnPos, Quaternion.identity);
            enemyInstance.transform.SetParent(transform);

            var enemyClass = enemyInstance.GetComponent<Enemy>();
            enemyClass.InitEnemy(maxHealth, enemyDamage, enemySpeed, stoppingDistance, enemyType, spawnPos);

            AllEnemies.Add(enemyClass);
            return enemyInstance;
        }

        public void SpawnRandomEnemy()
        {
            var enumType = Utils.GetRandomEnum<EnemyTypes>();
            float maxHealth, enemyDamage, enemySpeed;
            var spawnPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 0)) * SpawnDistance;

            switch (enumType)
            {
                case EnemyTypes.BasicMelee:
                    {
                        maxHealth = Random.Range(100, 150);
                        enemyDamage = Random.Range(10, 20);
                        enemySpeed = Random.Range(5, 8);
                        SpawnEnemy(maxHealth, enemyDamage, enemySpeed, 1f, enumType, BasicMeleePrefab, spawnPos);
                        break;
                    }
                case EnemyTypes.BasicRanged:
                    {
                        maxHealth = Random.Range(75, 100);
                        enemyDamage = Random.Range(25, 30);
                        enemySpeed = Random.Range(5, 8);
                        SpawnEnemy(maxHealth, enemyDamage, enemySpeed, 10f, enumType, BasicRangedPrefab, spawnPos);
                        break;
                    }
                case EnemyTypes.BasicTank:
                    {
                        maxHealth = Random.Range(200, 250);
                        enemyDamage = Random.Range(5, 20);
                        enemySpeed = Random.Range(2, 3);
                        SpawnEnemy(maxHealth, enemyDamage, enemySpeed, 1f, enumType, BasicTankPrefab, spawnPos);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}