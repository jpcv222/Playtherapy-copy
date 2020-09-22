using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
	public class ScoreManager : MonoBehaviour
	{
		public GameObject resultPanel;
		public GameObject endPantalla;
		public static int score;
		// The player's score.
		private int nextLevel;
		//Puntaje necesario para cambiar de nivel
		private string nameScene;
		Text text;
		// Reference to the Text component.
		private StatusGame statusGame;
		// Referacia Al objeto de status que es enviado a todas las escenas.
		private Slider timeSlider;
		private float timer_game;
		private GameObject player;
		PlayerHealth playerHealth;
		/*Pantalla de Resulados*/
		private Text desenpeno;
		private Text mejorResultado;
		private Text indicatorNextLevel;




		void Awake ()
		{
			// Set up the reference.
			indicatorNextLevel = GameObject.Find ("IndicatorNextLevel").GetComponent<Text> ();
			timeSlider = GameObject.Find ("SliderTime").GetComponent<Slider> (); //Contador de Tiempo
			text = GetComponent <Text> ();
			statusGame = GameObject.Find ("StatusGame").GetComponent<StatusGame> ();
			timer_game = statusGame.timer_game; // Tiempo Global
			score = statusGame.scoreGlobal; // Puntuacion General del Juego        
			Scene scene = SceneManager.GetActiveScene ();
			nameScene = scene.name;
			//Busca Jugador
			player = GameObject.FindGameObjectWithTag ("Player");
			playerHealth = player.GetComponent <PlayerHealth> ();
			relationTimeStar (); //Relecion de estrellas con cantidad de tiempo.
           
		}


		void Update ()
		{ 
			
			// Set the displayed text to be the word "Score" followed by the score value.
			text.text = "Puntaje: " + score;
			indicatorNextLevel.text = " " + (int)score + " / " + (int)nextLevel;
			statusGame.scoreGlobal = score;

			//Cambio de Escena
			if (score >= nextLevel && "Urban".Equals (nameScene)) {
				
				SceneManager.LoadScene ("ParkNatural");

			}
			if (score >= nextLevel && "ParkNatural".Equals (nameScene)) {
				
				SceneManager.LoadScene ("ParkNaturalDark");

			}
			if (score >= nextLevel && "ParkNaturalDark".Equals (nameScene)) {
				score += (int)(playerHealth.currentHealth * 5 + timer_game * 5);//Bonificaion se se termina el juego
				/*Pantalla de Resulados*/
				ResultadosPanel ();



			}

			//Contador de Tiempo
			TimerController ();
		}

		void TimerController ()
		{
		
			if (timer_game > 0) {

				timer_game -= Time.deltaTime;
				statusGame.timer_game = timer_game;
				float inicial = statusGame.tiempoInicial * 60;

				//NGUIDebug.Log ("Timer:  "+ timer_game + "Status Time "+ inicial);
				timeSlider.value = (timer_game / inicial) * 100;
				//NGUIDebug.Log ("Slider Value:  " + timeSlider.value);
			} else {
				//Termina el juego cuando el contador llega a 0
				playerHealth.currentHealth = 0;
				ResultadosPanel ();
			}
		}
		/*Relacion de Tiempo Con respecto al numero de estrellas*/
		void relationTimeStar ()
		{

			if (statusGame.tiempoInicial < 5) {
				nextLevel = 80 + statusGame.scoreGlobal;
			}
			if (statusGame.tiempoInicial >= 5) {
				nextLevel = 150 + statusGame.scoreGlobal;
			}
			if (statusGame.tiempoInicial >= 10) {
				nextLevel = 200 + statusGame.scoreGlobal;
			}
		}

		//Muestra la pantalla de resultados
		public void ResultadosPanel ()
		{
			GameObject.Find ("slideTimeUI").SetActive (false);
			GameObject.Find ("HealthUI").SetActive (false);
			GameObject.Find ("Player").transform.position = endPantalla.transform.position;
			GameObject.Find ("Player").SetActive (false);
			resultPanel.SetActive (true);
			desenpeno = GameObject.Find ("Score Text").GetComponent<Text> ();
			mejorResultado = GameObject.Find ("Best Score Text").GetComponent<Text> ();
			desenpeno.text = "Desempeño: " + score;
			mejorResultado.text = "Mejor: " + score;
			Destroy (gameObject);
		}
	}
}