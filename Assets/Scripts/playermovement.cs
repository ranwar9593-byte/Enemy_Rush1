using UnityEngine;

public class playermovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public Rigidbody2D myRigidbody;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); 
        myRigidbody.linearVelocity = new Vector2(horizontalInput * speed, myRigidbody.linearVelocity.y);

        //Flip Player when it move to left and Right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            jump();
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void jump()
    {
        myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") 
            grounded = true;
    }
}
