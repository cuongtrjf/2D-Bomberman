using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;


    [Range(0f, 1f)]//gioi han pham vi cua itemSpawnChance
    public float itemSpawnChance = 0.2f;//co hoi spawn ra item khi pha tuong
    public GameObject[] spawnableItem;//list item co the spawn ra



    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,destructionTime);//pha huy cell sau 1 giay khi bat dau tao object
    }

    private void OnDestroy()
    {
        if(spawnableItem.Length > 0 && Random.value < itemSpawnChance)//random.value cho gia tri 0-1
        {
            int randomIndex = Random.Range(0, spawnableItem.Length);
            Instantiate(spawnableItem[randomIndex],transform.position,Quaternion.identity);
        }
    }
}
