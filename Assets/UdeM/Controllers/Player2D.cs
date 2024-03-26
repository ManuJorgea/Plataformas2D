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
            _jumpForce = 500F;

        }
        
        
        protected override void Update() {
            base.Update();

            _axisH = Input.GetAxisRaw("Horizontal");

            _currentSpeed = _axisH * _speed;

            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Crouch(true);
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {

                Crouch(false);
            }

            _rb2d.velocity = new Vector2(_currentSpeed, _rb2d.velocity.y);

        }

        protected override void LateUpdate() {
            base.LateUpdate();

        }

        protected override void Jump() {
            base.Jump();

            _rb2d.AddForce(Vector2.up * _jumpForce);


        }


    }
}
