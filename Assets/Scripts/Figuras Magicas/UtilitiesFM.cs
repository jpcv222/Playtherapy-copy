using System.Collections;
public class UtilitiesFM {

	public static T[] ShuffleArray<T>(T[] array,int seed)
	{
		System.Random prng = new System.Random (seed);

		for (int i = 0; i < array.Length-1; i++) {
			int randomIndex = prng.Next (i, array.Length);
			T tempFreeItem= array[randomIndex];
			array [randomIndex] = array [i];
			array [i] = tempFreeItem;
		}
		return array;
	}
}
