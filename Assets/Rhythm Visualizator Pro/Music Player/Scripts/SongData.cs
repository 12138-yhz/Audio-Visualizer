using UnityEngine;
using System.Collections.Generic;
using System;


public class SongData : MonoBehaviour {
	
	// Metadata
	public string title;
	public string artist;

	public AudioClip songClip;

	void OnEnable () {
		this.name = string.Format ("{0} - {1}", artist, title);
	}
}
