using UnityEngine;
using System.Collections;

public class LoadGameButton : Button
{
    public float Speed = 10.0F;
    public Material LookMaterial;
    public Material NormalLookMaterial;
    public Material FallMaterial;

    private GameObject Ofelia;

    // Use this for initialization
    void Start()
    {
        Ofelia = GameObject.Find("Ofelia");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (KeyPressed)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);

        }
    }

    protected override void OnMouseEnter()
    {
        if (!KeyPressed)
            Ofelia.renderer.material = LookMaterial;
    }

    protected override void OnMouseExit()
    {
        if (!KeyPressed)
            Ofelia.renderer.material = NormalLookMaterial;
    }

    protected override void OnMouseDown()
    {
        KeyPressed = true;
        Ofelia.renderer.material = FallMaterial;
        Ofelia.rigidbody.useGravity = true;
    }

}
