using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgrAreaController : MonoBehaviour
{
    private GameObject ogrObj;
    private Animator animator;
    private Ogr unit;
    private bool isComplite;
    UIcontroller scriptUI;

    void Start()
    {
        ogrObj = GameObject.Find("Ogr");
        animator = ogrObj.GetComponent<Animator>();
        unit = ogrObj.GetComponent<Ogr>();
        isComplite = false;
        scriptUI = GameObject.Find("Controller").GetComponent<UIcontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unitScript = collider.GetComponent<Unit>();
        if (unitScript && unitScript is Character)
        {
            if (!isComplite) StartCoroutine(WaitAwake());
            scriptUI.bossScript = unit;
            scriptUI.bossIsActive = true;
        }
        else if (unitScript && unitScript is Ogr)
        {
            Ogr ogrScript = collider.GetComponent<Ogr>();
            ogrScript.GoHome();
            scriptUI.bossIsActive = false;
        }
    }

    IEnumerator WaitAwake()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(2f);
        unit.enabled = true;
    }
}
