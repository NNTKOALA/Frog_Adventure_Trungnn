using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStage : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float force;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().JumpForce(force);
            anim.SetTrigger("active");
        }
    }
}
