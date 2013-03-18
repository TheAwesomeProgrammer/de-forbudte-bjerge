//FadeIn Script for Unity - Zerofractal 2006 
var buttonName="Jump";
var fadeDuration:float=0.5;
var FadeOut : boolean;
var FadeIn : boolean;

private var timeLeft:float=0.5;
 
function Awake () {
    timeLeft = fadeDuration;
}
 
function Update () {
 
    WhatToFade();
}

function WhatToFade()
{
    if(FadeOut && !FadeIn)
    {
        
        fade(false);
    }
    else if(FadeOut && FadeIn)
    {
        FadeIn = true;
        FadeOut = false;
        yield new WaitForSeconds(fadeDuration);
        FadeIn = false;
        FadeOut = true;
    }
    else if(FadeIn && !FadeOut)
    {
        fade(true);
    }
}
 
function fade(direction:boolean){
    var alpha;
    timeLeft = timeLeft - Time.deltaTime;
    if (direction){
        if (guiElement.color.a < 0.5){
            timeLeft = timeLeft - Time.deltaTime;
            alpha = (timeLeft/fadeDuration);
            guiElement.color.a=0.5-(alpha/2);
        } else {
            timeLeft = fadeDuration;
        }
    } else {
     
        if (guiElement.color.a > 0)
        {
            alpha = (timeLeft/fadeDuration);
            guiElement.color.a=alpha/2;
        } 
        else 
        {
            timeLeft = fadeDuration;
        }

    }

}