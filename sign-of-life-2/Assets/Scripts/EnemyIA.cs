using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnemyIA;

public class EnemyIA : MonoBehaviour
{
    public GameObject jugador;
    Rigidbody rb;
    public NavMeshAgent Gps;
    Animator anim;
    AudioSource sonidos;

    AudioClip ataque;

    public float velocidad;
    public bool visible;
    public float DistanciaParaPerseguir;

    public enum posiblesEstados { Patrullar, Atacar, Huir };

    public posiblesEstados estado;

    public int Ruta = 0;
    public Transform[] destinos1;
    public Transform[] destinos2;
    public Transform[] destinos3;
    public Transform[] destinos4;
    public Transform[] destino;

    void Start()
    {
        jugador = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        Gps = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        sonidos = GetComponent<AudioSource>();
        Gps.destination = transform.position; //ves a donde estas ( destino en la posicion inicial)
        estado = posiblesEstados.Patrullar;
    }


    void Update()
    {

        Switch();

        // Segun el estado tener, activar un estado o otro
        switch (estado)
        {
            case posiblesEstados.Patrullar:
                EstadoPatrullar();
                break;
            case posiblesEstados.Atacar:
                EstadoAtacar();
                break;


        }


    }

    void Detectar()
    {
        //si el jugador esta corriendo , que el alien acceda a la "zona" en la que esta el jugador para ir a patrullar alli.
    }
    void EstadosBuscar()
    {
        anim.Play("LookArround");
        //bool en el script del jugador( escondido)
        //cuando el jugador se esconda , el alien se quede cerca y parado unos segundos(animacion buscar)
    }
    void EstadoAtacar()
    {
        //ver al jugador
        Debug.DrawLine(transform.position, jugador.transform.position, Color.red);
        RaycastHit hit;
        if (Physics.Linecast(transform.position, jugador.transform.position, out hit))
        {
            //la linea ha encontrado algo
            if (hit.transform.CompareTag("Player"))
            {
                //es el JUGADORR
                visible = true; //variable escondido del jugador
            }
            else
            {
                // hay alguna obstaculo 
                visible = false;
            }
            //PERSEGUIRLO ( al jugador)

            if (visible) //si te ve te persigue
            {
                Gps.destination = jugador.transform.position;

            }
            else
            {
                rb.velocity = Vector3.zero; //si no te ve que se quede quieto
            }
        }
    }

    void EstadoPatrullar()
    {
        //varias rutas, en funcion del personaje este corriendo , agachado ... el alien usara una ruta( le detectara mas facil)

        //TODO  :  Moverse de un punto a otro
        Rutas();




    }
        void Switch()
        {

            if (estado == posiblesEstados.Patrullar)
            {
                //si el jugador esta cerca...
                if (Vector3.Distance(transform.position, jugador.transform.position) < DistanciaParaPerseguir)
                {
                    //cambio
                    estado = posiblesEstados.Atacar;

                }
            }
            if (estado == posiblesEstados.Atacar)
            {
                //si el jugador esta lejos
                if (Vector3.Distance(transform.position, jugador.transform.position) > DistanciaParaPerseguir)
                {
                    //cambio 
                    estado = posiblesEstados.Patrullar;
                }
                //huir esta dentro de los otros estados con la condicion de si tiene poca vida(huye) o no
            }

        }
        IEnumerator Perdervida()
        {

            for (int i = 0; i < 5; i++)
            {
                //perder vida
                //  vida.jugador = vida.jugador - 1;        DEL JUGADOR
                //ESPERATE ( para que se dibuje)... y luego sigues
                yield return new WaitForSeconds(1);

            }
        }
         void OnTriggerEnter(Collider other)
        {
            if (CompareTag("Player"))
            {
                StartCoroutine("Perdervida");
                sonidos.PlayOneShot(ataque);
            }

        }
        void OnTriggerExit(Collider other)
        {
            if (CompareTag("Player"))
            {
                StopCoroutine("Perdervida");
            }
        }
    void Rutas()
    {


        if (Ruta == 0)
        {
            destino = destinos1;
        }

        if (Ruta == 1)
        {
            destino = destinos2;
        }
        if (Ruta == 2)
        {
            destino = destinos3;
        }
        if (Ruta == 3)
        {
            destino = destinos4;
        }

        //Gps.destinacion = destino[0].position;
        for (int S = 1; S <= destino.Length; S++)
        {
            if (Gps.remainingDistance <= Gps.stoppingDistance)
            {

                Gps.destination = destino[S].position;
            }
        }
        Ruta++;

        if (Ruta > 3)
        {
            Ruta = 0;
        }
    }
} 









