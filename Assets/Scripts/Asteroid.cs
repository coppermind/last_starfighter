using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	[SerializeField]
	private Sprite[] spriteArray;
	
	[SerializeField]
	private float minGravity;
	
	[SerializeField]
	private float maxGravity;
	
	[SerializeField]
	private float damagePoints = 30f;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rigidBody;
	
	private float screenBottomEdge;

	void Start() {
		spriteRenderer    = GetComponent<SpriteRenderer>();
		rigidBody         = GetComponent<Rigidbody2D>();
		
		Camera camera     = Camera.main;
		float distance    = transform.position.z - camera.transform.position.z;
		screenBottomEdge  = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
		
		SetRandomSprite();
		SetRandomGravity();
	}
	
	void Update() {
		if (transform.position.y <= screenBottomEdge) {
			Destroy(gameObject);
		}
	}
	
	void SetRandomSprite() {
		int randomIndex = Random.Range(0, (spriteArray.Length - 1));
		spriteRenderer.sprite = spriteArray[randomIndex];
	}
	
	void SetRandomGravity() {
		float gravityScale = Random.Range(minGravity, maxGravity);
		rigidBody.gravityScale = gravityScale;
	}
	
	public float DamagePoints {
		get { return damagePoints; }
		set { damagePoints = value; }
	}
}
