using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Controller : MonoBehaviour
{
    public Text moleNumberText;
    public static int moleNumber;
    public Text slimeNumberText;
    public static int slimeNumber;
    public Text playerHealth;
    public GameObject player;
    public GameObject fadeSprite;
    public bool doorEnterCooldown = false;
    public int currentRoomCode;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        moleNumberText.text = moleNumber.ToString();
        slimeNumberText.text = slimeNumber.ToString();
        playerHealth.text = player.GetComponent<Player_Controller>().health.ToString();
        if(int.Parse(playerHealth.text) <= 0)
        {
            StartCoroutine(Fade());
        }
    }

    public void ChangeArea(GameObject exit)
    {
        StartCoroutine(Fade());
        player.transform.position = exit.GetComponent<Transform>().position;
        currentRoomCode = exit.GetComponent<Door_Controller>().roomCode;
    }

    // adapted from https://turbofuture.com/graphic-design-video/How-to-Fade-to-Black-in-Unity
    IEnumerator Fade(bool fade = true, int fadeSpeed = 5)
    {
        Color colour = fadeSprite.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        if(fade)
        {
            while(fadeSprite.GetComponent<SpriteRenderer>().color.a < 1)
            {
                fadeAmount = colour.a + (fadeSpeed * Time.deltaTime);
                colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
                fadeSprite.GetComponent<SpriteRenderer>().color = colour;
                yield return null;
            }
        }
        else
        {
            while (fadeSprite.GetComponent<SpriteRenderer>().color.a > 0)
            {
                fadeAmount = colour.a - (fadeSpeed * Time.deltaTime);
                colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
                fadeSprite.GetComponent<SpriteRenderer>().color = colour;
                yield return null;
            }
        }
        
        if(fade == true)
        {
            StartCoroutine(Fade(false));
        }   
    }

    public static void AddEnemy(string enemy, int num = 1)
    {
        switch (enemy)
        {
            case "Mole":
                moleNumber += num;
                break;
            case "Slime":
                slimeNumber += num;
                break;
        }
        
    }

    public static void RemoveEnemy(string enemy, int num = 1)
    {
        switch (enemy)
        {
            case "Mole":
                moleNumber -= num;
                break;
            case "Slime":
                slimeNumber -= num;
                break;
        }
    }
}
