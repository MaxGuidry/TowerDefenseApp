using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    //TODO: towers target enemies based on stats.
    public class TowerBehavior : MonoBehaviour
    {
        public GameObject bulletHolder;
        public GameObject mainTarget;
        [HideInInspector]
        public List<GameObject> targets;
        public TowerData data;
        public GameObject bullet;
        
        public List<Tower.TowerData> levels;
      
        public void Awake()
        {
            data = levels[0];
        }

        private void Start()
        {
            targets = new List<GameObject>();
            GetComponent<CapsuleCollider>().radius = data.range;

            if (data.type == TowerData.TowerType.SPREAD)
                StartCoroutine(FireSpread(data.fireRate));
            else
                StartCoroutine(Fire(data.fireRate));
        }

        // Update is called once per frame
        void Update()
        {
            if (targets == null || targets.Count == 0 || targets[0] == null)
                return;
            else
            {
                mainTarget = targets[0];
                transform.LookAt(mainTarget.transform);
            }
        }

        IEnumerator Fire(float rate)
        {
            while (true)
            {
                while (mainTarget == null)
                    yield return null;
                GameObject b = Instantiate(bullet, transform.position, transform.rotation, bulletHolder.transform);
                b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, Random.rotation, data.spreadAngle);
                b.GetComponent<Rigidbody>().AddForce(b.transform.forward * data.bulletFireVel);
                b.GetComponent<Bullet>().damage = data.damage;
                Destroy(b, 5);
                yield return new WaitForSeconds(rate);
            }
        }

        IEnumerator FireSpread(float rate)
        {
            while (true)
            {
                while (mainTarget == null)
                    yield return null;
                for (int i = 0; i < data.bulletCount; i++)
                {
                    GameObject b = Instantiate(bullet, transform.position, transform.rotation, bulletHolder.transform);
                    b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, Random.rotation, data.spreadAngle);
                    b.GetComponent<Rigidbody>().AddForce(b.transform.forward * data.bulletFireVel);
                    b.GetComponent<Bullet>().damage = data.damage;
                    Destroy(b, 5);
                }
                yield return new WaitForSeconds(rate);
            }
        }

        public TowerData UpgradeTower()
        {
            int currentLevelIndex = levels.IndexOf(data);
            int maxLevelIndex = levels.Count - 1;
            if (currentLevelIndex < maxLevelIndex)
            {
                return levels[currentLevelIndex + 1];
            }
            else
                return data;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
                return;
            else
                targets.Add(other.gameObject);
        }

        public void OnTriggerExit(Collider other)
        {
            targets.Remove(other.gameObject);
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.gameObject == null)
                targets.Remove(other.gameObject);
        }
    }
}