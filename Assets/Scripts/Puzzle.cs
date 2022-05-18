using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Puzzle : MonoBehaviour
{
    private new TilemapCollider2D collider;
    private Material material;
    private float fadeChange;
    private float fade = 1f;
    private float msToNextChange;
    private float animMsLeft;
    private float direction = -1f;

    [SerializeField] private float interval;
    [SerializeField] private float delay;
    [SerializeField] private float animDuration;

    void Start()
    {
        collider = GetComponent<TilemapCollider2D>();
        material = GetComponent<TilemapRenderer>().material;
        msToNextChange = interval + delay - animDuration;
        animMsLeft = animDuration;
    }

    void FixedUpdate()
    {
        if (msToNextChange <= 0)
        {
            if (collider.enabled)
            {
                direction = -1f;
            }
            else
            {
                direction = 1f;
            }

            fade += direction * (1f / (animDuration / Time.fixedDeltaTime)); 
            material.SetFloat("_Fade", fade);
            animMsLeft -= Time.fixedDeltaTime;

            if (animMsLeft <= 0f)
            {
                if (collider.enabled)
                {
                    collider.enabled = false;
                }
                else
                {
                    collider.enabled = true;
                }
                msToNextChange = interval - animDuration;
                animMsLeft = animDuration;
            }
        }
        else
        {
            msToNextChange -= Time.fixedDeltaTime;
        }
    }
}