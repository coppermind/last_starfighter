using UnityEngine;
using System.Collections;

public class TorpedoContainer : MonoBehaviour {

	[SerializeField]
	private bool isAutoTarget = true;
	
	[SerializeField]
	private PlayerTorpedo torpedoPrefab;
	
	[SerializeField]
	private int totalTorpedoes = 10;

	void Start() {
		float targetX = -9f;
		float targetY =  8f;
		for (int i = 1; i < totalTorpedoes; i++) {
			EnemyShip newTarget = GetUntargetedShip();
			if (newTarget && isAutoTarget) {
				PlayerTorpedo t = Instantiate(torpedoPrefab, transform.position, Quaternion.identity) as PlayerTorpedo;
				t.IsAutoTarget = true;
				t.targetObject = newTarget;
			} else {
				Vector3 velocity = new Vector3(targetX, targetY, 0f);
				PlayerTorpedo t = Instantiate(torpedoPrefab, transform.position, Quaternion.identity) as PlayerTorpedo;
				t.SetVelocity(velocity);
				targetX += 2f;
			}
		}
	}

	void Update () {
		if (0 >= transform.childCount) {
			Destroy(gameObject);
		}
	}
	
	private EnemyShip GetUntargetedShip() {
		EnemyShip[] enemies = FindObjectsOfType<EnemyShip>();
		foreach (EnemyShip e in enemies) {
			if (!e.IsTargeted) {
				e.IsTargeted = true;
				return e;
			}
		}
		return null;
	}
}
