using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;

namespace FirebaseExperiments
{
    public class UploadFiles : MonoBehaviour
    {
        void Start()
        {
            UploadLocalFileToFirebase("Assets/AssetBundles/testbundle",
                "3D_models/file_to_delete",
                "gs://v-cafe-maids.appspot.com/3D_models");
            DeleteFileInFirebase("3D_models/testbundle_to_delete",
                "gs://v-cafe-maids.appspot.com");
        }

        static void UploadLocalFileToFirebase(string localFilePath, string destinationInStorage, string firebaseStoragePath)
        {
            FirebaseStorage storage = FirebaseStorage.DefaultInstance;
            StorageReference storageReference = storage.GetReferenceFromUrl(firebaseStoragePath);
            
            StorageReference uploadFileReference = storageReference.Child(destinationInStorage);
            uploadFileReference.PutFileAsync(localFilePath)
                .ContinueWithOnMainThread(task => {
                    if (task.IsFaulted || task.IsCanceled) {
                        Debug.Log("Uploading FAILED");
                        Debug.Log(task.Exception.ToString());
                    }
                    else {
                        Debug.Log("Uploading SUCCEED");
                    }
                });
        }

        static void DeleteFileInFirebase(string fileToDeletePath, string firebaseStoragePath)
        {
            FirebaseStorage storage = FirebaseStorage.DefaultInstance;
            StorageReference storageReference = storage.GetReferenceFromUrl(firebaseStoragePath);
            
            StorageReference fileToDeleteReference = storageReference.Child(fileToDeletePath);
            fileToDeleteReference.DeleteAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted) {
                    Debug.Log("Deleting SUCCEED");
                } else {
                    Debug.Log("Deleting FAILED"); 
                }
            });
        }
    }
}
