using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///Controls the menu
public class Menu : MonoBehaviour {
    
    ///Instructions text
    public GameObject instructions;
    ///Level select buttons
    public GameObject startButtons;

    ///Shows instructions, hides start buttons
    public void ShowInstructions(){
        instructions.SetActive(true);
        startButtons.SetActive(false);
    }

    ///Shows start buttons, hides instructions
    public void ShowLevels() {
        startButtons.SetActive(true);
        instructions.SetActive(false);
    }

    ///Starts selected level
    ///buildIndex: The build index of the level to start
    public void StartLevel(int buildIndex) {
        SceneManager.LoadScene(buildIndex);
    }
}
