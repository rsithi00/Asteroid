using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{ 
    [SerializeField] private float force = 5f;
    [SerializeField] private double lifespan = 1;      // How long can this thing live (in seconds)?
    [SerializeField] GameObject score;
    [SerializeField] AudioSource laser;

    private double ttl;                 // How much longer until we self-destruct?

    // Start is called before the first frame update
    void Start()
    {
        ttl = lifespan;
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.up * force);
        laser.Play();
    }

    // Update is called once per frame
    void Update()
    {
        ttl -= Time.deltaTime;
      
        if (ttl < 0.0) Destroy(gameObject);       // Destroy the gameObject tied to this script (the bullet itself)
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            Destroy(gameObject);                        // TODO: update the score

            
    }

}
