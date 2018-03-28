using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class GUI_Handler : MonoBehaviour {

	public GameObject menu; //Assign canvas to this in inspector, make sure script is on EventHandler
	public string hideShow; //Set to the key you want to press to open and close the GUI
	public string FileName; //Title the file you want to export data to!  Will be saved in resources.

	private bool isShowing;
	public InputField raName, partic,exp,age,height,weight,other; 
	//TODO: Ask around to see if there's a more efficient and elegant method to get references to all of the input fields!
	public Toggle left, right;
	public Dropdown sex;
	public Button export;

	// Use this for initialization
	void Start () {
		menu = GameObject.Find ("ResearchGUI");
		raName = GameObject.Find ("RA_Name").GetComponent<InputField>();
		partic = GameObject.Find ("Participant_Name").GetComponent<InputField>();
		exp = GameObject.Find ("Exp_Name").GetComponent<InputField>();
		age = GameObject.Find ("Age_input").GetComponent<InputField>();
		height = GameObject.Find ("Height").GetComponent<InputField>();
		weight = GameObject.Find ("Weight").GetComponent<InputField>();
		other = GameObject.Find ("Other").GetComponent<InputField>();
		left = GameObject.Find ("LeftToggle").GetComponent<Toggle>();
		right = GameObject.Find ("RightToggle").GetComponent<Toggle>();
		sex = GameObject.Find ("GenderInput").GetComponent<Dropdown>();
		export = GameObject.Find ("Export").GetComponent<Button> ();

		isShowing = true;
		export.onClick.AddListener (exportData);
		if (hideShow.Equals(""))
			hideShow = "escape";
		if (FileName.Equals (""))
			FileName = "default";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (hideShow)) {
			isShowing = !isShowing;
			menu.SetActive (isShowing);
		}
	}

	void exportData() {
		string path = @"Assets\Data\" + FileName + ".txt";
		string theTime = DateTime.Now.ToString ("hh:mm:ss");
		string theDate = DateTime.Now.ToString ("d");
		if (!File.Exists (path)) {
			string header = "Time\tName\tP #\tExp #\tAge\tHeight\tWeight\tGender\tHand\tOther\r\n";
			File.WriteAllText (path, header);
		}
		string hand = "";
		if (left.isOn)
			hand = hand + "L";
		if (right.isOn)
			hand = hand + "R";
		string appendText = theTime+" "+theDate +"\t"+ raName.text +"\t"+ partic.text +"\t"+ exp.text +"\t"+ age.text +
			"\t"+ height.text +"\t"+ weight.text +"\t"+ sex.captionText.text +"\t"+ hand +"\t"+ other.text + "\r\n";
		File.AppendAllText (path, appendText);
	}

}
