using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]

public class Player : MonoBehaviour
{
    public Animator animator;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float jumpHeight = 3;
    public float timeToJumpApex = .4f;
    float acceleartionTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 8;
    public LayerMask deathMask;
    private float coyoteTimerMax = 0.3f;
    private float coyoteTimerCurrent = 0.3f;
    float gravity;
    float jumpVelocity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;
    Vector3 velocity;



    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {
        if(controller.collisions.below)
        {
            coyoteTimerCurrent = coyoteTimerMax;
        }
        if (Mathf.Abs(velocity.y) > 0.2f)
        {
            coyoteTimerCurrent -= Time.deltaTime;
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (input.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (input.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && (controller.collisions.below || coyoteTimerCurrent > 0))
        {
            velocity.y = maxJumpVelocity;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }
        
        animator.SetFloat("Speed", Mathf.Abs(input.x));
        animator.SetFloat("VelocityY", velocity.y);

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:acceleartionTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 0.1f, deathMask);
        if (hit)
        {
            if (hit.transform.gameObject.CompareTag("Death"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}