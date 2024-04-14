using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene("Menu_Start_UI");
    }

    public void PlayLevel01()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel02()
    {
        SceneManager.LoadScene("Level2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    // Menú de pausa de nivel
    ///Panel oculto
    public GameObject menuPause;
    public bool juegoPausado = false;

    void Udpate(){

        //Activación por medio del teclado
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Condición que revisa si el juego está o no en pausa
            if (juegoPausado)
            {
                //Juego activo
                Debug.Log("El juego está activo");
                ContinuarJuego();
            }
            else
            {
                //Juego pausado
                Debug.Log("El juego está pausado");
                PausaJuego();
            }
        }

    }

    public void PausaJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0; //Pausar el juego
        menuPause.SetActive(true);
    }

    public void ContinuarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1;
        menuPause.SetActive(false);
    }
}
