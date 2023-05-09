using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0) * Random.Range(1, 10));
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
            Destroy(gameObject);                        // TODO: update the score
    }
}
