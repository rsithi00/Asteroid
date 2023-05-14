using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    private GameControl control;
    public float size { get; set; }
    private Rigidbody2D rb;
    void Start()
    {
        control = FindObjectOfType<GameControl>();
        GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0) * Random.Range(1, 10));
        rb = GetComponent<Rigidbody2D>();
    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            if ((size * 0.5f) >= 0.65f)
            {
                Split();
                Split();
            }

            if (size < 0.7f)
            {
                control.AddPoints(100);
            }
            else if (size < 1.2f)
            {
                control.AddPoints(50);
            }
            else
            {

                control.AddPoints(25);
            }

            // control.asteroids.RemoveAt(control.asteroids.Count - 1);
            Destroy(gameObject);
            // GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().AsteroidsLeft -= 1;
        }

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("asteroid"))
        {
            if ((size * 0.5f) >= 0.5f)
            {
                Split();
                Split();

                // control.asteroids.RemoveAt(control.asteroids.Count - 1);
                Destroy(gameObject);
                // GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().AsteroidsLeft -= 1;

            }
            else
            {
                return;
            }
        }

    }

    public void Split()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().AsteroidSpawn(position, size * 0.25f);
        // GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().AsteroidsLeft += 1;

    }

}
