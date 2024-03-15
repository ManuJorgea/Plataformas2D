using System.Collections;
using System.Collections.Generic;
using UdeM.Controllers;
using UnityEngine;

public class JonDowController : Player2D
{
    private Animator _anim;

    protected override void Start() { 
        base.Start();

        _anim = transform.Find("Animations").GetComponent<Animator>();
    
    }

    protected override void Update()
    {
        base.Update();

        _anim.SetBool("IsWalking",(_currentSpeed != 0));
        _anim.SetBool("IsGrounded", _isGrounded);
        _anim.SetBool("IsFalling", _isFalling);
    }

    protected override void Jump()
    {
        base.Jump();
        _anim.SetTrigger("Jump");
    }
}
