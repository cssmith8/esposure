using CS = CardState;

public class LocalCard : Card {
    
    public override void Awake() { 
        DM = display.GetComponent<DisplayManager>();
        Hand = LocalHand.instance;
    }
    
    void OnMouseEnter()
    {
        if (state == CS.Idle)
        {
            Move(CS.Hovered);
            Hand.MoveCardToTop(HandIndex);
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
            Hand.SelectCard(slot.GetComponent<CardSlot>().index);
            //Flip();
            Select();
        } else if (state == CS.Selected)
        {
            Deselect();
        }
    }
}
