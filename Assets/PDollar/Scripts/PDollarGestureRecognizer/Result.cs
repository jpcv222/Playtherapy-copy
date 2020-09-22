namespace PDollarGestureRecognizer {

	public struct Result {

		public string GestureClass;
		public float Score;


		public Result(string name,float confidence)
		{

			GestureClass = name;
			Score=confidence;
		}
	}
}