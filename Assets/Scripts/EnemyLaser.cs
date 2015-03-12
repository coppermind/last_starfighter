using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {

	[SerializeField]
	private float damagePoints = 20f;
	
	private float screenBottomEdge;
	
	void Start () {
		Camera camera  = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		screenBottomEdge  = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
	}
	
	void Update () {
		float laserBottomEdge = transform.position.y;
		if (laserBottomEdge <= screenBottomEdge) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		PlayerShip player = collider.gameObject.GetComponent<PlayerShip>();
		
		if (player) {
			Debug.Log("Hit player ship!");
			Destroy(gameObject);
		}
	}
	
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
}
