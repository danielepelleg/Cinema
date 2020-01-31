using EasyConsole;

namespace Cinema
{
    internal class AddEvent : Page
    {
        private CinemaProgram cinemaProgram;

        public AddEvent(CinemaProgram cinemaProgram)
        {
            this.cinemaProgram = cinemaProgram;
        }
    }
}