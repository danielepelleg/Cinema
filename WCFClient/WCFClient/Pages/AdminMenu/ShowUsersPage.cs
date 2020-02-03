﻿using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class ShowUsers : Page
    {
        public ShowUsers(Program program)
            : base("Show Users", program)
        {
        }

        public override void Display()
        {
            base.Display();

            /*
             * Get data from the Database
             */
            try
            {
                Output.WriteLine("USERS LIST: ");
                TablePrinter.User(SessionManager.GetServiceClient().GetUsersList());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Type: {0}", ex.GetType());
                Console.WriteLine("Message: {0}", ex.Message);
            }
            
            /*
             * Navigate back
             */
            Input.ReadString("Press [Enter] to navigate back");
            Program.NavigateBack();
        }
    }
}