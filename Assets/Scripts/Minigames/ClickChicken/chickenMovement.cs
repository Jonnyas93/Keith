using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenMovement : MonoBehaviour
{
    [SerializeField] private int lives;

    private Animator anim;

    private bool win = false;

    private float x;
    private float y;
    private int waitTimer = 0;
    private int waitCount = 60;

    private float[] xCord = { 0f, -0.1f, -0.05f, 0.05f, 0.1f };
    private float[] yCord = { 0f, -0.1f, -0.05f, 0.05f, 0.1f };

    void Start()
    {
        x = 0f;
        y = 0f;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //if (win == false)
        //{
        //    DirectionModifier();
        //    Movement();
        //}
    }

    private void FixedUpdate()
    {
        if (win == false)
        {
            DirectionModifier();
            Movement();
        }
    }

    public void DirectionModifier() // might not be the best way to execute this Function every few seconds
    {
        if (waitTimer == waitCount)
        {
            int xindex = Random.Range(1, 5); // an array starts at 1 apparently
            int yindex = Random.Range(1, 5);

            x = xCord[xindex];
            y = yCord[yindex];

            waitTimer = 0;
            waitCount = Random.Range(15, 60);
        }

        waitTimer++;
    }

    public void Movement()
    {
        transform.position += new Vector3(x, y, 0);
    }

    public void Hit()
    {
        anim.Play("hit");
        win = true;
    }
}
