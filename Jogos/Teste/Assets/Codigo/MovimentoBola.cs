using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovimentoBola : MonoBehaviour
{
    public float z; 
    public bool direcao;
    public GameObject gameOver;
    public int ponto;
    public Text pontucao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verficar se clicou
        if (Input.GetMouseButtonDown(0)){
            direcao = !direcao;
        }
        //Mover as bolas e se clicou vai mudar a direção
        if (direcao == false){
            transform.Rotate(new Vector3(0f, 0f, z * Time.deltaTime));
        }else{
           transform.Rotate(new Vector3(0f, 0f, -z * Time.deltaTime)); 
        }
        
    }

    public void ChamarGameOver(){
        gameOver.SetActive(true);
    }
    public void Reiniciar(){
        SceneManager.LoadScene(0);
    }
    public void AtualizarPonto(){
        ponto ++;
        pontucao.text = ponto.ToString();
    }
}
