using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    public Image healthBarCharacter;
    public Image staminaBarCharacter;
    public Image bossHealth;
    public GameObject canvasHealth;
    public bool bossIsActive;
    Character characterScript;
    public Monsters bossScript;
    public GameObject inventory;

    void Start()
    {
        characterScript = GameObject.Find("Character").GetComponent<Character>();
    }

    void Update()
    {
        BossBarManager();
        GameBarManager();
        if (Input.GetButtonDown("Inventory"))
        {
            if (inventory.activeInHierarchy) inventory.SetActive(false);
            else inventory.SetActive(true);

        }
    }

    public void BossBarManager()
    {
        if (bossIsActive)
        {
            canvasHealth.SetActive(true);
            bossHealth.fillAmount = bossScript.health * 0.01f;
        }
        else canvasHealth.SetActive(false);
    }

    public void GameBarManager()
    {
        healthBarCharacter.fillAmount = characterScript.health * 0.01f;
        staminaBarCharacter.fillAmount = characterScript.health * 0.01f;
    }
}
