using System;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayManager : MonoBehaviour{
    [HideInInspector] public int RoleID;
    public Role Role; // leave unimplemented for now
    [HideInInspector] public Sprite img; // leave unimplemented for now
    private SpriteRenderer sr;
    
    // incredibly hacky, FOR DEMONSTRATION ONLY
    public Sprite managementSprite;
    public Sprite operationsSprite;
    public Sprite marketingSprite;
    public Sprite technologySprite;
    public Sprite financeSprite;
    public Sprite noneSprite;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }
    
    // incredibly hacky, FOR DEMONSTRATION ONLY
    public void setImage(Branch branch) {
        Sprite imageToSet;
        switch (branch)
        {
            case Branch.Management:
                imageToSet = managementSprite;
                break;
            case Branch.Operations:
                imageToSet = operationsSprite;
                break;
            case Branch.Marketing:
                imageToSet = marketingSprite;
                break;
            case Branch.Technology:
                imageToSet = technologySprite;
                break;
            case Branch.Finance:
                imageToSet = financeSprite;
                break;
            case Branch.None:
            default:
                imageToSet = noneSprite;
                break;
        }

        sr.sprite = imageToSet;
    }
}