using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	
	public static Hashtable GetBoundaries(Camera c, Transform t, float padding = 0f) {
		Hashtable boundaries = new Hashtable();
		float distance = t.position.z - c.transform.position.z;
		
		boundaries["minX"] = c.ViewportToWorldPoint(new Vector3 (0, 0, distance)).x + padding;
		boundaries["minY"] = c.ViewportToWorldPoint(new Vector3 (0, 0, distance)).y + padding;
		boundaries["maxX"] = c.ViewportToWorldPoint(new Vector3 (1, 1, distance)).x - padding;
		boundaries["maxY"] = c.ViewportToWorldPoint(new Vector3 (1, 1, distance)).y - padding;
		
		return boundaries;
	}
}

