using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class Firestore_ReadData : MonoBehaviour
{
    private void OnEnable()
    {
        // CreateExampleData(); 
    }
    void CreateExampleData()
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            CollectionReference citiesRef = db.Collection("cities");
            
            citiesRef.Document("SF").SetAsync(new Dictionary<string, object>(){
                { "Name", "San Francisco" },
                { "State", "CA" },
                { "Country", "USA" },
                { "Capital", false },
                { "Population", 860000 }
            }).ContinueWithOnMainThread(task =>
                citiesRef.Document("LA").SetAsync(new Dictionary<string, object>(){
                    { "Name", "Los Angeles" },
                    { "State", "CA" },
                    { "Country", "USA" },
                    { "Capital", false },
                    { "Population", 3900000 }
                })
            ).ContinueWithOnMainThread(task =>
                citiesRef.Document("DC").SetAsync(new Dictionary<string, object>(){
                    { "Name", "Washington D.C." },
                    { "State", null },
                    { "Country", "USA" },
                    { "Capital", true },
                    { "Population", 680000 }
                })
            ).ContinueWithOnMainThread(task =>
                citiesRef.Document("TOK").SetAsync(new Dictionary<string, object>(){
                    { "Name", "Tokyo" },
                    { "State", null },
                    { "Country", "Japan" },
                    { "Capital", true },
                    { "Population", 9000000 }
                })
            ).ContinueWithOnMainThread(task =>
                citiesRef.Document("BJ").SetAsync(new Dictionary<string, object>(){
                    { "Name", "Beijing" },
                    { "State", null },
                    { "Country", "China" },
                    { "Capital", true },
                    { "Population", 21500000 }
                })
            ).ContinueWithOnMainThread(debug =>
                Debug.Log("SUCCEED"));
        }
    
    void Start()
    {
        // GetSingleDocument();
        // GetMultipleDocument();
        performSingleQuery();
    }

    void GetMultipleDocument()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        Query capitalQuery = db.Collection("cities").WhereEqualTo("Capital", true);
        
        capitalQuery.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            QuerySnapshot capitalQuerySnapshot = task.Result;
            
            foreach (DocumentSnapshot documentSnapshot in capitalQuerySnapshot.Documents) {
                Debug.Log($"Document data for {documentSnapshot.Id} document:".ToUpperInvariant());
                
                Dictionary<string, object> city = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city) {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }

                // Newline to separate entries
                Debug.Log("");
            };
        });
        
    }
    
    void GetSingleDocument()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        DocumentReference docRef = db.Collection("cities").Document("SF");
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists) {
                Debug.Log($"Document data for {snapshot.Id} document:");
                
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city) {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }

                String cityString = snapshot.ToString();
                Debug.Log(cityString);
            } else {
                Debug.Log(String.Format($"Document {snapshot.Id} does not exist!"));
            }
        });
    }

    void performSingleQuery()
    {   
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        CollectionReference citiesRef = db.Collection("cities");
        Query query = citiesRef.WhereEqualTo("State", "CA");
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents) {
                Debug.Log($"Document {documentSnapshot.Id} returned by query State=CA");
            } 
        });
    }
}
