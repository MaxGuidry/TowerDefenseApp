using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyClass
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Hub")]
        public GameObject Center;
        [Header("Enemy Prefabs")]
        public GameObject BasicMeleePrefab;
        public GameObject BasicRangedPrefab;
        public GameObject BasicTankPrefab;

        public List<Enemy> AllEnemies;

        void Start()
        {
            StartCoroutine(BeginSpawningEnemies(Random.Range(1, 2)));
        }

        void Update()
        {
            foreach (var enemy in AllEnemies)
            {
                var step = enemy.EnemySpeed * Time.deltaTime;
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, Center.transform.position, step);
            }
        }

        public GameObject SpawnEnemy(float maxHealth, float enemyDamage, float enemySpeed, EnemyTypes enemyType, GameObject enemyPrefab, Vector3 spawnPos)
        {
            GameObject enemyInstance;
            switch (enemyType)
            {
                case EnemyTypes.BasicMelee:
                    {
                        enemyInstance = Instantiate(BasicMeleePrefab, spawnPos, Quaternion.identity);
                        break;
                    }
                case EnemyTypes.BasicRanged:
                    {
                        enemyInstance = Instantiate(BasicRangedPrefab, spawnPos, Quaternion.identity);
                        break;
                    }
                case EnemyTypes.BasicTank:
                    {
                        enemyInstance = Instantiate(BasicTankPrefab, spawnPos, Quaternion.identity);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
            enemyInstance.transform.SetParent(transform);
            var enemyClass = enemyInstance.GetComponent<Enemy>();
            enemyClass.InitEnemy(maxHealth, enemyDamage, enemySpeed, enemyType, spawnPos);
            AllEnemies.Add(enemyClass);
            return enemyInstance;
        }

        public void SpawnRandomEnemy()
        {
            var enumType = GetRandomEnum<EnemyTypes>();
            float maxHealth;
            float enemyDamage;
            float enemySpeed;
            var spawnPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 0)) * 30;
            switch (enumType)
            {
                case EnemyTypes.BasicMelee:
                    {
                        maxHealth = Random.Range(100, 150);
                        enemyDamage = Random.Range(10, 20);
                        enemySpeed = Random.Range(5, 8);
                        SpawnEnemy(maxHealth, enemyDamage, enemySpeed, enumType, BasicMeleePrefab, spawnPos);
                        break;
                    }
                case EnemyTypes.BasicRanged:
                    {
                        maxHealth = Random.Range(75, 100);
                        enemyDamage = Random.Range(25, 30);
                        enemySpeed = Random.Range(5, 8);
                        SpawnEnemy(maxHealth, enemyDamage, enemySpeed, enumType, BasicRangedPrefab, spawnPos);
                        break;
                    }
                case EnemyTypes.BasicTank:
                    {
                        maxHealth = Random.Range(200, 250);
                        enemyDamage = Random.Range(5, 20);
                        enemySpeed = Random.Range(5, 8);
                        SpawnEnemy(maxHealth, enemyDamage, enemySpeed, enumType, BasicTankPrefab, spawnPos);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerator BeginSpawningEnemies(int delay)
        {
            while (true)
            {
                SpawnRandomEnemy();
                yield return new WaitForSeconds(delay);
            }
        }

        private static T GetRandomEnum<T>()
        {
            var values = Enum.GetValues(typeof(T));
            var enumType = values.GetValue(Random.Range(0, values.Length));
            return (T)enumType;
        }
    }
}