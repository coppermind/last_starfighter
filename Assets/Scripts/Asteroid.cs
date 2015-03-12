using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	[SerializeField]
	private GameObject explosionPrefab;
	
	[SerializeField]
	private float minGravity;
	
	[SerializeField]
	private float maxGravity;
	
	[SerializeField]
	private float damagePoints = 30f;

	private Rigidbody2D rigidBody;
	
	private float screenBottomEdge;

	void Start() {
		rigidBody         = GetComponent<Rigidbody2D>();
		
		Camera camera     = Camera.main;
		float distance    = transform.position.z - camera.transform.position.z;
		screenBottomEdge  = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;

		SetRandomGravity();
	}
	
	void Update() {
		if (transform.position.y <= screenBottomEdge) {
			Destroy(gameObject);
		}
	}
	
	void SetRandomGravity() {
		float gravityScale = Random.Range(minGravity, maxGravity);
		rigidBody.gravityScale = gravityScale;
	}
	
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
	
	public void Destroy() {
		Destroy(gameObject);
		
		GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		Destroy(explosion, 0.2f);
	}

}
