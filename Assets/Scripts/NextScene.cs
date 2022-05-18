using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public Animator animator;


    new private BoxCollider2D collider;
    private Bounds bounds;
    public float rayLength;
    public LayerMask collisionMask;
    public float closingDuration;
    public bool startBlinking = false;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        bounds = collider.bounds;


    }
    private void Update()
    {   
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(bounds.min.x, bounds.max.y), Vector2.up, rayLength, collisionMask);
        if(hit)
        {
            if(hit.transform.gameObject.CompareTag("Player"))
            {
                startBlinking = true;
                animator.Play("blinkCloseAnim");
                animator.SetFloat("multiplier", 1);
            }
        }
        if(startBlinking)
        {
            closingDuration -= Time.deltaTime;
        }
        if(closingDuration <= 0)
        {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
