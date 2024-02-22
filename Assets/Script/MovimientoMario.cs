using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMario : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float velocidad = 3;
    bool salto = false;
    bool suelo = true;
    public GameObject Setilla;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Saltar();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            suelo = true;
        }
        if (collision.gameObject.name == "MisteryBlock")
        {
            Setilla.SetActive(true);
        }
        if (collision.gameObject.name == "Seta")
        {
            animator.SetLayerWeight(1, 1);
            Destroy(collision.gameObject);
        }
    }
    private void Movimiento()
    {
        float movX = Input.GetAxis("Horizontal");
        /*/if (Input.GetButton(movX))
        {
            animator.SetBool("Pabajo", suelo);
        }*/
        Vector2 movilidad = new Vector2(movX * 3, rb.velocity.y);
        rb.velocity = movilidad;
        animator.SetFloat("MovDer", movilidad.x);
        animator.SetFloat("MovIzq", movilidad.x);
        animator.SetBool("Jump", !suelo);
    }
    private void Saltar()
    {
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
        if (salto && suelo)
        {
            rb.AddForce(Vector2.up * 7.5f, ForceMode2D.Impulse);
            salto = false;
            suelo = false;
        }
    }
}
