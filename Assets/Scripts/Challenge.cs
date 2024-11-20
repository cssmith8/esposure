// using System;
// using System.Collections.Generic;
// using System.IO;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// [Serializable]
// public class Challenge
// {
//     public int ID;
//     public string Description;
//     public string Role;
//     public string BranchString; // Keep Branch as string for JSON deserialization
//
//     [NonSerialized]
//     public Branch Branch; // Enum for easier usage within Unity
//
//     // Method to parse the Branch string into an enum
//     public void ParseBranch()
//     {
//         if (Enum.TryParse(BranchString, true, out Branch parsedBranch))
//         {
//             Branch = parsedBranch;
//         }
//         else
//         {
//             Branch = Branch.None; // Default if parsing fails
//         }
//     }
// }
//
// public class ChallengeLoader : MonoBehaviour
// {
//     public Challenge loadedChallenge;
//
//     void Start()
//     {
//         string jsonPath = Application.dataPath + "/RoleCards/Data/ChallengeData.json";
//
//         if (File.Exists(jsonPath))
//         {
//             string jsonContent = File.ReadAllText(jsonPath);
//
//             // Deserialize the JSON into a Challenge object
//             loadedChallenge = JsonUtility.FromJson<Challenge>(jsonContent);
//
//             // Parse the Branch field into the enum
//             loadedChallenge.ParseBranch();
//
//             // Debug log for verification
//             Debug.Log($"Challenge ID: {loadedChallenge.ID}");
//             Debug.Log($"Description: {loadedChallenge.Description}");
//             Debug.Log($"Role: {loadedChallenge.Role}");
//             Debug.Log($"Branch: {loadedChallenge.Branch}");
//         }
//         else
//         {
//             Debug.LogError("JSON file not found: " + jsonPath);
//         }
//     }
// }