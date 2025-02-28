using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;

namespace SonarCloudVulnerableApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            // **Vulnerability 1: SQL Injection**
            string connString = "Server=myServer;Database=myDB;User Id=myUser;Password=myPass;";
            SqlConnection conn = new SqlConnection(connString);
            string query = "SELECT * FROM Users WHERE username = '" + username + "' AND password = '" + password + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("User: " + reader["username"]);
            }
            conn.Close();

            // **Vulnerability 2: Hardcoded Credentials**
            string apiKey = "12345-ABCDE-SECRET";  // Hardcoded secret
            Console.WriteLine("Using API Key: " + apiKey);

            // **Vulnerability 3: Insecure HTTP Connection**
            WebClient client = new WebClient();
            string data = client.DownloadString("http://insecure-website.com"); // Uses HTTP instead of HTTPS
            Console.WriteLine("Downloaded Data: " + data);

            // **Vulnerability 4: Insecure File Write**
            string filePath = "C:\\Users\\Public\\logs.txt"; // Publicly accessible
            File.WriteAllText(filePath, "Sensitive Log Data");

            // **Vulnerability 5: Weak Encryption (Hardcoded Key)**
            string encryptedData = EncryptData("SensitiveInfo", "weak_key"); // Hardcoded weak key
            Console.WriteLine("Encrypted Data: " + encryptedData);
        }

        static string EncryptData(string data, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            for (int i = 0; i < dataBytes.Length; i++)
            {
                dataBytes[i] ^= keyBytes[i % keyBytes.Length]; // Simple XOR encryption (Weak)
            }
            return Convert.ToBase64String(dataBytes);
        }
    }
}
