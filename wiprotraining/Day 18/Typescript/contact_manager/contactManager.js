var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
// ContactManager class that manages contacts
var ContactManager = /** @class */ (function () {
    function ContactManager() {
        this.contacts = [];
    }
    // Add a new contact
    ContactManager.prototype.addContact = function (contact) {
        this.contacts.push(contact);
        console.log("\u2705 Contact added: ".concat(contact.name));
    };
    // View all contacts
    ContactManager.prototype.viewContacts = function () {
        console.log("Contact List:");
        this.contacts.forEach(function (contact) {
            console.log("ID: ".concat(contact.id, ", Name: ").concat(contact.name, ", Email: ").concat(contact.email, ", Phone: ").concat(contact.phone));
        });
        return this.contacts;
    };
    // Modify a contact
    ContactManager.prototype.modifyContact = function (id, updatedContact) {
        var index = this.contacts.findIndex(function (c) { return c.id === id; });
        if (index === -1) {
            console.log("Error: Contact with ID ".concat(id, " not found."));
            return;
        }
        var existingContact = this.contacts[index];
        this.contacts[index] = __assign(__assign({}, existingContact), updatedContact);
        console.log("Contact with ID ".concat(id, " modified successfully."));
    };
    // Delete a contact
    ContactManager.prototype.deleteContact = function (id) {
        var index = this.contacts.findIndex(function (c) { return c.id === id; });
        if (index === -1) {
            console.log("Error: Contact with ID ".concat(id, " not found."));
            return;
        }
        var deleted = this.contacts.splice(index, 1)[0];
        console.log("Contact deleted: ".concat(deleted.name));
    };
    return ContactManager;
}());
// --------- Testing ---------
var manager = new ContactManager();
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
