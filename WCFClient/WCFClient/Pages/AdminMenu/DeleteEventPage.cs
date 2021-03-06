﻿using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class DeleteEvent : Page
    {
        public DeleteEvent(Program program)
            : base("Delete Event", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Console.BackgroundColor = ConsoleColor.Blue;
            Output.WriteLine(ConsoleColor.White, "--------== {0} ==--------\n", base.Title);
            Console.BackgroundColor = ConsoleColor.Black;

            /*
             * Change output encoding to allow special 
             * characters output such as € 
             */
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try {
                /*
                 * Show the Admin the Events
                 */
                if (SessionManager.GetServiceClient().GetEventsList().Count != 0)
                {
                    Output.WriteLine("EVENTS LIST: ");
                    TablePrinter.Event(SessionManager.GetServiceClient().GetEventsList());

                    /* 
                     * Delete Event Form
                     * 
                     * Every Primary Key input must be valid.
                     * When a Event is deleted, the Prenotations and the Reservations
                     * linked to it are deleted too.
                     */
                    Output.WriteLine("\n------ DELETE EVENT ------- ");
                    string event_code = Input.ReadString("Insert the Code of the Event to delete: ");
                    // Navigate back if User type "\\" on first input
                    if (event_code.Contains("\\"))
                        Program.NavigateBack();
                    int eventCode = Controls.CheckIntForeignKey(event_code.ToString(), "Evento");

                    /*
                     * Send data to Database
                     */
                    if (SessionManager.GetServiceClient().DeleteEvent(eventCode))
                        Output.WriteLine("\nEVENT CANCELLATION SUCCESS!\n");
                    else Output.WriteLine("\nEVENT CANCELLATION FAILED! Retry!\n");

                } else Console.WriteLine("There are no Events in the DataBase!");
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