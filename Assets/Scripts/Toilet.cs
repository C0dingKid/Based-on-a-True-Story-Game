using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Toilet : MonoBehaviour
{
    public bool isPlayerNearToilet = false;
    public Text popupText;
    public GameObject toilet;
    public GameObject blackScreen;
    public GameObject player;
    public GameObject canvas4;





    private void Start()
    {
        blackScreen.gameObject.SetActive(false);

    }
    private void Update()
    {
        if (isPlayerNearToilet && Input.GetKeyDown(KeyCode.E))
        {
            Poop();
        }
    }








    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearToilet = true;
            popupText.text = "Press E to Poop";
            popupText.gameObject.SetActive(true); // Show popupText when near hole
        }

    }




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearToilet = false;
            popupText.gameObject.SetActive(false); // Hide popupText when player exits
        }
    }




    private void Poop()
    {
        blackScreen.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        StartWaiting();
    }
    private void StartWaiting()
    {
        StartCoroutine(WaitAndChangeScene(5f)); // Wait for 3 seconds and then change to "TargetScene"
    }


    // Define the coroutine
    private IEnumerator WaitAndChangeScene(float waitTime)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);


        // Code to execute after the wait
        ChangeScene();
    }


    // The method that changes the scene
    private void ChangeScene()
    {
        // Load the scene with the specified name
        SceneManager.LoadScene("Poop");
    }
}












