using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheeseScript : MonoBehaviour
{
    public bool isPlayerNearCheese = false;
    public Text popupText;
    public GameObject Cheese;
    public bool haveCheese = false;

    private void Update()
    {
        if (isPlayerNearCheese && Input.GetKeyDown(KeyCode.E))
        {
            TakeCheese();
        }
    }

    private void Start()
    {
        Cheese.gameObject.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearCheese = true;
            popupText.text = "Press E to Take Cheese";
            popupText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearCheese = false;
            popupText.gameObject.SetActive(false);
        }
    }

    private void TakeCheese()
    {
        Cheese.gameObject.SetActive(false);
        haveCheese = true;
    }
}
