using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criador : MonoBehaviour
{
    public GameObject criar;
    float tempo; 
    float tempoTotal;
    public float tempomin; 
    public float tempomax;
    // Start is called before the first frame update
    void Start()
    {
        tempoTotal = Random.Range(tempomin, tempomax);
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        if(tempo >= tempoTotal){
            Instantiate(criar, transform.position, transform.rotation);
            tempoTotal = Random.Range(tempomin, tempomax);
            tempo = 0f;
        }
    }
}
