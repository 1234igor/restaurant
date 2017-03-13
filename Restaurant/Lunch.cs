using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Lunch
    {
        private string _date;
        private string _time;
        private string _name;
        private string _phone;
        private int _persons;

        public string date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public int persons
        {
            get { return _persons; }
            set { _persons = value; }
        }


        /*
        public string Info()
        {
            return "Name: " + name + "; Ust kap: " + ust_kapital + "; Comm kap: " + comm_kapital + ";";
        }
        public string Info_To_File()
        {
            return name + ";" + ust_kapital + ";" + comm_kapital;
        }
        */

        public Lunch(string d, string t, string n, string p, int pr)
        {
            _date = d;
            _time = t;
            _name = n;
            _phone = p;
            _persons = pr;
        }
    }
}
