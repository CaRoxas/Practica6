using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovimientoMario : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float velocidad = 3;
    bool salto = false;
    bool suelo = true;

    const int CAPA_MARIO_CHIQUITO = 0;
    const int CAPA_MARIO_GRANDOTE = 1;

    //GAMEOBJECTS
    public GameObject Setilla;
    
    //AUDIOS
    public AudioSource Principal;
    public AudioSource GameOverNormal;
    public AudioSource GameOverGoomba;
    public AudioSource SetaGrande;
    public AudioSource Saltillo;
    public AudioSource Moneda;
    public AudioSource Goombameh;
    public AudioSource Victoria;

    //SCRIPTS
    public Goomba ScriptGoomba;
    // Start is called before the first frame update
    void Start()
    {
        Principal.Play();
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
        /*/if (collision.gameObject.name == "MisteryBlock")
        {
            Setilla.SetActive(true);
        }*/
        if (collision.gameObject.name == "Seta")
        {
            animator.SetLayerWeight(1, 1);
            Destroy(collision.gameObject);
            SetaGrande.Play();
        }
        if (animator.GetLayerWeight(CAPA_MARIO_GRANDOTE) == 0 && collision.gameObject.name == "Goomba")
        {
            animator.SetBool("ChoqueGoom", false);
            // addforce
            transform.Translate(3 * Vector2.up * Time.deltaTime);
            GameOverGoomba.Play();
        }
        if (animator.GetLayerWeight(CAPA_MARIO_GRANDOTE) == 1 && collision.gameObject.name == "Goomba")
        {
            animator.SetLayerWeight(CAPA_MARIO_CHIQUITO, 1);
            animator.SetLayerWeight(CAPA_MARIO_GRANDOTE, 0);
        }
        if (collision.gameObject.name == "Goomba" && transform.position.y >= 2)
        {
            ScriptGoomba.AnimacionGoomba();
            Destroy(collision.gameObject);
            Goombameh.Play();
        }
    }
    private void Movimiento()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
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
            Saltillo.Play();
        }
        if (salto && suelo)
        {
            rb.AddForce(Vector2.up * 7.5f, ForceMode2D.Impulse);
            salto = false;
            suelo = false;
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
}
