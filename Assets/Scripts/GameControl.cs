using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int AsteroidCount = 20;
    public GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0.0f));
        Vector3 pos;

        for (int i = 0; i < AsteroidCount; i++) 
        {
            pos = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0);
            Instantiate(asteroid, pos, Quaternion.identity);
        }
    }

}
