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
    public static CardDataManager Instance { get; private set; }
    public List<List<Role>> Roles { get; private set; } = new();
    public List<List<Challenge>> Challenges { get; private set; } = new();
    public Dictionary<int, Role> RoleDict { get; private set; } = new();
    
    public void Awake() {
        Debug.Log("CardDataManager Awakens");
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple CardDataManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        Debug.Log("Import starts");
        ImportRoles();
        Debug.Log("Role import finished. Number of role families:  " + Roles.Count);
        ImportChallenges();
        Debug.Log("Challenge import finished. Number of challenge families: " + Challenges.Count);
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
            
            // Add lists for every branch
            foreach (var _ in Enum.GetValues(typeof(Branch))) {
                Roles.Add(new List<Role>());
            }
            
            foreach (var role in roleCollection.Roles)
            {
                role.ParseBranch();
                Roles[(int)role.BranchEnum].Add(role);
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
            string wrappedJson = "{ \"Challenges\": " + jsonContent + " }";
            ChallengeCollection challengeCollection = JsonUtility.FromJson<ChallengeCollection>(wrappedJson);
            
            foreach (var _ in Enum.GetValues(typeof(Branch))) {
                Challenges.Add(new List<Challenge>());
            }
            
            // Iterate over the roles and process them
            foreach (var challenge in challengeCollection.Challenges)
            {
                challenge.ParseBranch();
                Challenges[(int)challenge.BranchEnum].Add(challenge);
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

[Serializable]
public class ChallengeCollection
{
    public Challenge[] Challenges;
}