using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	#region Transform Properties
	private float screenBottomEdge;
	#endregion
	
	
	#region Gameplay Properties
	[SerializeField]
	private float damagePoints = 30f;
	
	[SerializeField]
	private float minGravity;
	
	[SerializeField]
	private float maxGravity;
	
	private float gravityScale;
	#endregion


	#region Component Properties
	private Rigidbody2D rigidBody;
	#endregion
	
	
	#region GameObject Properties
	[SerializeField]
	private GameObject explosionPrefab;
	
	private GameManager gameManager;
	#endregion
	
	
	#region Unity Methods
	void Start() {
		gameManager = FindObjectOfType<GameManager>();
	
		rigidBody = GetComponent<Rigidbody2D>();
		
		screenBottomEdge = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];

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
