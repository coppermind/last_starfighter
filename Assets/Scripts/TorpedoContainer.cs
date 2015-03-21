using UnityEngine;
using System.Collections;

public class TorpedoContainer : MonoBehaviour {

	void Update () {
		if (0 >= transform.childCount) {
			Destroy(gameObject);
		}
	}
	
}
