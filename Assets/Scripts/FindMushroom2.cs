using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindMushroom2 : MonoBehaviour
{
    public bool isPlayerNearMushroom = false;
    public Text popupText;
    public GameObject Mushroom;
    public bool canShrink = false;
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
        canShrink = true;
        isEating = true;

    }
}











