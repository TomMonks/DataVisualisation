using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DataVisualisation
{

    /// <summary>
    /// A normal datagrid preconfigured for displaying the statistics
    /// </summary>
    public class StatisticsDataGridView : DataGridView 
    {
        private DataGridViewTextBoxColumn Statistic;
        //private System.ComponentModel.IContainer components;
        private DataGridViewTextBoxColumn Value;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Statistic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // Statistic
            // 
            dataGridViewCellStyle9.NullValue = null;
            this.Statistic.DefaultCellStyle = dataGridViewCellStyle9;
            this.Statistic.HeaderText = "Statistic";
            this.Statistic.Name = "Statistic";
            this.Statistic.Width = 69;
            // 
            // Value
            // 
            dataGridViewCellStyle10.Format = "N2";
            this.Value.DefaultCellStyle = dataGridViewCellStyle10;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 59;
            // 
            // StatisticsDataGridView
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Statistic,
            this.Value});
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }


        /// <summary>
        /// Set the visualisiation object for the datagrid
        /// </summary>
        /// <param name="visualiser"></param>
        public void SetStatisticsVisualiser(BasicStatisticsVisualiser visualiser)
        {

            this.DataSource = visualiser.Visualisation.Select(x => new
            {
                Statistics = x.Key,
                Value = x.Value
            }).ToList();

            //seems to reset this after data is loaded?  So reset here.
            this.Columns[1].DefaultCellStyle.Format = "N2";
        }
    }
}
