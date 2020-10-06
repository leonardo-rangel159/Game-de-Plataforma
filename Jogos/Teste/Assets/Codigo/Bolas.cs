using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolas : MonoBehaviour
{
    public MovimentoBola controlador;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ponto"){
            Destroy(collision.gameObject);
            controlador.AtualizarPonto();
        }
        if(collision.gameObject.tag == "Quadrado"){
            controlador.ChamarGameOver();
        }
    }
}
