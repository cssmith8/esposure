using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public string Branch;
    public string Description;
    public List<int> ChallengeIDs;
    public Branch BranchEnum;

    // Converts string Branch to BranchEnum
    public void ParseBranch() {
        BranchEnum = Enum.TryParse(Branch.Trim(), true, out Branch parsedBranch) ? parsedBranch : global::Branch.None;
    }
    
    public override string ToString() {
        // Convert the list of ChallengeIDs to a comma-separated string
        string challenges = ChallengeIDs != null ? string.Join(", ", ChallengeIDs) : "None";

        return $"ID: {ID}\n" +
               $"Name: {Name}\n" +
               $"Branch (string): {Branch}\n" +
               $"BranchEnum: {BranchEnum}\n" +
               $"Description: {Description}\n" +
               $"ChallengeIDs: {challenges}";
    }
}

[Serializable]
public class Challenge
{
    public int ID;
    public string Description;
    public string Role;
    public string Branch;
    public Branch BranchEnum;

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
    
    public override string ToString()
    {
        return $"ID: {ID}\n" +
               $"Description: {Description}\n" +
               $"Role: {Role}\n" +
               $"Branch (string): {Branch}\n" +
               $"BranchEnum: {BranchEnum}";
    }
}

[Serializable]
public class CardDataManager : MonoBehaviour {
    public static CardDataManager Instance { get; private set; }
    public List<List<Role>> Roles { get; private set; } = new();
    public List<List<Challenge>> Challenges { get; private set; } = new();
    // All challenges in a single list
    public List<Challenge> ChallengesFlatList { get; private set; } = new();
    public Dictionary<int, Role> RoleDict { get; private set; } = new();
    public Dictionary<int, Challenge> ChallengeDict { get; private set; } = new();
    
    public void Awake() {
        Debug.Log("CardDataManager Awakens");
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple CardDataManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        ImportData();
        
        // DontDestroyOnLoad(gameObject);
    }

    //get role id from role name
    public int GetRoleID(string roleName)
    {
        foreach (var roleList in Roles)
        {
            foreach (var role in roleList)
            {
                if (role.Name == roleName)
                {
                    return role.ID;
                }
            }
        }
        return -1;
    }

    private void ImportData() {
        Debug.Log("Import starts");
        ImportRoles();
        Debug.Log("Role import finished. Number of role families:  " + Roles.Count);
        ImportChallenges();
        Debug.Log("Challenge import finished. Number of challenge families: " + Challenges.Count);
        Debug.Log("Flat challenge import finished. Number of challenges in flat list: " + ChallengesFlatList.Count);
    }
    
    private void ClearAllData()
    {
        Roles.ForEach(roleList => roleList.Clear());
        Roles.Clear();
        Challenges.ForEach(challengeList => challengeList.Clear());
        Challenges.Clear();
        ChallengesFlatList.Clear();
        RoleDict.Clear();
        ChallengeDict.Clear();
        Debug.Log("Role and challenge data cleared.");
    }

    public void ReimportData() {
        ClearAllData();
        ImportData();
    }

    private void ImportRoles() {
        string jsonPath = $"Role Card Data/RoleData";
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonPath);

        if (jsonFile != null) {
            string jsonContent = jsonFile.text;
            string wrappedJson = "{ \"Roles\": " + jsonContent + " }";
            RoleCollection roleCollection = JsonUtility.FromJson<RoleCollection>(wrappedJson);
            
            foreach (var _ in Enum.GetValues(typeof(Branch))) {
                Roles.Add(new List<Role>());
            }
            
            foreach (var role in roleCollection.Roles) {
                role.ParseBranch();
                Roles[(int)role.BranchEnum].Add(role);
                RoleDict.Add(role.ID, role);
            }
        }
        else {
            Debug.LogError($"JSON file not found: {jsonPath}");
        }
    }

    private void ImportChallenges() {
        string jsonPath = $"Role Card Data/ChallengeData";
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonPath);
    
        if (jsonFile != null)
        {
            string jsonContent = jsonFile.text;
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
                ChallengeDict.Add(challenge.ID, challenge);
                
                ChallengesFlatList.Add(challenge);
            }
            
            Debug.Log($"{ChallengesFlatList.Count} challenges in flat list found.");
        }
        else
        {
            Debug.LogError($"JSON file not found: {jsonPath}");
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