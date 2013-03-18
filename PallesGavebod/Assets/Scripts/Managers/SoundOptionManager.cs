using UnityEngine;
using System.Collections;

public class SoundOptionManager : MonoBehaviour
{

  
   
	// Use this for initialization
	public void Start ()
	{
        
       renderer.material.color = new Color(0,0,0,0);
       SetMaterialOnChildren(new Color(0,0,0,0));
	
	}
	
	// Update is called once per frame
	void Update ()
	{
     
    }

    public void DeleteAllKeys()
    {
        for(int i = 1;i<= 16;i++)
        {
            PlayerPrefs.DeleteKey("Selected"+i);
        }
    }



    Button FindButtonPressed()
    {
        SoundOptionButton tButtonPressed = null;
      foreach (SoundOptionButton tButton in transform.GetComponentsInChildren<SoundOptionButton>())
       {
           if (tButton.Selected == 1)
        {
            tButtonPressed = tButton;
        }
           
      }
        return tButtonPressed;
    }


   
   

   public void SetAllToNormalChildren()
    {
        foreach (SoundOptionButton tButton in transform.GetComponentsInChildren<SoundOptionButton>())
        {
            tButton.Selected = 0;
            tButton.renderer.material = tButton.NormalMaterial;
        }
    }

   public void SetAllToMuteChildren()
   {
       foreach (SoundOptionButton tButton in transform.GetComponentsInChildren<SoundOptionButton>())
       {
           tButton.Selected = 0;
           tButton.renderer.material = tButton.HoverMaterial;
       }
   }

    public void SetMaterialOnChildren(Color pColor)
    {
       foreach (SoundOptionButton tButton in transform.GetComponentsInChildren<SoundOptionButton>())
       {
          
         if(FindButtonPressed() != null)
            {
                if (FindButtonPressed().transform.position.x < tButton.transform.position.x)
                {
                    tButton.renderer.material = tButton.HoverMaterial;
                    tButton.HoverMaterial.color = new Color(tButton.HoverMaterial.color.r, tButton.HoverMaterial.color.g, tButton.HoverMaterial.color.b, pColor.a);
                }

                if (FindButtonPressed().transform.position.x >= tButton.transform.position.x)
                {
                    tButton.renderer.material = tButton.NormalMaterial;
                    tButton.NormalMaterial.color = new Color(tButton.NormalMaterial.color.r, tButton.NormalMaterial.color.g, tButton.NormalMaterial.color.b, pColor.a);
                }
            }
            
            
        }
    }
}
