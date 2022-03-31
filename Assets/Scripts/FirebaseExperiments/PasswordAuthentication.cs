using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordAuthentication : MonoBehaviour
{
    void Start()
    {
        // TODO (@haims): Mày phải tìm cách để ẩn password đi nhé.
        CreatePasswordBasedAccount("maisonhai3@gmail.com", "a12345");
        SignInUserWithEmailAndPassword("maisonhai3@gmail.com", "a12345");
    }

    private static void CreatePasswordBasedAccount(string email, string password)
    {
        Firebase.Auth.FirebaseAuth firebaseAuth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        
        firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was CANCELED.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync ENCOUNTERED AN ERROR: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created SUCCESSFULLY: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    private static void SignInUserWithEmailAndPassword(string email, string password)
    {
        Firebase.Auth.FirebaseAuth firebaseAuth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        
        firebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was CANCELED.");
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync ENCOUNTERED AN ERROR: " + task.Exception);
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
