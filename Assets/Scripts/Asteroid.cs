using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	#region Transform Properties
	float screenBottomEdge;
	#endregion
	
	
	#region Gameplay Properties
	public float damagePoints = 30f;
	
	public float minGravity, maxGravity;
	
	float gravityScale;
	#endregion


	#region Component Properties
	Rigidbody2D rigidBody;
	#endregion
	
	
	#region GameObject Properties
	[SerializeField]
	GameObject explosionPrefab;
	
	GameManager gameManager;
	#endregion
	
	
	#region Unity Methods
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
	
		rigidBody = GetComponent<Rigidbody2D>();
		
		screenBottomEdge = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];

		GameObjects.SetParent(transform, "Asteroid Container");
		
		SetRandomGravity();
	}
	
	void Update() {
		if (gameManager.GameIsPaused) {
			rigidBody.gravityScale = 0f;
			rigidBody.isKinematic = true;
		} else {
			rigidBody.isKinematic = false;
			rigidBody.gravityScale = gravityScale;
		}
		
		if (transform.position.y <= screenBottomEdge) {
			Destroy(gameObject);
		}
	}
	#endregion
	
	
	#region Private Methods
	void SetRandomGravity() {
		gravityScale = Random.Range(minGravity, maxGravity);
		rigidBody.gravityScale = gravityScale;
	}
	#endregion
	
	
	#region Public Methods
	public void Destroy() {
		GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.parent = transform;
		
		Transform body = transform.Find("Body");
		if (body) {
			Destroy(body.gameObject);
		}
		Destroy(gameObject, 0.2f);
	}
	#endregion
	
	
	#region Properties
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
	#endregion
}
