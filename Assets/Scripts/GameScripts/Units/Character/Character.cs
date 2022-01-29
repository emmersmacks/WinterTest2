using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    public float jumpForce;
    private SpriteRenderer spriteBody;
    private SpriteRenderer spriteFoot;
    private Rigidbody2D rb;
    private bool isGround;
    Vector3 movement;
    private bool waitRecharge;
    public GameObject inventory;  

    private States State
    {
        get { return (States)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteBody = transform.Find("Body").GetComponent<SpriteRenderer>();
        spriteFoot = transform.Find("Foot").GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isGround = false;
        waitRecharge = false;
        health = 100f;
    }

    void FixedUpdate()
    {
        Run();
    }

    void Update()
    {
        CheckGround();
        if (Input.GetButtonDown("Hit") && !waitRecharge) Hit();

        if (isGround) State = States.idle;
        else State = States.jump;

        if (Input.GetButton("Horizontal")) MoveHorizontal();
        if (Input.GetButtonDown("Vertical") && isGround) MoveVertical(jumpForce);
    }

    private void MoveHorizontal()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        spriteBody.flipX = movement.x < 0.0f;
        spriteFoot.flipX = movement.x < 0.0f;
        if (isGround)
        {
            State = States.go;
        }
    }

    private void Run()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(moveHorizontal, 0.0f);
        transform.Translate(movement * speed * Time.fixedDeltaTime);
    }

    private void MoveVertical(float jf)
    {
        rb.AddForce(Vector2.up * jf, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1F);
        isGround = colliders.Length > 1;
    }

    private void Hit()
    {
        StartCoroutine(WaitHit());
    }

    IEnumerator WaitHit()
    {
        waitRecharge = true;
        animator.SetBool("hit", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("hit", false);
        waitRecharge = false;
    }
}

public enum States
{
    idle,
    go,
    run,
    jump,
    hit,
}
    
