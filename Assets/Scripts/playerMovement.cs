using UnityEngine;

 
public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;


    private void Awake()
    {
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider =GetComponent<BoxCollider2D>();
    }
 
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        //flip player when moving left-right
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one * 3;
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-3,3,3);
        

        if (Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();

        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }
    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        anim.SetTrigger("jump");
    }

    private bool isGrounded(){
      RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
      return raycastHit.collider != null;
    }
    public bool canAttack(){
        return horizontalInput == 0 && isGrounded();
    }
}
