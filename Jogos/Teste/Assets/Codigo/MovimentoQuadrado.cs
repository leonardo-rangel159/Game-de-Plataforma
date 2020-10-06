using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoQuadrado : MonoBehaviour
{
    public float velocida;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 12f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(Mathf.PingPong(Time.time, 0.8f), transform.localScale.y);
        transform.Translate(new Vector2(0f, velocida * Time.deltaTime));
    }
}
