using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public Sprite[] sprite;


    public void ChangeSprite(int num)
    {
        GetComponent<SpriteRenderer>().sprite = sprite[num];
    }

}
