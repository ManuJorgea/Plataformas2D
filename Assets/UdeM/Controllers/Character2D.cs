using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UdeM.Controllers
{
    public class Character2D : EntityController
    {
        [SerializeField] protected float velocidadEscalar;
        protected bool permitirMov = true;

        protected float gravedadInicial;
        protected bool escalando;

        protected Rigidbody2D _rb2d;

        [SerializeField] protected bool _isFacingRight = true;
        [SerializeField] protected bool _isJumping;

        protected override void LateUpdate() {  
            base.LateUpdate();
            _isFalling = (_rb2d.velocity.y < 0 && !_isGrounded && !escalando); 
        
        }


        protected override void FixedUpdate() { 
            //base.FixedUpdate();

            //if (_currentSpeed > 0 ) {

            //    _isFacingRight = true;           
            
            //} else if (_currentSpeed < 0){

            //    _isFacingRight = false;
            
            //}
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

                if(_isJumping == true)
                {
                    _isJumping = false;
                }
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            ActivateGrounded(collision);

            // verifica si el trigger que toco tiene el tag ZonaMuerte, entonces recarga la escena actual
            if (collision.gameObject.CompareTag("ZonaMuerte"))
            {
                Scene escenaActual = SceneManager.GetActiveScene();

                SceneManager.LoadScene(escenaActual.buildIndex);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            // verifica si el trigger que toco tiene el tag cherry, entonces pasa a la siguiente escena
            if (collision.gameObject.CompareTag("Cherry"))
            {
                Scene escenaActual = SceneManager.GetActiveScene();

                SceneManager.LoadScene(escenaActual.buildIndex + 1);
            }
            // verifica si el trigger que toco tiene el tag Flag, entonces vuelve al menu
            if (collision.gameObject.CompareTag("Flag"))
            {
                SceneManager.LoadScene(0);
            }
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