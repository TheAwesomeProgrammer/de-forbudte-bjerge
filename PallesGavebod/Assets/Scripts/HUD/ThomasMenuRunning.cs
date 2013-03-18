using UnityEngine;
using System.Collections;

public class ThomasMenuRunning : MonoBehaviour
{

    public float Speed = 10.0F;
    public float TimeToWaitBetweenFrames;

    public bool Run { get; set; }
    public bool ReachedEnd { get; set; }

    public Material ThomasLarsen1;
    public Material ThomasLarsen2;
    public Material ThomasLarsen3;
    public Material ThomasLarsen4;
    public Material ThomasLarsen5;
    public Material ThomasLarsen6;
    public Material ThomasLarsen7;
    public Material ThomasLarsen8;

    private bool mAnimationStart = true;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(mAnimationStart)
        {
            StartCoroutine(Animation());
        }
       
    if(transform.position.x < (Camera.main.ViewportToWorldPoint(new Vector2(1,0.5f)).x)+transform.lossyScale.x && Run)
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
    else if (transform.position.x > (Camera.main.ViewportToWorldPoint(new Vector2(1,0.5f)).x)+transform.lossyScale.x)
    {
        ReachedEnd = true;
    }

	}

    IEnumerator Animation()
    {
        mAnimationStart = false;
        renderer.material = ThomasLarsen2;
        yield return new WaitForSeconds(TimeToWaitBetweenFrames);
        renderer.material = ThomasLarsen3;
        yield return new WaitForSeconds(TimeToWaitBetweenFrames);
        renderer.material = ThomasLarsen4;
        yield return new WaitForSeconds(TimeToWaitBetweenFrames);
        renderer.material = ThomasLarsen5;
        yield return new WaitForSeconds(TimeToWaitBetweenFrames);
        renderer.material = ThomasLarsen6;
        yield return new WaitForSeconds(TimeToWaitBetweenFrames);
        renderer.material = ThomasLarsen7;
        yield return new WaitForSeconds(TimeToWaitBetweenFrames);
        renderer.material = ThomasLarsen8;
        yield return new WaitForSeconds(TimeToWaitBetweenFrames);
        renderer.material = ThomasLarsen1;
        yield return new WaitForSeconds(TimeToWaitBetweenFrames);
        mAnimationStart = true;

    }
}
