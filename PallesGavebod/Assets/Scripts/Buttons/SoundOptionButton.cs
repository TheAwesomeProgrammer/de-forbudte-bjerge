using UnityEngine;
using System.Collections;

public enum Volume
{
    Music,
    Sound,
    Mute
}

public class SaveMusicAndSound
{
   
}

public class SoundOptionButton : Button
{
    public Volume WhichVolume;

    public float MusicVolumeToSet;

    public float Id;
    
    public int Selected { get; set; }

   public override void Start()
    {
       //PlayerPrefs.DeleteAll();
        base.Start();
        Selected = Load();
        print("SELECTED "+Selected);
        }
    
	// Update is called once per frame
    void Update()
    {
       




        if (KeyPressed)
        {
                OtherSelected();
                Selected = 1;
                KeyPressed = false;
        }

        if (Selected == 1)
        {
           if (WhichVolume == Volume.Sound)
            {
             
                   GameObject.Find("SoundOptionManager").GetComponent<SoundOptionManager>().SetMaterialOnChildren(
                       new Color(0, 0, 0,  transform.parent.renderer.material.color.a));
               
                
                
                GameObject.Find("AudioManager").GetComponent<PlaySounds>().SetSoundVolume(MusicVolumeToSet);
            }
            else if (WhichVolume == Volume.Music)
            {
               
                    GameObject.Find("MusicOptionManager").GetComponent<SoundOptionManager>().SetMaterialOnChildren(
                        new Color(0, 0, 0, transform.parent.renderer.material.color.a));
                
                GameObject.Find("AudioManager").GetComponent<PlaySounds>().SetMusicVolume(MusicVolumeToSet);
            }

        }

        Save();
     }

    void OtherSelected()
    {
       foreach (SoundOptionButton tSoundOptionButton in transform.parent.GetComponentsInChildren<SoundOptionButton>())
        {
            if(tSoundOptionButton.Selected == 1 && tSoundOptionButton.gameObject != gameObject)
            {
               tSoundOptionButton.Selected = 0;
            }
        }
    }
    
    void Save()
    {
        PlayerPrefs.SetInt("Selected"+Id,Selected);
        PlayerPrefs.Save();
     }

    int Load()
    {
        return PlayerPrefs.GetInt("Selected"+Id);
    }

   

}
