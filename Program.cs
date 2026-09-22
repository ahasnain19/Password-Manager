using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

class PasswordEntry
{
    public string Website { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

class Program
{
    static string filePath = "passwords.dat";
    static List<PasswordEntry> entries = new List<PasswordEntry>();

    static void Main()
    {
        LoadPasswords();

        while (true)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("       C# Password Manager");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("1. Add Password");
            Console.WriteLine("2. View Passwords");
            Console.WriteLine("3. Generate Password");
            Console.WriteLine("4. Delete Password");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddPassword();
                    break;

                case "2":
                    ViewPasswords();
                    break;

                case "3":
                    GeneratePassword();
                    break;

                case "4":
                    DeletePassword();
                    break;

                case "5":
                    SavePasswords();
                    Console.WriteLine("\nPasswords saved. Goodbye!");
                    return;

                default:
                    Console.WriteLine("\nInvalid option.");
                    Pause();
                    break;
            }
        }
    }

    static void AddPassword()
    {
        Console.Clear();
        Console.WriteLine("========== Add Password ==========");
        Console.WriteLine();

        Console.Write("Website/Service: ");
        string website = Console.ReadLine();

        Console.Write("Username/Email: ");
        string username = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        PasswordEntry entry = new PasswordEntry
        {
            Website = website,
            Username = username,
            Password = password
        };

        entries.Add(entry);
        SavePasswords();

        Console.WriteLine("\nPassword saved successfully.");
        Pause();
    }

    static void ViewPasswords()
    {
        Console.Clear();
        Console.WriteLine("========== Saved Passwords ==========");
        Console.WriteLine();

        if (entries.Count == 0)
        {
            Console.WriteLine("No passwords have been saved.");
            Pause();
            return;
        }

        for (int i = 0; i < entries.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {entries[i].Website}");
            Console.WriteLine($"    Username: {entries[i].Username}");
            Console.WriteLine($"    Password: {entries[i].Password}");
            Console.WriteLine();
        }

        Pause();
    }

    static void GeneratePassword()
    {
        Console.Clear();
        Console.WriteLine("========== Password Generator ==========");
        Console.WriteLine();

        Console.Write("Enter password length: ");

        if (!int.TryParse(Console.ReadLine(), out int length) || length < 8)
        {
            Console.WriteLine("\nPassword length must be at least 8 characters.");
            Pause();
            return;
        }

        const string characters =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz" +
            "0123456789" +
            "!@#$%^&*";

        StringBuilder password = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            int index = RandomNumberGenerator.GetInt32(characters.Length);
            password.Append(characters[index]);
        }

        Console.WriteLine("\nGenerated Password:");
        Console.WriteLine(password);

        Pause();
    }

    static void DeletePassword()
    {
        Console.Clear();
        Console.WriteLine("========== Delete Password ==========");
        Console.WriteLine();

        if (entries.Count == 0)
        {
            Console.WriteLine("No passwords have been saved.");
            Pause();
            return;
        }

        for (int i = 0; i < entries.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {entries[i].Website}");
        }

        Console.Write("\nEnter the number to delete: ");

        if (!int.TryParse(Console.ReadLine(), out int number) ||
            number < 1 ||
            number > entries.Count)
        {
            Console.WriteLine("\nInvalid selection.");
            Pause();
            return;
        }

        entries.RemoveAt(number - 1);
        SavePasswords();

        Console.WriteLine("\nPassword deleted successfully.");
        Pause();
    }

    static void SavePasswords()
    {
        string json = JsonSerializer.Serialize(entries);

        byte[] data = Encoding.UTF8.GetBytes(json);

        byte[] encryptedData = ProtectedData.Protect(
            data,
            null,
            DataProtectionScope.CurrentUser
        );

        File.WriteAllBytes(filePath, encryptedData);
    }

    static void LoadPasswords()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        try
        {
            byte[] encryptedData = File.ReadAllBytes(filePath);

            byte[] decryptedData = ProtectedData.Unprotect(
                encryptedData,
                null,
                DataProtectionScope.CurrentUser
            );

            string json = Encoding.UTF8.GetString(decryptedData);

            entries = JsonSerializer.Deserialize<List<PasswordEntry>>(json)
                      ?? new List<PasswordEntry>();
        }
        catch
        {
            Console.WriteLine("Unable to load the password file.");
            Pause();
        }
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
