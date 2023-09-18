using UnityEngine;
using System.Collections;

public class MainMenu : MenuBase
{
    public GameObject GameManager;
    public GUIStyle menuFont;
    public GUIStyle buttonFont;
    public GUIStyle instructionFont;

    public GUIStyle MuteFont;
    public GUIStyle UnMuteFont;

    public GUIStyle PauseFont;
    public GUIStyle UnPauseFont;

    public GUIStyle emptyFont;


    private int menuSelection = 0;

    public float MenuDelayTime = 2;
    private float remainingMenuDelayTime;
    private float scale;

    public enum MenuState { Playing = 5, MainMenu = 0, InstructionOne = 1, Credits = 2, InstructionTwo = 3 };

    protected override void Start()
    {
        base.Start();


        scale = Mathf.Sqrt(Screen.height * Screen.width) / 340f;
        //float diff = scale - 1;
        //scale = 1 + diff / 2f;
        instructionFont.fontSize = (int)(10 * scale);
        buttonFont.fontSize = (int)(18 * scale);
        MuteFont.fontSize = instructionFont.fontSize;
        UnMuteFont.fontSize = instructionFont.fontSize;
        emptyFont.fontSize = (int)(18 * scale);
        //newX = 32 * (scale);
        //font.fontSize = (int)(20 * scale);

        LoadSetting();
        if (PlaySound == 0)
            GameManager.SendMessage("UnMute");
        else
            GameManager.SendMessage("Mute");
    }

    // Update is called once per frame
    void Update()
    {
        if (menuSelection != 0 && remainingMenuDelayTime > 0)
        {
            remainingMenuDelayTime -= Time.deltaTime;
            if (remainingMenuDelayTime <= 0)
            {
                menuSelection = 0;
                GameManager.SendMessage("Stop");
            }
        }
    }

    private int adCounter = 0;
    private int adSwapAfter = 1;

    void Crashed()
    {
        adCounter++;
        remainingMenuDelayTime = MenuDelayTime;
        if (adCounter > adSwapAfter)
        {
            adCounter = 0;
        }
    }

    private int PlaySound = 0;
    private void SaveSetting()
    {
        PlayerPrefs.SetInt("Sound", PlaySound);
    }

    private void LoadSetting()
    {
        PlaySound = PlayerPrefs.GetInt("Sound");
    }

    void OnSoundButton(int WindowID)
    {
        GUIStyle selectedFont = PlaySound == 1 ? MuteFont : UnMuteFont;
        if (GUI.Button(new Rect(0, 0, MuteFont.fontSize * 2, MuteFont.fontSize * 2), "", selectedFont))
        {
            PlaySound = (PlaySound + 1) % 2;
            if (PlaySound == 0)
                GameManager.SendMessage("UnMute");
            else
                GameManager.SendMessage("Mute");
            SaveSetting();
        }
    }

    private bool pause;

    public bool Paused
    {
        get
        {
            return pause;
        }
        set
        {
            pause = value;
            if (pause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }


    void OnApplicationPause(bool pauseStatus)
    {
        Paused = true;
    }

    void OnPauseButton(int WindowID)
    {
        GUIStyle selectedFont = !Paused ? PauseFont : UnPauseFont;
        if (GUI.Button(new Rect(0, 0, MuteFont.fontSize * 2, MuteFont.fontSize * 2), "", selectedFont))
        {
            Paused = !Paused;
        }
    }

    void OnWindow(int windowID)
    {
        if (GUI.Button(new Rect(xPadding, buttonPadding, width - xPadding * 2, buttonHeight), "Start", buttonFont))
        {
            GameManager.SendMessage("Restart");
            menuSelection = 5;
            Paused = false;
        }

        if (GUI.Button(new Rect(xPadding, buttonHeight * 1 + buttonPadding * 2, width - xPadding * 2, buttonHeight), "Instructions", buttonFont))
        {
            menuSelection = 1;
        }

        if (GUI.Button(new Rect(xPadding, buttonHeight * 2 + buttonPadding * 3, width - xPadding * 2, buttonHeight), "Quit", buttonFont))
        {
            Application.Quit();
        }
    }

    void OnInstructions(int windowID)
    {
        string toDisplay = "";
        string buttonText = "";
        if (menuSelection == 1)
        {
            toDisplay = "The goal of Crystal Copter is to collect as many crystals as possible without crashing into terrain. \r\n\nHold down to accelerate upwards and release to fall.";
            buttonText = "Next";
        }
        else
        {
            toDisplay = "a Health dot is displayed in the top left corner. This will change color as you damage your copter. \r\n\nYou are allowed to bump into terrain twice before completely blowing up";
            buttonText = "Back";
        }

        GUI.Label(new Rect(xPadding, buttonPadding, width - xPadding * 2, buttonHeight * 2 + buttonPadding), toDisplay, instructionFont);

        if (GUI.Button(new Rect(xPadding, buttonHeight * 2 + buttonPadding * 3, width - xPadding * 2, buttonHeight), buttonText, buttonFont))
        {
            if (menuSelection == 1)
                menuSelection = 3;
            else
                menuSelection = 0;
        }
    }

    void OnCredits(int windowID)
    {

    }

    void OnGUI()
    {
        switch (menuSelection)
        {
            case (int)MenuState.MainMenu:
                GUI.Window(0, new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 1.75f, width, height), OnWindow, "", menuFont);
                break;
            case (int)MenuState.InstructionOne:
                GUI.Window(2, new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 1.75f, width, height), OnInstructions, "", menuFont);
                break;
            case (int)MenuState.Credits:
                GUI.Window(3, new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 1.75f, width, height), OnCredits, "", menuFont);
                break;
            case (int)MenuState.InstructionTwo:
                GUI.Window(2, new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 1.75f, width, height), OnInstructions, "", menuFont);
                break;
            case (int)MenuState.Playing:
                GUI.Window(10, new Rect(Screen.width - MuteFont.fontSize * 2 - 5, 5, MuteFont.fontSize * 2, MuteFont.fontSize * 2), OnPauseButton, "", emptyFont);
                break;
            default:
                break;
        }

        if (menuSelection != (int)MenuState.Playing)
            GUI.Window(20, new Rect(Screen.width - MuteFont.fontSize * 2 - 5, 5, MuteFont.fontSize * 2, MuteFont.fontSize * 2), OnSoundButton, "", emptyFont);

        if (Paused)
        {
            GUI.Window(15, new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - menuFont.fontSize * 2, width, menuFont.fontSize+5), OnPauseMessage, "Game Paused", emptyFont);
        }
    }

    void OnPauseMessage(int WindowID)
    {

    }
}
