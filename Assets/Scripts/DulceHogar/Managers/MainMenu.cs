using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
	public class MainMenu : MonoBehaviour
	{
		public Slider rangoSlider;
		public Slider tiempoSlider;
		public Text rangoText;
		public Text tiempoText;
		public Dropdown manoSelect;

		public static MainMenu status;
		public float timer_game;
		private StatusGame statusGame;
		// Referacia Al objeto de status que es enviado a todas las escenas


		void Awake ()
		{
			rangoSlider.minValue = 35;
			rangoSlider.maxValue = 80;
			tiempoSlider.minValue = 5;
			tiempoSlider.maxValue = 15;
			statusGame = GameObject.Find ("StatusGame").GetComponent<StatusGame> ();

		}

		void Start ()
		{




		}
		// Update is called once per frame
		void Update ()
		{
			SlideUpdate ();
		}

		/*Inicia el juego con los parametros de entrada*/
		public void StartButton ()
		{
			StartGameControllerPlay ();
			NGUIDebug.Log ("Timer SetUp :" + statusGame.timer_game + " Tiempo Inicial: " + statusGame.tiempoInicial);
			//SceneManager.LoadScene ("ParkNatural");//Carga la escena principal del juego.
			SceneManager.LoadScene ("Urban");//Carga la escena principal del juego.
		}
		//Esta funcion cambia el indicador que es mostrado en pantalla dependiendo si se realiza por tiempo o numero de Items
		// Tomando Como referencia el DropDown


		void SlideUpdate(){
			rangoText.text = ":"+((int)rangoSlider.value).ToString() + "º Grados";
			tiempoText.text = ":"+((int)tiempoSlider.value).ToString() + " Minutos";


		}

		public void StartGameControllerPlay(){
			statusGame.scoreGlobal = 1;
			statusGame.healthPlayer = 100; //Establese la salud del jugador en 100 esto evita errores al reinciar el juego.
			float tiempo = tiempoSlider.value;
			int rango = (int)rangoSlider.value;
			//int repeticiones = (int)repeticioneSlider.value;

			int mano = (int)manoSelect.value;
			/*Para le seleccion e de una mano el valor de 0 izquierdad el valor 1 derecha*/

			statusGame.SetInformationGame (tiempo,rango,mano);
		}
	}
}