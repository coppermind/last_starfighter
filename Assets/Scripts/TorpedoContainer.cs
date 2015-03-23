using UnityEngine;
using System.Collections;

public class TorpedoContainer : MonoBehaviour {

	[SerializeField]
	private bool isAutoTarget = true;
	
	[SerializeField]
	private PlayerTorpedo torpedoPrefab;
	
	[SerializeField]
	private int totalTorpedoes = 20;

	void Start() {
		for (int i = 1; i < totalTorpedoes; i++) {
			PlayerTorpedo t = Instantiate(torpedoPrefab, transform.position, Quaternion.identity) as PlayerTorpedo;
			if (isAutoTarget) {
				t.IsAutoTarget = true;
				t.targetObject = GetUntargetedShip();
			}
		}
	}

	void Update () {
		if (0 >= transform.childCount) {
			Destroy(gameObject);
		}
	}
	
	
	private Transform GetUntargetedShip() {
		EnemyShip[] enemies = FindObjectsOfType<EnemyShip>();
		foreach (EnemyShip e in enemies) {
			if (!e.IsTargeted) {
				e.IsTargeted = true;
				return e.transform;
			}
		}
		return null;
	}
}
