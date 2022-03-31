using System;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Storage;

namespace HaiMS
{
    public class OnlyDownloadToLocal : MonoBehaviour
    {
        void Start()
        {
            DownloadFromFirebase();
            LoadObjectFromLocal();
        }
        
        static void DownloadFromFirebase()
        {
            string itemURL = "gs://v-cafe-maids.appspot.com/3D_models/Maid_Face.fbx";
            string itemLocalPath = 
                @"D:\0_Projects\V_cafe_maid\Experiments\FirebaseStorageIO\Assets\MaidModels\Maid_Face.fbx";
            
            FirebaseStorage firebaseStorage = FirebaseStorage.DefaultInstance;
            StorageReference itemRef = firebaseStorage.GetReferenceFromUrl(itemURL);

            itemRef.GetFileAsync(itemLocalPath).ContinueWithOnMainThread(task => {
                if (!task.IsCanceled && !task.IsFaulted) {
                    Debug.Log("Downloading started");
                }

                if (task.IsCompleted) {
                    Debug.Log("Downloading finished");
                }
            } );

        }

        static void LoadObjectFromLocal()
        {
            string modelPath =
                @"D:\0_Projects\V_cafe_maid\Experiments\FirebaseStorageIO\Assets\AssetBundles\3d_models\face";

            var loadedObject = AssetBundle.LoadFromFile(modelPath);
            try
            {
                var prefab = loadedObject.LoadAsset<GameObject>("Face");
                Instantiate(prefab);
                Debug.Log("Creating succeed");
            }
            catch (Exception exception)
            {
                Debug.Log("Can not create prefab from 3D model asset");
                Debug.Log(exception);
            }
        }
    }
}