using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

	[SerializeField]
	private float damagePoints = 20f;
	
	private float screenTopEdge;
	private EnemyFormation enemyFormation;

	void Start () {
		enemyFormation = FindObjectOfType<EnemyFormation>();
		
		screenTopEdge  = (float) GameCamera.GetBoundaries(Camera.main, transform)["maxY"];
	}
	
	void Update () {
		float laserBottomEdge = transform.position.y;
		if (laserBottomEdge >= screenTopEdge) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyShip enemy = collider.gameObject.GetComponent<EnemyShip>();
		Asteroid asteroid = collider.gameObject.GetComponent<Asteroid>();
		
		if (enemy) {
			Debug.Log("Hit enemy ship!");
			Destroy(gameObject);
			enemyFormation.KillEnemy();
		} 
		
		if (asteroid) {
			Destroy(gameObject);
			asteroid.Destroy();
		}
	}
	
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
}
