using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Camera GameCamera;
    public GameObject Player;
    public GameObject HealthBar;
    public GameObject Background;
    public GameObject Scoreboard;
    public GameObject TerrainController;
    public GameObject SoundManager;
    public GameObject SoundButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Restart()
    {
        Player.SendMessage("Restart");
        Scoreboard.SendMessage("Restart");
        HealthBar.SendMessage("Restart");
        Background.SendMessage("Restart");
        TerrainController.SendMessage("Restart");
        SoundButton.SendMessage("Hide");
    }

    void Score()
    {
        var score = Scoreboard.GetComponent<DrawScore>().Score();
        SoundManager.SendMessage("PlayPickup");

		int levelUpdateInterval = 20;
		var indexValue = Mathf.Max(Mathf.Min((score + levelUpdateInterval) / levelUpdateInterval, 6), 1);
        TerrainController.SendMessage("TerrainSwap", indexValue);
        Background.SendMessage("TerrainSwap", indexValue);
    }

    void HealthUpdate(int damage)
    {
        HealthBar.SendMessage("UpdateHP", damage);
        SoundManager.SendMessage("Damage");
    }

    void Crashed()
    {
        GameCamera.SendMessage("Crashed");
        SoundManager.SendMessage("Crashed");
    }

    void Stop()
    {
        Scoreboard.SendMessage("Stop");
        SoundButton.SendMessage("Show");
    }

    void Mute()
    {
        SoundManager.SendMessage("Mute");
    }

    void UnMute()
    {
        SoundManager.SendMessage("UnMute");
    }
}
