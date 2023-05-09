using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private Vector3 bottomLeft;
    private Vector3 topRight;

    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0.0f));
    }

    void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if(pos.x < 0.0)
            transform.position = new Vector3(topRight.x, transform.position.y, transform.position.z);
        else if (pos.x > 1.0) 
            transform.position = new Vector3(bottomLeft.x, transform.position.y, transform.position.z);
        
        if (pos.y < 0.0) 
            transform.position = new Vector3(transform.position.x, topRight.y, transform.position.z);
        else if (pos.y > 1.0) 
            transform.position = new Vector3(transform.position.x, bottomLeft.y, transform.position.z);
    }
}
