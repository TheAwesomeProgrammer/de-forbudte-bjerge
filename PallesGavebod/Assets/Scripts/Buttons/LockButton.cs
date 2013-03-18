using UnityEngine;
using System.Collections;

public class LockButton : Button
{

    public Material[] Materials;
    public float TimeBetweenFrames;

    
    private AnimationsManager mAnimationManager;

    private bool mMoveToHidingPlace;

    public bool Locked;

    // Use this for initialization
   
        // Update is called once per frame
        void Update()
        {
            mAnimationManager = GetComponentInChildren<AnimationsManager>();
         
            if (KeyPressed)
            {
                  StartCoroutine(mAnimationManager.MakeAnimation(Materials, renderer, TimeBetweenFrames, false, false, true));
               
                KeyPressed = false;
                mMoveToHidingPlace = true;
            }
            if (mAnimationManager.Finish && mMoveToHidingPlace)
            {
                MoveToPosition();
                mMoveToHidingPlace = false;
                Locked = false;
            }
        

        }

        public void Lock()
        {
            transform.position = mStartPosition;
            renderer.material = NormalMaterial;
            Locked = true;
        }
        public void Unlock()
        {
            transform.position = MoveToVector3;
            renderer.material = HoverMaterial;
            Locked = false;
        }

    
}
