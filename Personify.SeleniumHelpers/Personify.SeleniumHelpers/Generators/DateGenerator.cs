using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Generators
{
    public class DateGenerator
    {
        /// <summary>
        /// Returns a random DateTime()
        /// </summary>
        /// <returns>DateTime()</returns>
        public static DateTime GetRandomDate()
        {
            return RandomDayFunc().Invoke();
        }
        /// <summary>
        /// Returns a future DateTime()
        /// </summary>
        /// <returns>DateTime()</returns>
        public static DateTime GetRandomFutureDate()
        {
            return RandomFutureDayFunc().Invoke();
        }

        #region private methods
        private static Func<DateTime> RandomDayFunc()
        {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            return () => start.AddDays(gen.Next(range));
        }
        private static Func<DateTime> RandomFutureDayFunc()
        {
            DateTime start = DateTime.Today;
            Random gen = new Random();
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            return () => start.AddDays(gen.Next(range));
        }
        #endregion
    }
}
