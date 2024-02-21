using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{

    public Light spotLight;
   

    

    public void ToggleSpotLight()
    {
        if (spotLight != null)
            spotLight.enabled = !spotLight.enabled;
    }

   
}
