using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] protected float moveSpeed = 1;

    string animName;

    protected bool isDeath = false;
    protected bool isRight;

    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }

    public void ChangedDirect(bool isRight)
    {
        if (isRight)
        {
            transform.rotation = Quaternion.identity; //Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else 
        {
            transform.rotation = Quaternion.Euler(Vector2.up * 180); //Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    public virtual void Hit()
    {
        isDeath = true;
    }
}
