using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PrimeraPersona : MonoBehaviour
{
    //Poner al jugador la etiqueta player, congelar posision en Y y congelar rotacion en XYZ




    // martes manana: arreglar camara
    // martes tarde: UI, crear escenas 

  

    // REFERENCIA AL PERSONAJE
    Rigidbody rb;


    // MOVIMIENTO
    public float velocidad;
    public InputAction joystick;
    float movHorizontal;
    float movVertical;
    Vector3 direccionFinal;


    // ROTAR
    public float velocidadRotar;
    public float maxVerticalAngle = 80f; // Limit vertical rotation angle


    // VIDA
    public int vidaMax = 3;
    public int vidaAct;
    public int botiquinAct;
    public int botiquinMax = 3;
    public InputAction recuperarse;

    //UI
    public Text lifeText;
    public Text medicineText;


    // CAMARA
    public GameObject camara;


    // MINIJUEGO
    public InputAction empezarMiniJuego;
    public GameObject miniGamePrefab;
    public bool isInRange;
    private bool isPromptActive;
    //UI
    public GameObject promptTextObject;


    void Start()
    {
        
        //PERSONAJE
        rb = GetComponent<Rigidbody>();


        //ACTIVAR INPUTS
        joystick.Enable();


        //VIDA
        vidaAct = vidaMax;
        botiquinAct = 0;

     
        //CAMARA
        camara =Camera.main.gameObject;


        //FIJAR PANTALLA
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the center of the screen


        // MINIJUEGO
        promptTextObject.SetActive(false);

    }


    void Update()

    {
        
        Movimiento();
        // ApuntarConMouse();


        RecuperarVida();


        // CAMARA



        //MINIJUEGO

        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            Minijuego();
        }



    }

    //MOVIMIENTO
    private void Movimiento()
    {
        Vector2 movimientoInput = joystick.ReadValue<Vector2>();
        Vector3 direccion = new Vector3(movimientoInput.x, 0f, movimientoInput.y).normalized;
        rb.velocity = direccion * velocidad + new Vector3(0, rb.velocity.y, 0);

        // Move the camera along with the player
        camara.transform.position = transform.position + Vector3.up * 2f; // Adjust the Y value as needed
    }

  /*  void ApuntarConMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Rotate the camera towards the point hit by the raycast
            camara.transform.LookAt(hit.point);
        }
    }
  */




    //VIDA
    public void RecibirDano (int cantidadDeDano)
    {
        vidaAct -= cantidadDeDano;
    }

 
    public void RecuperarVida()
    {
        if (botiquinAct > 0 && vidaAct < 4 && Input.GetKeyDown(KeyCode.G))
        {
            vidaAct++;
            vidaAct = Mathf.Min(vidaAct, vidaMax);
            botiquinAct--;
        }

    }




    private void OnTriggerEnter(Collider other)
    {
        //VIDA
        if (other.CompareTag("Botiquin"))
        {
            Destroy(other.gameObject);
            botiquinAct++;

        }


        //MINIJUEGO
        {
            // Check if the player collides with the interactable object
            if (other.CompareTag("terminal"))
            {
                isInRange = true;
                if (isInRange == true)
                {
                    Debug.Log("mostrar texto que indique presionar f");
                }
            }
        }
    }




    void OnTriggerExit(Collider other)
    {
        //MINIJUEGO
        if (other.CompareTag("terminal"))
        {
            isInRange = false;
        }
    }

    private void Minijuego()
    {
        //Instantiate(miniGamePrefab, Vector3.zero, Quaternion.identity);
        Debug.Log("empieza minijuego");
    }
}

