# C# Password Manager
<img width="200" height="150" alt="ChatGPT Image Jun 24, 2025, 12_51_25 PM" src="https://github.com/user-attachments/assets/54e19382-f7db-4831-b749-c512dd8e0258" />


This is a simple password manager built with C# that securely stores passwords, generates strong passwords, and encrypts saved data. (yes i know im cool)

## Features

* Add and store passwords
* View saved passwords
* Delete saved passwords
* Generate strong random passwords
* Encrypt stored password data
* Save password data locally
* Uses Windows Data Protection API (DPAPI)

## Requirements

* .NET SDK
* Windows

## How to Run

1. Open the project folder in VS Code.
2. Open the terminal.
3. Run:

```bash
dotnet run
```

4. Choose an option from the menu.

## Menu

```text
1. Add Password
2. View Passwords
3. Generate Password
4. Delete Password
5. Exit
```

## Example

The password generator lets you choose a password length:

```text
Enter password length: 16

Generated Password:
X7@kP2#mL9!qR4$z
```

You can then save passwords for different websites and services.

## Security

The password data is encrypted before being saved to the computer.

The project uses `ProtectedData` and Windows Data Protection API (DPAPI) to protect the stored data.

The encrypted password database is saved locally as:

```text
passwords.dat
```

## Technologies Used

* C#
* .NET
* System.Security.Cryptography
* Windows DPAPI
* JSON
* File handling

## What I Learned

* How to store and retrieve data in C#
* How encryption can protect sensitive data
* How to generate secure random passwords
* How to use C# classes and methods
How password managers protect stored information
<h1>[!WARNING]</h1>
This is a learning project created to demonstrate C# programming and basic cybersecurity concepts.

It should not be used to store important real-world passwords. Professional password managers use additional security measures and have undergone extensive security testing.
<p> ty for reading <3 </P>
