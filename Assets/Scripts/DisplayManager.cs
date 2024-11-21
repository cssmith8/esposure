using System;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayManager : MonoBehaviour{
    [HideInInspector] public int RoleID;
    public Role Role; // leave unimplemented for now
    [HideInInspector] public Sprite img; // leave unimplemented for now
    private SpriteRenderer sr;
    
    // incredibly hacky, FOR DEMONSTRATION ONLY
    private Sprite portraitSprite;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    
    public void setImage(int RoleID) {
        string path = $"Role Card Portraits/{RoleID.ToString()}";
        var createdSprite = Resources.Load<Sprite>(path);
        
        if (createdSprite != null)
        {
            if (sr != null)
            {
                sr.sprite = createdSprite;
            }
            else
            {
                Debug.Log("No SpriteRenderer found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError($"Failed to load texture: {RoleID}");
        }
    }
    
}