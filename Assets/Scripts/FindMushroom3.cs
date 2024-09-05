using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindMushroom3 : MonoBehaviour
{
    public bool isPlayerNearMushroom = false;
    public Text popupText;
    public Text poopText;
    public GameObject Mushroom;
    public GameObject bathDoor;
    public FindMushroom findMushroom;
    public bool ateMush3 = false;
    public bool isEating;


    // Start is called before the first frame update
    void Start()
    {
        bathDoor.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (isPlayerNearMushroom && Input.GetKeyDown(KeyCode.E))
        {
            TakeShroom();
        }
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
        bathDoor.gameObject.SetActive(true);
        poopText.text = "I have explosive diarrhea";
        poopText.gameObject.SetActive(true);
        findMushroom.canReverse = false;
        ateMush3 = true;
        isEating = true;

    }
}
