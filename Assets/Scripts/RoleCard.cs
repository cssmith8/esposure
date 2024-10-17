using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCard : MonoBehaviour
{
    public bool IsUp { get; private set; }
    public int Index { get; private set; }
    private RectTransform _rt;
    private float _screenHeight;
    [SerializeField] private float selectedPos = 0.25f;
    [SerializeField] private float defaultPos = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // _rt = GetComponent<RectTransform>();
        _screenHeight = Screen.height;
        MoveDown();
    }

    public void MoveUp()
    {
        StopCoroutine(MoveDownCoroutine());
        StartCoroutine(MoveUpCoroutine());
    }
    
    public void MoveDown()
    {
        StopCoroutine(MoveUpCoroutine());
        StartCoroutine(MoveDownCoroutine());
    }

    //coroutine to move the card up
    IEnumerator MoveUpCoroutine()
    {
        IsUp = true; //set the card to be up
        float time = Time.timeSinceLevelLoad;
        
        Vector3 position = transform.position;

        // move the card up
        while (transform.position.y < _screenHeight * selectedPos) {
            position.y += 2 * _screenHeight * (Time.timeSinceLevelLoad - time);
            transform.position = position;
            time = Time.timeSinceLevelLoad;

            yield return null;
        }
    }

    //coroutine to move the card down
    IEnumerator MoveDownCoroutine()
    {
        IsUp = false; //set the card to be down
        float time = Time.timeSinceLevelLoad;
        Vector3 position = transform.position;

        // move the card down
        while (transform.position.y < _screenHeight * defaultPos) {
            position.y -= 2 * _screenHeight * (Time.timeSinceLevelLoad - time);
            transform.position = position;
            time = Time.timeSinceLevelLoad;

            yield return null;
        }
    }

}
