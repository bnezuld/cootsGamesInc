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
    private float preAnglVelocisty;

    public float jumpScaler;
    public float jumpLimit;
    public float jumpMulti;
    public float jumpForward;
    public float jumpMin;

    private SpriteRenderer spriteRend;

    public Sprite defaultSprite;
    public Sprite[] walking;

    private float walkState = 0;

    
    public Sprite[] jumping;
    

    void Awake()
    {
        // rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        // rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Vector2 scale = GetComponent<Collider2D>().bounds.extents * 2;
        float RectWidth = scale.x;
        float RectHeight = scale.y;
        // Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "ground" && !isGrounded){
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
            if (contactPoint.y > center.y && //checks that circle is on top of rectangle
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
                Debug.Log("top");
            }
            else if (contactPoint.y < center.y &&
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
                Debug.Log("bottom");
                isGrounded = true;
            }
            else if (contactPoint.x > center.x &&
                (contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2)) {
                Debug.Log("right");
                Debug.Log("wall" + _rigidbody.velocity + " a:" + _rigidbody.angularVelocity);
                _rigidbody.velocity = new Vector2(-preVelocity.x*0.5f,preVelocity.y);
                spriteRend.flipX = false;
                Debug.Log("wall2" + _rigidbody.velocity + " a:" + preAnglVelocisty);
            }
            else if (contactPoint.x < center.x &&
                (contactPoint.y <= center.y + RectHeight / 2 && contactPoint.y >= center.y - RectHeight / 2)) {
                Debug.Log("left");
                Debug.Log("wall" + _rigidbody.velocity + " a:" + _rigidbody.angularVelocity);
                spriteRend.flipX = true;
                _rigidbody.velocity = new Vector2(-preVelocity.x*0.5f,preVelocity.y);
                Debug.Log("wall2" + _rigidbody.velocity + " a:" + preAnglVelocisty);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // if(collision.gameObject.tag == "ground"){
        //     isGrounded = false;
        // }
    }

    void OnDrawGizmos()
    {
        Vector2 pos = GetComponent<Collider2D>().bounds.center;
        Vector2 scale = GetComponent<Collider2D>().bounds.extents * 2+ new Vector3(.01f,.01f, 0);
        // scale = scale * 
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(pos.x, pos.y, 0), new Vector3(scale.x, scale.y, 1));
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        preVelocity = _rigidbody.velocity;
        preAnglVelocisty = _rigidbody.angularVelocity;
        Vector2 pos = GetComponent<Collider2D>().bounds.center;
        Vector2 scale = GetComponent<Collider2D>().bounds.extents * 2+ new Vector3(.001f,.001f, 0);
        Collider2D[] Colliders = Physics2D.OverlapBoxAll(pos,
                                    scale,
                                    0.0f);
        bool found = false;
        for(int i = 0; i < Colliders.Length; i++)
        {
            // Debug.Log(Colliders[i].gameObject.tag);
            if(Colliders[i].gameObject.tag == "ground"){
                //isGrounded = true;
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
                _rigidbody.velocity = new Vector2(0, 0);
                spriteRend.sprite = jumping[0];
                jump += Time.deltaTime * jumpScaler;
                if(jump < jumpMin)
                {
                    jump = jumpMin;
                }
                if(jump > jumpLimit)
                {
                    jump = jumpLimit;
                }
            }
        }else if(isGrounded && jump > 0)
        {
            if(jump > jumpMin)
            {
                Debug.Log("jump: " + jump);
                _rigidbody.AddForce(transform.up * jump * jumpMulti);
                if(Input.GetKey("d"))
                {
                    _rigidbody.AddForce(transform.right * jumpForward);
                }
                if(Input.GetKey("a"))
                {
                    _rigidbody.AddForce(-transform.right * jumpForward);
                }  
            } 
            jump = 0;
        }  
        else if(jump == 0 && isGrounded)
        {
 
            if(Input.GetKey("d"))
            {
                walkState+=Time.deltaTime*4;
                _rigidbody.velocity = new Vector2(speed, 0);
                // transform.position += transform.right * speed * Time.deltaTime;
            }
            if(Input.GetKey("a"))
            {
                walkState+=Time.deltaTime*4;
                _rigidbody.velocity = new Vector2(-speed, 0);
                // transform.position += -1 * transform.right * speed * Time.deltaTime;
            }

            if(walkState > walking.Length)
            {
                walkState = 0;
            }
            if(!Input.GetKey("d") && !Input.GetKey("a"))
            {
                _rigidbody.velocity = new Vector2(0, 0);
                spriteRend.sprite = defaultSprite;
            }else
            {
                spriteRend.sprite = walking[(int)Mathf.Floor(walkState)];
            }
        }

        if(isGrounded)
        {
            if(Input.GetKey("d"))
            {
                spriteRend.flipX = true;
            }
            if(Input.GetKey("a"))
            {
                spriteRend.flipX = false;
            }
        }else
        {
            spriteRend.sprite = jumping[1];
        }
    }
}
