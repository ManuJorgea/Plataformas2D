using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Controllers
{

    public class Enemy2D : Character2D
    {
        protected int _direction;

        protected override void Update() {
            base.Update();

            _currentSpeed = _direction * _speed;
            _rb2d.velocity = new Vector2(_currentSpeed, _rb2d.velocity.y);

        }
    }
}
