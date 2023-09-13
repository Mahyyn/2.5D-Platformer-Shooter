using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoginScript : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text messageText; // Text element for displaying messages
    public TMP_InputField UsernameInput; // Input field for entering the username
    public TMP_InputField PasswordInput; // Input field for entering the password

    // Function that is called when the Register button is clicked
    public void RegisterButton()
    {
        // Check if the username is valid
        if (!IsUsernameValid(UsernameInput.text))
        {
            messageText.text = "The username should be characters only!";
            return;
        }

        // Check if the password is valid
        if (!IsPasswordValid(PasswordInput.text))
        {
            messageText.text = "The password should be at least 6 characters!";
            return;
        }

        // Create a RegisterPlayFabUserRequest object with the entered username and password
        var request = new RegisterPlayFabUserRequest
        {
            Username = UsernameInput.text,
            Password = PasswordInput.text,
            RequireBothUsernameAndEmail = false
        };

        // Call the RegisterPlayFabUser API and pass the request object and the success and error callbacks
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    // Callback function that is called when the registration is successful
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = UsernameInput.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request,null,null);
        messageText.text = "Registered and logged in";
        SceneManager.LoadScene(1);
    }

    // Function that is called when the Login button is clicked
    public void LoginButton()
    {
        // Check if the username is valid
        if (!IsUsernameValid(UsernameInput.text))
        {
            messageText.text = "The username should be characters only!";
            return;
        }

        // Check if the password is valid
        if (!IsPasswordValid(PasswordInput.text))
        {
            messageText.text = "The password should be at least 6 characters!";
            return;
        }

        // Create a LoginWithPlayFabRequest object with the entered username and password
        var request = new LoginWithPlayFabRequest
        {
            Username = UsernameInput.text,
            Password = PasswordInput.text
        };

        // Call the LoginWithPlayFab API and pass the request object and the success and error callbacks
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }

    // Callback function that is called when the login is successful
    void OnLoginSuccess(LoginResult result)
    {
        SceneManager.LoadScene(1);

        messageText.text = "Logged In!";
        Debug.Log("Successfully logged in");
    }

    // Callback function that is called when an error occurs during the API call
    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    private bool IsUsernameValid(string username)
    {
        // Check if the username contains only characters
        return System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z]+$");
    }

    private bool IsPasswordValid(string password)
    {
        // Check if the password is at least 6 characters long
        return password.Length >= 6;
    }



}
