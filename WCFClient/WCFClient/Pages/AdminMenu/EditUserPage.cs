﻿using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class EditUserPage : Page
    {
        
        public EditUserPage(Program program)
            : base("Edit User", program)
        {
        }
       
        public override void Display()
        {
            base.Display();

            Console.BackgroundColor = ConsoleColor.Blue;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
            Console.BackgroundColor = ConsoleColor.Black;

            try {
                /*
                 * Show the Admin the Users
                */
                Output.WriteLine("USER LIST: ");
                TablePrinter.User(SessionManager.GetServiceClient().GetUsersList());

                /* 
                 * Edit User Form
                 * 
                 * Every input must be valid and checked. The password is hashed with 
                 * a MD5 algorithm before to be stored in the database, for a security reason.
                 */
                Output.WriteLine("-------== EDIT USER ==--------");
                string old_username = Input.ReadString("Username of the User you want to edit: ");
                // Navigate back if User type "\\" on first input
                if (old_username.Contains("\\"))
                    Program.NavigateBack();
                string oldUsername = Controls.CheckStringForeignKey(old_username, "UtenteFree");
                string newUsername = Input.ReadString("New Username (max 30 characters): ");
                newUsername = Controls.CheckUserInput("Username", newUsername);
                string newPassword = Input.ReadString("New Password (max 32 characters): ");
                string newHashedPassword = EasyEncryption.MD5.ComputeMD5Hash(Controls.CheckUserInput("Password", newPassword));
                string newName = Input.ReadString("New Name (max 20 characters): ");
                newName = Controls.CheckUserInput("Nome", newName);
                string newSurname = Input.ReadString("New Surname (max 20 characters): ");
                newSurname = Controls.CheckUserInput("Cognome", newSurname);
                if (SessionManager.GetServiceClient().EditUser(oldUsername, newUsername, newHashedPassword, newName, newSurname)) {
                    Output.WriteLine("USER UPDATE SUCCESS!\n");
                }
                else Output.WriteLine("USER UPDATE ERROR\n Retry!\n");
            }
            catch {
                Console.WriteLine("Error! Retry later!");
            }

            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }

}
