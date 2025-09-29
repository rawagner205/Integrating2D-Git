using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 10f;
    [SerializeField] float lateralSpeed = 5f;
    Rigidbody2D rb;

    public GameObject explosionEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float steer = 0f;

        // Left and Right steering
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            steer = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            steer = 1f;
        }

        // Apply movement and rotation 
        
        float steerAmount = steer * lateralSpeed * Time.deltaTime;

        Debug.Log("Forward Speed: " + forwardSpeed);
        if (forwardSpeed <= 0.0f)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            float moveAmount = forwardSpeed * Time.deltaTime;
            transform.Translate(0, moveAmount, 0);
            transform.Translate(steerAmount, 0, 0);
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If collided with Boost, increase maneuvering speed
        if (collision.CompareTag("Boost"))
        {
            lateralSpeed = 20f;
            Destroy(collision.gameObject);
        }

        //When finish line is crossed, stop all sprite movement
        if (collision.CompareTag("Finish"))
        {
            lateralSpeed = 0f;
            forwardSpeed = -1.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy player sprite with explosion on collision
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject.transform.parent.gameObject);

    }
}
