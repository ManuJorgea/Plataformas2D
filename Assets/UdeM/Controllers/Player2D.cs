using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Controllers { 

    public class Player2D : Character2D 
    {
        protected float _axisH;

        protected override void Awake()
        {
            base.Awake();
            _jumpForce = 400F;

        }
        
        
        protected override void Update() {
            base.Update();

            _axisH = Input.GetAxisRaw("Horizontal");

            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        protected override void LateUpdate() {
            base.LateUpdate();

            _rb2d.velocity = new Vector2(_axisH * _speed, _rb2d.velocity.y);
        
        }

        protected override void Jump() {
            base.Jump();

            _rb2d.AddForce(Vector2.up * _jumpForce);


        }
    }
}
