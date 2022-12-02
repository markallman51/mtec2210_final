using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    private float playerX;
    private float playerY;
    private Vector3 playerScreenPos;
    private float screenQuarterX = Screen.width/4;
    private float screenThirdY = Screen.height / 4;

    public float xBoundLeft = -8f;
    public float xBoundRight = 4.1f;

    public float yBoundTop = 19f;
    public float yBoundBottom = -1f;

    public float speed = .5f;
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        playerScreenPos = cam.WorldToScreenPoint(player.transform.position);

        //Debug.Log(playerScreenPos);


        //if (playerX < xBoundRight && playerX > xBoundLeft)
            
        //transform.Translate(0, 0, -10);
        if (playerScreenPos.x < screenQuarterX || playerScreenPos.x > Screen.width - screenQuarterX)
        {
            //we need both ifs.
            //the first to check if the player is at each quarter of the screen.
            //the second to check if the player is at each end of the play area.
            if (playerX < xBoundRight && playerX > xBoundLeft)
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerX, speed * Time.deltaTime),
                    Mathf.Lerp(transform.position.y, playerY, speed * Time.deltaTime), -10);

            //transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);

            /*
             11/26 Update:
            WorldToScreenPoint gets the player positon relative to the screen. now the camera doesn't move with the players every movement
            HOWEVER the camera is studdery not. need to figure this out because I'll neeed this method for camfollow on the y. the other
            method wont work

            11/30 Update:
            The way things are built now, camera is still kinda studdery but I like the way it functions. 
            If I can keep boundaries on x axis while keeping this the way it is, cam dun.
             */
        }

        if(playerScreenPos.x < screenThirdY || playerScreenPos.x > Screen.height - screenThirdY
            || playerScreenPos.y < 0)
        {
            //if (playerY < yBoundTop && playerY > yBoundBottom)
            if (playerY < yBoundTop && playerY > yBoundBottom)
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerX, speed * Time.deltaTime), 
                Mathf.Lerp(transform.position.y, playerY, speed * Time.deltaTime), -10);
        }
    }

    ///LERP101 FROM PROF
    //float timeElapsed = 0;
    // float duration = 2;

    //if (timeElapsed < duration)
    //{
    //    timeElapsed += Time.deltaTime;
    //}



    //transform.position = Vector3.Lerp(startPosition, endPosition, timeElapsed/duration);
    //transform.position = Vector3.Lerp(transform.position, endPosition , speed * Time.deltaTime);
    //float x = Mathf.Lerp(a, b, t);
    //MOVEMENT
    //redo this using physics, speed should be in fixedupdate
}
