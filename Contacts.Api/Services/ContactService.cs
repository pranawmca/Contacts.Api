﻿using Contacts.Api.Models;
using System.Text.Json;

namespace Contacts.Api.Services
{
    public class ContactService
    {
        private readonly string _filePath = "contacts.json";

        public async Task<List<Contact>> GetContactsAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Contact>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var contacts = await GetContactsAsync();
            return contacts.FirstOrDefault(c => c.Id == id);
        }

        public async Task AddContactAsync(Contact contact)
        {
            var contacts = await GetContactsAsync();
            contact.Id = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;  // Auto-incrementing ID
            contacts.Add(contact);
            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(contacts));
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            var contacts = await GetContactsAsync();
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(contacts));
            }
        }

        public async Task DeleteContactAsync(int id)
        {
            var contacts = await GetContactsAsync();
            var contactToRemove = contacts.FirstOrDefault(c => c.Id == id);
            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(contacts));
            }
        }
    }
}
