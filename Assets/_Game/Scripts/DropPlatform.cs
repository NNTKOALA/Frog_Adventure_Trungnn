using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D collider;

    Vector2 startPoint;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        collider.isTrigger = true;
        Invoke(nameof(OnReset), 3f);
    }

    private void OnReset()
    {
        transform.position = startPoint;
        rb.bodyType = RigidbodyType2D.Dynamic;
        collider.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0.05f)
        {
            Invoke(nameof(Drop), 0.5f);
        }
    }
}
