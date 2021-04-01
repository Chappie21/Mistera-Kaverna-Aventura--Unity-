using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    // Variables globales, si son públicas se pueden cambiar desde Unity
    public float saltoVelocidad = 7f;
    public float maxVelocidad = 10f;
    private float movHorizontal = 0f;
    private float movVertical = 0f; 
    private bool mirandoDerecha = true;

    // Manejar las animaciones, mirar la ventana "Animator" para observar las animaciones
    public Animator animator;

    // *Salto*
    // Dentro de Unity se creo un objeto "checkSuelo" pegado al jugador para saber si toca el suelo
    private bool isSuelo;
    public Transform checkSuelo;
    public float checkRadio;
    public LayerMask objetosSuelo; 

    void Awake(){
        // Obtener el cuerpo del jugador
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // En el update va el input del usuario
    void Update()
    {
        movHorizontal = Input.GetAxisRaw("Horizontal");
        // ^ si se presiona "a" o "flechaIzq" retorna -1, y si se presiona "d" o "flechaDer" retorna 1 
        movVertical = Input.GetAxisRaw("Vertical");
    }

    // Aqui van los cambios al personaje
    void FixedUpdate(){
        isSuelo = Physics2D.OverlapCircle(checkSuelo.position, checkRadio, objetosSuelo);

        rigidBody2D.velocity = new Vector3(maxVelocidad * movHorizontal, rigidBody2D.velocity.y);

        if (movVertical > 0 && isSuelo == true)
        {
            rigidBody2D.velocity = Vector2.up * saltoVelocidad;
        }
        
        if (mirandoDerecha == true && rigidBody2D.velocity.x < 0)
        {
            Voltear();
        }
        if (mirandoDerecha == false && rigidBody2D.velocity.x > 0)
        {
            Voltear();
        }
        // Actualizar los parámetros para que funcionen las animaciones
        animator.SetFloat("Velocidad", Mathf.Abs(movHorizontal));
        animator.SetBool("isSuelo", isSuelo);
    }

    void Voltear(){
        mirandoDerecha = !mirandoDerecha;
        Vector3 Scaler = transform.localScale; // Valores de x, y, z, para el scale
        Scaler.x *= -1;
        transform.localScale = Scaler; 
    }
}
