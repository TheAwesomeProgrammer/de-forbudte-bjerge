using UnityEngine;
using System.Collections;

public class MuteButton : Button {

    public bool Mute { get; set; }

    public Volume WhichVolume;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        if (KeyPressed)
        {
            
            if (!Mute)
            {
                if(WhichVolume == Volume.Sound)
                {
                    GameObject.Find("SoundOptionManager").GetComponent<SoundOptionManager>().SetAllToMuteChildren();
                    GameObject.Find("AudioManager").GetComponent<PlaySounds>().SetSoundVolume(0);

                }
                else if(WhichVolume == Volume.Music)
                {
                    GameObject.Find("MusicOptionManager").GetComponent<SoundOptionManager>().SetAllToMuteChildren();
                    GameObject.Find("AudioManager").GetComponent<PlaySounds>().SetMusicVolume(0);
                }
                renderer.material = HoverMaterial;
             
            }
           

            else if (Mute)
            {
                if (WhichVolume == Volume.Sound)
                {
                    GameObject.Find("SoundOptionManager").GetComponent<SoundOptionManager>().SetAllToNormalChildren();
                    GameObject.Find("AudioManager").GetComponent<PlaySounds>().SetSoundVolume(1);

                }
                else if (WhichVolume == Volume.Music)
                {
                    GameObject.Find("MusicOptionManager").GetComponent<SoundOptionManager>().SetAllToNormalChildren();
                    GameObject.Find("AudioManager").GetComponent<PlaySounds>().SetMusicVolume(1);
                }
              
                renderer.material = NormalMaterial;

            }
            Mute = !Mute;
            KeyPressed = false;
        }

	}
	}

