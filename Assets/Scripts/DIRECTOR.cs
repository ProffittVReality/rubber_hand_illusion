using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using Neuron;

/* This is a central executive program. Most of the key aspects of the program
 * are contained here. Parts of this experiment require input from the user
 * and the researcher, so as a rule, any user input is done on the right side
 * of the keyboard (WASD) and any researcher input is done from the left side
 * (arrow keys + numpad).
 */

public class DIRECTOR : MonoBehaviour {
	private GameObject arm;
	private Image dim;
	private int inTransition = 0;
	private float startTime = 0;
	private float pause;
	private Slider slider;
	private GameObject marker;
	private string path = @"Assets\Data\RUBBERHAND.txt";
	private GameObject eyes;
	public Vector3 diff = Vector3.zero;
	private Transform lbound;
	public GameObject brush;
	private GameObject boxhand;
	private GameObject hand;
	public GameObject lagdetect;
	private bool lagtrans;
	private GameObject box;

	private Vector3 handworld;
	public float pauseLength = 1f;
	public int stage = 0;
	public float offset = 0;
	public int lag = 0;
	public float avglag = 0;

	// Initialize all variables listed above, deactivate some, set default values for others.
	void Start () {
		box = GameObject.Find ("Hand Illusion Box");
		boxhand = GameObject.FindGameObjectWithTag ("box");
		boxhand.SetActive (false);
		hand = GameObject.FindGameObjectWithTag ("real");
		eyes = GameObject.Find ("CenterEyeAnchor");
		arm = GameObject.Find ("NeuronRobot_MultiMesh");
		dim = GameObject.Find ("darkness").GetComponent<Image>();
		slider = GameObject.Find ("Slider").GetComponent<Slider> ();
		slider.transform.parent.gameObject.SetActive (false);
		marker = GameObject.Find ("Marker v2.0");
		lbound = GameObject.Find ("Invisible Wall 2").transform;
		brush = GameObject.Find ("RightHandAnchor");
		lagdetect = GameObject.Find ("Robot_RightArm");
		lagtrans = false;
		Transform sphere = brush.transform.Find ("Sphere");
//		sphere.Translate (new Vector3 (offset, 0, 0));
		brush.SetActive (false);
		marker.SetActive (false);
		if (offset == 0)
			offset = -.3f;
	}
		
	void Update () {

		// Change the lag of hand trackers.
		if (NeuronActor.FRAME_DELAY != lag) {
			NeuronActor.FRAME_DELAY = lag;
			NeuronActor.rotationDelay.Clear ();
			NeuronActor.positionDelay.Clear ();
			avglag = lag * Time.deltaTime;
		}
		// Black out screen while lag is loading.
		if (lagdetect.transform.rotation.eulerAngles == new Vector3 (0, 340, 0) && dim.color != Color.black && !lagtrans) {
			lagtrans = true;
			dim.color = Color.black;
		}
		if (lagdetect.transform.rotation.eulerAngles != new Vector3 (0, 340, 0) && dim.color == Color.black && lagtrans) {
			lagtrans = false;
			dim.color = Color.clear;
		}			
		
		// Use the right arrow key to transition to the next stage
		// of the experiment.
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			stage++;
			inTransition = 1;
			startTime = Time.time;
		}

		// Use the numpad's 1 key to set a good arm position.
		if (Input.GetKeyDown (KeyCode.Keypad1))
			diff = eyes.transform.position;

		// Once a good arm position has been set, allow the user to
		// return to a good position, and move the arm to them with
		// the numpad's 2 key.
		if (Input.GetKeyDown (KeyCode.Keypad2)) {
			diff -= eyes.transform.position;
			eyes.transform.parent.position += diff;
		}

		// Use the numpad's 4 key to switch to a block hand.
		if (Input.GetKeyDown (KeyCode.Keypad4)) {
			foreach (Transform child in hand.transform) {
				if(child.gameObject.layer != 5)
					child.gameObject.SetActive (false);
			}
			boxhand.SetActive (true);
		}

		// Use the numpad's 5 key to switch back to a normal hand.
		if (Input.GetKeyDown (KeyCode.Keypad5)) {
			foreach (Transform child in hand.transform)
				if(child.gameObject.layer != 5)
					child.gameObject.SetActive (true);
			boxhand.SetActive (false);
		}

		// Fades to black between transitions and calls nextStage()
		// to set up the next part of the experiment.
		switch (inTransition) {
		case 0:
			break;
		case 1: 
			float f = Mathf.Clamp01 (.4f * (Time.time-startTime));
			dim.color = Color.Lerp (Color.clear, Color.black, f);
			if (f == 1) {
				inTransition = 2;
				pause = 0;
				nextStage ();
			}
			break;
		case 2:
			pause += Time.deltaTime;
			if (pause > pauseLength) {
				inTransition = 3;
				startTime = Time.time;
			}
			break;
		case 3:
			float g = Mathf.Clamp01 (.4f * (Time.time-startTime));
			dim.color = Color.Lerp (Color.black, Color.clear, g);
			if (g == 1)
				inTransition = 0;
			break;
		}
	}


	// Sets up objects for the proper part of the experiment.
	void nextStage(){
		switch (stage) {
		case 1:
			brush.SetActive (true);
			arm.transform.Translate (new Vector3 (offset, 0, 0));
			slider.transform.parent.gameObject.SetActive (true);
			StartCoroutine ("trackValues");
			break;
		case 2:
			brush.SetActive (false);
			StopCoroutine ("trackValues");
			exportValues (slider.value, false);
			marker.SetActive (true);
			arm.SetActive (false);
			box.SetActive (false);
			break;
		case 3:
			exportValues (marker.transform.position.x - lbound.position.x, true);
			arm.SetActive (true);
			arm.transform.Translate (new Vector3 (-offset, 0, 0));
			marker.SetActive (false);
			box.SetActive (true);
			stage = 0;
			slider.transform.parent.gameObject.SetActive (false);
			break;
		}
	}


	// Record "feels like my hand" slider values every 15 seconds.
	IEnumerator trackValues(){
		while (true) {
			exportValues (slider.value, false);
			Debug.Log (slider.value);
			yield return new WaitForSeconds (15f);
		}
	}


	// Store slider values and output them the file @path.
	void exportValues(float f, bool mark){
		string theTime = DateTime.Now.ToString ("hh:mm:ss");
		string appendText;
		if (mark)
			appendText = "Marker: " + Math.Round ((double)f, 3) + "\r\n";
		else
			appendText = theTime + "\t" + Math.Round((double)f,1) + "\r\n";
		File.AppendAllText (path, appendText);
	}		
}
