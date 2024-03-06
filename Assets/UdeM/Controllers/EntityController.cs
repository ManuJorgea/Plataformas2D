using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Controllers
{
    public abstract class EntityController : MonoBehaviour
    {
        protected float _speed;
        protected float _jumpForce;
        protected float _gravityMod;

        protected LayerMask _terrainLayer;

        [SerializeField] protected bool _isGrounded;


        protected virtual void Awake() {
            _speed = 10f;
            _jumpForce = 10f;
            _gravityMod = 1f;
            _isGrounded = false;

            _terrainLayer = LayerMask.NameToLayer("Terrain");
            if ( _terrainLayer == -1) {
                Debug.LogWarning("El layer de terreno no ha sido encontrado");
            }

        }
        protected virtual void Start () { }
        protected virtual void Update () {
            CheckGrounded();
        }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }

        protected abstract void CheckGrounded();
    }
}
