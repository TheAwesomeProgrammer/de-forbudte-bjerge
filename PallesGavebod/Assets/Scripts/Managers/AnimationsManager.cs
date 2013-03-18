using UnityEngine;
using System.Collections;

public class AnimationsManager : MonoBehaviour {

    public bool Finish { get; set; }
    public bool RunForever { get; set; }
    public bool PlayingBackWards { get; set; }
    public bool PlayingNormal { get; set; }
    

	// Use this for initialization
	void Start () {
        Finish = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StopAnimation()
    {
        RunForever = false;
    }

    public IEnumerator MakeAnimation(Material[] pMaterialsToAnimate,Renderer pRendererToAnimate,float pTimeBetweenFrames,bool pRunForever,bool pPlayBackwards,bool Normalplay)
    {
        print("HELO");
        RunForever = pRunForever;
        Finish = false;
        while (RunForever)
        {
            
            for (int i = 0; i < pMaterialsToAnimate.Length; i++)
            {
               if(RunForever)
               {
                   pRendererToAnimate.material = pMaterialsToAnimate[i];
                   yield return new WaitForSeconds(pTimeBetweenFrames);
               }
            }
            for (int i = pMaterialsToAnimate.Length - 1; i >= 0; i--)
            {
                if (RunForever)
                {
                    pRendererToAnimate.material = pMaterialsToAnimate[i];
                    yield return new WaitForSeconds(pTimeBetweenFrames);
                }
            }
        }
        
        

         if(pPlayBackwards)
         {
             PlayingBackWards = true;
            for (int i = pMaterialsToAnimate.Length-1; i >=0; i--)
            {
              
                pRendererToAnimate.material = pMaterialsToAnimate[i];
                yield return new WaitForSeconds(pTimeBetweenFrames);

            }
             
                Finish = true;
            }
         else if(Normalplay)
         {
           
             PlayingNormal = true;
             for (int i = 0; i < pMaterialsToAnimate.Length; i++)
             {
                 pRendererToAnimate.material = pMaterialsToAnimate[i];
                 yield return new WaitForSeconds(pTimeBetweenFrames);

             }
             Finish = true;
         }

        PlayingBackWards = false;
        PlayingNormal = false;

    }


}
