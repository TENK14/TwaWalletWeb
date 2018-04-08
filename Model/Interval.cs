using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwaWallet.Model
{
    /// <summary>
    /// Slouží pro pravidelné platby
    /// </summary>
    public class Interval : BaseEntity,IEntity
    {
        private const string TAG = "X:" + nameof(Interval);

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string IntervalId { get; set; }

        //public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// YYMMDD
        /// </summary>
        //[StringLength(6)]
        [Required]
        public string IntervalCode { get; set; }

        public DateTime BeforeDateTime(DateTime startDateTime)
        {
            //Log.Debug(TAG, $"{nameof(NextDateTime)} - {nameof(startDateTime)}:{startDateTime.ToShortDateString()}");

            DateTime result = startDateTime;

            try
            {
                int years, months, days;

                if (int.TryParse(IntervalCode.Substring(0, 2), out years))
                {
                    result = result.AddYears(-years);
                }
                else
                {
                    throw new FormatException("Years couldn't be parsed.");
                }

                if (int.TryParse(IntervalCode.Substring(2, 2), out months))
                {
                    result = result.AddMonths(-months);
                }
                else
                {
                    throw new FormatException("Months couldn't be parsed.");
                }

                if (int.TryParse(IntervalCode.Substring(4, 2), out days))
                {
                    result = result.AddDays(-days);
                }
                else
                {
                    throw new FormatException("Days couldn't be parsed.");
                }
            }
            catch (Exception ex)
            {
                //Log.Error(TAG, nameof(NextDateTime), ex.Message);
                throw;
            }

            return result;
        }

        public DateTime NextDateTime(DateTime startDateTime)
        {
            //Log.Debug(TAG, $"{nameof(NextDateTime)} - {nameof(startDateTime)}:{startDateTime.ToShortDateString()}");

            DateTime result = startDateTime;

            try
            {
                int years, months, days;

                if (int.TryParse(IntervalCode.Substring(0, 2), out years))
                {
                    result = result.AddYears(years);
                }
                else
                {
                    throw new FormatException("Years couldn't be parsed.");
                }

                if (int.TryParse(IntervalCode.Substring(2, 2), out months))
                {
                    result = result.AddMonths(months);
                }
                else
                {
                    throw new FormatException("Months couldn't be parsed.");
                }

                if (int.TryParse(IntervalCode.Substring(4, 2), out days))
                {
                    result = result.AddDays(days);
                }
                else
                {
                    throw new FormatException("Days couldn't be parsed.");
                }
            }
            catch (Exception ex)
            {
                //Log.Error(TAG, nameof(NextDateTime), ex.Message);
                throw;
            }

            return result;
        }

        public override string ToString()
        {
            //var logger = services.GetRequiredService<ILogger<Program>>();
            //logger.LogError(ex, "An error occurred seeding the DB.");
            //Log.Debug(TAG, nameof(ToString));

            return Description;
        }
    }
}
