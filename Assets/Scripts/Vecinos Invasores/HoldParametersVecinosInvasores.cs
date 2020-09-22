using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
public class HoldParametersVecinosInvasores {






	public static float min_repeticiones=1;
	public static float max_repeticiones=60;

	public static float min_tiempo=1;
	public static float max_tiempo=30;


	public static bool use_time=false;
	public static int repeticiones_restantes;






	public static int min_animals=10;
	public static int max_animals=100;

	public static int select_animals;

	public static int min_enemies=10;
	public static int max_enemies=30;

	public static int select_enemies;

	public const int USE_FINGERS=0;
	public const int USE_PINCHS=1;

	public const int SIMPLE=0;
	public const int COLORS=1;

	public static int type_game=USE_FINGERS;
	public static int mode_game=SIMPLE;


	public static int min_time_per_ship=3;//seconds
	public static int max_time_per_ship=12;//seconds
	public static int select_time_per_ship=min_time_per_ship;//seconds
	public static float select_jugabilidad=5;
	public static float select_strenght_pinch=0.1f;
	public static int best_score=0;

	public static List<Finger.FingerType> fingerTypes= new List<Finger.FingerType>();
}
