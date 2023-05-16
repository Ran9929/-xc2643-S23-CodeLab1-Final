using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody rb;

    public float forceAmount = 200f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.transform.position.y < -7)
        {
            GameManager.Instance.LoseGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(Vector3.up * 500f);

        if (collision.gameObject.CompareTag("E0"))
        {
            Vector3 direction = Vector3.right + Vector3.back;
            rb.AddForce(direction * forceAmount);
            GameManager.Instance.Score++;
        }
        else if (collision.gameObject.CompareTag("E1"))
        {
            Vector3 direction = Vector3.back;
            rb.AddForce(direction * forceAmount);
            GameManager.Instance.Score++;
        }
        else if (collision.gameObject.CompareTag("E2"))
        {
            Vector3 direction = Vector3.right;
            rb.AddForce(direction * forceAmount);
            GameManager.Instance.Score++;
        }
        else if (collision.gameObject.CompareTag("myFloor"))
        {
            GameManager.Instance.LoseGame();
        }
    }
}
