using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Text.RegularExpressions;

namespace GotDotNet.Exslt
{
	/// <summary>
	/// This class implements the EXSLT functions in the http://exslt.org/dates-and-times namespace.
	/// </summary>
	public class ExsltDatesAndTimes
	{
		
		/// <summary>
		/// Implements the following function
		///   string date:date-time()
		/// </summary>
		/// <returns>The current time</returns>		
		public string dateTime(){		
			return DateTime.Now.ToString("s"); 
		}
        
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string dateTime_RENAME_ME() 
        {
            return dateTime();
        }    

		/// <summary>
		/// Implements the following function
		///   string date:date-time()
		/// </summary>
		/// <returns>The current date and time or the empty string if the 
		/// date is invalid </returns>        
		public string dateTime(string d){		
			try{
				return DateTime.Parse(d).ToString("s"); 				 
			}catch(FormatException){
				return ""; 
			}
		}
		
		/// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string dateTime_RENAME_ME(string d) 
        {
            return dateTime(d);
        }

		/// <summary>
		/// Implements the following function
		///   string date:date()
		/// </summary>
		/// <returns>The current date</returns>        
		public string date(){
		  string date = DateTime.Now.ToString("s"); 
		  string[] dateNtime = date.Split('T'); 
		  return dateNtime[0]; 
		}

		/// <summary>
		/// Implements the following function
		///   string date:date(string)
		/// </summary>
		/// <returns>The date part of the specified date or the empty string if the 
		/// date is invalid</returns>        
		public string date(string d){
			try{
				string[] dateNtime = DateTime.Parse(d).ToString("s").Split('T'); 
				return dateNtime[0]; 
			}catch(FormatException){
			  return ""; 
			}
		}

		/// <summary>
		/// Implements the following function
		///   string date:time()
		/// </summary>
		/// <returns>The current time</returns>        
		public string time(){
			string date = DateTime.Now.ToString("s"); 
			string[] dateNtime = date.Split('T'); 
			return dateNtime[1]; 
		}

		/// <summary>
		/// Implements the following function
		///   string date:time(string)
		/// </summary>
		/// <returns>The time part of the specified date or the empty string if the 
		/// date is invalid</returns>        
		public string time(string d){
			try{
				string[] dateNtime = DateTime.Parse(d).ToString("s").Split('T'); 
				return dateNtime[1]; 
			}catch(FormatException){
				return ""; 
			}
		}
		

		/// <summary>
		/// Implements the following function
		///   number date:year()
		/// </summary>
		/// <returns>The current year</returns>        
		public double year(){
			return DateTime.Now.Year;
		}

		/// <summary>
		/// Implements the following function
		///   number date:year(string)
		/// </summary>
		/// <returns>The year part of the specified date or the empty string if the 
		/// date is invalid</returns>
		/// <remarks>Does not support dates in the format of the xs:yearMonth or 
		/// xs:gYear types</remarks>        
		public double year(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.Year; 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}

		/// <summary>
		/// Helper method for calculating whether a year is a leap year. Algorithm 
		/// obtained from http://mindprod.com/jglossleapyear.html
		/// </summary>        
		private static bool IsLeapYear ( int year) { 
		
			return CultureInfo.CurrentCulture.Calendar.IsLeapYear(year); 
		}


		/// <summary>
		/// Implements the following function
		///   boolean date:leap-year()
		/// </summary>
		/// <returns>True if the current year is a leap year</returns>        
		public bool leapYear(){
			return IsLeapYear(DateTime.Now.Year);
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public bool leapYear_RENAME_ME() 
        {
            return leapYear();
        }

		/// <summary>
		/// Implements the following function
		///   boolean date:leap-year(string)
		/// </summary>
		/// <returns>True if the specified year is a leap year</returns>
		/// <remarks>Does not support dates in the format of the xs:yearMonth or 
		/// xs:gYear types</remarks>        
		public bool leapYear(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return IsLeapYear(date.Year); 
			}catch(FormatException){
				return false; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public bool leapYear_RENAME_ME(string d) 
        {
            return leapYear(d);
        }

		/// <summary>
		/// Implements the following function
		///   number date:month-in-year()
		/// </summary>
		/// <returns>The current month</returns>        
		public double monthInYear(){
			return DateTime.Now.Month;
		}
        
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double monthInYear_RENAME_ME() 
        {
            return monthInYear();
        }

		/// <summary>
		/// Implements the following function
		///   number date:month-in-year(string)
		/// </summary>
		/// <returns>The month part of the specified date or the empty string if the 
		/// date is invalid</returns>
		/// <remarks>Does not support dates in the format of the xs:yearMonth or 
		/// xs:gYear types</remarks>        
		public double monthInYear(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.Month; 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double monthInYear_RENAME_ME(string d) 
        {
            return monthInYear(d);
        }

		/// <summary>
		/// Helper method uses local culture information. 
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		private double weekInYear(DateTime d){
		    Calendar calendar = CultureInfo.CurrentCulture.Calendar; 
		    return calendar.GetWeekOfYear(d,CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, 
										CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek); 
		}		        

		/// <summary>
		/// Implements the following function
		///   number date:week-in-year()
		/// </summary>
		/// <returns>The current week. This method uses the Calendar.GetWeekOfYear() method 
		/// with the CalendarWeekRule and FirstDayOfWeek of the current culture.
		/// THE RESULTS OF CALLING THIS FUNCTION VARIES ACROSS CULTURES</returns>        
		public double weekInYear(){
			return this.weekInYear(DateTime.Now);
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double weekInYear_RENAME_ME() 
        {
            return weekInYear();
        }

		/// <summary>
		/// Implements the following function
		///   number date:week-in-year(string)
		/// </summary>
		/// <returns>The week part of the specified date or the empty string if the 
		/// date is invalid</returns>
		/// <remarks>Does not support dates in the format of the xs:yearMonth or 
		/// xs:gYear types. This method uses the Calendar.GetWeekOfYear() method 
		/// with the CalendarWeekRule and FirstDayOfWeek of the current culture.
		/// THE RESULTS OF CALLING THIS FUNCTION VARIES ACROSS CULTURES</remarks>        
		public double weekInYear(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return this.weekInYear(date); 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double weekInYear_RENAME_ME(string d) 
        {
            return weekInYear(d);
        }

        /// <summary>
        /// Helper method uses local culture information. 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private double weekInMonth(DateTime d){
            //Probably this is too rough implementation
            return (double)(int)((d.Day-1)/7 + 1);
        }

        /// <summary>
        /// Implements the following function
        ///   number date:week-in-month()
        /// </summary>
        /// <returns>The current week in month as a number. This method uses the Calendar.GetWeekOfYear() method 
        /// with the CalendarWeekRule and FirstDayOfWeek of the current culture.
        /// THE RESULTS OF CALLING THIS FUNCTION VARIES ACROSS CULTURES</returns>        
        public double weekInMonth()
        {
            return this.weekInMonth(DateTime.Now);
        }
        
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double weekInMonth_RENAME_ME() 
        {
            return weekInMonth();
        }

        /// <summary>
        /// Implements the following function
        ///   number date:week-in-month(string)
        /// </summary>
        /// <returns>The week in month of the specified date or the empty string if the 
        /// date is invalid</returns>
        /// <remarks>Does not support dates in the format of the xs:yearMonth or 
        /// xs:gYear types. This method uses the Calendar.GetWeekOfYear() method 
        /// with the CalendarWeekRule and FirstDayOfWeek of the current culture.
        /// THE RESULTS OF CALLING THIS FUNCTION VARIES ACROSS CULTURES</remarks>        
        public double weekInMonth(string d)
        {
            try
            {
                DateTime date = DateTime.Parse(d); 
                return this.weekInMonth(date); 
            }
            catch(FormatException)
            {
                return System.Double.NaN; 
            }
        }
        
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double weekInMonth_RENAME_ME(string d) 
        {
            return weekInMonth(d);
        }


		/// <summary>
		/// Implements the following function
		///   number date:day-in-year()
		/// </summary>
		/// <returns>The current day. </returns>        
		public double dayInYear(){
			return DateTime.Now.DayOfYear;
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double dayInYear_RENAME_ME() 
        {
            return dayInYear();
        }

		/// <summary>
		/// Implements the following function
		///   number date:day-in-year(string)
		/// </summary>
		/// <returns>The day part of the specified date or the empty string if the 
		/// date is invalid</returns>        
		public double dayInYear(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.DayOfYear; 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double dayInYear_RENAME_ME(string d) 
        {
            return dayInYear(d);
        }

		/// <summary>
		/// Implements the following function
		///   number date:day-in-week()
		/// </summary>
		/// <returns>The current day in the week. 1=Sunday, 2=Monday,...,7=Saturday</returns>        
		public double dayInWeek(){
			return ((int) DateTime.Now.DayOfWeek) + 1;
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double dayInWeek_RENAME_ME() 
        {
            return dayInWeek();
        }

		/// <summary>
		/// Implements the following function
		///   number date:day-in-week(string)
		/// </summary>
		/// <returns>The day in the week of the specified date or the empty string if the 
		/// date is invalid. <returns>The current day in the week. 1=Sunday, 2=Monday,...,7=Saturday
		/// </returns>        
		public double dayInWeek(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return ((int)date.DayOfWeek) + 1; 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double dayInWeek_RENAME_ME(string d) 
        {
            return dayInWeek(d);
        }


		/// <summary>
		/// Implements the following function
		///   number date:day-in-month()
		/// </summary>
		/// <returns>The current day. </returns>        
		public double dayInMonth(){
			return DateTime.Now.Day;
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double dayInMonth_RENAME_ME() 
        {
            return dayInMonth();
        }

		/// <summary>
		/// Implements the following function
		///   number date:day-in-month(string)
		/// </summary>
		/// <returns>The day part of the specified date or the empty string if the 
		/// date is invalid</returns>
		/// <remarks>Does not support dates in the format of the xs:MonthDay or 
		/// xs:gDay types</remarks>        
		public double dayInMonth(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.Day; 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double dayInMonth_RENAME_ME(string d) 
        {
            return dayInMonth(d);
        }

		/// <summary>
		/// Helper method.
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		private double dayOfWeekInMonth(int day){
		
		int toReturn = 0;
 
			do{ 
				toReturn++; 
				day-= 7; 
			}while(day > 0);

		return toReturn; 
		}

		/// <summary>
		/// Implements the following function
		///   number date:day-of-week-in-month()
		/// </summary>
		/// <returns>The current day of week in the month as a number. For instance 
		/// the third Tuesday of the month returns 3</returns>        
		public double dayOfWeekInMonth(){
			return this.dayOfWeekInMonth(DateTime.Now.Day);
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double dayOfWeekInMonth_RENAME_ME() 
        {
            return dayOfWeekInMonth();
        }

		/// <summary>
		/// Implements the following function
		///   number date:day-of-week-in-month(string)
		/// </summary>
		/// <returns>The day part of the specified date or the empty string if the 
		/// date is invalid</returns>        
		public double dayOfWeekInMonth(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return this.dayOfWeekInMonth(date.Day); 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double dayOfWeekInMonth_RENAME_ME(string d) 
        {
            return dayOfWeekInMonth(d);
        }
	
		/// <summary>
		/// Implements the following function
		///   number date:hour-in-day()
		/// </summary>
		/// <returns>The current hour of the day as a number.</returns>        
		public double hourInDay(){
			return DateTime.Now.Hour;
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double hourInDay_RENAME_ME() 
        {
            return hourInDay();
        }

		/// <summary>
		/// Implements the following function
		///   number date:hour-in-day(string)
		/// </summary>
		/// <returns>The current hour of the specified time or the empty string if the 
		/// date is invalid</returns>        
		public double hourInDay(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.Hour; 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double hourInDay_RENAME_ME(string d) 
        {
            return hourInDay(d);
        }

		/// <summary>
		/// Implements the following function
		///   number date:minute-in-hour()
		/// </summary>
		/// <returns>The minute of the current hour as a number. </returns>        
		public double minuteInHour(){
			return DateTime.Now.Minute;
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double minuteInHour_RENAME_ME() 
        {
            return minuteInHour();
        }

		/// <summary>
		/// Implements the following function
		///   number date:minute-in-hour(string)
		/// </summary>
		/// <returns>The minute of the hour of the specified time or the empty string if the 
		/// date is invalid</returns>        
		public double minuteInHour(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.Minute; 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}

        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double minuteInHour_RENAME_ME(string d) 
        {
            return minuteInHour(d);
        }

		/// <summary>
		/// Implements the following function
		///   number date:second-in-minute()
		/// </summary>
		/// <returns>The seconds of the current minute as a number. </returns>        
		public double secondInMinute(){
			return DateTime.Now.Second;
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double secondInMinute_RENAME_ME() 
        {
            return secondInMinute();
        }		

		/// <summary>
		/// Implements the following function
		///   number date:second-in-minute(string)
		/// </summary>
		/// <returns>The seconds of the minute of the specified time or the empty string if the 
		/// date is invalid</returns>        
		public double secondInMinute(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.Second; 
			}catch(FormatException){
				return System.Double.NaN; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public double secondInMinute_RENAME_ME(string d) 
        {
            return secondInMinute(d);
        }

		/// <summary>
		/// Implements the following function
		///   string date:day-name()
		/// </summary>
		/// <returns>The name of the current day</returns>        
		public string dayName(){
			return DateTime.Now.DayOfWeek.ToString();
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string dayName_RENAME_ME() 
        {
            return dayName();
        }

		/// <summary>
		/// Implements the following function
		///   string date:day-name(string)
		/// </summary>
		/// <returns>The name of the day of the specified date or the empty string if the 
		/// date is invalid</returns>        
		public string dayName(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.DayOfWeek.ToString();
			}catch(FormatException){
				return ""; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string dayName_RENAME_ME(string d) 
        {
            return dayName(d);
        }

		/// <summary>
		/// Implements the following function
		///   string date:day-abbreviation()
		/// </summary>
		/// <returns>The abbreviated name of the current day</returns>        
		public string dayAbbreviation(){
			return DateTime.Now.DayOfWeek.ToString().Substring(0,3);
		}
		
        public string dayAbbreviation_RENAME_ME() 
        {
            return dayAbbreviation();
        }

		/// <summary>
		/// Implements the following function
		///   string date:day-abbreviation(string)
		/// </summary>
		/// <returns>The abbreviated name of the day of the specified date or the empty string if the 
		/// date is invalid</returns>        
		public string dayAbbreviation(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				return date.DayOfWeek.ToString().Substring(0,3);
			}catch(FormatException){
				return ""; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string dayAbbreviation_RENAME_ME(string d) 
        {
            return dayAbbreviation(d);
        }


		/// <summary>
		/// Implements the following function
		///   string date:month-name()
		/// </summary>
		/// <returns>The name of the current month</returns>        
		public string monthName(){
			string month = DateTime.Now.ToString("m");
			string[] splitmonth = month.Split(' ');
			return splitmonth[0];
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string monthName_RENAME_ME() 
        {
            return monthName();
        }

		/// <summary>
		/// Implements the following function
		///   string date:month-name(string)
		/// </summary>
		/// <returns>The name of the month of the specified date or the empty string if the 
		/// date is invalid</returns>
		/// <remarks>Does not support dates in the format of the xs:yearMonth or 
		/// xs:gYear types</remarks>        
		public string monthName(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				string month = date.ToString("m");
				string[] splitmonth = month.Split(' ');
				return splitmonth[0];
			}catch(FormatException){
				return ""; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string monthName_RENAME_ME(string d) 
        {
            return monthName(d);
        }

		/// <summary>
		/// Implements the following function
		///   string date:month-abbreviation()
		/// </summary>
		/// <returns>The abbreviated name of the current month</returns>        
		public string monthAbbreviation(){
			string month = DateTime.Now.ToString("m");
			string[] splitmonth = month.Split(' ');
			return splitmonth[0].Substring(0,3); 
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string monthAbbreviation_RENAME_ME() 
        {
            return monthAbbreviation();
        }

		/// <summary>
		/// Implements the following function
		///   string date:month-abbreviation(string)
		/// </summary>
		/// <returns>The abbreviated name of the month of the specified date or the empty string if the 
		/// date is invalid</returns>
		/// <remarks>Does not support dates in the format of the xs:yearMonth or 
		/// xs:gYear types</remarks>        
		public string monthAbbreviation(string d){
			try{
				DateTime date = DateTime.Parse(d); 
				string month = date.ToString("m");
				string[] splitmonth = month.Split(' ');
				return splitmonth[0].Substring(0, 3);;
			}catch(FormatException){
				return ""; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string monthAbbreviation_RENAME_ME(string d) 
        {
            return monthAbbreviation(d);
        }

		/// <summary>
		/// Implements the following function
		///   string date:format-date(string, string)
		/// </summary>
		/// <param name="date">The date to format</param>
		/// <param name="format">One of the format strings understood by the 
		/// Java 1.1 SimpleDateFormat method:
		/// 
		///  Symbol   Meaning                 Presentation        Example
		///------   -------                 ------------        -------
		///G        era designator          (Text)              AD
		///y        year                    (Number)            1996
		///M        month in year           (Text & Number)     July & 07
		///d        day in month            (Number)            10
		///h        hour in am/pm (1~12)    (Number)            12
		///H        hour in day (0~23)      (Number)            0
		///m        minute in hour          (Number)            30
		///s        second in minute        (Number)            55
		///S        millisecond             (Number)            978
		///E        day in week             (Text)              Tuesday
		///D        day in year             (Number)            189
		///F        day of week in month    (Number)            2 (2nd Wed in July)
		///w        week in year            (Number)            27
		///W        week in month           (Number)            2
		///a        am/pm marker            (Text)              PM
		///k        hour in day (1~24)      (Number)            24
		///K        hour in am/pm (0~11)    (Number)            0
		///z        time zone               (Text)              Pacific Standard Time
		///'        escape for text         (Delimiter)
		///''       single quote            (Literal)           '
		///</param>
		/// <returns>The formated date</returns>        
		public string formatDate(string d, string format)
		{
			try
			{
				DateTime oDate = DateTime.Parse(d);
				StringBuilder retString = new StringBuilder("");
				for (int i=0; i < format.Length;)
				{
					int s = i;
					switch(format.Substring(i, 1))
					{
						case "G"://        era designator          (Text)              AD
							while (i < format.Length && format.Substring(i, 1)=="G"){i++;}
							if (oDate.Year < 0)
							{
								retString.Append("BC");
							}
							else
							{
								retString.Append("AD");
							}
							break;
						case "y"://        year                    (Number)            1996
							while (i < format.Length && format.Substring(i, 1)=="y"){i++;}
							if (i-s > 2)
							{
								retString.Append(oDate.Year);
							}
							else
							{
								retString.Append(oDate.Year.ToString().Substring(4-(i-s)));
							}
							break;
						case "M"://        month in year           (Text &amp; Number)     July &amp; 07
							while (i < format.Length && format.Substring(i, 1)=="M"){i++;}
							if (i-s <= 2)
							{
								retString.Append(oDate.Month.ToString().PadLeft(i-s, '0'));
							}
							else
							{
								retString.Append(oDate.ToString("".PadRight(i-s, 'M')));
							}
							break;
						case "d"://        day in month            (Number)            10
							while (i < format.Length && format.Substring(i, 1)=="d"){i++;}
							retString.Append(oDate.Day.ToString().PadLeft(i-s, '0'));
							break;
						case "h"://        hour in am/pm (1~12)    (Number)            12
							while (i < format.Length && format.Substring(i, 1)=="h"){i++;}
							retString.Append(oDate.ToString(format.Substring(s, i-s)));
							break;
						case "H"://        hour in day (0~23)      (Number)            0
							while (i < format.Length && format.Substring(i, 1)=="H"){i++;}
							retString.Append(oDate.ToString(format.Substring(s, i-s)));
							break;
						case "m"://        minute in hour          (Number)            30
							while (i < format.Length && format.Substring(i, 1)=="m"){i++;}
							retString.Append(oDate.Minute.ToString().PadLeft(i-s, '0'));
							break;
						case "s"://        second in minute        (Number)            55
							while (i < format.Length && format.Substring(i, 1)=="s"){i++;}
							retString.Append(oDate.Second.ToString().PadLeft(i-s, '0'));
							break;
						case "S"://        millisecond             (Number)            978
							while (i < format.Length && format.Substring(i, 1)=="S"){i++;}
							retString.Append(oDate.ToString("".PadRight(i-s, 'f')));
							break;
						case "E"://        day in week             (Text)              Tuesday
							while (i < format.Length && format.Substring(i, 1)=="E"){i++;}
							if (i-s <= 3)
							{
								retString.Append(oDate.ToString("ddd"));
							}
							else
							{
								retString.Append(oDate.ToString("dddd"));
							}
							break;
						case "D"://        day in year             (Number)            189
							while (i < format.Length && format.Substring(i, 1)=="D"){i++;}
							retString.Append(oDate.DayOfYear);
							break;
						case "F"://        day of week in month    (Number)            2 (2nd Wed in July)
							while (i < format.Length && format.Substring(i, 1)=="F"){i++;}
							retString.Append(dayOfWeekInMonth(oDate.Day));
							break;
						case "w"://        week in year            (Number)            27
							while (i < format.Length && format.Substring(i, 1)=="w"){i++;}
							retString.Append(weekInYear(oDate));
							break;
						case "W"://        week in month           (Number)            2
							while (i < format.Length && format.Substring(i, 1)=="W"){i++;}
							retString.Append(weekInMonth(oDate));
							break;
						case "a"://        am/pm marker            (Text)              PM
							while (i < format.Length && format.Substring(i, 1)=="a"){i++;}
							retString.Append(oDate.ToString("tt").ToUpper());
							break;
						case "k"://        hour in day (1~24)      (Number)            24
							while (i < format.Length && format.Substring(i, 1)=="k"){i++;}
							retString.Append(oDate.Hour + 1);
							break;
						case "K"://        hour in am/pm (0~11)    (Number)            0
							while (i < format.Length && format.Substring(i, 1)=="K"){i++;}
							if (oDate.Hour > 12)
							{
								retString.Append(oDate.Hour - 12);
							}
							else
							{
								retString.Append(oDate.Hour);
							}
							break;
						case "z"://        time zone               (Text)              Pacific Standard Time
							while (i < format.Length && format.Substring(i, 1)=="z"){i++;}
							// Time zones cannot be easily supported by Microsoft
							break;
						case "'"://        escape for text         (Delimiter)
							if (i < format.Length && format.Substring(i+1, 1) == "'")
							{
								i++;
								while (i < format.Length && format.Substring(i, 1)=="'"){i++;}
								retString.Append("'");
							}
							else
							{
								i++;
								while (i < format.Length && format.Substring(i, 1)!="'" && i <= format.Length){retString.Append(format.Substring(i++, 1));}
								if (i >= format.Length)return "";
								i++;
							}
							break;
						default:
							retString.Append(format.Substring(i, 1));
							i++;
							break;
					}
				}
	
				return retString.ToString();
			}

		
			catch(FormatException)
			{
				return ""; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string formatDate_RENAME_ME(string d, string format) 
        {
            return formatDate(d, format);
        }


		/// <summary>
		/// Implements the following function
		///   string date:parse-date(string, string)
		/// </summary>
		/// <param name="date">The date to parse</param>
		/// <param name="format">One of the format strings understood by the 
		/// DateTime.ToString(string) method.</param>
		/// <returns>The parsed date</returns>        
		public string parseDate(string d, string format){
			try{
				DateTime date = DateTime.ParseExact(d, format, CultureInfo.CurrentCulture); 
				return XmlConvert.ToString(date);
			}catch(FormatException){
				return ""; 
			}
		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string parseDate_RENAME_ME(string d, string format) 
        {
            return parseDate(d, format);
        }
	
		/// <summary>
		/// Implements the following function 
		///    string:date:difference(string, string)
		/// </summary>
		/// <param name="start">The start date</param>
		/// <param name="end">The end date</param>
		/// <returns>A positive difference if start is before end otherwise a negative
		/// difference. The difference is in the format [-][d.]hh:mm:ss[.ff]</returns>        
		public string difference(string start, string end){
		
			try{
				DateTime startdate = DateTime.Parse(start); 
				DateTime enddate   = DateTime.Parse(end); 
				return XmlConvert.ToString(enddate.Subtract(startdate));
			}catch(FormatException){
				return ""; 
			}
		}

		/// <summary>
		/// Implements the following function
		///    string:date:add(string, string)
		/// </summary>
		/// <param name="datetime">A date/time</param>
		/// <param name="duration">the duration to add</param>
		/// <returns>The new time</returns>        
		public string add(string datetime, string duration){
			
			try{
				DateTime date = DateTime.Parse(datetime); 
				TimeSpan timespan = System.Xml.XmlConvert.ToTimeSpan(duration); 
				return XmlConvert.ToString(date.Add(timespan));
			}catch(FormatException){
				return ""; 
			}

		}


		/// <summary>
		/// Implements the following function
		///    string:date:add-duration(string, string)
		/// </summary>
		/// <param name="datetime">A date/time</param>
		/// <param name="duration">the duration to add</param>
		/// <returns>The new time</returns>        
		public string addDuration(string duration1, string duration2){
			
			try{
				TimeSpan timespan1 = XmlConvert.ToTimeSpan(duration1);
				TimeSpan timespan2 = XmlConvert.ToTimeSpan(duration2); 
				return XmlConvert.ToString(timespan1.Add(timespan2));
			}catch(FormatException){
				return ""; 
			}

		}
		
        /// <summary>
        /// This wrapper method will be renamed during custom build 
        /// to provide conformant EXSLT function name.
        /// </summary>
        public string addDuration_RENAME_ME(string duration1, string duration2) 
        {
            return addDuration(duration1, duration2);
        }

		/// <summary>
		/// Implements the following function
		///		number date:seconds()
		/// </summary>
		/// <returns>The amount of seconds since the epoch (1970-01-01T00:00:00Z)</returns>        
		public double  seconds(){
		 
			try{
				
			DateTime epoch  = new DateTime(1970, 1, 1, 0,0,0,0, CultureInfo.CurrentCulture.Calendar);
			TimeSpan duration = DateTime.Now.Subtract(epoch); 
			return duration.TotalSeconds; 

			}catch(Exception){
				return System.Double.NaN; 
			} 
		}


		/// <summary>
		/// Implements the following function
		///		number date:seconds(string)
		/// </summary>
		/// <returns>The amount of seconds between the specified date and the 
		/// epoch (1970-01-01T00:00:00Z)</returns>
		public double  seconds(string datetime){
		 
			try{
				
				DateTime epoch  = new DateTime(1970, 1, 1, 0,0,0,0, CultureInfo.CurrentCulture.Calendar);
				DateTime date   = DateTime.Parse(datetime);; 
				return date.Subtract(epoch).TotalSeconds; 

			}catch(FormatException){ ; } //might be a duration

			try{
				TimeSpan duration = XmlConvert.ToTimeSpan(datetime); 
				return duration.TotalSeconds;
			}catch(FormatException){
				return System.Double.NaN;
			}
		}

		/// <summary>
		/// Implements the following function 
		///		string date:sum(node-set)
		/// </summary>
		/// <param name="iterator">The nodeset</param>
		/// <returns>The sum of the values within the node set treated as </returns>        
		public string sum(XPathNodeIterator iterator){
			
			TimeSpan sum = new TimeSpan(0,0,0,0); 
 
			if(iterator.Count == 0){
				return ""; 
			}

			try{ 
				while(iterator.MoveNext()){
					sum = XmlConvert.ToTimeSpan(iterator.Current.Value).Add(sum);
				}
				
			}catch(FormatException){
				return ""; 
			}

			return XmlConvert.ToString(sum) ; //XmlConvert.ToString(sum);
			}



		/// <summary>
		/// Implements the following function 
		///    string date:duration(number)
		/// </summary>
		/// <param name="seconds"></param>
		/// <returns></returns>        
		public string duration(double seconds){
		
			return XmlConvert.ToString(new TimeSpan(0,0,(int)seconds)); 
		}
	
	}
}
