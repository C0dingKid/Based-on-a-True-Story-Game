using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindMushroom : MonoBehaviour
{
    public bool isPlayerNearMushroom = false;
    public Text popupText;
    public GameObject Mushroom;
    public GameObject LockedDoor;
    public GameObject TopDoor;
    public GameObject DoorChandelier;
    public GameMan gameManager;
    public bool canReverse = false;
    public bool isEating;

    private void Update()
    {
        if (isPlayerNearMushroom && Input.GetKeyDown(KeyCode.E))
        {
            TakeShroom();
        }
    }

    private void Start()
    {
        Mushroom.gameObject.SetActive(true);
        DoorChandelier.gameObject.SetActive(true);
        LockedDoor.gameObject.SetActive(true);
        TopDoor.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearMushroom = true;
            popupText.text = "Press E to Take Shrooms";
            popupText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearMushroom = false;
            popupText.gameObject.SetActive(false);
        }
    }

    private void TakeShroom()
    {
        Mushroom.gameObject.SetActive(false);
        DoorChandelier.gameObject.SetActive(false);
        LockedDoor.gameObject.SetActive(false);
        TopDoor.gameObject.SetActive(true);
        canReverse = true;
        isEating = true;
    }
}






