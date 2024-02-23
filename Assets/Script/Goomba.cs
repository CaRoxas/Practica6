using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    int velocidad = 1;
    bool derecha = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 9.95)
        {
            derecha = false;
        }
        else if (transform.position.x < 5.25)
        {
            derecha = true;
        }
        if (derecha == true)
        {
            transform.Translate(velocidad * Vector2.right * Time.deltaTime);
        }
        else
        {
            transform.Translate(velocidad * Vector2.left * Time.deltaTime);
        }
    }
    public void AnimacionGoomba()
    {
        animator.SetBool("aplastado", false);
    }
}

