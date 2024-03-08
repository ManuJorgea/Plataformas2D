using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Controllers
{
    public class Character2D : EntityController
    {
        protected Rigidbody2D _rb2d;

        protected override void Start() {
            base.Start();
            _rb2d = GetComponent<Rigidbody2D>();
            if ( _rb2d == null ) {
                _rb2d = gameObject.AddComponent<Rigidbody2D>();
                _rb2d.freezeRotation = true;
            }
        }


        protected virtual void ActivateGrounded (Collision2D other, bool activate = true) {

            if (other.gameObject.layer == _terrainLayer)
            {
                _isGrounded = true;
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