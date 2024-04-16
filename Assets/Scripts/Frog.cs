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
        // se verifica que colisione con el player
        if(other.gameObject.CompareTag("Player"))
        {
            // recorre cada uno de los puntos en los que el enemigo y el player colisionaron
            foreach (ContactPoint2D punto in other.contacts)
            {
                // En caso de que el jugador salte encima de la rana, esta muere
                if(punto.normal.y <= -0.9)
                {
                    anim.SetTrigger("Die");
                    other.gameObject.GetComponent<JonDowController>().Rebote();
                }
                // En caso de que el jugador no salte encima de la rana, esta le hará daño
                else
                {
                    try
                    {
                        // Le hace daño al player llamando a la funcion TomarDano
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
