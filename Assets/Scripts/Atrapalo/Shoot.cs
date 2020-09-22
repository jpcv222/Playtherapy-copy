using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{

    public class Shoot : MonoBehaviour
    {


        bool flag = true;
        float angle;
        GameAngles calc;
        bool flagSide = true;
        float  timeArrive ;
        private GameManagerAtrapalo gameM;
        private Vector3 pointFin;
		private float timeLaunch;
        private float vel;
        private bool side;
		public int type;
        private bool flagArrive;
        private Vector3 pointIni;
        private Rigidbody posNew;
        public float speed = 1.5f; 
		/* Global variants for the use of indicator cubes in cannons */

		private GameObject cubeRightIndicator;
		private GameObject cubeLeftIndicator;

		private Color indicatorOn;
		private Color indicatorOff;

		private Color indicatorLeft;
		private Color indicatorRight;


        // Use this for initialization
        void Start()
        {	
			/*Obtengo instacion de los GameObject cube*/
			cubeLeftIndicator = GameObject.FindGameObjectWithTag("cubeLeft");
			cubeRightIndicator = GameObject.FindGameObjectWithTag("cubeRight");
			/*I define the colors that will be used as indicators "indicatorOn" color to designate the exit of the ball
			 * "IndicatorOff" Color to indicate the standby time */
			indicatorOn = new Color (0.2f,0.2f,0.2f);
			indicatorOff = new Color (0,1,0);
			/*Inicio con ambos cubos con el color de espera*/
			indicatorRight = indicatorOff;
			indicatorLeft = indicatorOff;

			gameM = GameObject.Find("GameManager").GetComponent<GameManagerAtrapalo>();
			gameM.ballsAlive++;
			angle = (180*gameM.level)/6;
            calc = new GameAngles(angle, gameM.planeShootFront, gameM.planeShootLat);
            timeLaunch = gameM.launchTime;
            side = true;

            if (type == 3)
            {
                timeLaunch = timeLaunch / 2;
            }
            timeArrive = Time.time + timeLaunch;
            Vector3 pointIni = transform.position;
            flagArrive = false;
            //posNew = this.GetComponent<Rigidbody>();



        }

        // Update is called once per frame
        void Update()
        {
			/*"SetupIndicator ();" Configura los indicadores en el momento*/
			SetupIndicator ();
            if (flag)
            {               
                if (gameM.side)
                {
                    Debug.Log("lado der");
                    this.shootPosition("ShoulderRight", "HandRight", "right");
                    gameM.side = false;
                }
                else
                {
                    Debug.Log("lado izq");
                    this.shootPosition("ShoulderLeft", "HandLeft", "left");
                    gameM.side = true;
                }
                this.vel = (pointFin - gameObject.transform.position).magnitude * speed / timeLaunch;
                //this.vel = vel * Time.deltaTime;
                flag = false;
            }
            else {
                if (gameObject.transform.position.x > pointFin.x) {
                    //posNew.MovePosition(pointIni+pointFin*Time.deltaTime);
                    //this.transform.position = Vector3.MoveTowards(transform.position, pointFin, this.vel);
                    this.transform.position = Vector3.MoveTowards(transform.position, pointFin, this.vel*Time.deltaTime);
                }
                else
                {
                    if (!flagArrive)
                    {
                        pointFin = pointFin * 2;
                        flagArrive = true;
                    }
                    //this.transform.position = Vector3.MoveTowards(transform.position, pointFin, this.vel);
                    this.transform.position = Vector3.MoveTowards(transform.position, pointFin, this.vel * Time.deltaTime);
                }
            }

                         
        }



        Vector3 calculateSpeedVector(float t, Vector3 point)
        {
            Vector3 initSpeed = new Vector3();

            float Vx = 0;
            float Vy = 0;
            float Vz = 0;
            float X0 = gameObject.transform.position.x;
            float Z0 = gameObject.transform.position.z;
            float Y0 = gameObject.transform.position.y;
            float Y1 = point.y;
            float X1 = point.x;
            float Z1 = point.z;

            Vx = (X1 - X0) / t;
            Vz = (Z1 - Z0) / t;

            Vy = (Y1-Y0)/t;


            initSpeed.x = Vx;
            initSpeed.y = Vy;
            initSpeed.z = Vz;

            return initSpeed;
        }


		public void shootPosition(string jointOneName, string jointTwoName, string side)
        {
			/* Recibe como entrada la posicion de salida de la pelota
			 * para decidir que indicador entra en estado de espera o activo "ControllerIndicator(jointOneName);"*/
			ControllerIndicator(jointOneName);

            if (gameM.shootOpt==3)
            {
                counterPlane();
            }

            Debug.Log("plano"+gameM.plane);

            float angleRad = calc.setRamdomAngle(side, gameM.plane);
                
            Vector3 pointOne = GameObject.FindGameObjectWithTag(jointOneName).transform.position;
            Vector3 pointTwo = GameObject.FindGameObjectWithTag(jointTwoName).transform.position;

            Debug.Log("point ini 1");
            Debug.Log(pointIni);
            this.pointFin = calc.getPosition(pointOne, calc.createPointTwoShoulderAF(pointOne, pointTwo), angleRad, 0.0f, gameM.plane);
            Debug.Log("point fin 1");
            Debug.Log(pointFin);
            this.pointFin = pointFinal(pointIni, pointFin, -60.0f);
            Debug.Log("point fin 2");
            Debug.Log(pointFin);
            //gameObject.transform.position = pointFin;
            gameM.NewRepetition();      
			


        }

        void OnDestroy(){
			gameM.ballsAlive--;
		}

        // Function to count the balls shoot to a plane
        public void counterPlane()
        {
            if(gameM.countBallPlane != 2)
            {
                gameM.countBallPlane++;
            }else
            {
                gameM.countBallPlane = 1;
                gameM.changePlane();
            }
        }


        /**
         * Function to calculate a point with a line equation in the x pos correction
         **/
        public Vector3 pointFinal(Vector3 pointOne, Vector3 pointTwo, float xFinal)
        {

            Vector3 directorVector = pointTwo - pointOne;

            Vector3 finalPos = new Vector3();
            finalPos.x = xFinal;
            float eqOne = (xFinal - pointOne.x) / directorVector.x;
            finalPos.y = eqOne * directorVector.y + pointOne.y;
            finalPos.z = eqOne * directorVector.z + pointOne.z;

            return finalPos;
        }

		/* Controls which indicator is on standby or active depending on "ControllerIndicator(string ubication)"*/
		public void ControllerIndicator(string ubication){
			SetupIndicator ();
			if (string.ReferenceEquals("ShoulderRight",ubication)) {

				indicatorLeft = indicatorOff;
				indicatorRight = indicatorOn;


			} else {

				indicatorLeft = indicatorOn;
				indicatorRight = indicatorOff;
			}
		}

		public void SetupIndicator(){
			/*Behavior configuration for indicator cubes"SetupIndicator()" */

			cubeLeftIndicator.GetComponent<Renderer> ().material.color = indicatorLeft;
			cubeRightIndicator.GetComponent<Renderer> ().material.color = indicatorRight;

		}
    }
}
