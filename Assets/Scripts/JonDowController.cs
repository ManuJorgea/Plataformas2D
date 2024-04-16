using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UdeM.Controllers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JonDowController : Player2D
{
    [SerializeField] private float vida = 3;

    [SerializeField] private float tiempoPerdidaControl;

    [SerializeField] private GameObject corazon1;
    [SerializeField] private GameObject corazon2;
    [SerializeField] private GameObject corazon3;

    private Animator _anim;

    [SerializeField] private Transform ceilController;
    [SerializeField] private float distance;
    RaycastHit2D ceilInfo;

    protected override void Start() { 
        base.Start();

        _anim = transform.Find("Animations").GetComponent<Animator>();

        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

        _capsuleCollider.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        // se setean todos los paramentros del animator del player
        _anim.SetBool("IsWalking",(_currentSpeed != 0));
        _anim.SetBool("IsGrounded", _isGrounded);
        _anim.SetBool("IsFalling", _isFalling);
        _anim.SetBool("IsCrouching", _isCrouching);
        _anim.SetBool("Climbing", escalando);
        _anim.SetBool("IsJumping", _isJumping);

        // ejecuta la animacion de escalar si el jugador esta presionando la tecla W
        if (Mathf.Abs(_rb2d.velocity.y) > Mathf.Epsilon)
        {
            _anim.SetFloat("VelocityY", Mathf.Sign(_rb2d.velocity.y));
        }
        // detiene la animacion de escalar si el jugador no esta presionando la tecla W
        else
        {
            _anim.SetFloat("VelocityY", 0);
        }

        checkarVida();

        ceilInfo = Physics2D.Raycast(ceilController.position, Vector2.up, distance);    
    }

    protected override void Jump()
    {
        base.Jump();

        // imprime una fuerza hacia arriba al rigidbody del player
        _rb2d.AddForce(Vector2.up * _jumpForce);

        _anim.SetTrigger("Jump");

        _isJumping = true;
    }

    public override void Crouch()
    {
        base.Crouch(); 

        // verifica no tenga techo encima de si
        if (ceilInfo == false)
        {
            _isCrouching = !_isCrouching;

            // verifica si la capsule collider esta desactivada
            if (!_capsuleCollider.enabled)
            {
                _speed *= 0.5f;

                // se activa el capsule collider y se desactiva el box collider
                _capsuleCollider.enabled = true;
                _boxCollider.enabled = false;
            }
            else
            {
                _speed *= 2;

                // se desactiva el capsule collider y se activa el box collider
                _capsuleCollider.enabled = false;
                _boxCollider.enabled = true;
            }
        }
    }

    // se ejecuta cuando el player choca con los enemigos
    public void TomarDano(float dano, Vector2 posicion)
    {
        vida -= dano;

        checkarVida();

        _anim.SetTrigger("Hurt");

        StartCoroutine(DesactivarColision());

        StartCoroutine(PerderControl());

        Rebote(posicion);
    }

    private IEnumerator DesactivarColision()
    {
        // desactiva la colision del player con los enemigos durante tiempoPerdidaControl
        Physics2D.IgnoreLayerCollision(0, 2, true);
        yield return new WaitForSeconds(tiempoPerdidaControl);

        // reactiva la colision del player con los enemigos durante tiempoPerdidaControl
        Physics2D.IgnoreLayerCollision(0, 2, false);
    }

    // hace que el jugador no se pueda volver durante un breve periodo de tiempo despues de golpear con un enemigo
    private IEnumerator PerderControl()
    {
        permitirMov = false;

        // espera durante tiempoPerdidaControl/2 segundos antes de seguir con la ejecucion e impide el movimiento del player
        yield return new WaitForSeconds(tiempoPerdidaControl/2);

        permitirMov = true;
    }

    public void checkarVida()
    {
        List<GameObject> vidas = new List<GameObject>();

        // se le asignan los 3 corazones del UI
        vidas.Add(corazon1);
        vidas.Add(corazon2);
        vidas.Add(corazon3);

        if (vida == 3)
        {
            // se activan todos los corazones
            foreach (GameObject cora in vidas)
            {
                cora.SetActive(true);
            }
        }
        else if (vida == 2)
        {
            // se desactivan el ultimo corazon
            vidas[2].SetActive(false);
        }
        else if (vida == 1)
        {
            // se desactivan los ultimos dos corazones
            vidas[2].SetActive(false);
            vidas[1].SetActive(false);
        }
        else if(vida <= 0)
        {
            // se desactivan todos los corazones
            _anim.SetTrigger("Died");
            vidas[0].SetActive(false);
            _rb2d.velocity = Vector3.zero;

            StartCoroutine(Morir());
        }
    }

    IEnumerator Morir()
    {
        // espera durante 2 segundos antes de seguir con la ejecucion
        yield return new WaitForSeconds(2);

        // recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // dibuja el rayo que verifica si hay techo sobre el player
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(ceilController.transform.position, ceilController.transform.position + Vector3.up * distance);
    }
}
