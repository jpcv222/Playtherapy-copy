using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{

    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth;
        public int currentHealth;
        public Slider healthSlider;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(0f, 0f, 1f, 0.1f);
		public GameObject ScoreObject;
		private ScoreManager gameScore;


        Animator anim;
        AudioSource playerAudio;
        PlayerController playerMovement;
        //PlayerShooting playerShooting;
        bool isDead;
        bool damaged;
        private StatusGame statusGame;
        // Referacia Al objeto de status que es enviado a todas las escenas.


        void Awake()
        {
			gameScore = ScoreObject.GetComponent<ScoreManager> ();
            //Objeto Indestrutible de Status
            statusGame = GameObject.Find("StatusGame").GetComponent<StatusGame>();
            this.startingHealth = (int)statusGame.healthPlayer; // Salud Global del Player

            anim = GetComponent <Animator>();
            playerAudio = GetComponent <AudioSource>();
            playerMovement = GetComponent <PlayerController>();
            //playerShooting = GetComponentInChildren <PlayerShooting> ();
            currentHealth = startingHealth;
            healthSlider.value = startingHealth;
        }


        void Update()
        {
            if (damaged)
            {
                damageImage.color = flashColour;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            damaged = false;
        }


        public void TakeDamage(int amount)
        {
            damaged = true;

            currentHealth -= amount;
            statusGame.healthPlayer -= amount; //Asignacion global del Juego
            healthSlider.value = statusGame.healthPlayer;
            NGUIDebug.Log("Slider Health:  " + healthSlider.value);


            playerAudio.Play();

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }

        public void BonusDamage(int amount)
        {
            damaged = true;
            currentHealth += amount;
            healthSlider.value = currentHealth;


        }


        public void Death()
        {
            isDead = true;
            playerMovement.enabled = false;
			gameScore.ResultadosPanel ();

        }


        public void RestartLevel()
        {
            SceneManager.LoadScene("Urban");
        }
    }
}