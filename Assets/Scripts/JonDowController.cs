using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UdeM.Controllers;
using UnityEngine;

public class JonDowController : Player2D
{
    [SerializeField] private float vida = 3;

    [SerializeField] private float tiempoPerdidaControl;

    [SerializeField] private GameObject corazon1;
    [SerializeField] private GameObject corazon2;
    [SerializeField] private GameObject corazon3;


    private Animator _anim;

    protected override void Start() { 
        base.Start();

        _anim = transform.Find("Animations").GetComponent<Animator>();
    
    }

    protected override void Update()
    {
        base.Update();

        _anim.SetBool("IsWalking",(_currentSpeed != 0));
        _anim.SetBool("IsGrounded", _isGrounded);
        _anim.SetBool("IsFalling", _isFalling);
        _anim.SetBool("IsCrouching", _isCrouching);
        _anim.SetBool("Climbing", escalando);

        if(Mathf.Abs(_rb2d.velocity.y) > Mathf.Epsilon)
        {
            _anim.SetFloat("VelocityY", Mathf.Sign(_rb2d.velocity.y));
        }
        else
        {
            _anim.SetFloat("VelocityY", 0);
        }
    }

    protected override void Jump()
    {
        base.Jump();
        _anim.SetTrigger("Jump");
    }

    public override void Crouch(bool foxy)
    {
        base.Crouch(foxy);
        _isCrouching = foxy;
        _anim.SetBool("IsCrouching", true);
    }

    public void TomarDano(float dano)
    {
        vida -= dano;
    }

    public void TomarDano(float dano, Vector2 posicion)
    {
        vida -= dano;
        _anim.SetTrigger("Hurt");

        StartCoroutine(DesactivarColision());

        StartCoroutine(PerderControl());

        Rebote(posicion);

        checkarVida();
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

        yield return new WaitForSeconds(tiempoPerdidaControl);

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
        else if(vida == 0)
        {
            vidas[0].SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
}
