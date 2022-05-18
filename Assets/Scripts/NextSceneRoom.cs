using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneRoom : MonoBehaviour
{
    public Animator animator;


    new private BoxCollider2D collider;
    private Bounds bounds;
    public float rayLength;
    public LayerMask collisionMask;


    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        bounds = collider.bounds;

        animator.Play("blinkOpenAnim");

    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(bounds.min.x, bounds.max.y), Vector2.up, rayLength, collisionMask);
        if (hit)
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
        }
       
    }

}
