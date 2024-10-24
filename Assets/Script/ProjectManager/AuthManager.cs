using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthManager : Singleton<AuthManager>
{
    private string authToken;

    //save token when login success
    public void SaveAuthToken(string token)
    {
        authToken = token;
    }

    //Get token to request api
    public string GetAuthToken()
    {
        return authToken;
    }

    // Remove token wwhen user log out
    public void ClearAuthToken()
    {
        authToken = null;
    }
}
