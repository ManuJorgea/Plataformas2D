using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oposuum : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D punto in other.contacts)
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
