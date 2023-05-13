using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    private int points = 10;
    private GameControl control;
    void Start()
    {
        control = FindObjectOfType<GameControl>();
        GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0) * Random.Range(1, 10));
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            control.AddPoints(points);
            control.asteroids.RemoveAt(control.asteroids.Count - 1);
            Destroy(gameObject);                        // TODO: update the score
        }
    }
}
