using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Monologue : MonoBehaviour
{

    new private BoxCollider2D collider;
    private Bounds bounds;
    public float rayLength;
    public LayerMask collisionMask;

    public int horizontalRayCount = 4;
    float horizontalRaySpacing;
    RaycastOrigins raycastOrigins;

    public string sentence;


    public GameObject subtitleMenuUI;
    public TextMeshProUGUI subtitle;
    private bool used = false;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        bounds = collider.bounds;
        CalculateRaySpacing();

        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
    }
    private void Update()
    {
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * rayLength, Color.red);

            if (hit)
            {
                Debug.Log("gfdgdfhndhg");
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    subtitle.text = sentence;
                    subtitleMenuUI.SetActive(true);
                    used = true;
                    break;
                }
            }
            else if (used)
            {
                subtitleMenuUI.SetActive(false);
                used = false;
            }
        }
        
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.x / (horizontalRayCount - 1);

    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

}
