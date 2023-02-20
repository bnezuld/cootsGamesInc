using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpKitty : GameWinCondition
{
    public float speed = 5.0f;
    public float thrust = 200.0f;
    public bool isGrounded = false;

    private float jump;

    public GameObject gameCamera;

    private Rigidbody2D _rigidbody;
    private Vector2 preVelocity;
    

    void Awake()
    {
        // rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        // rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool collideFromLeft;
        bool collideFromTop;
        bool collideFromRight;
        bool collideFromBottom;

        float RectWidth = 1;
        float RectHeight = 1;
        // Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "ground"){
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f; 

            Debug.Log("points: " +  collision.contacts.Length);
            Vector2 contactPoint = new Vector3(0,0,0); 
            for(int i = 0; i < collision.contacts.Length; i++)
            {
                Debug.Log("points"+i+": " +  collision.contacts[i].point);
                contactPoint = contactPoint + collision.contacts[i].point;
            }
            contactPoint = contactPoint / collision.contacts.Length;
            Vector2 center = GetComponent<Collider2D>().bounds.center;
            Debug.Log("c: " + center + " cont:" + contactPoint + " h:" + RectHeight);
            Debug.Log(contactPoint.y +"<"+ center.y +"+"+ RectHeight +"/"+ 2 +"&&"+ contactPoint.y +">"+ center.y +"-"+ RectHeight +"/"+ 2);
            Debug.Log("value:" + (center.y - RectHeight / 2));
            Debug.Log((contactPoint.x > center.x) + " " + (contactPoint.y < center.y + RectHeight / 2) + " " + (contactPoint.y > center.y - RectHeight / 2));
            if (contactPoint.y > center.y && //checks that circle is on top of rectangle
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
                Debug.Log("top");
                collideFromTop = true;
            }
            else if (contactPoint.y < center.y &&
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
                Debug.Log("bottom");
                collideFromBottom = true;
                isGrounded = true;
            }
            else if (contactPoint.x > center.x &&
                (contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2)) {
                Debug.Log("right");
                // collideFromRight = true;
                            Debug.Log("wall" + _rigidbody.velocity);
            _rigidbody.velocity = new Vector2(-preVelocity.x,_rigidbody.velocity.y);
            Debug.Log("wall2" + _rigidbody.velocity);
            }
            else if (contactPoint.x < center.x &&
                (contactPoint.y <= center.y + RectHeight / 2 && contactPoint.y >= center.y - RectHeight / 2)) {
                // collideFromLeft = true;
                Debug.Log("left");
                _rigidbody.velocity = new Vector2(-preVelocity.x,_rigidbody.velocity.y);
            }
        }
        if(collision.gameObject.tag == "wall"){
            Debug.Log("wall" + _rigidbody.velocity);
            _rigidbody.velocity = new Vector2(-preVelocity.x,_rigidbody.velocity.y);
            Debug.Log("wall2" + _rigidbody.velocity);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // if(collision.gameObject.tag == "ground"){
        //     isGrounded = false;
        // }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        preVelocity = _rigidbody.velocity;
        Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 scale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        Collider2D[] Colliders = Physics2D.OverlapBoxAll(pos,
                                    scale,
                                    0.0f);
        bool found = false;
        for(int i = 0; i < Colliders.Length; i++)
        {
            // Debug.Log(Colliders[i].gameObject.tag);
            if(Colliders[i].gameObject.tag == "ground"){
                isGrounded = true;
                found = true;
            }
        }
        if(!found)
        {
            isGrounded = false;
        }

        Vector3 velocity = new Vector3(0,0,0);
        gameCamera.transform.position = Vector3.SmoothDamp(gameCamera.transform.position, transform.position + new Vector3(_rigidbody.velocity.x/2, (_rigidbody.velocity.y/2)+1f,-10), ref velocity, .1f);
        // gameCamera.transform.position = transform.position + new Vector3(0,0,-10);
        if(Input.GetKey("w") || Input.GetKey("space"))
        {
            if(isGrounded)
            {
                jump += Time.deltaTime * 200;
                if(jump > 125)
                {
                    jump = 125;
                }
            }
        }else if(isGrounded && jump > 0)
        {
            Debug.Log("jump: " + jump);
            _rigidbody.AddForce(transform.up * jump * 3f);
            if(Input.GetKey("d"))
            {
                _rigidbody.AddForce(transform.right * jump);
            }
            if(Input.GetKey("a"))
            {
                _rigidbody.AddForce(-transform.right * jump);
            }   
            jump = 0;
        }  
        if(jump == 0 && isGrounded)
        {
 
            if(Input.GetKey("d"))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            if(Input.GetKey("a"))
            {
                transform.position += -1 * transform.right * speed * Time.deltaTime;
            }    
            if(Input.GetKey("s"))
            {
                transform.position += -1 * transform.up * speed * Time.deltaTime;
            }
        }
    }
}
