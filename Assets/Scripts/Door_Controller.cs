using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    public GameObject exit;
    public Level_Controller levelController;
    public int roomCode;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player") && levelController.doorEnterCooldown == false)
        {
            StartCoroutine(EnterDoor());
        }
    }

    IEnumerator EnterDoor()
    {
        levelController.doorEnterCooldown = true;
        levelController.ChangeArea(exit);
        yield return new WaitForSeconds(1.0f);
        levelController.doorEnterCooldown = false;
    }

}
