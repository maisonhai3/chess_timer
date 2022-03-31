using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirestoreInteraction : MonoBehaviour
{
    void Start()
    {
        AddData();
        GetData();
    }

    void AddData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document("alovelace");
        
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "First", "Ada" },
            { "Last", "Lovelace" },
            { "Born", 1815 },
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("SUCCESS");
        });
    }
    
    private void GetData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log(String.Format("User: {0}", document.Id));
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Debug.Log(String.Format("First: {0}", documentDictionary["First"]));
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Debug.Log(String.Format("Middle: {0}", documentDictionary["Middle"]));
                }

                Debug.Log(String.Format("Last: {0}", documentDictionary["Last"]));
                Debug.Log(String.Format("Born: {0}", documentDictionary["Born"]));
            }

            Debug.Log("Read all data from the users collection.");
        });
    }

}
