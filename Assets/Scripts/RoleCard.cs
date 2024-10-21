using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCard : MonoBehaviour
{
    public bool IsUp { get; private set; }
    public int CardID { get; private set; }
    public string SlotID { get; private set; }
    private float _screenHeight;
    private Camera _mainCam;
    [SerializeField] private float selectedPos = -2f;
    [SerializeField] private float defaultPos = -3f;
    [SerializeField] private float cardMoveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        // _rt = GetComponent<RectTransform>();
        MoveToIdle();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 key pressed.");
            if (IsUp) {
                MoveToIdle();
            }
            else {
                MoveToSelected();
            }
        }
    }

    public void MoveToSelected()
    {
        StopCoroutine(MoveToIdleCoroutine());
        StartCoroutine(MoveToSelectedCoroutine());
    }
    
    public void MoveToIdle()
    {
        StopCoroutine(MoveToSelectedCoroutine());
        StartCoroutine(MoveToIdleCoroutine());
    }

    //coroutine to move the card up
    IEnumerator MoveToSelectedCoroutine()
    {
        IsUp = true; //set the card to be up
        Debug.Log("IsUp = true");
        float time = Time.timeSinceLevelLoad;
        Vector3 position = transform.position;

        float targetYPos = selectedPos;
        Vector3 targetPos = new Vector3(position.x, targetYPos, position.z);

        // move the card up
        while (transform.position != targetPos) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, cardMoveSpeed);
            yield return null;
        }
    }

    //coroutine to move the card down
    IEnumerator MoveToIdleCoroutine()
    {
        IsUp = false; //set the card to be down
        Debug.Log("IsUp = false");
        float time = Time.timeSinceLevelLoad;
        Vector3 position = transform.position;

        float targetYPos = defaultPos;
        Vector3 targetPos = new Vector3(position.x, targetYPos, position.z);

        // move the card down
        while (transform.position != targetPos) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, cardMoveSpeed);
            yield return null;
        }
    }

}
