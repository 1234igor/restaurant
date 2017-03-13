using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Settings
    {
        private string _openTime;
        private string _closeTime;
        private string _waitTime;
        private int _days;
        private Dictionary<int, int> _tables;

        public string openTime
        {
            get { return _openTime; }
            set { _openTime = value; }
        }

        public string closeTime
        {
            get { return _closeTime; }
            set { _closeTime = value; }
        }

        public string waitTime
        {
            get { return _waitTime; }
            set { _waitTime = value; }
        }

        public int days
        {
            get { return _days; }
            set { _days = value; }
        }

        public Dictionary<int, int> tables
        {
            get { return _tables; }
            set { _tables = value; }
        }

        public List<string> availibleTime()
        {
            List<string> r = new List<string>();
            int openTimeNum = getTimeNum(_openTime);
            int closeTimeNum = getTimeNum(_closeTime);
            int waitTimeNum = getTimeNum(_waitTime);
            int currentTimeNum = openTimeNum;

            while (currentTimeNum+waitTimeNum<=closeTimeNum)
            {
                r.Add(getTimeString(currentTimeNum));
                currentTimeNum += waitTimeNum;
            }

            return r;
        }


        public List<string> availibleDates()
        {
            List<string> r = new List<string>();
            DateTime today = DateTime.Today;
            for (int i = 0; i<_days ;i++)
            { r.Add((today.AddDays(i)).ToString("d")); }
            return r;
        }

        public List<int> availiblePersons()
        {
            List<int> r = new List<int>();
            foreach (var item in _tables)
            {
                r.Add(item.Key);
            }
            return r;

        }



        public int getTimeNum(string time)
        {
            return int.Parse(((time.Split(':'))[0])) * 60 + int.Parse(((time.Split(':'))[1]));
        }

        public string getTimeString(int time)
        {
            if (((time % 60).ToString()).Length < 2)
                return ((time / 60).ToString()) + ":0" + ((time % 60).ToString());
            else
                return ((time / 60).ToString()) + ":" + ((time % 60).ToString());
        }

      
        /*
        public Settings (string o, string c, string w, string d, Dictionary<int, int> t)
        {
            _openTime = o;
            _closeTime = c;
            _waitTime = w;
            _days = d;
            _tables = t;
        }
        */

    }
}
