using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCharacter : MonoBehaviour
{
    public float speed = 5.0f;
    public float thrust = 200.0f;
    private bool isGrounded = false;
    

    void Awake()
    {
        // rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        // rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "ground"){
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground"){
            isGrounded = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey("d"))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if(Input.GetKey("a"))
        {
            transform.position += -1 * transform.right * speed * Time.deltaTime;
        }
        if(Input.GetKey("w") || Input.GetKey("space"))
        {
            if(isGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);
            }
        }        
        if(Input.GetKey("s"))
        {
            transform.position += -1 * transform.up * speed * Time.deltaTime;
        }
    }
}
