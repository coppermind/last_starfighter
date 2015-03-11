using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

	[SerializeField]
	private float damagePoints = 20f;
	
	private float screenTopEdge;
	private EnemyFormation enemyFormation;

	void Start () {
		Camera camera  = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		screenTopEdge  = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).y;
		
		enemyFormation = FindObjectOfType<EnemyFormation>();
	}
	
	void Update () {
		float laserBottomEdge = transform.position.y;
		if (laserBottomEdge >= screenTopEdge) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyShip enemy = collider.gameObject.GetComponent<EnemyShip>();
		if (enemy) {
			Debug.Log("Hit enemy ship!");
			Destroy(gameObject);
			enemyFormation.KillEnemy();
		}
	}
	
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
}
