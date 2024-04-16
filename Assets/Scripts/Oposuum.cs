using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oposuum : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        // se verifica que colisione con el player
        if (other.gameObject.CompareTag("Player"))
        {
            // recorrre cada uno de los puntos en los que el enemigo y el player colisionaron
            foreach (ContactPoint2D punto in other.contacts)
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
