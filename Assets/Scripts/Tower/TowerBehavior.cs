using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    public class TowerBehavior : MonoBehaviour
    {

        public TowerData data;
        public GameObject bullet;
        void Awake()
        {
            bullets = new List<Quaternion>(data.bulletCount);
            for (int i = 0; i < data.bulletCount; i++)
                bullets.Add(Quaternion.Euler(Vector3.zero));
        }

        // Update is called once per frame
        void Update()
        {
            data.targets = Physics.OverlapSphere(transform.position, data.range);
            Debug.Log("oiu");
            if (Input.GetKeyDown(KeyCode.Space))
                if (data.type == TowerData.TowerType.SPREAD)
                    StartCoroutine(FireSpread(0));
                else
                    StartCoroutine(Fire(0, data.mainTarget));
            for (int i = 0; i < data.targets.Length; i++)
            {
                if (data.targets[i].gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    data.mainTarget = data.targets[i].gameObject;
                    break;
                }
                else
                    data.mainTarget = null;
            }
        }

        IEnumerator Fire(float rate, GameObject target)
        {
            yield return new WaitForSeconds(rate);
            if (data.mainTarget == null)
                yield break;
            transform.LookAt(data.mainTarget.transform);
            GameObject b = Instantiate(bullet, transform.position, transform.rotation, transform);
            b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, Random.rotation, data.spreadAngle);
            b.GetComponent<Rigidbody>().AddForce(b.transform.forward * data.bulletFireVel);
        }

        List<Quaternion> bullets;
        IEnumerator FireSpread(float rate)
        {
            yield return new WaitForSeconds(rate);
            foreach (Quaternion quat in bullets)
            {
                GameObject b = Instantiate(bullet, transform.position, transform.rotation, transform);
                b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, Random.rotation, data.spreadAngle);
                b.GetComponent<Rigidbody>().AddForce(b.transform.forward * data.bulletFireVel);
            }
        }
        public void Upgrade()
        {

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, data.range);
        }
    }
}