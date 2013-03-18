@script RequireComponent(GUITexture)
 
static private var size = 0.0;
// at what screen height should the texture be it's preset size (as setup in the inspector)?
static private var minAtScreenHeight = 384;
// at what screen height should the texture be fully scaled by maxFactor? 
static private var maxAtScreenHeight = 768;
static private var maxFactor = 2;
 
static function GetSize () : float
    {
        if(size == 0)
        {
            var factor = Mathf.InverseLerp(minAtScreenHeight, maxAtScreenHeight, Screen.height);
            size = Mathf.Lerp(1, maxFactor, factor);
        }
        return size;
    }
 
 
    function Awake () 
    {
        var mySize = GetSize();
        gui = GetComponent(GUITexture) as GUITexture;
        gui.pixelInset.x *= mySize;
        gui.pixelInset.y *= mySize;			
        gui.pixelInset.width *= mySize;			
        gui.pixelInset.height *= mySize;			
    }