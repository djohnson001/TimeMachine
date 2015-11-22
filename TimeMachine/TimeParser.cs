using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMachine
{
    public class TimeParser
    {
        public Time Parse(string timeText)
        {
            if (string.IsNullOrWhiteSpace(timeText))
            {
                return null;
            }

            bool isAm = true;

            if (timeText.ToUpper().Contains("A"))
            {
                isAm = true;
            }
            else if (timeText.ToUpper().Contains("P"))
            {
                isAm = false;
            }

            List<string> pieces;
            if (timeText.Contains(":"))
            {
                pieces = timeText.Split(':').ToList();
            }
            else
            {
                pieces = timeText.Split(' ').ToList();
            }

            pieces = pieces.Where(queryPiece => !string.IsNullOrWhiteSpace(queryPiece)).ToList();

            if (pieces.Count == 1)
            {
                return ParseSinglePieceTime(pieces[0], isAm);
            }
            else if (pieces.Count >= 2)
            {
                return ParseTwoPieceTime(pieces[0], pieces[1], isAm);
            }

            return null;
        }

        public bool IsTimeValid(int hours, int minutes)
        {
            return hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60;
        }

        public string FilterOutNonDigits(string text)
        {
            StringBuilder filteredText = new StringBuilder();
            foreach (char ch in text)
            {
                if (char.IsDigit(ch))
                {
                    filteredText.Append(ch);
                }
            }

            return filteredText.ToString();
        }

        private Time ParseSinglePieceTime(string singlePiece, bool isAm)
        {
            string onlyDigits = FilterOutNonDigits(singlePiece);
            if (string.IsNullOrWhiteSpace(onlyDigits))
            {
                return null;
            }

            int timeInt;
            decimal timeNum;
            if (!int.TryParse(onlyDigits, out timeInt))
            {
                timeInt = 0;
            }

            timeNum = timeInt;

            while (timeNum > 100)
            {
                timeNum /= 100;
            }

            int hours = (int)timeNum;
            if (hours < 0 || hours == 24)
            {
                hours = 0;
            }

            if (hours < 12 && !isAm)
            {
                hours += 12;
                timeNum += 12;
            }

            if (hours == 24)
            {
                hours -= 24;
            }

            timeNum -= hours;

            if (hours == 12 && isAm)
            {
                hours -= 12;
            }

            decimal minutes = timeNum * 100;
            if (minutes < 0)
            {
                minutes = 0;
            }

            if (hours > 24)
            {
                minutes = hours % 10;
                hours /= 10;
            }

            return IsTimeValid(hours, (int)minutes) ? new Time(hours, (int)minutes) : null;

        }

        private Time ParseTwoPieceTime(string pieceA, string pieceB, bool isAm)
        {
            string hoursDigits = FilterOutNonDigits(pieceA);
            if (string.IsNullOrWhiteSpace(hoursDigits))
            {
                pieceA = pieceB;
                pieceB = "0";

                hoursDigits = FilterOutNonDigits(pieceA);
            }

            int timeInt;
            decimal timeNum;
            if (!int.TryParse(hoursDigits, out timeInt))
            {
                timeInt = 0;
            }

            timeNum = timeInt;

            while (timeNum > 100)
            {
                timeNum /= 100;
            }

            int hour = (int)timeNum;
            if (hour < 12 && !isAm)
            {
                hour += 12;
            }

            timeNum -= hour;
            decimal minutes = timeNum * 100;

            if (minutes > 0)
            {
                return new Time(hour, (int)minutes);
            }

            string minutesDigits = FilterOutNonDigits(pieceB);
            int minutesInt;
            if (!int.TryParse(minutesDigits, out minutesInt))
            {
                minutesInt = 0;
            }

            minutes = minutesInt;

            if (minutes < 0)
            {
                minutes = 0;
            }

            while (minutes > 100)
            {
                minutes /= 100;
            }

            // The 12:00 AM case
            if (hour == 12 && isAm && minutes == 0)
            {
                hour = 0;
            }

            return IsTimeValid(hour, (int)minutes) ? new Time(hour, (int)minutes) : null;

        }
    }
}
