﻿using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	private AudioSource audioSource;
	private AudioClip[] playlist;
	private MusicPlaylist musicPlaylist;
	private int currentTrack = 0;
	
	void Awake() {
		DontDestroyOnLoad(gameObject);
	}
	
	void Start () {
		audioSource = GetComponent<AudioSource>();
		SetVolume(PlayerPrefsManager.GetMusicVolume());
		LoadPlaylist();
	}
	
	void OnLevelWasLoaded(int level) {
		LoadPlaylist();
	}
	
	void LoadPlaylist() {
		musicPlaylist = FindObjectOfType<MusicPlaylist>();
		if (musicPlaylist) {
			playlist = musicPlaylist.playlist;
			currentTrack = 0;
			Play();
		}
	}
	
	void PlayNextTrack() {
		if (musicPlaylist.shuffle) {
			currentTrack = Random.Range(0, (playlist.Length-1));
		} else {
			currentTrack++;
			if (currentTrack >= playlist.Length) {
				currentTrack = 0;
			}
		}
		Play();
	}
	
	void Play() {
		audioSource.clip = playlist[currentTrack];
		audioSource.Play();
		Invoke("PlayNextTrack", playlist[currentTrack].length);
	}
	
	public void SetVolume(float amount) {
		audioSource.volume = amount;
	}
}