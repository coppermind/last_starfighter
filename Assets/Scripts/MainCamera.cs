using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	float width = 31.5f;
	float height = 18f;

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

}
