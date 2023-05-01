using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnenmyControl : MonoBehaviour
{
    public float speed = 1f;
    public float direction = 1f;
    public float ylow = -7f;
    public float yhigh = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= ylow)
        {
            direction = 1f;
        }
        if (transform.position.y >= yhigh)
        {
            direction = -1f;
        }

        Vector3 yPos = transform.position;
        yPos.y += speed * direction * Time.deltaTime;
        transform.position = yPos;
    }
}
