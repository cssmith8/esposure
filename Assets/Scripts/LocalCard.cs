using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.GraphicsBuffer;
using CS = CardState;

public class LocalCard : Card {
    public DisplayManager PortraitManager { get; private set; }
    
    void Awake() { 
        PortraitManager = display.GetComponent<DisplayManager>();
        // Assign(transform.parent.gameObject);
    }
    
    public void AssignCard(GameObject card) {
        
    }

    void Start() {
        
    }
    
    void OnMouseEnter()
    {
        if (state == CS.Idle)
        {
            Move(CS.Hovered);
            
        }
    }

    void OnMouseExit()
    {
        if (state == CS.Hovered)
        {
            Move(CS.Idle);
        }
    }

    void OnMouseDown()
    {
        if (state == CS.Hovered)
        {
            LocalHand.instance.SelectCard(slot.GetComponent<CardSlot>().index);
            //Flip();
            Select();
            return;
        }
        if (state == CS.Selected)
        {
            Deselect();
            return;
        }
    }
}
