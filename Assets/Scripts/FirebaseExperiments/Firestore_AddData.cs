using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class Firestore_AddData : MonoBehaviour
{
    [Firebase.Firestore.FirestoreData]
    public class City
    {
        [FirestoreProperty] public string Name { get; set; }
        [FirestoreProperty] public string State { get; set; }
        [FirestoreProperty] public string Country { get; set; }
        [FirestoreProperty] public bool Capital { get; set; }
        [FirestoreProperty] public long Population { get; set; }
    }
    
    void Start()
    {
        // SetDocument();
        // SetDocumentWithCustomObject();
        AddDocument();
    }

    void SetDocument()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("cities").Document("Hanoi");
        Dictionary<string, object> city = new Dictionary<string, object> {
                { "Khu vuc", "Cau Giay" },
                { "Hai song o day", true },
                { "Thu nested-data", new Dictionary<string, object>()
                    {
                        {"Ten", "ten cai thang bo may"},
                        {"Tuoi", "tuoi cai thang bo may"},
                        {"Gioi tinh", "Gioi tinh cai thang bo may"},
                    }},
                { "Thu array", new List<string>(){"a", "b", "c", "d"}},
                { "Tiep tuc thu array", new List<string>(){"a", "b", "c", "d"}}
            };
        docRef.SetAsync(city, SetOptions.MergeAll).ContinueWithOnMainThread(task => {
            Debug.Log("Added SUCCESS");
        });
    }
    
    void SetDocumentWithCustomObject()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("cities").Document("LA");
        City city = new City
        {
            Name = "Los Angeles",
            State = "CA",
            Country = "USA",
            Capital = false,
            Population = 3900000L
        };
        docRef.SetAsync(city);
        Debug.Log("SUCCESSED");

    }

    void AddDocument()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        Dictionary<string, object> city = new Dictionary<string, object>()
        {
            {"Ten", "Hanoi"},
            {"Quan", "Cau Giay"},
            {"Nha may a", true}
        };
        db.Collection("cities").AddAsync(city).ContinueWithOnMainThread(task =>
        {
            Debug.Log(task.Result); 
            Debug.Log(task.Result.Id); 
            Debug.Log("SUCCESSED");
        });
        
    }
}
