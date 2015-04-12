using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public int scorePoints;

	[SerializeField]
	GameObject objPrefab;

	[SerializeField]
	float gravityRate = -1f;

	Rigidbody2D rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.velocity = new Vector3(0f, gravityRate, 0f);
		
		GameObjects.SetParent(transform, "PowerUp Container");
	}
	
	void Update () {
		if (-1f >= transform.position.y) {
			Destroy(gameObject);
		}
	}
	
	public GameObject PowerUpItem {
		get { return objPrefab; }
		set { objPrefab = value; }
	}
}
