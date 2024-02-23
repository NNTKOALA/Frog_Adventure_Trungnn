using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float forceJump = 120;
    [SerializeField] ParticleSystem jumpVFX;


    private Vector3 savePoint;
    bool isUseButton;
    float direct;
    int strawBerry = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*ChangeAnim("run");*/
        savePoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        //set van toc
        if (!isUseButton)
        {
            float direct = Input.GetAxis("Horizontal");
        }
#endif

        Control(direct);

        HandleDirect(direct);

        HandleAnimation(direct);

        if (!isDeath)
        {
            return;
        }
    }

    public void SetDirectButton(float direct)
    {
        this.direct = direct;
        isUseButton = direct != 0 ;
    }

    public void JumpButton()
    {
        JumpForce(forceJump);
    }

    private void Control(float direct)
    {
        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpForce(forceJump);        
        }

        rb.velocity = Vector2.right * direct * moveSpeed + Vector2.up * rb.velocity.y;
    }

    private void HandleDirect(float direct)
    {
        //xoay huong nhan vat
        if (direct != 0)
        {
            ChangedDirect(direct > 0);
        }
    }

    private void HandleAnimation(float direct)
    {
        //dieu khien anim
        if (Mathf.Abs(rb.velocity.y) > 0.05f && rb.velocity.y > 0)
        {
            ChangeAnim("jump");
        }
        else if (Mathf.Abs(rb.velocity.y) > 0.05f && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
        }
        else //Mathf.Abs(rb.velocity.y) <= 0.05f
        {
            if (direct == 0)
            {
                ChangeAnim("idle");
            }
            else
            {
                ChangeAnim("run");
            }
        }
    }



    public void SaveCheckPoint(Vector3 newCheckPOint)
    {
        savePoint = newCheckPOint;
    }

    public void LoadCheckPoint()
    {
        ChangeAnim("idle");
        isDeath = false;
        transform.position = savePoint;
    }

    public override void Hit()
    {
        base.Hit();
        rb.velocity = Vector2.zero;
        ChangeAnim("hit");
        Invoke(nameof(LoadCheckPoint), 1f);
    }

    internal void JumpForce(float force)
    {
        if (Mathf.Abs(rb.velocity.y) <= 0.05f)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * force);
            Instantiate(jumpVFX, transform.position + Vector3.down * 0.07f, Quaternion.identity);
        }
    }

    public void AddStrawBerry(int amount = 1)
    {
        strawBerry += amount;

        UIManager.Instance.SetStrawberry(strawBerry);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isDeath)
        {
            collision.gameObject.GetComponent<Enemy>().Hit();
            JumpForce(forceJump);
        }
    }
}
