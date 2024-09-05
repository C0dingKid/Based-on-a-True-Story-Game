using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class mouseHole : MonoBehaviour
{
    public bool isPlayerNearMouseHole = false;
    public Text popupText;
    public Text mouseTalk;
    public GameObject MouseHole;
    public GameObject Mushroom3;
    public CheeseScript cheeseScript;
    public PlayerMovement playerMovement;
    public bool givenCheese;
    public bool isTalking = false;




    private void Start()
    {
        Mushroom3.gameObject.SetActive(false);
    }




    private void Update()
    {
        if (isPlayerNearMouseHole && Input.GetKeyDown(KeyCode.E))
        {
            EnterHole();
        }


        if (isTalking)
        {
            mouseTalk.gameObject.SetActive(true); // Ensure mouseTalk is active if talking
        }
        else
        {
            mouseTalk.gameObject.SetActive(false); // Ensure mouseTalk is inactive if not talking
        }
    }








    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMovement.size <= 0.3)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isPlayerNearMouseHole = true;
                popupText.text = "Press E to Talk";
                if (!isTalking)
                {
                    popupText.gameObject.SetActive(true); // Show popupText when near hole
                }
            }
        }
    }




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearMouseHole = false;
            popupText.gameObject.SetActive(false); // Hide popupText when player exits
            mouseTalk.gameObject.SetActive(false); // Hide mouseTalk when player exits
            isTalking = false;
        }
    }




    private void EnterHole()
    {
        isTalking = true;
        popupText.gameObject.SetActive(false); // Hide popupText when entering the hole


        if (cheeseScript.haveCheese == false && givenCheese == false)
        {
            mouseTalk.text = "The mouse is hungry. Don't disturb him!";
        }
        else if (cheeseScript.haveCheese == true)
        {
            mouseTalk.text = "The mouse accepts your offering!";
            Mushroom3.gameObject.SetActive(true);
            cheeseScript.haveCheese = false;
        }
        else if (givenCheese == true)
        {
            mouseTalk.text = "The mouse has nothing left to give!";
        }
        mouseTalk.gameObject.SetActive(true); // Show mouseTalk
    }
}












