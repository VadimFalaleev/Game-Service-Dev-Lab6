using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource music;
    private static float musicVolume = 1;

    private void Start()
    {
        music = GetComponent<AudioSource>();
    }

    private void Update()
    {
        music.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}