using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonasAlarma : MonoBehaviour
{
    public int NumeroZona=1;
    public EnemyIA Enemy;
    
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(CompareTag("Player")) 
        {
            //el jugador esta corriengo?
         /*   if(Jugador.corriendo==true)
            {
                //que el enemigo haga la ruta , que este en la zona de trigger donde el jugador esta corriendo
            }
            //si
            //avisar al alien
            Enemy.Gps.destination = Enemy.destino[S].position; */
        }
        if (CompareTag("Enemy"))
        {
            print("1");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        //el jugador esta corriengo?
        //si
        //avisar al alien
    }
}
