using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBehaviour : MonoBehaviour
{
    private GameObject      myPlayerObject;
    private Player          myPlayerScript;
    private float           myTimer;
    private int             myCurrentEvent;
    
    private enum Direction
    {
        eLeft = -1,
        eRight = 1
    }
    
    void DoStuff()
    {
        Walk(Direction.eLeft, 0.3f);
        Walk(Direction.eRight, 0.5f);
    }

    // Use this for initialization
    void Start ()
    {
        myCurrentEvent      = 0;

        myPlayerObject = GameObject.Find("Ajzak");
        myPlayerScript = myPlayerObject.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        myTimer += Time.deltaTime;
    }

    private void Walk(Direction aDirection, float aHowLong)
    {
        myPlayerScript.Walk((int)aDirection);
    }
}
