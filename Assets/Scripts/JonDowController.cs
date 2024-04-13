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

        _anim.SetBool("IsWalking",(_currentSpeed != 0));
        _anim.SetBool("IsGrounded", _isGrounded);
        _anim.SetBool("IsFalling", _isFalling);
        _anim.SetBool("IsCrouching", _isCrouching);
        _anim.SetBool("Climbing", escalando);

        if (Mathf.Abs(_rb2d.velocity.y) > Mathf.Epsilon)
        {
            _anim.SetFloat("VelocityY", Mathf.Sign(_rb2d.velocity.y));
        }
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
        _anim.SetTrigger("Jump");
    }

    public override void Crouch()
    {
        base.Crouch(); 

        if (ceilInfo == false)
        {
            _isCrouching = !_isCrouching;

            if (!_capsuleCollider.enabled)
            {
                _speed *= 0.5f;
                _capsuleCollider.enabled = true;
                _boxCollider.enabled = false;
            }
            else
            {
                _speed *= 2;
                _capsuleCollider.enabled = false;
                _boxCollider.enabled = true;
            }
        }
    }

    public void TomarDano(int dano, Vector2 posicion)
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
        Physics2D.IgnoreLayerCollision(0, 2, true);
        yield return new WaitForSeconds(tiempoPerdidaControl);
        Physics2D.IgnoreLayerCollision(0, 2, false);
    }

    private IEnumerator PerderControl()
    {
        permitirMov = false;

        yield return new WaitForSeconds(tiempoPerdidaControl/2);

        permitirMov = true;
    }

    public void checkarVida()
    {
        List<GameObject> vidas = new List<GameObject>();

        vidas.Add(corazon1);
        vidas.Add(corazon2);
        vidas.Add(corazon3);

        if (vida == 3)
        {
            foreach (GameObject cora in vidas)
            {
                cora.SetActive(true);
            }
        }
        else if (vida == 2)
        {
            vidas[2].SetActive(false);
        }
        else if (vida == 1)
        {
            vidas[2].SetActive(false);
            vidas[1].SetActive(false);
        }
        else if(vida <= 0)
        {
            _anim.SetTrigger("Died");
            vidas[0].SetActive(false);
            _rb2d.velocity = Vector3.zero;

            StartCoroutine(Morir());
        }
    }

    IEnumerator Morir()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(ceilController.transform.position, ceilController.transform.position + Vector3.up * distance);
    }
}
