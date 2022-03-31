using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using Firebase.Auth;
using Firebase.Extensions;
using TMPro;

public class CreateUser_MustInvalid : MonoBehaviour
{
    private void Start()
    {
        TestCreateUserWithEmailPassword();
    }

    public void TestCreateUserWithEmailPassword()
    {
        string[][] testCase = new string[5][];
        testCase[0] = new string[] {"haims@gmail.com.vn", "123456"};
        testCase[1] = new string[] {"haims@gmail.com.vn", "123456"};
        testCase[2] = new string[] {"haims@", "123456"};
        testCase[3] = new string[] {"haims2@gmail.com.vn", "123"};
        
        FirebaseAuth firebaseAuthGateway = FirebaseAuth.DefaultInstance;
        foreach (string[] item in testCase) {
            System.Diagnostics.Debug.Assert(item != null, nameof(item) + " != null");
            CreateUser(firebaseAuthGateway, item[0], item[1]);
        }
    }
    
    void CreateUser(FirebaseAuth authGateway , string email, string password)
    {
        authGateway
            .CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {   
                if (!task.IsCanceled && !task.IsFaulted) return;
                Debug.LogWarning("Không tạo được user");
                return;
            });
    }
}
