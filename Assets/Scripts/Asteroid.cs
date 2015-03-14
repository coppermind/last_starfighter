using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	#region Transform Properties
	private float screenBottomEdge;
	#endregion
	
	
	#region Gameplay Properties
	[SerializeField]
	private float minGravity;
	
	[SerializeField]
	private float maxGravity;
	
	[SerializeField]
	private float damagePoints = 30f;
	#endregion


	#region Component Properties
	private Rigidbody2D rigidBody;
	#endregion
	
	
	#region GameObject Properties
	[SerializeField]
	private GameObject explosionPrefab;
	#endregion
	
	
	#region Unity Methods
	void Start() {
		rigidBody         = GetComponent<Rigidbody2D>();
		
		screenBottomEdge  = (float) GameCamera.GetBoundaries(Camera.main, transform)["minY"];

		SetRandomGravity();
	}
	
	void Update() {
		if (transform.position.y <= screenBottomEdge) {
			Destroy(gameObject);
		}
	}
	#endregion
	
	
	#region Private Methods
	void SetRandomGravity() {
		float gravityScale = Random.Range(minGravity, maxGravity);
		rigidBody.gravityScale = gravityScale;
	}
	#endregion
	
	
	#region Public Methods
	public void Destroy() {
		Destroy(gameObject);
		
		GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		Destroy(explosion, 0.2f);
	}
	#endregion
	
	
	#region Properties
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
	
	
	#endregion
}
