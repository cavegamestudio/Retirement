using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class script_button_press_test : MonoBehaviour {

	public Selectable button;

	// Use this for initialization
	void Start () {
		button = gameObject.GetComponent<Selectable>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnClick(){
		Debug.Log (transform.gameObject.name + " is pressed");

	}
}
