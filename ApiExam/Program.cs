using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class Contact
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class ContactDAL
{
    public async Task<List<Contact>> GetAllContactsAsync()
    {
        //  TODO: Məlumat bazasından kontaktları əldə etmək üçün kodu tətbiq edin
        //  Verilənlər bazasından əldə edilmiş kontaktların siyahısını qaytarın
    }

    public async Task AddContactAsync(Contact contact)
    {
        // TODO: Məlumat bazasına kontakt əlavə etmək üçün kodu tətbiq edin
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        // TODO: Verilənlər bazasında kontaktı yeniləmək üçün kodu tətbiq edin
    }

    public async Task DeleteContactAsync(Contact contact)
    {
        // TODO: Kontaktı verilənlər bazasından silmək üçün kodu tətbiq edin
    }
}

public class ApiIntegration
{
    private readonly HttpClient _httpClient;

    public ApiIntegration()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetApiResponseAsync(string apiUrl)
    {
        // TODO: API-yə HTTP GET sorğusu etmək üçün kodu tətbiq edin
        // Cavab məzmununu sətir kimi qaytarın
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        List<Contact> contacts = ParseContactsFromFile("contacts.txt");

        ContactDAL contactDAL = new ContactDAL();
        ApiIntegration apiIntegration = new ApiIntegration();

        await PerformDataAccessOperationsAsync(contactDAL, contacts);
        await PerformApiIntegrationAsync(apiIntegration);
        await PerformMultithreadingAsync();

        Console.WriteLine("Application completed successfully. Press any key to exit.");
        Console.ReadKey();
    }

    private static List<Contact> ParseContactsFromFile(string filePath)
    {
        var contacts = new List<Contact>();

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length >= 2)
            {
                var contact = new Contact
                {
                    Name = parts[0],
                    Email = parts[1]
                };
                contacts.Add(contact);
            }
        }

        return contacts;
    }

    private static async Task PerformDataAccessOperationsAsync(ContactDAL contactDAL, List<Contact> contacts)
    {
        var allContacts = await contactDAL.GetAllContactsAsync();
        foreach (var contact in allContacts)
        {
            Console.WriteLine($"Name: {contact.Name}, Email: {contact.Email}");
        }

        var newContact = new Contact { Name = "John Doe", Email = "john.doe@example.com" };
        await contactDAL.AddContactAsync(newContact);

        var existingContact = allContacts.FirstOrDefault();
        if (existingContact != null)
        {
            existingContact.Name = "Updated Name";
            existingContact.Email = "updated.email@example.com";
            await contactDAL.UpdateContactAsync(existingContact);
        }

        var contactToDelete = allContacts.LastOrDefault();
        if (contactToDelete != null)
        {
            await contactDAL.DeleteContactAsync(contactToDelete);
        }
    }

    private static async Task PerformApiIntegrationAsync(ApiIntegration apiIntegration)
    {
        var apiUrl = "https://api.example.com";
        var response = await apiIntegration.GetApiResponseAsync(apiUrl);
        Console.WriteLine($"API Response: {response}");
    }

    private static async Task PerformMultithreadingAsync()
    {
        var urls = new List<string>
        {
            "https://example.com/file1.txt",
            "https://example.com/file2.txt",
            "https://example.com/file3.txt",
        };
    }
}
