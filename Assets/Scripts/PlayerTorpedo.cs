using UnityEngine;
using System.Collections;

public class PlayerTorpedo : MonoBehaviour {

	#region Transform Members
	public float moveSpeed = 10f;
	
	float screenTopEdge;
	
	Vector3 initialVelocity;
	#endregion

	#region Gameplay Members
	public float damagePoints = 20f;

	bool autoTarget = false;
	#endregion
	
	#region Component Members
	Rigidbody2D rigidBody;
	#endregion
	
	#region GameObject Members
	public EnemyShip targetObject;
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
				Die();
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
			Die();
			enemy.HitWith(damagePoints);
		} 
		
		if (asteroid) {
			Die();
			asteroid.Destroy();
		}
	}
	#endregion
	
	
	#region Private Methods
	void Die() {
		Destroy(gameObject);
		if (targetObject) {
			targetObject.IsTargeted = false;
		}
	}
	
	void Move() {
		float step = moveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, step);
	}
	
	bool IsEnemyDead() {
		return (!targetObject);
	}
	#endregion
	
	public bool IsAutoTarget {
		get { return autoTarget; }
		set { autoTarget = value; }
	}
	
	public void SetVelocity(Vector3 velocity) {
		initialVelocity = velocity;
	}
}
