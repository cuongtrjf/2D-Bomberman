using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,destructionTime);//pha huy cell sau 1 giay khi bat dau tao object
    }
}
