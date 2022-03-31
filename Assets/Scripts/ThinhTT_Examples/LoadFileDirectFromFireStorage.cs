using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;

public class LoadFileDirectFromFireStorage : MonoBehaviour
{

    private float loadingTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        /* Load 3D model from Firebase by GetByteAsync,
           Then set it to a game object.
        */

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://test-time-loading.appspot.com/body/thick.loli");

        long maxSizeBytes = 1024 * 1024 * 10; // file max 10Mbs
        storageRef.GetBytesAsync(maxSizeBytes).ContinueWithOnMainThread(task =>
        {
           if (task.IsFaulted || task.IsCanceled) {
               Debug.Log("Fail to load file");
               Debug.LogException(task.Exception);
           }
           else {
               byte[] fileContents = task.Result;
               var asset = AssetBundle.LoadFromMemory(fileContents);
               var assetGameObject = asset.LoadAsset<GameObject>("FormalBodyThickCloseLoli");

               if (asset == null) {
                   Debug.Log("Failed to load mainmenu AssetBundle!");
               }
               
               //instantiate objects
               Instantiate(assetGameObject);
               Debug.Log("Finished Load file direct from fireStorage in " + loadingTime + " seconds");
           }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

}
