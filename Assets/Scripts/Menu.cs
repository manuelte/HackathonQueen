using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GUIStyle titleStyle;
    public GUIStyle buttonStyle;

    void OnGUI()
    {
        // Settings for the title text
        titleStyle.fontSize = 60;
        titleStyle.alignment = TextAnchor.MiddleCenter;

        // Settings for the button text
        buttonStyle.normal.textColor = Color.black;
        buttonStyle.fontSize = 40;
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 235, 400, 100), "#QUEEN", titleStyle);
        if (GUI.Button(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 50, 200, 75), "Start", buttonStyle))
            SceneManager.LoadScene("Level01");
        
        if (GUI.Button(new Rect(Screen.width / 2 - 300, Screen.height / 2 + 50, 200, 75), "Quit", buttonStyle))
            Application.Quit();
    }
}
