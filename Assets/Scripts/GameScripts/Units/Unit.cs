using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float health;

    public void StrongBlow(Rigidbody2D rigid)
    {
        SpriteRenderer unitSprite = GameObject.Find("Ogr").GetComponentInChildren<SpriteRenderer>();
        rigid.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);

        if(unitSprite.flipX) 
            rigid.AddForce(Vector2.left * 6f, ForceMode2D.Impulse);
        else 
            rigid.AddForce(Vector2.right * 6f, ForceMode2D.Impulse);
    }

    public void GetDamage(float pointDamage)
    {
        health -= pointDamage;
    }
}
