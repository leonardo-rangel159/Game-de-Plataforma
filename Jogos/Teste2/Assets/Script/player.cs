using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    private int pontos = 0;
    
    public float velocidade;
    public float forcaPulo;
    public Rigidbody2D rig;
    public Animator anime;
    public GameObject explosion; //objeto da explosao
    public int lives = 3;
    public GameObject[] livesObj; //lista de objetos da vida na HUD
    public Text exibirPontos;
    public Sprite bau;

    bool estaPulado=false;
    bool estaSubindo=false;
    bool mover=true;
    int pulos=0;
    float descer=0;
    float gravidade;

    // Start is called before the first frame update
    void Start(){
        gravidade = rig.gravityScale;//pega a gravidade do personagem
        
    }

    // Update is called once per frame
    void Update(){
        Movimentar(mover);//mover para direita e esquerda
        Subir(estaSubindo);//mover para cima e para baixo
        Pular();//para saltar
    }

    void Movimentar(bool a){//pessonagem move para direita e esquerda
        if(a == true){
            float direcao = Input.GetAxis("Horizontal");//se o jogador aperta um dos botões permidos de direção
            rig.velocity = new Vector2(direcao * velocidade, rig.velocity.y);// vai mover o pessonagem
            if(direcao > 0f && estaPulado == false){//mover para direita
                transform.eulerAngles = new Vector2(0f, 0f);//direção da animação
                anime.SetInteger("Transicao",2);//roda uma animação andando
            }

            if(direcao < 0f && estaPulado == false){//mover para esquerda
                transform.eulerAngles = new Vector2(0f, 180f);//direção da animação
                anime.SetInteger("Transicao",2);//roda uma animação andando
            }

            if(direcao == 0 && !estaPulado && pulos == 0 && !(Input.GetAxis("Vertical") == -1)){//se tiver parado
                anime.SetInteger("Transicao",0);//roda uma animação está parado
            }

            /*if(Input.GetAxis("Vertical") == -1 && estaPulado == false){
                anime.SetInteger("Transicao",5);//roda uma animação andando
            }*/
        }
    }

    void Pular(){//pessonagem pula ate duas vezes
        if(Input.GetButtonDown("Jump") && pulos < 2){//Input.GetButtonDown("Jump") para saber se o jogador apertou espaço
            anime.SetInteger("Transicao",1);//roda uma animação
            rig.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);//faz o personagem pular
            pulos++;//para saber quantos pulos foram
        }

        if(descer == 12){//Quando cai em espinho
            rig.AddForce(new Vector2(0f, forcaPulo * 2), ForceMode2D.Impulse);//faz o personagem pular
            descer = 0;//para o pessonagem poder pular
            TomarDano();//perde vida
        }
    }

    void Subir(bool a){//pessonagem sobe e descer
        if(a){
            float direcao = Input.GetAxis("Vertical");
            if(descer == 1 || (descer == 2 && direcao == 1) || (descer == 2 && direcao == -1)){
                rig.isKinematic = true;//pode atraversar collision
            }

            if(descer == 0 && direcao > 0){
                rig.isKinematic = false;//não pode atraversar collision
            }
            anime.SetInteger("Transicao",3);//animação de subindo
            rig.velocity = new Vector2(rig.velocity.x, direcao * velocidade);//mover o pessonagem para cima ou para baixo
        }
        
    }

    private void OnCollisionEnter2D(Collision2D Collision){
        if(Collision.gameObject.layer == 8 || Collision.gameObject.layer == 9){//para saber se o pessonagem pode ou não pular
            estaPulado = false;
            pulos = 0;//para poder pular duas vezes de novo
            anime.SetInteger("Transicao",0);//animação de parado
        }

        if(Collision.gameObject.layer == 12){//Quando encosta em espinhos
            descer = 12;
            Pular();//para pular
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("AtaqueDoInimigo")){
            TomarDano();//perde vida
        }

        if(collision.CompareTag("AtaceAoInimigo")){//Para poder matar inimigo
            rig.AddForce(new Vector2(0f, forcaPulo-5), ForceMode2D.Impulse);//pode pular ao atacar o inimigo
            GameObject exp = Instantiate(explosion, collision.transform.position, collision.transform.rotation); //cria explosao
            Destroy(exp, 1.1f);// destroi explosao
            Destroy(collision.transform.parent.gameObject);//destroi o inimigo
        }

        if(collision.CompareTag("Escada")){
            estaPulado = true;//para saber se pode pular
            estaSubindo = true;//para saber se pode subir
            descer = 2;//para poder atraversar o topo da escada
            rig.gravityScale = 0;//para não cair na escada
        }
        
        if(collision.CompareTag("Topo")){
            descer = 0;//para não descer no chão
        }

        if(collision.CompareTag("Estrela")){
           Destroy(collision.gameObject);//destroi o estrela
           pontos += 100;//vai somar os pontos
           exibirPontos.text = pontos.ToString();//vai exibir os pontos
        }

        if(collision.CompareTag("Bau")){
            SpriteRenderer b = collision.gameObject.GetComponent<SpriteRenderer>();//para pegar o componente de bau que fica a imagem
            b.sprite = bau;//troca a imagem
            pontos += 10;//vai somar os pontos
            exibirPontos.text = pontos.ToString();//vai exibir os pontos
        }

        if(collision.CompareTag("Cenoura")){
            pontos += 1;//vai somar os pontos
            exibirPontos.text = pontos.ToString();//vai exibir os pontos
            Destroy(collision.gameObject);//destroi o cenoura
        }
        if(collision.CompareTag("Casa")){
            /*pontoFinal.text = "Pontuação "+pontos.ToString();
            WinGame.SetActive(true);
            lives = 12;//permitir jogar novamente
            mover = false;//não pode mover*/
            SceneManager.LoadScene(2);
        }
    }
    
    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Escada")){
            estaSubindo = false;//para não poder subir
            descer = 0;//para não descer no chão
            rig.isKinematic = false;//para não atravessa as coisas
            rig.gravityScale = gravidade;//voltar a gravidade ao normal
            anime.SetInteger("Transicao",0);//animação parado
        }
        
        if(collision.CompareTag("Topo")){
            descer = 1;//atraversar objetos
        }
    }

    void TomarDano(){
        lives--;//perde vida
        if(lives == 2){
            livesObj[2].SetActive(false);//trocar a imagem de vida
        }

        if(lives == 1){
            livesObj[1].SetActive(false);//trocar a imagem de vida
        }

        if(lives <= 0){
           SceneManager.LoadScene(1);
        }
    }
}