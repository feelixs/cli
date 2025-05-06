using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace GotDotNet.Exslt
{
    /// <summary>
    /// This class implements additional functions in the http://gotdotnet.com/exslt/dates-and-times namespace.
    /// </summary>
    public class GDNDatesAndTimes
    {        
        private string duration(double seconds)
        {		
            return XmlConvert.ToString(new TimeSpan(0,0,(int)seconds)); 
        }
        
        /// <summary>
        /// Implements the following function 
        ///    string date:avg(node-set)
        /// </summary>
        /// <param name="iterator"></param>
        /// <returns></returns>
        /// <remarks>THIS FUNCTION IS NOT PART OF EXSLT!!!</remarks>
        public string avg(XPathNodeIterator iterator)
        {

            TimeSpan sum = new TimeSpan(0,0,0,0); 
            int count = iterator.Count;

            if(count == 0)
            {
                return ""; 
            }

            try
            { 
                while(iterator.MoveNext())
                {
                    sum = XmlConvert.ToTimeSpan(iterator.Current.Value).Add(sum);
                }
				
            }
            catch(FormatException)
            {
                return ""; 
            }			 

            return duration(sum.TotalSeconds / count); 
        }

        /// <summary>
        /// Implements the following function 
        ///    string date:min(node-set)
        /// </summary>
        /// <param name="iterator"></param>
        /// <returns></returns>
        /// <remarks>THIS FUNCTION IS NOT PART OF EXSLT!!!</remarks>
        public string min(XPathNodeIterator iterator)
        {

            TimeSpan min, t; 

            if(iterator.Count == 0)
            {
                return ""; 
            }

            try
            { 

                iterator.MoveNext(); 
                min = XmlConvert.ToTimeSpan(iterator.Current.Value);
			
                while(iterator.MoveNext())
                {
                    t = XmlConvert.ToTimeSpan(iterator.Current.Value);
                    min = (t < min)? t : min; 
                }
				
            }
            catch(FormatException)
            {
                return ""; 
            }		

            return XmlConvert.ToString(min); 
        }

		
        /// <summary>
        /// Implements the following function 
        ///    string date:max(node-set)
        /// </summary>
        /// <param name="iterator"></param>
        /// <returns></returns>
        /// <remarks>THIS FUNCTION IS NOT PART OF EXSLT!!!</remarks>
        public string max(XPathNodeIterator iterator)
        {

            TimeSpan max, t; 

            if(iterator.Count == 0)
            {
                return ""; 
            }
	
            try
            { 

                iterator.MoveNext(); 
                max = XmlConvert.ToTimeSpan(iterator.Current.Value);

			
                while(iterator.MoveNext())
                {
                    t = XmlConvert.ToTimeSpan(iterator.Current.Value);
                    max = (t > max)? t : max; 
                }
				
            }
            catch(FormatException)
            {
                return ""; 
            }		

            return XmlConvert.ToString(max); 
        }
    }
}
