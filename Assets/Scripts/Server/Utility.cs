﻿using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
//using System.Web.Configuration;

public class Utility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Hashing Algorithm
    public static HashAlgorithm HA = new SHA256Managed();

    /**
     * This function generates the salt value using RNGCryptoServiceProvider for password hashing.
     * @return Salt value used in password hashing.
     */
    public static string GenerateSaltValue()
    {
        // Define min and max salt sizes.
        int minSaltSize = 4;
        int maxSaltSize = 8;

        // Generate a random number for the size of the salt.
        System.Random random = new System.Random();
        int saltSize = random.Next(minSaltSize, maxSaltSize);

        // Allocate a byte array, which will hold the salt.
        byte[] saltBytes = new byte[saltSize];

        // Initialize a random number generator.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        // Fill the salt with cryptographically strong byte values.
        rng.GetNonZeroBytes(saltBytes);

        string saltValue = Convert.ToBase64String(saltBytes);

        return saltValue;
    }

    /**
     * This function perform password hashing based on the hash algorithm.
     * @param plainText The plain text to hash.
     * @param saltValue The salt value to be added to the plain text for hashing.
     * @param hash The hashing algorithm.
     * @return Hashed password.
     */
    public static string HashPassword(string plainText, string saltValue, HashAlgorithm hash)
    {
        if (saltValue == null)
        {
            // Generate a salt value.
            saltValue = GenerateSaltValue();
        }

        // Convert salt value into byte array.
        byte[] saltBytes = Encoding.UTF8.GetBytes(saltValue);

        // Convert plain text into a byte array.
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        // Allocate array, which will hold plain text and salt.
        byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

        // Copy plain text bytes into resulting array.
        for (int i = 0; i < plainTextBytes.Length; i++)
            plainTextWithSaltBytes[i] = plainTextBytes[i];

        // Append salt bytes to the resulting array.
        for (int i = 0; i < saltBytes.Length; i++)
            plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

        // Compute hash value of our plain text with appended salt.
        byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

        // Create array which will hold hash and original salt bytes.
        byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

        // Copy hash bytes into resulting array.
        for (int i = 0; i < hashBytes.Length; i++)
            hashWithSaltBytes[i] = hashBytes[i];

        // Append salt bytes to the result.
        for (int i = 0; i < saltBytes.Length; i++)
            hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

        // Convert result into a base64-encoded string.
        string hashValue = Convert.ToBase64String(hashWithSaltBytes);

        // Return the result.
        return hashValue;
    }

    /**
     * This function perform decryption.
     * @param cipherText The cipher text to decrypt.
     * @param keyIV The key to perform decryption to get the plain text.
     * @return Plain text.
     */
    public static string Decrypt(string cipherText, string keyIV)
    {
        SymmetricAlgorithm sa = new AesCryptoServiceProvider();

        byte[] KeyIV = Convert.FromBase64String(keyIV);
        byte[] Key = new byte[sa.Key.Length];
        for (int i = 0; i < Key.Length; i++)
            Key[i] = KeyIV[i];
        byte[] IV = new byte[sa.IV.Length];
        for (int i = 0; i < IV.Length; i++)
            IV[i] = KeyIV[i + Key.Length];

        sa.Key = Key;
        sa.IV = IV;

        ICryptoTransform decryptTransform = sa.CreateDecryptor();
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        byte[] plainBytes = decryptTransform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        string plainText = Encoding.UTF8.GetString(plainBytes);

        return plainText;
    }

    /**********************************************************************************************************************************/

    /**
     * This function sends the email.
     * @param receiver The receiver's email address.
     * @param subject The subject to be stated in the email.
     * @param message The message to be sent in the email.
     * @return Outcome which indicates if the email sent is successfully.
     */
    /*public static string sendEmail(string receiver, string subject, string message)
    {
        string result = "";
        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("ntuproject@hotmail.com", "PlayToLearn");
            mailMessage.To.Add(receiver);

            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.live.com";
            string EKeyIV = WebConfigurationManager.AppSettings["EKeyIV"];
            string EPass = WebConfigurationManager.AppSettings["EPass"];
            //string Pass = Decrypt(EPass, EKeyIV);
            smtpClient.Credentials = new System.Net.NetworkCredential("ntuproject@hotmail.com", Decrypt(EPass, EKeyIV));
            smtpClient.EnableSsl = true;

            smtpClient.Send(mailMessage);
            //Response.Write("E-mail sent!");
            result = "Success";
        }
        catch (Exception ex)
        {
            //Response.Write("Could not send the e-mail - error: " + ex.Message);
            result = "- Error occur in sending email, error: " + ex.Message;
        }

        return result;
    }
    */
    /**********************************************************************************************************************************/

    private static System.Random random = new System.Random();

    /**
     * This function generates the random password for new account.
     * @return random generated password.
     */
    public static string GenerateRandomPW()
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
        return new string(Enumerable.Repeat(chars, 10)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
