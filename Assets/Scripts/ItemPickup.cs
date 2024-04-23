using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //phan loai item
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType Type;


    private void OnItemPickup(GameObject player)
    {
        switch (Type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BlastRadius:
                player.GetComponent<BombController>().PlusRadius();
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<MovementController>().PlusSpeed();
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {//neu nguoi choi va cham vao item 
            OnItemPickup(other.gameObject);
        }
    }
}
