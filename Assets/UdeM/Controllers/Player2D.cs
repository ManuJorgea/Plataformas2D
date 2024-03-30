using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Controllers { 

    public class Player2D : Character2D 
    {
        [SerializeField] private Vector2 velocidadRebote;

        protected CapsuleCollider2D _capsuleCollider;
        protected BoxCollider2D _boxCollider;

        protected float _axisH;
        protected float _axisV;

        protected override void Awake()
        {
            base.Awake();
            _jumpForce = 500F;
        }   

        protected override void Update() {
            base.Update();

            if(permitirMov)
            {
                _axisH = Input.GetAxisRaw("Horizontal");
                _axisV = Input.GetAxisRaw("Vertical");
                
                Escalar();

                _currentSpeed = _axisH * _speed;

                if (_isGrounded && Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    Crouch();
                }

                _rb2d.velocity = new Vector2(_currentSpeed, _rb2d.velocity.y);
            }
        }

        protected override void LateUpdate() {
            base.LateUpdate();

        }

        protected override void Jump() {
            base.Jump();

            _rb2d.AddForce(Vector2.up * _jumpForce);

        }

        public void Rebote()
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, velocidadRebote.y);
        }

        public void Rebote(Vector2 puntoGolpe)
        {
            _rb2d.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
        }

        public void Escalar()
        {
            if ((_axisV != 0 || escalando) && (_capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Escaleras"))))
            {
                Vector2 velocidadSubida = new Vector2(_rb2d.velocity.x, _axisV * velocidadEscalar);

                _rb2d.velocity = velocidadSubida;
                _rb2d.gravityScale = 0;

                escalando = true;
            }
            else
            {
                _rb2d.gravityScale = gravedadInicial;
                escalando = false;
            }

            if(_isGrounded)
            {
                escalando = false;
            }
        }
    }
}
