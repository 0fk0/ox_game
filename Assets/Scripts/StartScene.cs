using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private string text = "ボタンを押してね。Scpaceキーでも良いです";
    private void OnGUI()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        Rect rect1 = new Rect(screenWidth * 1/8, screenHeight * 1/3, screenWidth * 3/4, 20);
        GUI.Label(rect1, text);

        rect1.y = screenHeight * 1/3 + 20;
        rect1.height = 50;
        if (GUI.Button(rect1, "Push Start")
            || Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}

