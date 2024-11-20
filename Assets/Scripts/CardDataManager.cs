using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public enum Branch
{
    None = 0,
    Management = 1,
    Operations = 2,
    Marketing = 3,
    Technology = 4,
    Finance = 5
}

[Serializable]
public class Role
{
    public int ID;
    public string Name;
    public string Branch; // Temporarily store the branch as a string
    public string Description;
    public List<int> ChallengeIDs;
    public Branch BranchEnum;

    // Converts the Role into one with properly parsed enums
    public void ParseBranch() {
        BranchEnum = Enum.TryParse(Branch.Trim(), true, out Branch parsedBranch) ? parsedBranch : global::Branch.None;
    }
}

[Serializable]
public class Challenge {
    public int ID;
    public string Description;
    public string Role;
    public string Branch; // Keep Branch as string for JSON deserialization
    public Branch BranchEnum; // Enum for easier usage within Unity

    // Method to parse the Branch string into an enum
    public void ParseBranch()
    {
        if (Enum.TryParse(Branch, true, out Branch parsedBranch))
        {
            BranchEnum = parsedBranch;
        }
        else
        {
            BranchEnum = global::Branch.None; // Default if parsing fails
        }
    }
}

[Serializable]
public class CardDataManager : MonoBehaviour {
    public List<Role> Roles { get; private set; } = new List<Role>();
    public List<Challenge> Challenges { get; private set; }
    public static CardDataManager Instance { get; private set; }

    public void Awake() {
        Debug.Log("CardDataManager Awakens");
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple GameManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        Debug.Log("Import starts");
        ImportRoles();
        ImportChallenges();
        Debug.Log("Import ends");
    }

    private void ImportRoles()
    {
        string jsonPath = Application.dataPath + "/RoleCards/Data/RoleData.json";

        if (File.Exists(jsonPath))
        {
            string jsonContent = File.ReadAllText(jsonPath);

            // Wrap the JSON content in an artificial array wrapper
            string wrappedJson = "{ \"Roles\": " + jsonContent + " }";

            // Deserialize into a RoleCollection object
            RoleCollection roleCollection = JsonUtility.FromJson<RoleCollection>(wrappedJson);

            // Iterate over the roles and process them
            foreach (var role in roleCollection.Roles)
            {
                role.ParseBranch();
                Roles.Add(role);

                // Print details for verification
                Debug.Log($"ID: {role.ID}");
                Debug.Log($"Role Name: {role.Name}");
                Debug.Log($"Branch: {role.BranchEnum}");
                Debug.Log($"Challenge IDs: {string.Join(", ", role.ChallengeIDs)}");
                Debug.Log($"Description: {role.Description}");
            }
        }
        else
        {
            Debug.LogError("JSON file not found: " + jsonPath);
        }
    }
    
    private void ImportChallenges()
    {
        string jsonPath = Application.dataPath + "/RoleCards/Data/ChallengeData.json";
    
        if (File.Exists(jsonPath))
        {
            string jsonContent = File.ReadAllText(jsonPath);
    
            // Wrap the JSON content in an artificial array wrapper
            string wrappedJson = "{ \"Roles\": " + jsonContent + " }";
    
            // Deserialize into a RoleCollection object
            Challenges = JsonUtility.FromJson<List<Challenge>>(wrappedJson);
    
            // Iterate over the roles and process them
            foreach (Challenge challenge in Challenges)
            {
                challenge.ParseBranch();
    
                // Print details for verification
                Debug.Log($"ID: {challenge.ID}");
                Debug.Log($"Description: {challenge.Description}");
                Debug.Log($"Challenge IDs: {challenge.Role}");
                Debug.Log($"Branch: {challenge.BranchEnum}");
            }
        }
        else
        {
            Debug.LogError("JSON file not found: " + jsonPath);
        }
    }
}

[Serializable]
public class RoleCollection
{
    public Role[] Roles;
}
