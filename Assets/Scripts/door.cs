using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorHandle : MonoBehaviour
{
    public Text popupText;
    public bool isPlayerNearDoor = false;

    public Canvas canvas1;
    public Canvas canvas2;
    public GameObject title;

    // Start is called before the first frame update
    void Start()
    {
        popupText.gameObject.SetActive(false);

    }
    void OpenDoor()
    {
        if (canvas1.gameObject.activeSelf)
        {
            canvas1.gameObject.SetActive(false);
            canvas2.gameObject.SetActive(true);
            title.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (isPlayerNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearDoor = true;
            popupText.text = "Press E to Enter";
            popupText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearDoor = false;
            popupText.gameObject.SetActive(false);
        }
    }
}
