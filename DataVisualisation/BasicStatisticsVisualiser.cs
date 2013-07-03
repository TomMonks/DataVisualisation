using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Statistics.Descriptive;

namespace DataVisualisation
{
    /// <summary>
    /// Helper class to display statistics to user. Overloaded constructor takes a BasicStatistics object and inverts it. 
    /// Useful for binding to a datagrid
    /// </summary>
    public class BasicStatisticsVisualiser
    {
        #region Constants

        protected const string SAMPLE_SIZE = "N";
        protected const string MEAN = "Mean";
        protected const string STANDARD_DEV = "Std Dev";
        protected const string STANDARD_ERROR = "Std Error";
        protected const string LOWER_CONFIDENCE_INTERVAL = "95% LCI";
        protected const string UPPER_CONFIDENCE_INTERVAL = "95% UCI";
        protected const string MEDIAN = "Median";
        protected const string IQR = "IQR";
        protected const string LOWER_QUARTILE = "25th Percentile";
        protected const string UPPER_QUARTILE = "75th Percentile";
        protected const string FIFTH_PERCENTILE = "5th Percentile";
        protected const string NINTYFIFTH_PERCENTILE = "95th Percentile";
        protected const string MAXIMUM = "Max";
        protected const string MINIMUM = "Min";

        #endregion

        protected Dictionary<string, double> statistics;
        

        /// <summary>
        /// List of statistics stored
        /// </summary>
        public List<string> Statistic 
        {
            get
            {
                return this.statistics.Keys.ToList<string>();
            }
        }


        /// <summary>
        /// List of statistic values stored
        /// </summary>
        public List<double> Values
        {
            get
            {
                return this.statistics.Values.ToList<double>();
            }
        }

        public Dictionary<string, double> Visualisation
        {
            get
            {
                return this.statistics;
            }
        }
      
        /// <summary>
        /// Default constructor.  
        /// </summary>
        public BasicStatisticsVisualiser()
        {
            this.statistics = new Dictionary<string, double>();
        }

        /// <summary>
        /// Overloaded constructor.  Inverts a basic statistics object
        /// </summary>
        /// <param name="statistics">Contains the basic statistics</param>
        public BasicStatisticsVisualiser(BasicStatistics statistics)
        {

            this.statistics = new Dictionary<string, double>();

            AddStatistic(SAMPLE_SIZE, statistics.N);
            AddStatistic(MEAN, statistics.Mean);
            AddStatistic(STANDARD_DEV, statistics.StdDev);
            AddConfidenceIntervalStatisics(statistics);
            AddStatistic(MEDIAN, statistics.Median);
            AddStatistic(IQR, statistics.IQR);
            AddStatistic(LOWER_QUARTILE, statistics.Percentile(0.25));
            AddStatistic(UPPER_QUARTILE, statistics.Percentile(0.75));
            AddStatistic(FIFTH_PERCENTILE, statistics.Percentile(0.05));
            AddStatistic(NINTYFIFTH_PERCENTILE, statistics.Percentile(0.95));
            AddStatistic(MINIMUM, statistics.Minimum);
            AddStatistic(MAXIMUM, statistics.Maximum);
        }

        public void AddStatistic(string key, double value)
        {
            this.statistics.Add(key, value);
        }


        protected void AddConfidenceIntervalStatisics(BasicStatistics statistics)
        {
            var ci = new ConfidenceIntervalStandardNormal(statistics);
            AddStatistic(STANDARD_ERROR, ci.StandardError);
            AddStatistic(LOWER_CONFIDENCE_INTERVAL, ci.LowerBound);
            AddStatistic(UPPER_CONFIDENCE_INTERVAL, ci.UpperBound);
        }
    }
}
