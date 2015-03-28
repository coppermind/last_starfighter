using UnityEngine;
using System.Collections;

public class GameObjects : MonoBehaviour {

	public static void SetParent(Transform t, string parentName) {
		GameObject container = GameObject.Find(parentName);
		if (!container) {
			container = new GameObject(parentName);
		}
		t.parent = container.transform;
	}

}
