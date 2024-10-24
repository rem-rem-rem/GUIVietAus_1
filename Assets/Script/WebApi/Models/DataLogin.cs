using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataLogin
{
    public int id { get; set; }
    public string name { get; set; }
    public string department { get; set; }
    public string position { get; set; }
    public string date_Of_Birth { get; set; }
    public string hire_Day { get; set; }
    public int manager_Id { get; set; }
    public string username_A { get; set; }
    public string password_A { get; set; }
}