using UnityEngine;
using System.Collections;

public class GameMath : MonoBehaviour {

	public static bool IsProbable(float rate) {
		float probability = rate * Time.deltaTime;
		return (Random.value < probability);
	}
}
