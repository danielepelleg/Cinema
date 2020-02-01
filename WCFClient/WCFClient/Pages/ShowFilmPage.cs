﻿using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class ShowFilm : Page
    {
        public ShowFilm(Program program)
            : base("Show Film", program)
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
                Output.WriteLine("FILM LIST: ");
                TablePrinter.Film(SessionManager.GetServiceClient().GetFilmList());
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