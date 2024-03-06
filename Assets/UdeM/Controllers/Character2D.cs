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

        protected override void CheckGrounded() {
            _isGrounded = Physics2D.OverlapCircle(transform.position, 0.01f, _terrainLayer);
        }

    }

}