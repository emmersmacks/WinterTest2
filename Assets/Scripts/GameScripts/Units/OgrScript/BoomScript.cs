using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : Monsters
{

    new private Rigidbody2D rb;

    void Update()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unitScript = collider.GetComponent<Unit>();
        rb = collider.GetComponent<Rigidbody2D>();
        if (unitScript && unitScript is Character)
        {
            unitScript.StrongBlow(rb);
            unitScript.GetDamage(25f);
        }
    }
}
