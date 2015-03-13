using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	[SerializeField]
	private Transform target;

	private float width = 31.5f;
	private float height = 18f;

	void OnDrawGizmos() {
		float xMin, xMax, yMin, yMax;
		
		xMin = transform.position.x - 0.51f * width;
		xMax = transform.position.x + 0.51f * width;
		yMin = transform.position.y - 0.5f * height;
		yMax = transform.position.y + 0.5f * height;
		
		Gizmos.DrawLine(new Vector3(xMin, yMin, 0f), new Vector3(xMin, yMax, 0f));
		Gizmos.DrawLine(new Vector3(xMin, yMin, 0f), new Vector3(xMax, yMin, 0f));
		Gizmos.DrawLine(new Vector3(xMin, yMax, 0f), new Vector3(xMax, yMax, 0f));
		Gizmos.DrawLine(new Vector3(xMax, yMin, 0f), new Vector3(xMax, yMax, 0f));
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
