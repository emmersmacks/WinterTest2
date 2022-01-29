using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    public GameObject character;
    public GameObject characterClone;
    public GameObject camera;
    public InteractiveObject interactScript;

    void Start()
    {
        character = GameObject.FindWithTag("Player");
        characterClone = GameObject.FindWithTag("Clone");
    }

    void Update()
    {
        if (interactScript.canInteract)
        {
            if (Input.GetButtonDown("Interactive"))
            {
                character.SetActive(true);
                characterClone.SetActive(false);
                camera.SetActive(false);
               
            }
        }
    }
}
