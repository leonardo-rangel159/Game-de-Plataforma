using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planta : MonoBehaviour
{
    
    public float velocidadeAtaca;
    private Transform posicaoDoJogador;
    public Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        posicaoDoJogador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, posicaoDoJogador.localPosition) <= 7){//medir a distacia do jogdor e a planta
            anime.SetInteger("Transicao",1);//animação de ataque
        }else{
            anime.SetInteger("Transicao",0);//animação parada
        }
    }
}
