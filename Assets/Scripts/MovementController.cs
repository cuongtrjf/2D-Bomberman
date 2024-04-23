using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }//chi co player moi set rigi duoc

    private Vector2 direction = Vector2.down;//khoi tao nhan vat huong xuong duoi
    public float speed = 5f;//toc do cua nhan vat
    private float maxSpeed = 8f;


    //input movement
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;


    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;

    private AnimatedSpriteRenderer activeSpriteRenderer;//xac dinh xem cai animation nao dc active

    public AnimatedSpriteRenderer spriteRendererDeath;//animation death

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()
    {
        //kiem tra xem 1 nut nao do dang duoc nhan hay giu trong frame hien tai thi thuc hien function
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down,spriteRendererDown);
        }else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left,spriteRendererLeft);
        }else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right,spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero,activeSpriteRenderer);//tiep tuc ani dang hoat dong hoac hoat dong truoc do
        }
    }

    //fixedUpdate cho cac chuyen dong de duoc muot hon
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;//tinh toan di chuyen 
        rigidbody.MovePosition(position + translation);//di chuyen nhan vat toi vi tri moi
    }


    //set 1 huong moi cho nhan vat khi di chuyen
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;//gan trang thai enable cua spriteRedererUp vao ket qua cua so sanh ve phai
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;//render sprite hien tai
        activeSpriteRenderer.idle = direction == Vector2.zero;//gan trang thai idle
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))//neu va cham voi vu no bom
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;//vo hieu hoa move ment
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;

        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    public void PlusSpeed()
    {
        if (speed < maxSpeed)
        {
            speed++;
        }
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);//ngan toan bo hoat dong ve nguoi choi
        FindObjectOfType<GameManager>().CheckWinState();
    }

}
