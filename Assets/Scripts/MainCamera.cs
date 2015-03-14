using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	[SerializeField]
	private Transform target;

	private float width   = 32f;
	private float height  = 18f;
	private float padding = 0.5f;

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
