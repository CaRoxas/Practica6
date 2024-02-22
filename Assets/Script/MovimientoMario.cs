using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMario : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    public float velocidad = 3;
    bool salto = false;
    bool suelo = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Saltar();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            suelo = true;
        }
    }
    private void Saltar()
    {
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
        if (salto && suelo)
        {
            rb.AddForce(Vector2.up * 5, ForceMode.Impulse);
            salto = false;
            suelo = false;
        }
    }
}
