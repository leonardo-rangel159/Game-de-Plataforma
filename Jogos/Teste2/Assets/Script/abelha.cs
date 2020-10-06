using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abelha : MonoBehaviour
{
    public Rigidbody2D rig;
    public float velocidade;
    public float velocidadeAtaca;
    public float tempoNaDirecao;
    float tempo;
    float virar;
    float eixoY;
    private Transform posicaoDoJogador;
    // Start is called before the first frame update
    void Start()
    {
        eixoY = transform.position.y;
        posicaoDoJogador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(transform.position, posicaoDoJogador.localPosition) <= 7){//vai verificar a distancia da abelha ao jogador, para saber se está perto
            virar = posicaoDoJogador.localPosition.x - transform.position.x;//saber quando o pessonagem esta na frente ou atras da abelha
            if(virar < 0){//saber se o pessonagem esta atras ou na frente da abelha
                transform.eulerAngles = new Vector2(0f, 0f);//roda a abelha
                rig.velocity = new Vector2(velocidadeAtaca * Time.deltaTime, rig.velocity.y);//move a abelha, póis estava tendo um bug que fazia ela ficar parada
            }

            if(virar > 0){//saber se o pessonagem esta atras ou na frente da abelha
                transform.eulerAngles = new Vector2(0f, 180f);//roda a abelha
            }

            transform.position = Vector2.MoveTowards(transform.position, posicaoDoJogador.localPosition, velocidadeAtaca * 2 * Time.deltaTime);//faz a abelha per seguir o jogador
            
        }

        if((Vector3.Distance(transform.position, posicaoDoJogador.localPosition) >= 7)){//vai verificar a distancia da abelha ao jogador, para saber se não está perto
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

            if(eixoY < transform.position.y){//não deixar a abelha voando, quando não estiver per seguindo o personagem
                transform.position = new Vector2(rig.velocity.x, eixoY);//tras abelha para o seu lugar original
            }

            rig.velocity = new Vector2(velocidade, rig.velocity.y);//mover a abelha
        
    }}
}
