using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	[SerializeField]
	private float gravityRate = -1f;

	private Rigidbody2D rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.velocity = new Vector3(0f, gravityRate, 0f);
	}
	
	void Update () {
		if (-1f >= transform.position.y) {
			Destroy(gameObject);
		}
	}
}
