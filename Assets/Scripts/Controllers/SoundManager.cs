using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private bool muted = false;

    public AudioClip[] pickupClips;
    public AudioClip[] damageClips;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayPickup()
    {
        if (muted)
            return;

        var index = Random.Range(0, pickupClips.Length);
        var audioSource = this.transform.GetComponent<AudioSource>() as AudioSource;

        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = pickupClips[index];
        audioSource.Play();
    }

    void Damage()
    {
        if (muted)
            return;

        var index = Random.Range(0, damageClips.Length);
        var audioSource = this.transform.GetComponent<AudioSource>() as AudioSource;

        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = damageClips[index];
        audioSource.Play();
    }

    void Crashed()
    {

    }

    

    void Mute()
    {
		var soundManagers = GetComponentsInChildren<AudioSource>();
		foreach(var soundManager in soundManagers)
			if (soundManager.name.Equals("BackgroundMusic", System.StringComparison.OrdinalIgnoreCase))
				soundManager.Stop();

        muted = true;
    }

    void UnMute()
    {
		var soundManagers = GetComponentsInChildren<AudioSource>();
		foreach(var soundManager in soundManagers)
			if (soundManager.name.Equals("BackgroundMusic", System.StringComparison.OrdinalIgnoreCase))
				soundManager.Play();

        muted = false;
    }

}
