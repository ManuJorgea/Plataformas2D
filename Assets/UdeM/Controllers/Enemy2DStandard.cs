using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Controllers
{

    public class Enemy2DStandard : Enemy2D
    {
        protected GameObject _currentPiso;

        protected override void Start()
        {
            base.Start();

            _speed = 5f;
            _direction = (Random.Range(-1, 1) > 0) ? 1 : -1;

        }

        protected override void Update() {
            base.Update();
            CheckEdge();
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            _currentPiso = collision.collider.gameObject;        
        
        }


        protected virtual void CheckEdge() { 
        
            Vector3 pos = transform.position;
            Vector2 pini = new Vector2(pos.x, pos.y + 0.5f);

            RaycastHit2D hit = Physics2D.Raycast(pini, new Vector2(_direction, -1),1f);
            Debug.DrawRay(pini,new Vector2(_direction, -1),Color.red);

            if (hit.collider != null) {

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Terrain") && _currentPiso != hit.collider.gameObject)
                {
                    ChangeDirection(); 
                }
            }
            else
            {
                ChangeDirection();             
            }
        }
        private void ChangeDirection()
        {
            _direction *= -1;
        }


    }
}
