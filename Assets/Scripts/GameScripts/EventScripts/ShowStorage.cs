using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStorage : MonoBehaviour
{
    public GameObject StorageItem;
    public bool isShow = false;
    public InteractiveObject InteractScript;

    void Update()
    {
        if (Input.GetButtonDown("Interactive"))
        {
            if(!isShow)
            {
                if(InteractScript.canInteract)
                {
                    StorageItem.SetActive(true);
                    isShow = true;
                }
                
            }
            else
            {
                StorageItem.SetActive(false);
                isShow = false;
            }
        }
    }

    
}
