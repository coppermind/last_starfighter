using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TorpedoInterface : MonoBehaviour {

	Text torpedoesText;
	
	PlayerGun playerGun;
	
	void Start () {
		playerGun = FindObjectOfType<PlayerGun>();
		
		GameObject obj = transform.Find("Torpedoes Text").gameObject;
		torpedoesText = obj.GetComponent<Text>();
		
		if (playerGun) {
			torpedoesText.text = playerGun.TorpedoesLeft.ToString();
		} else {
			Debug.LogError("Player gun missing from scene");
		}
	}
	
	void Update () {
		torpedoesText.text = playerGun.TorpedoesLeft.ToString();
	}
}
