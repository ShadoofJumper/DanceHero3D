using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private string levelMusic;
    private float activeMusicvolume = 1.0f;


    public void StartUp()
    {
        Debug.Log("Sound manager started...");

        status = ManagerStatus.Started;
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load("Music/" + levelMusic) as AudioClip);
    }

    public void PlayMusic(AudioClip clip)
    {
        activeMusicvolume = 1.0f;
        musicSource.volume = activeMusicvolume;
        musicSource.clip = clip;
        musicSource.volume = activeMusicvolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        StartCoroutine(StopMusicFade());
    }
    private IEnumerator StopMusicFade()
    {
        float scaleRate = 1.5f * activeMusicvolume;
        while (musicSource.volume > 0.0f)
        {
            musicSource.volume -= scaleRate * Time.deltaTime;
            yield return null;
        }

        musicSource.Stop();

    }
}
