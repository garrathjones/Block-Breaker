using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float screenWidth = 16f;
    [SerializeField] float screenHeight = 12f;
    [SerializeField] float xMin = 1.05f;
    [SerializeField] float xMax = 14.95f;
    [SerializeField] float yMin = 1f;
    [SerializeField] float yMax = 2f;


    // cached refs
    GameSession theGameSession;
    Ball theBall;


    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        float yMousePosInUnits = Input.mousePosition.y / Screen.width * screenHeight;

        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(),xMin,xMax);
        paddlePos.y = Mathf.Clamp(yMousePosInUnits, yMin, yMax);

        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if(theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidth;
        }
    }
}
