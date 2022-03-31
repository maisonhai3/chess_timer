using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Firebase.Auth;
using Firebase.Extensions;
using TMPro;

public class CreateFirebaseAccount : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputVerifiedPassword;

    public void CreateAccountWithEmailPassword()
    {
        string email = inputEmail.text;
        string password = inputPassword.text;
        string verifiedPassword = inputVerifiedPassword.text;

        string[] need_debug = new[] {email, password, verifiedPassword};
        foreach (var item in need_debug)
            Debug.Log(item);
        
        FirebaseAuth firebaseAuthGateway = FirebaseAuth.DefaultInstance;

        if (password != verifiedPassword)
        {
            Debug.LogError("Verified password does not match"); 
            return;
        }
        
        if (password == verifiedPassword)
        {
            firebaseAuthGateway
                .CreateUserWithEmailAndPasswordAsync(email, password)
                .ContinueWithOnMainThread(task => 
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("Chay den day ko 11");
                        // var newUser = task.Result;
                        // Debug.Log($"{newUser.DisplayName}");
                        Debug.Log("Chay den day ko 22");
                    }
                    
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        Debug.Log("Khong tao dc");
                        // return;
                    }
                    
                    Debug.Log(task.Status);

                });
        }
        
        // if (password == verifiedPassword)
        // {
        //     firebaseAuthGateway
        //         .CreateUserWithEmailAndPasswordAsync(email, password)
        //         .ContinueWithOnMainThread(task => 
        //         {   
        //             Debug.LogWarning("KHÔNG tạo được user");
        //             if (!task.IsCanceled && !task.IsFaulted) return;
        //             Debug.LogWarning("Không tạo được user");
        //             return;
        //         });
        // }

    }
}
