using UnityEngine;
using System.Collections;

public class SpawnEnemyScript : MonoBehaviour {

	public GameObject objectToSpawn;
	public float timeToWaitBetweenSpawns = 2.0f;
	private float timer = 0;
    
	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {	
		timer += Time.deltaTime;
		if (timer > timeToWaitBetweenSpawns) {
			// todo: spawn the objectToSpawn
			// we need to reset the timer
			Instantiate(objectToSpawn, transform.position, transform.rotation);
			timer = 0;
		}
	
	}
}
