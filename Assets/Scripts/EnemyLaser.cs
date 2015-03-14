using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {

	[SerializeField]
	private float damagePoints = 20f;
	
	private float screenBottomEdge;
	
	void Start () {
		screenBottomEdge = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];
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
