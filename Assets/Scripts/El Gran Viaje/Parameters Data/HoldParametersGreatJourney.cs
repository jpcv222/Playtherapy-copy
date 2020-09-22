using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldParametersGreatJourney{

	public static float min_repeticiones=1;
	public static float max_repeticiones=60;

	public static float min_tiempo=1;
	public static float max_tiempo=30;

	public static float min_angle=12;
	public static float max_angle=45;

	public static float min_sostener=0;
	public static float max_sostener=6;


	public const int LADO_TODOS=0;
	public const int LADO_IZQ_DER=1;
	public const int LADO_DERECHO=2;
	public const int LADO_IZQUIERDO=3;
	public const int LADO_ABAJO=4;




	public const int MOVIMIENTO_MIEMBROS_INFERIORES = 0;
	public const int MOVIMIENTO_TRONCO = 1;


	public static bool sostener_aleatorio=true;
	public static bool use_time=false;

	public static float min_descanso=8;
	public static float max_descanso=30;

	public static int repeticiones_restantes;

	public static float select_jugabilidad;
	public static float select_angle_min;
	public static float select_angle_min_frontal;
	public static float select_angle_max;
	public static float select_sostener;
	public static float select_descanso;
	public static int select_movimiento;
	public static int lados_involucrados = 0;


	public static double best_angle_left=0;
	public static double best_angle_right=0;




}
