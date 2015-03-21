using UnityEngine;
using System.Collections;

public class PlayerTorpedo : MonoBehaviour {

	[SerializeField]
	private float damagePoints = 20f;

	[SerializeField]
	private bool autoTarget = false;
	
	[SerializeField]
	private Vector3 initialVelocity;

	private float screenTopEdge;

	Rigidbody2D rigidBody;
	
	private Transform targetObject;
	
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		
		screenTopEdge  = (float) GameCamera.GetBoundaries(Camera.main, transform)["maxY"];
		
		if (!autoTarget) {
			rigidBody.velocity = initialVelocity;
		}
	}
	
	void Update () {
		if (autoTarget) {
			//
		}
		
		float torpedoEdge = transform.position.y;
		if (torpedoEdge >= screenTopEdge) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyShip enemy = collider.gameObject.GetComponent<EnemyShip>();
		Asteroid asteroid = collider.gameObject.GetComponent<Asteroid>();
		
		if (enemy) {
			Destroy(gameObject);
			enemy.HitWith(damagePoints);
		} 
		
		if (asteroid) {
			Destroy(gameObject);
			asteroid.Destroy();
		}
	}
}
