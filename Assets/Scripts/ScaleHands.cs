using UnityEngine;
using System.Collections;

public class ScaleHands : MonoBehaviour {
//	private GameObject left;
	private GameObject right;
	private float speed = .1f;
	// Use this for initialization
	void Start () {
//		left = GameObject.Find ("Robot_LeftHand");
		right = GameObject.Find ("Robot_RightArm");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
//			left.transform.localScale += new Vector3 (1, 1, 1) * Time.deltaTime * speed;
			right.transform.localScale += new Vector3 (1, 1, 1) * Time.deltaTime * speed;
//			Debug.Log ("up arrow pressed");
		} else if (Input.GetKey (KeyCode.DownArrow)) {
//			left.transform.localScale += new Vector3 (-1, -1, -1) * Time.deltaTime * speed;
			right.transform.localScale += new Vector3 (-1, -1, -1) * Time.deltaTime * speed;
//			Debug.Log ("down arrow pressed");
		}
	}
}
