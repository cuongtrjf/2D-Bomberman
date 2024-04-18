using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;


    //set animation nao cua bomb se dc render ra
    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }


    //set huong animation cua bom
    public void SetDirection(Vector2 direction)//direction o day la huong muon quay toi
    {
        float angle= Mathf.Atan2(direction.y,direction.x);//tra ve 1goc rad giua direction va y,x
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);//vector3.forward la truc z huong ve mat ta, quay quanh truc z do
    }

    public void DestroyAfter(float seconds)//pha huy hoat anh sau seconds
    {
        Destroy(gameObject, seconds);
    }
}
