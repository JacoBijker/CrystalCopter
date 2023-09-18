using UnityEngine;
using System.Collections;

public class TopRightCornerPlacement : MonoBehaviour
{
    public GameObject GameManager;
    public Camera mainCamera;

    public GUIStyle MuteFont;
    public GUIStyle UnmuteFont;

    private int currentTag = 2;
    private float lastPressTimer;
    // Use this for initialization
    void Start()
    {
        var topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - 32, Screen.height - 32, 10));
        this.transform.position = topRight;

        LoadSetting();

        if (currentTag == 2)
            UnMute();
		else
			Mute();
    }

    private void SaveSetting()
    {
        PlayerPrefs.SetInt("Sound", currentTag);
    }

    private void LoadSetting()
    {
		currentTag = PlayerPrefs.GetInt("Sound");
        if (currentTag == 0)
            currentTag = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPressTimer > 0)
        {
            lastPressTimer -= Time.deltaTime;
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (!hitCollider)
                return;

            lastPressTimer = 0.5f;

            switch (currentTag)
            {
                case 1:
                    UnMute();
                    break;
                case 2:
                    Mute();
                    break;
            }
        }
    }

    void Show()
    {
        SetSound(currentTag);
    }

    void Hide()
    {
        SetSound(0);
    }

    void Mute()
    {
        GameManager.SendMessage("Mute");
        currentTag = 1;
        SetSound(currentTag);
        SaveSetting();
    }

    void UnMute()
    {
        GameManager.SendMessage("UnMute");
        currentTag = 2;
        SetSound(currentTag);
        SaveSetting();
    }

    private void SetSound(int tag)
    {
        var allSprites = this.transform.GetComponentsInChildren<SpriteRenderer>();

        foreach (var sprite in allSprites)
        {
            sprite.enabled = sprite.tag.ToString().Equals(tag.ToString());
        }
    }
}
