using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Inicializador : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text _textContIntentos; //SA HOY
    public TMP_Text _textContAciertos;
    public TMP_Text _textContTiempo;
    public GameObject menuPausa;
    public GameObject menuGanador;
    //public bool menuPausaMostrado;
    void Start()
    {
        GameManager.SingletonGamemanager.textContIntentos = _textContIntentos; //SA HOY
        GameManager.SingletonGamemanager.textContAciertos = _textContAciertos;
        GameManager.SingletonGamemanager.textContTiempo = _textContTiempo;
        GameManager.SingletonGamemanager._menuGanador = menuGanador;    
        //GameManager.SingletonGamemanager.menuPausa = _menuPausa;
        GameManager.SingletonGamemanager.crearjuego();
        GameManager.SingletonGamemanager.cronometro = 0;
        GameManager.SingletonGamemanager.textContAciertos.enabled = true;
        GameManager.SingletonGamemanager.textContIntentos.enabled = true;
        GameManager.SingletonGamemanager.textContTiempo.enabled = false;
        //VolverJugar();


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MostrarMenuPausa()
    {
        menuPausa.SetActive(true);
       
       GameManager.SingletonGamemanager.menuPausaMostrado = true;
    }
    public void EsconderMenuPausa()
    {
        menuPausa.SetActive(false);
       
        GameManager.SingletonGamemanager.menuPausaMostrado = false;
        GameManager.SingletonGamemanager.ActualizarCronometro();
    }
    public void MenuInicial()
    {
        EsconderMenuPausa();
        GameManager.SingletonGamemanager.Reiniciar();
        SceneManager.LoadScene("Partida");
    }

    //public void MostrarMenuGanador()
    //{
    //    menuGanador.SetActive(true);

    //    GameManager.SingletonGamemanager.menuGanadorMostrado = true;
    //}
    //public void EsconderMenuGanador()
    //{
    //   menuGanador.SetActive(false);

    //    GameManager.SingletonGamemanager.menuGanadorMostrado = false;
       
    //}


    public void VolverJugar()
    {
        EsconderMenuPausa();
        GameManager.SingletonGamemanager.Reiniciar();
        SceneManager.LoadScene("Level1");

       
    }
    public void MostrarEstadisticas()
    {
        GameManager.SingletonGamemanager.textContAciertos.enabled = true;
        GameManager.SingletonGamemanager.textContIntentos.enabled = true;
        GameManager.SingletonGamemanager.textContTiempo.enabled = true;
    }
}
