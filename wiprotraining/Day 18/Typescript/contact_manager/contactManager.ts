// Define the interface for Contact
interface Contact {
  id: number;
  name: string;
  email: string;
  phone: string;
}

// ContactManager class that manages contacts
class ContactManager {
  private contacts: Contact[] = [];

  // Add a new contact
  addContact(contact: Contact): void {
    this.contacts.push(contact);
    console.log(`Contact added: ${contact.name}`);
  }

  // View all contacts
  viewContacts(): Contact[] {
    console.log("Contact List:");
    this.contacts.forEach((contact) => {
      console.log(`ID: ${contact.id}, Name: ${contact.name}, Email: ${contact.email}, Phone: ${contact.phone}`);
    });
    return this.contacts;
  }

  // Modify a contact
  modifyContact(id: number, updatedContact: Partial<Contact>): void {
    const index = this.contacts.findIndex(c => c.id === id);
    if (index === -1) {
      console.log(`Error: Contact with ID ${id} not found.`);
      return;
    }

    const existingContact = this.contacts[index]!;
    this.contacts[index] = { ...existingContact, ...updatedContact };
    console.log(`Contact with ID ${id} modified successfully.`);
  }

  // Delete a contact
  deleteContact(id: number): void {
    const index = this.contacts.findIndex(c => c.id === id);
    if (index === -1) {
      console.log(`Error: Contact with ID ${id} not found.`);
      return;
    }

    const deleted = this.contacts.splice(index, 1)[0]!;
    console.log(`Contact deleted: ${deleted.name}`);
  }
}

// --------- Testing ---------

const manager = new ContactManager();

// Add Contacts
manager.addContact({ id: 1, name: "Tejal", email: "tejalurhe056@gmail.com", phone: "1234567890" });
manager.addContact({ id: 2, name: "Kanishka", email: "kanishka@gmail.com", phone: "0987654321" });

// View Contacts
manager.viewContacts();

// Modify a Contact
manager.modifyContact(1, { phone: "10168358635" });

// Try modifying non-existing contact
manager.modifyContact(5, { phone: "0000000000" });

// View again
manager.viewContacts();

// Delete a Contact
manager.deleteContact(2);

// Try deleting non-existing contact
manager.deleteContact(9);

// Final list
manager.viewContacts();
