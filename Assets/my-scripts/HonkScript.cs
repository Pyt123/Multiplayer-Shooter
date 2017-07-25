using UnityEngine;
using System.Collections;

public class HonkScript : MonoBehaviour {

	AudioSource audioSource;
	public AudioClip clip;

    void Start () {
		audioSource = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" ) {
			//if (alreadyHonked == false) {
			//	alreadyHonked = true;
				audioSource.PlayOneShot (clip);
			//}
		}
	}
}
