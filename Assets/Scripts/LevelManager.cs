using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour {

	public static int numberOfLifes = 3;
    public Text displayedLives;

	// Load a new level when the START button is clicked
	public void LoadLevel(string name) {
		if(name == "Level01") {
			ResetLives();
		}
//		Brick.breakableCount = 0;
		Application.LoadLevel (name);
	}

	public void QuitRequest () {
		Debug.Log ("LevelManager.QuitRequest called");
		Application.Quit ();
		// Application.Quit is very limited. Does not work in debug or web.
		//It also can't be used on iOS or Android (and I assume Windows Phone)
		//since the app will be rejected from the store.
	}

/*	public void BrickDestroyed() {
		if(Brick.breakableCount <=0) {
			LoadNextLevel();
		}
	}
*/
	public void ResetLives() {
		numberOfLifes = 3;
	}

	public void LoadNextLevel() {
		Application.LoadLevel (Application.loadedLevel + 1);
	}
/*
    public void updateDisplayedLives()
    {
        displayedLives.text = numberOfLifes.ToString();
    }
*/
	void Start () {
		if (Application.loadedLevelName == "Level01" || Application.loadedLevelName == "Level02" || Application.loadedLevelName == "Level03" || Application.loadedLevelName == "Level04") {
			GetComponent<AudioSource>().Play ();
            //updateDisplayedLives();

        }
	}
}
