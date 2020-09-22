using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class HealthBonus : MonoBehaviour
    {

        // Use this for initialization
        Animator anim;
        GameObject player;
        public GameObject HealthItem;
        PlayerHealth playerHealth;
        public int scoreValue = 20;
        private int bonus = 30;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent <PlayerHealth>();
        }
		
        // Update is called once per frame
        void Update()
        {
			
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ScoreManager.score += scoreValue;
                playerHealth.BonusDamage(bonus);
                HealthItem.SetActive(false);
            }
        }
    }
}