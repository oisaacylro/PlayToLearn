using System.Data;
using UnityEngine;
using MySql.Data.MySqlClient;

public class Person : Component
{
    /**
    * The username of this person.
    */
    protected string username = "";
    /**
    * The first name of this person.
    */
    protected string firstName = "";
    /**
    * The last name of this person.
    */
    protected string lastName = "";
    /**
    * The gender of this person.
    */
    protected string gender = "";
    /**
    * The date of birth of this person.
    */
    protected string dateOfBirth = "";
    /**
    * The email of this person.
    */
    protected string email = "";
    /**
    * The password of this person.
    */
    protected string password = "";
    /**
    * The password salt for this person.
    */
    protected string userSalt = "";
    /**
    * The user type of this person.
    */
    protected string userType = "";
    /**
    * The id of this person.
    */
    protected string personID = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
	     * Creates a new Person with the person username, firstName, lastName, gender, dateOfBirth,
         * email, password, password Salt, account type and person ID.
	     * @param username This person's id.
	     * @param firstName This person's first name.
         * @param lastName This person's last name.
	     * @param gender This person's gender.
	     * @param dateOfBirth This person's date of birth.
         * @param email This person's email.
         * @param password This person's password.
         * @param userSalt This person's userSalt.
         * @param userType This person's account type.
         * @param personID This person's ID.
	     */
    public Person(string username, string firstName, string lastName, string gender,
        string dateOfBirth, string email, string password, string userSalt,
        string userType, string personID)
    {
        this.username = username;
        this.firstName = firstName;
        this.lastName = lastName;
        this.gender = gender;
        this.dateOfBirth = dateOfBirth;
        this.email = email;
        this.password = password;
        this.userSalt = userSalt;
        this.userType = userType;
        this.personID = personID;
    }

    /**
     * Creates a new Person with the person username, password and userSalt for login purpose.
     * @param username This Person's username.
     * @param password This Person's password.
     * @param userSalt This Person's userSalt.
     */
    public Person(string username, string password, string userSalt)
    {
        this.username = username;
        this.password = password;
        this.userSalt = userSalt;
    }

    /**
     * Gets the username of this person.
     * @return This person's username.
     * Sets the username of this person.
     */
    public string Username { get => username; set => username = value; }
    /**
     * Gets the first name of this person.
     * @return This person's first name.
     * Sets the first name of this person.
     */
    public string FirstName { get => firstName; set => firstName = value; }
    /**
     * Gets the last name of this person.
     * @return This person's last name.
     * Sets the last name of this person.
     */
    public string LastName { get => lastName; set => lastName = value; }
    /**
     * Gets the gender of this person.
     * @return This person's gender.
     * Sets the gender of this person.
     */
    public string Gender { get => gender; set => gender = value; }
    /**
     * Gets the date of birth of this person.
     * @return This person's date of birth.
     * Sets the date of birth of this person.
     */
    public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
    /**
     * Gets the email of this person.
     * @return This person's email.
     * Sets the email of this person.
     */
    public string Email { get => email; set => email = value; }
    /**
     * Gets the password of this person.
     * @return This person's password.
     * Sets the password of this person.
     */
    public string Password { get => password; set => password = value; }
    /**
     * Gets the user password salt of this person.
     * @return This person's user password salt.
     * Sets the user password salt of this person.
     */
    public string UserSalt { get => userSalt; set => userSalt = value; }
    /**
     * Gets the user account type of this person.
     * @return This person's user account type.
     * Sets the user account type of this person.
     */
    public string UserType { get => userType; set => userType = value; }
    /**
     * Gets the id of this person.
     * @return This person's id.
     * Sets the id of this person.
     */
    public string PersonID { get => personID; set => personID = value; }

    /**
     * This function check the person if exist within the datebase for login purpose.
     * @return Outcome which indicates if this Person is retrieved from the database successfully.
     */
    public Person CheckPersonExist(string username)
    {
        Person accountExist = null;

        string password, userSalt;

        string queryStr = "SELECT Username, Password, UserSalt FROM playtolearn.Person WHERE Username=@username";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@username", username);

        conn.Open();
        MySqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            username = dr["Username"].ToString();
            password = dr["Password"].ToString();
            userSalt = dr["UserSalt"].ToString();

            accountExist = new Person(username, password, userSalt);
        }

        conn.Close();
        dr.Close();
        dr.Dispose();

        return accountExist;
    }

    /**
     * This function check if the person username exist within the datebase.
     * @return Outcome which indicates if this Person username exists in the database.
     */
    public int CheckPersonUsernameExistCount(string username)
    {
        string queryStr = "SELECT Count(*) FROM playtolearn.Person WHERE Username=@username";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@username", username);

        int count;
        try
        {
            conn.Open();
            count = int.Parse(cmd.ExecuteScalar().ToString());
        }
        catch
        {
            count = -1;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        return count;
    }

    /**
     * This function check if the person id exist within the datebase.
     * @return Outcome which indicates if this Person id exists in the database.
     */
    public int CheckPersonIDExistCount(string personID)
    {
        string queryStr = "SELECT Count(*) FROM playtolearn.Person WHERE PersonID=@personID";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@personID", personID);

        int count;
        try
        {
            conn.Open();
            count = int.Parse(cmd.ExecuteScalar().ToString());
        }
        catch
        {
            count = -1;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        return count;
    }

    /**
    * This function inserts this Person into the Person's database.
    * @return Outcome which indicates if this Person is inserted into the database successfully.
    */
    public bool PersonCreate()
    {
        bool success = false;

        string queryStr = "INSERT INTO playtolearn.Person (Username, FirstName, LastName, Gender, DateOfBirth, Email, Password, UserSalt, UserType, PersonID) " +
                          "VALUES (@username, @firstName, @lastName, @gender, @dateOfBirth, @email, @password, @userSalt, @userType, @personID)";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);

        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@firstName", firstName);
        cmd.Parameters.AddWithValue("@lastName", lastName);
        cmd.Parameters.AddWithValue("@gender", gender);
        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@userSalt", userSalt);
        cmd.Parameters.AddWithValue("@userType", userType);
        cmd.Parameters.AddWithValue("@personID", personID);

        int noOfRow;
        try
        {
            conn.Open();
            noOfRow = cmd.ExecuteNonQuery();
        }
        catch
        {
            noOfRow = 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        if (noOfRow > 0)
            success = true;

        return success;
    }

    /**
     * This function retrieve the individual Person using its Person's username.
     * @param username The Person's username use to retrieve.
     * @return The Person object or null if not found.
     */
    public Person PersonRetrieveByUsername(string username)
    {
        Person personFound = null;

        string firstName, lastName, gender, dateOfBirth, email, password, userSalt, userType, personID;

        string queryStr = "SELECT * FROM playtolearn.Person WHERE Username=@username";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@username", username);

        conn.Open();
        MySqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            username = dr["Username"].ToString();
            firstName = dr["FirstName"].ToString();
            lastName = dr["LastName"].ToString();
            gender = dr["Gender"].ToString();
            dateOfBirth = dr["DateOfBirth"].ToString();
            email = dr["Email"].ToString();
            password = dr["Password"].ToString();
            userSalt = dr["UserSalt"].ToString();
            userType = dr["UserType"].ToString();
            personID = dr["PersonID"].ToString();

            personFound = new Person(username, firstName, lastName, gender, dateOfBirth, email, password, userSalt, userType, personID);
        }

        conn.Close();
        dr.Close();
        dr.Dispose();

        return personFound;
    }

    /**
     * This function retrieve the individual Person using its Person's id.
     * @param username The Person's id use to retrieve.
     * @return The Person object or null if not found.
     */
    public Person PersonRetrieveByPersonID(string personID)
    {
        Person personFound = null;

        string username, firstName, lastName, gender, dateOfBirth, email, password, userSalt, userType;

        string queryStr = "SELECT * FROM playtolearn.Person WHERE PersonID=@personID";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@personID", personID);

        conn.Open();
        MySqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            username = dr["Username"].ToString();
            firstName = dr["FirstName"].ToString();
            lastName = dr["LastName"].ToString();
            gender = dr["Gender"].ToString();
            dateOfBirth = dr["DateOfBirth"].ToString();
            email = dr["Email"].ToString();
            password = dr["Password"].ToString();
            userSalt = dr["UserSalt"].ToString();
            userType = dr["UserType"].ToString();
            personID = dr["PersonID"].ToString();

            personFound = new Person(username, firstName, lastName, gender, dateOfBirth, email, password, userSalt, userType, personID);
        }

        conn.Close();
        dr.Close();
        dr.Dispose();

        return personFound;
    }

    /**
     * This function updates the updated person into the Person's database.
     * @return Outcome which indicates if this updated Person is updated into the database successfully.
     */
    public bool PersonUpdate()
    {
        bool success = false;

        string queryStr = "UPDATE playtolearn.Person SET FirstName=@firstName, LastName=@lastName, " +
                            "Gender=@gender, DateOfBirth=@dateOfBirth, Email=@email, Password=@password, " +
                            "UserSalt=@userSalt, UserType=@userType, PersonID=@personID " +
                          "WHERE Username=@username";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);

        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@firstName", firstName);
        cmd.Parameters.AddWithValue("@lastName", lastName);
        cmd.Parameters.AddWithValue("@gender", gender);
        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@userSalt", userSalt);
        cmd.Parameters.AddWithValue("@userType", userType);
        cmd.Parameters.AddWithValue("@personID", personID);

        int noOfRow;
        try
        {
            conn.Open();
            noOfRow = cmd.ExecuteNonQuery();
        }
        catch
        {
            noOfRow = 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        if (noOfRow > 0)
            success = true;

        return success;
    }

    /**
     * This function updates the updated person's new password into the person's database.
     * @param username This Person's username.
     * @param newPassword This Person's new password.
     * @param newUserSalt This Person's user salt.
     * @return Outcome which indicates if this updated person is updated into the database successfully.
     */
    public int PersonUpdatePassword(string username, string newPassword, string newUserSalt)
    {
        string queryStr = "UPDATE playtolearn.Person SET Password=@password, UserSalt=@userSalt " +
                          "WHERE Username=@username";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);

        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", newPassword);
        cmd.Parameters.AddWithValue("@userSalt", newUserSalt);

        int noOfRow;
        try
        {
            conn.Open();
            noOfRow = cmd.ExecuteNonQuery();
        }
        catch
        {
            noOfRow = 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        return noOfRow;
    }

    /**
    * This function deletes the current Person from the Person's database.
    * @return Outcome which indicates if the current Person is deleted from the database successfully.
    */
    public bool PersonDelete()
    {
        bool success = false;

        string queryStr = "DELETE FROM playtolearn.Person WHERE PersonID=@personID";

        MySqlConnection conn = new MySqlConnection(getConnStr());
        MySqlCommand cmd = new MySqlCommand(queryStr, conn);
        cmd.Parameters.AddWithValue("@personID", personID);

        int noOfRow;
        try
        {
            conn.Open();
            noOfRow = cmd.ExecuteNonQuery();
        }
        catch
        {
            noOfRow = 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        if (noOfRow > 0)
            success = true;

        return success;
    }

    //To return Database Connection String to method requring DB connections
    private string getConnStr()
    {
        return "Server=172.21.148.165; Database=playtolearn; Uid=admin; Pwd=ptl1234!;";
    }

}
