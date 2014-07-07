using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {
	private static AudioSource audioSource;

	private void Awake(){
		audioSource = this.GetComponent<AudioSource>();
	}

	public static void FindSE(string path){
		AudioClip audioClip = Resources.Load(path) as AudioClip;
		audioSource.clip = audioClip;
		audioSource.Play();
	}

}
