using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//dieu chinh animation theo huong cua nhan vat, tao animation theo huong cua nhan vat
public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;//sprite idle
    public Sprite[] animationSprites;//mang luu tru cac sprite cua nhan vat

    public float animationTime = 0.25f;//thoi gian lap lai trong ham Invoke
    private int animationFrame = 0;// chi so frame hien tai 

    public bool loop = true;//trang thai lap lai cua animation
    public bool idle = true;//trang thai cua nhan vat dang idle hay di chuyen


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        idleSprite = animationSprites[0];
    }

    private void OnEnable()//ham nay duoc goi khi object duoc hien bat va hoat dong
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start()//duoc goi o frame dau tien khi object run, duoc sd de khoi tao trang thai ban dau cua doi tuong
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);//sau animationTime thi NextFrame dc goi lai
    }

    private void NextFrame()//ham nay dat lai chi so frame de khi lap lai khong loi
    {
        animationFrame++;//thay doi khung hinh
        if (loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        if (idle)
        {
            spriteRenderer.sprite = idleSprite;//neu dang trang thai idle thi dat spire thanh idle
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}
