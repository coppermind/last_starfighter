using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	[SerializeField]
	Transform target;

	float width   = 32f;
	float height  = 18f;
	float padding = 0.5f;

	void OnDrawGizmos() {
		GameGizmos.DrawBox(transform, width, height, padding);
	}

	void LateUpdate() {
		/*
		if (target) {
			Vector3 newPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
			transform.position = newPosition;
		}
		*/
	}
}
