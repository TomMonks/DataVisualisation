using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataVisualisation
{
    public class CrossTabDataTable : DataTable 
    {
        protected const string EMPTY = "";
        protected const string ROWS = "Rows";
        protected const string COLS = "Cols";

        protected string summaryStatistic;
        protected string crossTabTitle;
        protected List<CrossTabResult> data;
        protected Dictionary<string, List<string>> headers;       //row and column headers
        

        public CrossTabDataTable(List<CrossTabResult> data, string crossTabTitle, string summaryStatistic = "Median")
        {
            this.summaryStatistic = summaryStatistic;
            this.data = data;
            this.crossTabTitle = crossTabTitle;

            headers = new Dictionary<string, List<string>>();
            headers.Add(COLS, GetDistinctCols(data));
            headers.Add(ROWS, GetDistinctRows(data));

            headers[COLS].Insert(0, crossTabTitle);

            CreateColumns(headers[COLS], crossTabTitle);
            CreateRows(headers[ROWS]);
            
            DisplaySummaryStatistic(summaryStatistic);
        }


        private List<string> GetDistinctRows(List<CrossTabResult> data)
        {

            List<string> distinct = data
                .GroupBy(x => x.RowSubgroup)
                .Select(y => y.First().RowSubgroup)
                .Where(z => z != EMPTY)
                .ToList();

            return distinct;
        }

        private List<string> GetDistinctCols(List<CrossTabResult> data)
        {

            List<string> distinct = data
                .GroupBy(x => x.ColSubgroup)
                .Select(y => y.First().ColSubgroup)
                .Where(z => z != EMPTY)
                .ToList();

            return distinct;

        }

        /// <summary>
        /// Create columns within the cross tab
        /// </summary>
        /// <param name="columnTitles">DataTable containing the column titles in the crosstab</param>
        /// <param name="crossTabTitle"></param>
        private void CreateColumns(List<string> titles, string crossTabTitle)
        {
            titles.ForEach(col => this.Columns.Add(col));
        }


        /// <summary>
        /// Create the cross tab rows
        /// </summary>
        /// <param name="rowTitles">DataTable containing the row titles</param>
        private void CreateRows(List<string> titles)
        {
            foreach (string row in titles)
            {
                if (EMPTY != row)
                {
                    DataRow toAdd = this.NewRow();
                    toAdd[0] = row;
                    this.Rows.Add(toAdd);
                }
            }
           
        }




        public void DisplaySummaryStatistic(string summaryStatistic)
        {
            foreach (DataRow row in this.Rows)
            {
                for (int col = 1; col <= headers[COLS].Count -1; col++)
                {
                    if ("" != headers[COLS][col])
                    {
                        Console.WriteLine("{0}, {1}", headers[COLS][col], row[0].ToString());

                        List<double> temp = SelectSubgroupResult(summaryStatistic, row, col);

                        row[col] = temp[0];

                    }
                }
            }
        }



        private List<double> SelectSubgroupResult(string summaryStatistic, DataRow row, int col)
        {
            List<double> temp = (from crossTabResult in data
                                 where crossTabResult.ColSubgroup == headers[COLS][col]
                                 && crossTabResult.RowSubgroup.Contains(row[0].ToString())
                                 select (double)crossTabResult.GetType().GetProperty(summaryStatistic).GetValue(crossTabResult)).ToList<double>();
            return temp;
        }
    }
}
