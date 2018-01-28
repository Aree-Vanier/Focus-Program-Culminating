using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///Controls Game Over screen
public class GameOver : MonoBehaviour {

    ///Screen title
	public Text title;
    ///Screen content
	public Text body;
    ///White at start
    public Image white;

	void Start(){
        //Get the text from the player prefs
		title.text = PlayerPrefs.GetString ("GOTitle");
		body.text = PlayerPrefs.GetString ("GOText");
		if (title.text == "")
			title.text = "Title";
		if (body.text == "")
			body.text = "body";
	}

    void Update() {
        white.color = new Color(1, 1, 1, white.color.a - 0.005f);  
        if(white.color.a < 0) {
            white.gameObject.SetActive(false);
        }
    }

    ///Changes to the menu scene
	public void Menu(){
        SceneManager.LoadScene(0);
	}
}
