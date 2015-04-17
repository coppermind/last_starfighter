using UnityEngine;
using System.Collections;

public class PlayerBody : MonoBehaviour {

	Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}

	public void Kill() {
		Destroy(gameObject);
	}
	
	public void MakeVulnerable() {
		Debug.Log("MakeVulnerable!!!!");
		animator.SetTrigger("Idle trigger");
	}
}
