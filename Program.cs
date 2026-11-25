using FacadePattern_73464.Models;
using FacadePattern_73464.Facades;

// The client communicates only with the facade.
UserFacade userFacade = new UserFacade();

while (true)
{
    Console.Clear();

    Console.WriteLine("========================================");
    Console.WriteLine("    USER MANAGEMENT SYSTEM - 73464");
    Console.WriteLine("          FACADE DESIGN PATTERN");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Add User");
    Console.WriteLine("2. Display All Users");
    Console.WriteLine("3. Update User");
    Console.WriteLine("4. Delete User");
    Console.WriteLine("0. Exit");
    Console.WriteLine("========================================");

    Console.Write("Select an option: ");
    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                AddUser();
                break;

            case "2":
                DisplayUsers();
                break;

            case "3":
                UpdateUser();
                break;

            case "4":
                DeleteUser();
                break;

            case "0":
            case null:
                Console.WriteLine("Application closed.");
                return;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
}

// CREATE: Add a new user through the facade.
void AddUser()
{
    Console.WriteLine("\n--- ADD USER ---");

    Console.Write("Name: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "";

    User user = new User
    {
        Name = name.Trim(),
        Email = email.Trim()
    };

    userFacade.AddUser(user);
}

// READ: Display all registered users.
void DisplayUsers()
{
    Console.WriteLine("\n--- USER LIST ---");

    var users = userFacade.GetAllUsers();

    if (users.Count == 0)
    {
        Console.WriteLine("No users found.");
        return;
    }

    Console.WriteLine(new string('-', 75));
    Console.WriteLine($"{"ID",-6} {"Name",-25} {"Email",-40}");
    Console.WriteLine(new string('-', 75));

    foreach (var user in users)
    {
        Console.WriteLine(
            $"{user.Id,-6} {user.Name,-25} {user.Email,-40}");
    }

    Console.WriteLine(new string('-', 75));
    Console.WriteLine($"Total users: {users.Count}");
}

// UPDATE: Modify an existing user's information.
void UpdateUser()
{
    Console.WriteLine("\n--- UPDATE USER ---");

    DisplayUsers();

    Console.Write("Enter user ID: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid user ID.");
        return;
    }

    var existing = userFacade.GetUser(id);

    if (existing == null)
    {
        Console.WriteLine("User not found.");
        return;
    }

    Console.WriteLine("Press Enter to keep the existing value.");

    Console.Write($"Name ({existing.Name}): ");
    string? name = Console.ReadLine();

    Console.Write($"Email ({existing.Email}): ");
    string? email = Console.ReadLine();

    User updatedUser = new User
    {
        Id = existing.Id,
        Name = string.IsNullOrWhiteSpace(name)
            ? existing.Name : name.Trim(),

        Email = string.IsNullOrWhiteSpace(email)
            ? existing.Email : email.Trim()
    };

    userFacade.UpdateUser(updatedUser);
}

// DELETE: Remove a user through the facade.
void DeleteUser()
{
    Console.WriteLine("\n--- DELETE USER ---");

    DisplayUsers();

    Console.Write("Enter user ID: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid user ID.");
        return;
    }

    var user = userFacade.GetUser(id);

    if (user == null)
    {
        Console.WriteLine("User not found.");
        return;
    }

    Console.WriteLine($"User: {user.Name}");
    Console.Write("Confirm deletion (y/n): ");

    if (Console.ReadLine()?.Trim().ToLower() == "y")
    {
        userFacade.DeleteUser(id);
    }
    else
    {
        Console.WriteLine("Deletion cancelled.");
    }
}
