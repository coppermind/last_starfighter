using UnityEngine;
using System.Collections;

public class EnemyPosition : MonoBehaviour {

	void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, 0.5f);
	}

}
