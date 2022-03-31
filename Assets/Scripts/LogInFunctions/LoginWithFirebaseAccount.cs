using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class LoginWithFirebaseAccount : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;

    public void LogIn()
    {
        LoginWithEmailAndPass();
        LoginWithCredential();
    }

    public void LoginWithEmailAndPass()
    {
        FirebaseAuth firebaseAuthGateway = FirebaseAuth.DefaultInstance;     
        
        string username = usernameInput.text;
        string password = passwordInput.text;

        Debug.Log(username);
        Debug.Log(password);
        
        firebaseAuthGateway.SignInWithEmailAndPasswordAsync(username, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Log in SUCCEED");
                // Phải lưu lại các User đã đăng nhập vào chỗ nào đấy.
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("Log in CANCELLED"); 
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Log in FAILED"); 
            }
        });
    }
    
    private void LoginWithCredential()
    {
        throw new NotImplementedException();
    }

}
