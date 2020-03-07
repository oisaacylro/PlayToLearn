using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MySql.Data.MySqlClient;
using System;

public class PersonController : MonoBehaviour
{
    //Calls Person Object
    Person pEntity;

    // Start is called before the first frame update
    void Start()
    {
        pEntity = GetComponent<Person>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool validateLogin(string U, string P)
    {
        bool validAccount = false;
        Person personAccount = pEntity.CheckPersonExist(U);

        try
        {
            if (personAccount.UserSalt != null)
            {
                string userPassword = Utility.HashPassword(P, personAccount.UserSalt, Utility.HA);

                if (userPassword.Equals(personAccount.Password))
                    validAccount = true;
            }
        }
        catch(NullReferenceException e)
        {
            Debug.LogWarning(e.ToString());
        }

        return validAccount;
    }

    /*  Used to text DB connection during development stage
     * 
     * public void checkDBConnection()
    {
        print("Reached checkDBConnection");
        string connStr = "Server=172.21.148.165; Database=playtolearn; Uid=admin; Pwd=ptl1234!;";

        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();
        print("Connection: " + conn.ToString());
        conn.Close();
    }*/
        
}
