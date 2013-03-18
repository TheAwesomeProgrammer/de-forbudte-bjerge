using UnityEngine;
using System.Collections.Generic;

public class PlaySounds : MonoBehaviour
{
  
    public AudioSource[] AudioSourcesMusic;
    public AudioSource[] AudioSourcesSounds;

    public bool Mute;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    PlaySomeRandomMusic();
        if(Input.GetKeyDown(KeyCode.M))
        {
            SwicthMusic();
        }

     
            foreach (AudioSource tAudioSource in AudioSourcesMusic)
            {
                tAudioSource.mute = Mute;
            }
            foreach (AudioSource tAudioSource in AudioSourcesSounds)
            {
                tAudioSource.mute = Mute;
            }
        MakeSureOnlyOneTrackPlaying();

	}


    public void SetMusicVolume(float pVolume)
    {
        foreach (AudioSource tAudioSource in AudioSourcesMusic)
        {
            tAudioSource.volume = pVolume;
        }
    }

    public void SetSoundVolume(float pVolume)
    {
        foreach (AudioSource tAudioSource in AudioSourcesSounds)
        {
            tAudioSource.volume = pVolume;
        }
    }


    void MakeSureOnlyOneTrackPlaying()
    {
        List<AudioSource> tListAudioSources = new List<AudioSource>();
        foreach (AudioSource tAudioSource in AudioSourcesMusic)
        {
            if (tAudioSource.isPlaying)
            {
              tListAudioSources.Add(tAudioSource);
            }
        }
        if(tListAudioSources.Count >= 2)
        {
            for (int number = 0; number < tListAudioSources.Count; number++)
            {
                tListAudioSources.Remove(tListAudioSources[number]);
            }
        }
    }

    

    void PlaySomeRandomMusic()
    {
        bool Playing = false;
        foreach (AudioSource tAudioSource in AudioSourcesMusic)
        {
            if(tAudioSource.isPlaying)
            {
                Playing = true;
            }
        }

    if(!Playing)
    {
         int tRandomNumber = Random.Range(0, AudioSourcesMusic.Length);

        for(int i = 0;i < AudioSourcesMusic.Length;i++)
        {
            if(tRandomNumber == i)
            {
                AudioSourcesMusic[i].Play();
            }
        }
    }
}

    public void PlaySound(string name)
    {
        foreach (AudioSource tAudioSource in AudioSourcesSounds)
        {
            if(tAudioSource.name == name)
            {
                tAudioSource.Play();
            }
        }
    }

    void SwicthMusic()
    {
        bool Playing = false;
        AudioSource tAudioSourceRem = null;
        foreach (AudioSource tAudioSource in AudioSourcesMusic)
        {
            if (tAudioSource.isPlaying)
            {
                tAudioSource.Stop();
                tAudioSourceRem = tAudioSource;
            }
        }
    if(tAudioSourceRem != null)
    {
        int AudioSourceToPlay = 0;

        for (int i = 0; i < AudioSourcesMusic.Length; i++)
        {
            if(AudioSourcesMusic[i] == tAudioSourceRem)
            {
                if ((AudioSourceToPlay + (i + 1)) == AudioSourcesMusic.Length)
                {
                    AudioSourceToPlay = 0;
                }
                else
                {
                    AudioSourceToPlay = i + 1;
                }
               
            }
        }
        AudioSourcesMusic[AudioSourceToPlay].Play();
    }
       
    }
}
