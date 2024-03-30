using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Controllers
{
    public class Character2D : EntityController
    {
        [SerializeField] protected float velocidadEscalar;
        protected bool permitirMov = true;

        protected float gravedadInicial;
        protected bool escalando;

        protected Rigidbody2D _rb2d;

        protected bool _isFacingRight = true;

        protected override void LateUpdate() {  
            base.LateUpdate();
            _isFalling = (_rb2d.velocity.y < 0 && !_isGrounded && !escalando); 
        
        }


        protected override void FixedUpdate() { 
            base.FixedUpdate();

            if (_currentSpeed > 0 ) {

                _isFacingRight = true;           
            
            } else if (_currentSpeed < 0){

                _isFacingRight = false;
            
            }
            Flip();        
        }

        protected void Flip()
        {

            if (_isFacingRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
            } else {
                transform.localScale = new Vector3(-1, 1, 1);

            }
        
        
        }

        protected override void Start() {
            base.Start();
            _rb2d = GetComponent<Rigidbody2D>();
            if ( _rb2d == null ) {
                _rb2d = gameObject.AddComponent<Rigidbody2D>();
                _rb2d.freezeRotation = true;
            }

            gravedadInicial = _rb2d.gravityScale;
        }


        protected virtual void ActivateGrounded (Collision2D other, bool activate = true) {

            if (other.gameObject.layer == _terrainLayer)
            {
                _isGrounded = activate;
            }
        }

        protected virtual void OnCollisionEnter2D (Collision2D collision)
        {
            ActivateGrounded(collision);
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            ActivateGrounded(collision);
        }

        protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            ActivateGrounded(collision, false);
        }
    }

}