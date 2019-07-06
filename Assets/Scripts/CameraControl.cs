using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    
    public float target=3;
   
	public void ScalingCamera()
    {
        target += 2;
       
    }

    private void Update()
    {

       if (target!= Camera.main.orthographicSize)
       {

            Camera.main.orthographicSize += 0.06f;
           if (Camera.main.orthographicSize>target)
           {
               Camera.main.orthographicSize = target;
           }
       }
        
    }
}
