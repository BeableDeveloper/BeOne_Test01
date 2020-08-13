using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    private Touch touch;
    private float speedModifier;
    void Start()
    {
        speedModifier = 0.1f;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedModifier,                    
                    transform.position.y + touch.deltaPosition.y * speedModifier,
                    transform.position.z


                    ); ;
                
            }

        }  
    }
}
