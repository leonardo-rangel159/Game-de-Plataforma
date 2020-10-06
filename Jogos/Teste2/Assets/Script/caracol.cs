using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caracol : MonoBehaviour
{
    public Rigidbody2D rig;
    public float velocidade;
    public float tempoNaDirecao;
    float tempo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;//é cronomito
        if (tempo >= tempoNaDirecao){//para saber o tempo que a abelha vai para uma direção
            velocidade = -velocidade;
            tempo = 0f;
        }

        if(velocidade < 0){//rotacionar a abelha para esquerda
            transform.eulerAngles = new Vector2(0f, 0f);
        }

        if(velocidade > 0){//rotacionar a abelha para direita
            transform.eulerAngles = new Vector2(0f, 180f);
        }

        rig.velocity = new Vector2(velocidade, rig.velocity.y);//mover a caracol
    }
}
