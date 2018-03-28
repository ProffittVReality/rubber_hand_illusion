using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slider : MonoBehaviour {
	private Slider sldr;
	// Use this for initialization
	void Start () {
		sldr = gameObject.GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.W) || Input.GetKeyDown (KeyCode.S)) {
			sldr.Select ();
//			sldr.value += sldr.value * .15f * Time.deltaTime;
		}
	}
}
