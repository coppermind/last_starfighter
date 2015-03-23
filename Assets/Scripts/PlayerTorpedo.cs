using UnityEngine;
using System.Collections;

public class PlayerTorpedo : MonoBehaviour {

	#region Transform Members
	[SerializeField]
	private Vector3 initialVelocity;
	
	[SerializeField]
	private float moveSpeed = 10f;
	
	private float screenTopEdge;
	#endregion

	#region Gameplay Members
	[SerializeField]
	private float damagePoints = 20f;

	private bool autoTarget = false;
	#endregion
	
	#region Component Members
	Rigidbody2D rigidBody;
	#endregion
	
	#region GameObject Members
	public Transform targetObject;
	#endregion

		
	#region Unity Methods
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		
		screenTopEdge  = (float) GameCamera.GetBoundaries(Camera.main, transform)["maxY"];
		
		if (!autoTarget) {
			rigidBody.velocity = initialVelocity;
		} else {
			if (targetObject == null) {
				Destroy(gameObject);
			}
		}
	}
	
	void Update () {
		if (autoTarget) {
			if (!IsEnemyDead()) {
				Move();
			} else {
				AutoExplode();
			}
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
	#endregion
	
	
	#region Private Methods
	private void AutoExplode() {
		Destroy(gameObject);
	}
	
	private void Move() {
		float step = moveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetObject.position, step);
	}
	
	private bool IsEnemyDead() {
		return (!targetObject);
	}
	#endregion
	
	public bool IsAutoTarget {
		get { return autoTarget; }
		set { autoTarget = value; }
	}
}
