using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine
{
    public class Time
    {
        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Second { get; set; }

        public int Millisecond { get; set; }

        public Time()
        {
        }

        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public static Time Parse(string timeText)
        {
            if (string.IsNullOrWhiteSpace(timeText))
            {
                return null;
            }

            DateTime dateTime = DateTime.Parse(timeText);
            Time time = new Time
            {
                Hour = dateTime.Hour,
                Minute = dateTime.Minute
            };

            return time;
        }

        public static Time FromDateTime(DateTime dateTime)
        {
            Time time = new Time
            {
                Hour = dateTime.Hour,
                Minute = dateTime.Minute
            };

            return time;
        }

        public string ToString(string formatter)
        {
            DateTime currentTime = DateTime.Now;

            DateTime displayTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Hour, Minute, 0);
            return displayTime.ToString(formatter);
        }

        public override string ToString()
        {
            return ToString("HH:mm");
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Time time = obj as Time;
            if (time == null)
            {
                return false;
            }

            return time.GetHashCode() == GetHashCode();
        }

        public static bool operator <(Time a, Time b)
        {
            return GetComparisonScore(a) < GetComparisonScore(b);
        }

        public static bool operator >(Time a, Time b)
        {
            return GetComparisonScore(a) > GetComparisonScore(b);
        }

        private static long GetComparisonScore(Time time)
        {
            if (time == null)
            {
                return -1;
            }

            return 60 * 60 * 1000 * time.Hour + 60 * 1000 * time.Minute + 1000 * time.Second + time.Millisecond;
        }

        public static bool operator ==(Time a, Time b)
        {
            if ((object)a == null && (object)b == null)
            {
                return true;
            }

            if ((object)a == null)
            {
                return false;
            }

            if ((object)a == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Time a, Time b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return 60 * Hour + Minute;
        }
    }
}
