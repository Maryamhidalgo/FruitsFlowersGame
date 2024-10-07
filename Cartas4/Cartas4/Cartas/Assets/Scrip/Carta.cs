using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public int Id; //cada carta tiene su ppio id
    public Material materialTrasero;// SA todas las cartas tienen parte de atras aunq sea la misma para todas
    public Material materialCarta; //SA cada carta tiene su propio  material

    public int temporizador = 1;// SA HOY cuenta el tiempo
    bool mostrar = false;
    public Animator animaciones;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SA
    private void OnMouseDown()
    {
        if(GameManager.SingletonGamemanager.menuPausaMostrado == false && GameManager.SingletonGamemanager.menuGanadorMostrado==false)
        {
            StartCoroutine(DibujarCara()); 
        }
      /*  DibujarCara();*/ //SA si le hago click se pinta
      //SA HOY
       

    }


    //Crear materiales
    //    public void CrearParteAtras (Material _primerMaterial, string rutaMaterialTrasero)//SA
    //    {
    //        GetComponent<MeshRenderer> ().material = _primerMaterial; //accedo al material de la carta y le asigno el material q le paso
    //        GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(rutaMaterialTrasero, typeof(Texture2D)) as Texture2D;//cargo el string de la ruta 

    //    }

    //    public void CrearMaterialFrontal(Material listamateriales, string RutaTextura)
    //    {

    //    }

    ////Asignarlos 
    //    public void Reverso(Material _materialTrasero) // SA funcion que carga el material y textura y lo asigna el material de la parte trasera a mi carta
    //    {
    //        gameObject.GetComponent<Renderer>().material = _materialTrasero;
    //        GetComponent<MeshRenderer>() .material = _materialTrasero; //aplico el mat
    //    }

    //    public void Cara(Material _materialFrontal) //SA funcion que asigna el material de la parte delantera a mi carta
    //    {
    //        gameObject.GetComponent<Renderer>().material = _materialFrontal;
    //        materialCarta = _materialFrontal;
    //    }

    public void Reverso(Material _materialTrasero, string rutaTexturalTrasera)//SA
    {
        //creo el material trasero
        materialTrasero = _materialTrasero; //guardo en materialTrasero de mi carta el material trasero q le paso 
        materialTrasero.mainTexture = Resources.Load(rutaTexturalTrasera, typeof(Texture2D)) as Texture2D; ; //en la textura de ese material cargo la ruta a la imagen de la prte trasera

        //Aplico el material trasero ( el material que define cómo se ve el objeto se actualiza en tiempo real en la escena)
        //GetComponent<Renderer>().material = materialTrasero; 

    }


    public void Cara (Material _materialFrontal, string rutaTexturaFrontal)//SA funcion que crea y  asigna el material de la parte delantera a mi carta
    {

        //creo el material frontal y se lo guardo a cada carta
        materialCarta = _materialFrontal; //guardo en el materialCarta q es el material frontal de mi carta el material frontal q le paso 
        materialCarta.mainTexture = Resources.Load(rutaTexturaFrontal, typeof(Texture2D)) as Texture2D; //le aplico la textura correspondiente en fncion del bucle random q hice

      
    }

    public IEnumerator DibujarReverso()
    {
        yield return new WaitForSeconds(0.1f);
        animaciones.SetTrigger("Tapar");
        GetComponent<Renderer>().material = materialTrasero;
        mostrar = false;
        GameManager.SingletonGamemanager.permitirMostrar = true; //esto es pq hsta q no se me haya escondido una pareja no quiero volver a dejar destapar mas cartas
    }
    public IEnumerator DibujarCara() //SA carga el material pra q se vea 
    {
        yield return new WaitForSeconds(0.3f);
        //aplico/ dibujo el material sbre la carta
        if (!mostrar && GameManager.SingletonGamemanager.permitirMostrar) //pregunto si no esta pintada y si dejo que se pueda pintar
        {
            animaciones.SetTrigger("Destapar");
            mostrar = true;
            GetComponent<Renderer>().material = materialCarta;

            StartCoroutine(GameManager.SingletonGamemanager.HacerClick(this));  //llamo a la funcion hacer click y le paso el script de la carta pintada
            //Invoke("DibujarReverso", temporizador);


        }
       
    }
   public void volverDReverso()
    {
        StartCoroutine(DibujarReverso());
       
        GameManager.SingletonGamemanager.permitirMostrar = false; //justo dsp de esconderse de nuevo no se puede hacer click a mas cartas durante el tiempo de invocacion
        //una vez se invoke de nuevo dibujarreverso lo pongo a true
    }









}
