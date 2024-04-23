using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")] 
    public KeyCode inputKey = KeyCode.Space;//nhan space de tha boom
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;//thoi gian bomb phat no
    public int bombAmount = 1; //so luong bomb ban dau cua player
    public int bombRemaining = 0;//so luong bomb con lai 
    private int maxBomb = 3;

    //animation bomb explosion
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;//thoi gian hoat anh bomd no
    public int explosionRadius = 1;//ban kinh no cua bomb, ban dau se la 1 o vuong
    private int maxRadius = 3;

    //explode destructible
    [Header("Destructible")]
    public Destructible destructiblePrefab;
    public Tilemap destructibleTile;//tham chieu den tilemap destrucible de pha huy no



    private void OnEnable()//ham nay de cap nhat lai doi tuong khi moi lan duoc goi lai
    {
        bombRemaining = bombAmount;
    }

    private void Update()
    {
        if (bombRemaining > 0 && Input.GetKeyDown(inputKey))//neu 1 nut dc nhan 1 lan, k phai giu
        {//dieu kien de tha dc bomb
            StartCoroutine(PlaceBomb());//ham startcoroutine dung de chay 1 ham ma trong do no co the tam dung roi tiep tuc thuc thi
        }
    }

    private IEnumerator PlaceBomb()//dat bomb 
    {
        Vector2 pos = transform.position;//lay vi tri cua player lam vi tri cua bomb
        /*tuy nhien trong qua trinh player di chuyen thi co the player di chuyen k vao chuan cac o
         vi vay nen bomb se k dc dat chuan trong cac o, ta phai lam tron vi tri cua bomb*/
        pos.x= Mathf.Round(pos.x);//lam tron toa do x sang int
        pos.y= Mathf.Round(pos.y);//lam tron toa do y sang int

        GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);//identity la khong xoay
        bombRemaining--;//giam so luong bomb cua nguoi choi


        yield return new WaitForSeconds(bombFuseTime);//sau 3 giay thi bomb phat no

        pos = bomb.transform.position;//dat lai vi tri cua qua bomb
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);

        Explosion explosion= Instantiate(explosionPrefab, pos, Quaternion.identity);//render hoat anh no, coi no la 1 chuc nang nen kieu tra ve khac
        explosion.SetActiveRenderer(explosion.start);//render hoat anh start la dau tien
        explosion.DestroyAfter(explosionDuration);//xoa hoat anh sau khi het thoi gian no bom

        //render middle bom va end bom theo 4 huong lan luot
        Explode(pos, Vector2.up, explosionRadius);
        Explode(pos, Vector2.down, explosionRadius);
        Explode(pos, Vector2.left, explosionRadius);
        Explode(pos, Vector2.right, explosionRadius);


        Destroy(bomb);

        bombRemaining++;//sau khi bomb pha huy thi them lai bomb co the dat
    }




    //ham  nay xac dinh xem bom no o vi tri nao, theo huong nao, do dai bom la bnhieu
    private void Explode(Vector2 position, Vector2 direction,int lenght)
    {
        if(lenght <=0)//neu do dai qua bom <= 0 thi tuc la no da no het nen k lam gi ca
        {
            return;
        }

        position += direction;//vi tri moi theo huong len xuong trai phai, xu li tung huong pha huy cua bom



        if(Physics2D.OverlapBox(position, Vector2.one/2f, 0f, explosionLayerMask))
        //ham overlapBox tra ve 1 hop xem cac colider co dang chen len hop do khong
        //=> ham nay check bom no co va cham voi map k de k render hoat anh no
        //cac tham so posion la vi tri check, size box check la 1/2, layer check voi no (o day dc gan la stage)
        {
            //pha huy tilemap tai day
            ClearDestructible(position);
            return ;
        }
        

        Explosion explosion= Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(lenght > 1 ? explosion.middle : explosion.end);//neu > 1 la dang trong vu no thi render midle va ngc lai
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, lenght - 1);//giam ban kinh vu no cho toi khi bang 0 thi ket thuc render
    }


    private void ClearDestructible(Vector2 position)//xoa tile map khi bom no
    {
        Vector3Int cell= destructibleTile.WorldToCell(position);//no hoat dong voi vecto3,chuyen vitri thanh o de xoa
        TileBase tile= destructibleTile.GetTile(cell);//lay tile tai o do

        if (tile != null)
        {
            //neu o do k rong thi se tao hieu ung no brick, sau do xoa cell
            Instantiate(destructiblePrefab, position, Quaternion.identity);//sinh hieu ung pha tuong
            destructibleTile.SetTile(cell, null);//dat no thanh rong de xoa
        }

    }

    public void AddBomb()//phuc vu pick item tang so luong bom
    {
        if (bombAmount < maxBomb)
        {
            bombAmount++;
            bombRemaining++;
        }
    }

    public void PlusRadius()
    {
        if (explosionRadius < maxRadius)
        {
            explosionRadius++;
        }
    }


    private void OnTriggerExit2D(Collider2D other)//khi player tha bomb va roi khoi collider cua qua boom thi ham nay dc goi
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;//tat istrigger tren qua bomb
        }
    }
}
