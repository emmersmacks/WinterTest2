using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject buttonHelp;
    public bool canInteract;

    void OnTriggerEnter2D(Collider2D collider)
    {
        canInteract = true;
        Unit unitScript = collider.GetComponent<Unit>();
        if (unitScript is Character)
        {
            buttonHelp.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        canInteract = false;
        Unit unitScript = collider.GetComponent<Unit>();
        if (unitScript is Character)
        {
            buttonHelp.SetActive(false);
        }
    }
}
