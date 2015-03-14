using UnityEngine;
using System.Collections;

public class GameGizmos : MonoBehaviour {

	public static void DrawBox(Transform t, float width, float height, float padding = 0f) {
		float xMin, xMax, yMin, yMax;
		
		xMin = t.position.x - padding * width;
		xMax = t.position.x + padding * width;
		yMin = t.position.y - padding * height;
		yMax = t.position.y + padding * height;
		
		Gizmos.DrawLine(new Vector3(xMin, yMin, 0f), new Vector3(xMin, yMax, 0f));
		Gizmos.DrawLine(new Vector3(xMin, yMin, 0f), new Vector3(xMax, yMin, 0f));
		Gizmos.DrawLine(new Vector3(xMin, yMax, 0f), new Vector3(xMax, yMax, 0f));
		Gizmos.DrawLine(new Vector3(xMax, yMin, 0f), new Vector3(xMax, yMax, 0f));
	}

}
