using UnityEngine;
using System.Collections;

public class MoveCapsule : MonoBehaviour {
	private Vector3 leftBound;
	private Vector3 rightBound;
	// Use this for initialization
	void Start () {
		leftBound = GameObject.Find ("Invisible Wall 2").transform.position;
		rightBound = GameObject.Find ("Invisible Wall").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A)) {
			if (transform.position.x >= leftBound.x) {
				transform.Translate (.15f * Vector3.left * Time.deltaTime, Space.World);
//			Debug.Log ("left held down");
			}
		}
		if (Input.GetKey(KeyCode.D)) {
			if(transform.position.x <= rightBound.x){
				transform.Translate(.15f * Vector3.right * Time.deltaTime, Space.World);
//			Debug.Log ("right held down");
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("Collided");
	}
}
