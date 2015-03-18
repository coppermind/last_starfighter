using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FtlSpinner : MonoBehaviour {

	[SerializeField]
	private float spinSeconds = 60f;
	private float originalRight = 0f;
	
	private Transform statusObject;
	private RectTransform rectTrans;
	
	void Start () {
		statusObject = transform.Find("Status");
		rectTrans = statusObject.GetComponent<RectTransform>();
		originalRight = rectTrans.sizeDelta.x;
	}
	
	void Update () {
		float currentRight = rectTrans.sizeDelta.x;
		float step = originalRight / spinSeconds * Time.deltaTime;
		currentRight -= step;
		rectTrans.sizeDelta = new Vector2(currentRight, 0);
	}
}
