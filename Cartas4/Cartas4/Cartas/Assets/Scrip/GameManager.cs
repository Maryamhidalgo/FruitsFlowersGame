using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.UI;//ns si me vale
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //es estatica x tanto todos los objetos de la clase gamemanager tienen la misma variable 
    static public GameManager SingletonGamemanager; //una veriable de tipo gamemanager q se usa de singleton (una instancia q solo hay una)

    public int parejasCreadas;
    public Dificultad NivelesDificultad;
    public Estilo Estiloselecionado;
    public Carta PrefabCarta;// SA la he hecho tipo carta, le paso un prefab q es el objeto carta q es un cubo
    float separacion = 0.5f; //para separ cartas de otras
    public Vector3 posicionInicial = new Vector3(-3.1f, -3.05f, 87.03f);

    List<Material> _listaMateriales;//nombro la lista de materiales
    List<Carta> _listaCartas; //SA la he hecho de tipo carta, nombro la lista de cartas
    List<string>_listarutaTexturas;

    Material _primerMaterial;//ruta ak material de la parte de atrás
    string _primeraRutaTextura;//va a ser la ruta a la textura de la parte trasera


    //RUTAS Y ACCESOS SA
    string rutaCarpetaMateriales = "Materiales/";
    string rutaCarpetaTextura;

    public int numClicks =0; //SA HOY
    public TMP_Text textContIntentos;//SA HOY

    public int numAciertos = 0;
    public TMP_Text textContAciertos;
    public TMP_Text textContTiempo;

    public Carta CartaDestapada;
    public bool permitirMostrar = true;

    public int cronometro;
    public bool menuPausaMostrado;
    public bool menuGanadorMostrado;
    public GameObject _menuGanador;
    //public GameObject menuPausa;


    void Start()
    {

        _listaMateriales = new List<Material>();//inicializo la lista (abro)
        _listaCartas = new List<Carta>(); //SA lo mismo he cambiado gameobject a carta inicializo la lista (abro la bolsa antes de guardar cosas)
        _listarutaTexturas = new List<string>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()// se ejecuta antes q el start una vez al cargar la escena
    {
        if (SingletonGamemanager == null)
        {
            DontDestroyOnLoad(this);//q se guarde en memoria el gamemanager (pra q no se pierda entre escenas)
            SingletonGamemanager = this;
        }
        else
        {
            Destroy(this); //se destruye 
        }
    }

    public enum Dificultad//son strings enumeracion de niveles de dificultad
    {
        SinDificultad,
        Facil,
        Medio,
        Dificil,
    }
    public enum Estilo //enumeracion de estilos
    {
        Garden,
        Greengrocer,
    }


    public void crearjuego()
    {
        ActualizarCronometro();
        AccesoMaterialTexturas();//cargo todos los materiales y texturas


        if (NivelesDificultad == Dificultad.Facil)
        {
           
            CrearMatriz(4, 5);

        }
        if (NivelesDificultad == Dificultad.Medio)
        {
            
            posicionInicial.y = -3.30f;
            PrefabCarta.transform.localScale = new Vector3(0.9f, 1.35f, PrefabCarta.transform.localScale.z);
            separacion = 0.4f;
            CrearMatriz(5, 6);

        }
        if (NivelesDificultad == Dificultad.Dificil)
        {
            //posicionInicial.y = -0.87f;
            posicionInicial.x = -3.96f;
            separacion = 0.3f;
            CrearMatriz(5, 8);

        }
    }

    public void CrearMatriz(int f, int c)
    {   

        for (int height = 0; height < f; height++)
        {
            for (int length = 0; length < c; length++)
            {
                //Genera instancias del objeto carta 
                Carta nuevacarta = Instantiate(PrefabCarta);//SA tmb lo he cambiado x carta

                //Sitúa a la ficha en la posición que le corresponde según el index 
                nuevacarta.transform.position = new Vector3(
                  posicionInicial.x + (nuevacarta.GetComponent<Collider>().bounds.size.x + separacion) * length,
                    posicionInicial.y + (nuevacarta.GetComponent<Collider>().bounds.size.y + separacion) * height,
                    posicionInicial.z);


               _listaCartas.Add(nuevacarta);//voy añadiendo a la lista cada instancia creada
            }
        }

        AsignarMaterialTexturas();
       
    }

 


    public void AccesoMaterialTexturas()//SA
    {
        const string nombrePrimerMaterial = "Trasero";
        const string nombreMaterialBase = "Carta";
       

        if (Estiloselecionado == Estilo.Garden) //si el estilo selecionado a sido garden que cargue la carpeta de las flores
        {
            rutaCarpetaTextura = "Cartas/" + "Flowers/";
        }
        if (Estiloselecionado == Estilo.Greengrocer) //si ha sido greengrocer que cargue la de las frutas
        {

            rutaCarpetaTextura = "Cartas/" + "Fruits/";
        }
     




        //lista de texturas y materiales frontales
        for (int j = 1; j <= parejasCreadas; j++) 
        {
            //Materiales
            string rutaCarta = rutaCarpetaMateriales + nombreMaterialBase + j; //Material + Carta + J
            Material materialNecesario = Resources.Load(rutaCarta, typeof(Material)) as Material; //cargo cada material en total j uno x carta en una variable material
            _listaMateriales.Add(materialNecesario); //esta variable se va añadiendo a una lista de materiales

            //Texturas
            string rutaTexturaCarta = rutaCarpetaTextura + nombreMaterialBase + " "+j;// Cartas flowers/fruits + Carta + j
            _listarutaTexturas.Add(rutaTexturaCarta);
        }

        //lista de textura y material trasero
        _primeraRutaTextura = rutaCarpetaTextura + nombrePrimerMaterial; //  Cartas + /fruits o flowers + Trasero
        _primerMaterial = Resources.Load(rutaCarpetaMateriales + nombrePrimerMaterial, typeof(Material)) as Material;

    }

    public void AsignarMaterialTexturas()//SA del video
    {  
        int ran = Random.Range(0, _listaMateriales.Count); //seleciona un num random entre 0 y 20
        int[] repeticiones =new int[_listaMateriales.Count];

        for (int i = 0; i < _listaMateriales.Count; i++)
        {
           repeticiones[i] = 0;
        }

        foreach (Carta carta in _listaCartas)
        {
            int intentos = 0;
            int ran2 = ran;
            bool obligarMaterial = false;

            while (repeticiones[ran] >= 2 || ((ran2 == ran) && !obligarMaterial))
            {
                ran = Random.Range(0, _listaMateriales.Count);
                //
                intentos++;
                if (intentos > 100)
                {
                    for (int i = 0; i < _listaMateriales.Count; i++)
                    {
                        if (repeticiones[i] < 2)
                        {
                            ran = i;
                            obligarMaterial = true;
                        }
                    }
                    //
                    if (obligarMaterial == false)
                    {
                        return;
                    }

                }
            }

            carta.Id = ran;
            carta.Reverso(_primerMaterial, _primeraRutaTextura);
             StartCoroutine(carta.DibujarReverso());
            carta.Cara(_listaMateriales[ran], _listarutaTexturas[ran]);
           
            //carta.AplicarMaterialFrontal();

           repeticiones[ran] += 1;
           obligarMaterial = false;


        }



    }

    public IEnumerator HacerClick(Carta _laCarta)//contador de clicks
    { 

        if(CartaDestapada == null) //si no hay cartas destapadas esta es la primera q se destapa
        {
            CartaDestapada=_laCarta;
        }
        else //si hay alguna carta destapada ambas se dan la vuelta
        {   numClicks++;
           
            //EncontrarParejas(CartaDestapada, _laCarta);
            if (EncontrarParejas(CartaDestapada, _laCarta)) 
            {
                
                numAciertos++;
                if (numAciertos == parejasCreadas)
                {
                    _menuGanador.SetActive(true);
                    textContIntentos.enabled = false;
                    textContAciertos.enabled = false;

                    menuGanadorMostrado = true;
                }

            }
            else
            {
                //numClicks++;
                yield return new WaitForSeconds(1f);
                _laCarta.volverDReverso();
                CartaDestapada.volverDReverso();
               
            }
            //_laCarta.volverDReverso();
            //CartaDestapada.volverDReverso();
            CartaDestapada = null; // de nuevo no tengo cartas destapadas
         
        }
        ActualizarUI();
       

    }

    public bool EncontrarParejas(Carta primeraCarta, Carta segundaCarta)
    {
        if (primeraCarta.Id == segundaCarta.Id) 
        {
            return true;
        }
       else 
       { return false;}
    }

    public void ActualizarUI()
    {
        textContIntentos.text = "Movements: " + numClicks;
        textContAciertos.text = "Points: " + numAciertos;
        textContTiempo.text = "Time: " + cronometro + " seconds";
    }


 

    //public void PausarCronometro(int segundos)
    //{
    //    cronometro = segundos;
    //}
    public void ActualizarCronometro()
    {
        if(menuPausaMostrado == false && menuGanadorMostrado== false)
        {
            cronometro++;
            Invoke("ActualizarCronometro", 1f);
        }
       
    }
    public void Reiniciar()
    {
        _listaCartas.Clear();
        _listaMateriales.Clear();
        _listarutaTexturas.Clear();
        GameObject[] cartasEliminar = GameObject.FindGameObjectsWithTag("Carta");
        for (int i = 0; i < cartasEliminar.Length; i++)
        {
            Object.Destroy(cartasEliminar[i]);
        }

        numClicks = 0;
        numAciertos = 0;
        //CartaDestapada = null;
        textContIntentos.text = "Intentos";
        textContAciertos.text = "Aciertos";
        permitirMostrar = true;
        menuGanadorMostrado = false;
        menuPausaMostrado = false;

    }

   
  









}
