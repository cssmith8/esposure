using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCard : MonoBehaviour
{
    public bool isUp = false;
    private RectTransform rt;
    private float screenheight;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        screenheight = Screen.height;
        MoveDown();
    }

    public void MoveUp()
    {
        if (!isUp)
        {
            StartCoroutine(MoveUpCoroutine());
        }
    }

    //coroutine to move the card up
    IEnumerator MoveUpCoroutine()
    {
        //set the card to be up
        isUp = true;
        float time = Time.timeSinceLevelLoad;
        //move the card up
        while (rt.position.y < screenheight / 2)
        {
            rt.position = new Vector3(rt.position.x, rt.position.y + 2 * screenheight * (Time.timeSinceLevelLoad - time), rt.position.z);
            time = Time.timeSinceLevelLoad;
            yield return null;
        }

        
        
    }

    public void MoveDown()
    {
        if (isUp)
        {
            StartCoroutine(MoveDownCoroutine());
        }
    }

    //coroutine to move the card down
    IEnumerator MoveDownCoroutine()
    {
        //set the card to be down
        isUp = false;
        float time = Time.timeSinceLevelLoad;
        //move the card down
        while (rt.position.y > screenheight / 4)
        {
            rt.position = new Vector3(rt.position.x, rt.position.y - 2 * screenheight * (Time.timeSinceLevelLoad - time), rt.position.z);
            time = Time.timeSinceLevelLoad;
            yield return null;
        }

        
    }

}
