using UnityEngine;
using System.Collections;

public class DrawScore : MenuBase
{
    public GUIStyle font;
    public GUIStyle menuFont;

    private int currentScore = 0;
    private int highScore;
    private bool IsActive;
    private float newX;
    private float scale;
    protected override void Start()
    {
        base.Start();
        LoadScore();

        scale = Screen.width / 320f;
        float diff = scale - 1;
        scale = 1 + diff / 2f;
        newX = 32 * (scale+0.5f);
        font.fontSize = (int)(20 * scale);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Stop()
    {
        IsActive = false;
        highScore = Mathf.Max(currentScore, highScore);
        SaveScore();
    }

    void Restart()
    {
        IsActive = true;
        currentScore = 0;
    }

    void OnHighScore(int windowID)
    {
        var scaledTop = buttonPadding*scale/2;
        GUI.Label(new Rect(xPadding, scaledTop, width / 2 - xPadding * 3, buttonHeight * 2 + buttonPadding), "Score", font);
        GUI.Label(new Rect(xPadding, scaledTop + buttonPadding * 2, width / 2 - xPadding * 3, buttonHeight * 2 + buttonPadding), currentScore.ToString(), font);

        GUI.Label(new Rect(width / 2, scaledTop, width / 2 - xPadding * 2, buttonHeight * 2 + buttonPadding), "Best", font);
        GUI.Label(new Rect(width / 2, scaledTop + buttonPadding * 2, width / 2 - xPadding * 2, buttonHeight * 2 + buttonPadding), highScore.ToString(), font);
    }

    void OnGUI()
    {
        if (IsActive)
        {

            GUI.Label(new Rect(newX, scale * 4.5f, 140, 50), "Score: " + currentScore.ToString(), font);
        }
        else
        {
            GUI.Window(1, new Rect(Screen.width / 2 - width / 2, -buttonPadding, width, height / 2 - buttonPadding), OnHighScore, "", menuFont);
        }
    }

    public int Score()
    {
        if (IsActive)
            currentScore++;

        return currentScore;
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    private void LoadScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }
}
