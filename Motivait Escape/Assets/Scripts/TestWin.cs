using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWin : MonoBehaviour
{
    public GameObject highscoreScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            var player = other.transform.gameObject.GetComponent<CharacterMovement>();
            var timer = player.transform.GetComponent<Timer>();
            timer.isActive = false;
            highscoreScreen.SetActive(true);
        }
    }
}
