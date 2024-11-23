using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DisplayManager : MonoBehaviour{
    [HideInInspector] public int RoleID;
    public Role Role; // leave unimplemented for now
    [SerializeField] private SpriteRenderer portraitSprite;
    
    public void setImage(int RoleID) {
        string path = $"Role Card Portraits/{RoleID.ToString()}";
        var createdSprite = Resources.Load<Sprite>(path);
        
        if (createdSprite != null)
        {
            if (portraitSprite != null)
            {
                portraitSprite.sprite = createdSprite;
                Debug.Log($"Portrait sprite set to: {RoleID}");
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