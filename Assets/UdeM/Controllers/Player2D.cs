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

                if(_axisH > 0)
                {
                    _isFacingRight = true;
                }
                else if(_axisH < 0)
                {
                    _isFacingRight = false;
                }

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

        public void Rebote()
        {
            // empuja al jugador hacia arriba con la velocidad lateral que traia
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, velocidadRebote.y);
        }

        public void Rebote(Vector2 puntoGolpe)
        {
            // empuja al jugador dependiendo del punto con el que haya golpeado con el enemigo y con una velocidad determinada por velocidadRobote
            _rb2d.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
        }

        public void Escalar()
        {
            // verifica si la capa del objeto con el que esta colisionando es Escaleras
            if ((_axisV != 0 || escalando) && (_boxCollider.IsTouchingLayers(LayerMask.GetMask("Escaleras"))))
            {
                // determina la velocidad a la que va a subir la escalera
                Vector2 velocidadSubida = new Vector2(_rb2d.velocity.x, _axisV * velocidadEscalar);

                _rb2d.velocity = velocidadSubida;
                
                // se desactiva la gravedad
                _rb2d.gravityScale = 0;

                // seteamos la bandera en true
                escalando = true;
            }
            else
            {
                // se reactiva la gravedad
                _rb2d.gravityScale = gravedadInicial;
                // seteamos la bandera en false
                escalando = false;
            }

            if(_isGrounded)
            {
                // seteamos la bandera en false
                escalando = false;
            }
        }
    }
}
