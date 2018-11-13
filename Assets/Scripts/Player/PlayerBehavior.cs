using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerBehavior : MonoBehaviour
    {
        public Text text;

        public void Update()
        {
            if(Global.GlobalGameData.playerData.Health >= 0)
                text.text = Global.GlobalGameData.playerData.Health.ToString();
            if(!Global.GlobalGameData.playerData.Alive)
                SceneManager.LoadScene("PlayerMovement");
        }
        public void TakeDamage(float damage)
        {
            Global.GlobalGameData.playerData.Health -= damage;
            if (Global.GlobalGameData.playerData.Health <= 0)
                Global.GlobalGameData.playerData.Alive = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                TakeDamage(other.GetComponent<EnemyClass.Enemy>().EnemyDamage * Global.GlobalGameData.playerData.Defense);
        }

    }
}