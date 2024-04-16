using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D punto in other.contacts)
            {
                if(punto.normal.y <= -0.9)
                {
                    anim.SetTrigger("Die");
                    other.gameObject.GetComponent<JonDowController>().Rebote();
                }
                else
                {
                    try
                    {
                        other.gameObject.GetComponent<JonDowController>().TomarDano(0.5f, other.GetContact(0).normal);
                    }
                    catch { }
                }
            }
        }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
