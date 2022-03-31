using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;
using System.IO;

public class DownloadFileFromFireStorage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://test-time-loading.appspot.com/body/thick.loli");

        // Create local filesystem URL
        string localUrl =  @"D:\Unity\Test Project\Test\Assets\thick.loli";

        // Download to the local filesystem
        storageRef.GetFileAsync(localUrl).ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("File downloaded.");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
