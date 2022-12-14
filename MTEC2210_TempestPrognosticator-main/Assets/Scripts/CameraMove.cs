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

        //we need both ifs.
        //the first to check if the player is at each quarter of the screen.
        //the second to check if the player is at each end of the play area.
        if (playerScreenPos.x < screenQuarterX || playerScreenPos.x > Screen.width - screenQuarterX)
        {

            if (playerX < xBoundRight && playerX > xBoundLeft)
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerX, speed * Time.deltaTime),
                    transform.position.y, -10);

            //Mathf.Lerp(transform.position.y, playerY, speed * Time.deltaTime)
            //Mathf.Lerp(transform.position.x, playerX, speed * Time.deltaTime)


        }

        if (playerScreenPos.y < screenThirdY || playerScreenPos.y > Screen.height - screenThirdY
            || playerScreenPos.y < 0)
        {
            //if (playerY < yBoundTop && playerY > yBoundBottom)
            if (playerY < yBoundTop && playerY > yBoundBottom)
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, playerY, speed * Time.deltaTime), -10);
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
