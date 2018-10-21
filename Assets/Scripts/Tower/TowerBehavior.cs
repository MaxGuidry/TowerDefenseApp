using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    //TODO: towers target enemies based on stats.
    public class TowerBehavior : MonoBehaviour
    {
        public GameObject mainTarget;
        public Collider[] targets;
        public TowerData data;
        public GameObject bullet;
        void Awake()
        {

        }
        private void Start()
        {

            if (data.type == TowerData.TowerType.SPREAD)
                StartCoroutine(FireSpread(.5f));
            else
                StartCoroutine(Fire(.5f, mainTarget));

        }
        // Update is called once per frame
        void Update()
        {
            targets = Physics.OverlapSphere(transform.position, data.range, ~(1 << LayerMask.NameToLayer("Bullet") | 1 << LayerMask.NameToLayer("Tower")));


            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    mainTarget = targets[i].gameObject;
                    break;
                }
                else
                    mainTarget = null;
            }
        }

        IEnumerator Fire(float rate, GameObject target)
        {
            while (true)
            {
                yield return new WaitForSeconds(rate);
                while (mainTarget == null)
                    yield return null;
                transform.LookAt(mainTarget.transform);
                GameObject b = Instantiate(bullet, transform.position, transform.rotation, transform);
                b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, Random.rotation, data.spreadAngle);
                b.GetComponent<Rigidbody>().AddForce(b.transform.forward * data.bulletFireVel);
            }
        }

        IEnumerator FireSpread(float rate)
        {
            while (true)
            {
                yield return new WaitForSeconds(rate);
                while (mainTarget == null)
                {
                    yield return null;
                }
                for (int i = 0; i < data.bulletCount; i++)
                {
                    GameObject b = Instantiate(bullet, transform.position, transform.rotation, transform);
                    b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, Random.rotation, data.spreadAngle);
                    b.GetComponent<Rigidbody>().AddForce(b.transform.forward * data.bulletFireVel);
                }
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