using UnityEngine;
using System.Collections;

public class UiButton : MonoBehaviour {

	[SerializeField]
	AudioClip hoverClip;

	[SerializeField]
	AudioClip pressClip;

	AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayClip(string eventType) {
		switch (eventType) {
		case "click":
			audioSource.clip = pressClip;
			break;
		default:
			audioSource.clip = hoverClip;
			break;
		}
		audioSource.Play();
	}
}
