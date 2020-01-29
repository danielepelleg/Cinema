using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFDatabaseManager
{
    /*
     * Reserve Class
     * Store Reserve objects of the database
     * 
     * @author Daniele Pellegrini <daniele.pellegrini@studenti.unipr.it> - 285240
     * @author Riccardo Fava <riccardo.fava@studenti.unipr.it> - 287516
     */
    class Reserve
    {
        private int place_Number;
        private int prenotation_Code;

        public int placeNumber
        {
            get => this.place_Number;
            set => this.place_Number = value;
        }

        public int prenotationCode
        {
            get => this.prenotation_Code;
            set => this.prenotation_Code = value;
        }
    }
}