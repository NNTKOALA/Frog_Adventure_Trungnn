using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Animator anim;
    bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isActive == false)
        {
            collision.GetComponent<Player>().SaveCheckPoint(transform.position);
            anim.SetTrigger("active");
        }
    }
}
