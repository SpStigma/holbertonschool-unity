using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
       GetComponent<Rigidbody2D>().velocity = transform.up * speed;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
