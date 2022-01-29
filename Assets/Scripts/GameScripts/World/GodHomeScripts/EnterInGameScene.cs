using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterInGameScene : MonoBehaviour
{
    public GameObject CharacterObj;
    public GameObject buttonHelp;
    public GameObject camera;
    public string sceneName;
    public bool canInteract;
    public bool inLocation;
    public ExitScript exitScript;
    public InteractiveObject interactScript;

    void Start()
    {
        interactScript.canInteract = false;
    }

    void Update()
    {
        if (interactScript.canInteract)
        {
            if (Input.GetButtonDown("Interactive"))
            {
                EnterInLocation();
            }
        }
    }

    public void EnterInLocation()
    {
        if (!inLocation) 
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        
        CharacterObj.SetActive(false);
        inLocation = true;
        camera.SetActive(true);
    }
}
