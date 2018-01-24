using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Text title;
	public Text body;

	void Start(){
		title.text = PlayerPrefs.GetString ("GOTitle");
		body.text = PlayerPrefs.GetString ("GOText");
		if (title.text == "")
			title.text = "Title";
		if (body.text == "")
			body.text = "body";
	}

	public void Menu(){
	}
}
