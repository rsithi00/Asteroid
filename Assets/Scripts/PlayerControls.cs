using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;      // Use this if you swap scenes

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float maxThrust = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private GameObject bullet;

    private Rigidbody2D body;
    private bool applyThrust = false;
    private float currentRotation = 0;


    // Start is called before the first frame update
    void Start()
    {

        body = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (FindObjectOfType<GameControl>().asteroids.Count == 0)
        {
            FindObjectOfType<GameControl>().LevelComplete();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }

    }

    void FixedUpdate()
    {
        // Add rotation if necessary
        transform.Rotate(Vector3.forward * currentRotation * rotationSpeed);

        // Add thrust if necessary
        if (applyThrust) body.AddRelativeForce(Vector3.up * maxThrust);

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Create a bullet at our location and rotation

            Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    public void OnThrust(InputAction.CallbackContext context)
    {


        if (context.started) applyThrust = true;
        else if (context.canceled) applyThrust = false;

    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        currentRotation = context.ReadValue<float>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("asteroid"))
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = 0;
            gameObject.SetActive(false);
            FindObjectOfType<GameControl>().Death(this);
        }

    }

}
