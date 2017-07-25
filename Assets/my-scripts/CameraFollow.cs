using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target; // the main chracter that the camera follows
	public float cameraSmoothing = 5.0f;

	Vector3 offset; // the distance between the main character and the camera
	// Use this for initialization
	void Start () {
        if(target)
        {
            offset = transform.position - target.transform.position;
        }
    }

    // Update is called once per frame
    void Update() {
        if (target)
        {
            Vector3 targetCamera = target.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamera, Time.deltaTime * cameraSmoothing);
        }	
	}
}
