using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DisplayManager : MonoBehaviour{
    // [HideInInspector] public int roleID;
    [SerializeField] private SpriteRenderer portraitSprite;
    [SerializeField] private GameObject textCanvas;
    [SerializeField] private GameObject nameTextObj;
    [SerializeField] private GameObject descriptionTexObj; 
    private TextMeshProUGUI nameText; 
    private TextMeshProUGUI descriptionText;

    private void Awake() {
        nameText = nameTextObj.GetComponent<TextMeshProUGUI>();
        descriptionText = descriptionTexObj.GetComponent<TextMeshProUGUI>();
        portraitSprite.gameObject.SetActive(false);
        textCanvas.SetActive(false);
    }

    public void setRole(Role role) {
        setImage(role.ID);
        setName(role.Name);
        setDescription(role.Description);
    }

    public void setImage(int RoleID) {
        // roleID = RoleID;
        string path = $"Role Card Portraits/{RoleID.ToString()}";
        var createdSprite = Resources.Load<Sprite>(path);
        
        if (createdSprite != null)
        {
            if (portraitSprite != null)
            {
                portraitSprite.sprite = createdSprite;
                // Debug.Log($"Portrait sprite set to: {RoleID}");
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

    public void setName(string name) {
        name = name.ToUpper();
        nameText.text = name;
    }
    
    public void setDescription(string desc) {
        desc = desc.ToUpper();
        descriptionText.text = desc;
    }
}