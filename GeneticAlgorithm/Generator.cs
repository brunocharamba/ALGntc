using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static GeneticAlgorithm.GeneticAlgorithm;

namespace GeneticAlgorithm
{
    public partial class Generator : Form
    {
        //thread that will run the algorithm in background
        Thread algoThread; 

        //parameters for values obtained from textboxes and comboboxes
        int vPopulation;
        int vChromossomes;
        double vMin;
        double vMax;
        double vMutation;
        SelectionType vST;
        FitnessFunction vFF;
        int vIterations;

        /// <summary>
        /// Constructor
        /// </summary>
        public Generator()
        {
            InitializeComponent();

            //set culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //firts load of the graph basics
            LoadGraph();
            //load information of selection and fitness to the comboboxes
            LoadComboBoxes();
        }

        /// <summary>
        /// Run the algorithm in a second and background thread
        /// </summary>
        public void RunAlgorithm()
        {
            //create thread
            algoThread = new Thread(() => RunGeneticAlgorithm(vPopulation, vChromossomes, vIterations, vMutation, vMin, vMax, vST, vFF, this));
            //set thread to run in background
            Thread.CurrentThread.IsBackground = true;
            //run thread
            algoThread.Start();
        }

        /// <summary>
        /// Update graph method called in the background thread
        /// </summary>
        /// <param name="i">X value</param>
        /// <param name="fitness">Y value</param>
        public void UpdateGraph(int i, double fitness)
        {
            //refresh grid config
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.Maximum = fitness * 70; //approximate y scale as the values increase/decrease for better visualization

            //add point to graph: i = iteration, fitness = y value
            chart1.Series[0].Points.AddXY(i, fitness);
        }

        #region controls methods

        /// <summary>
        /// Load basic information of the Chart
        /// </summary>
        private void LoadGraph()
        {
            //create and customize the chart area
            ChartArea ca = new ChartArea();
            //ca.AxisX.Interval = 1000;
            ca.AxisX.Minimum = 0;
            ca.AxisX.IsStartedFromZero = true;
            ca.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            ca.AxisX.IntervalType = DateTimeIntervalType.Number;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisX.MinorGrid.Enabled = false;

            ca.AxisY.LabelStyle.Format = "{0:0.##E+00}";
            ca.AxisY.Minimum = 0;
            ca.AxisY.MajorGrid.Enabled = false;
            ca.AxisY.MinorGrid.Enabled = false;


            //ca.InnerPlotPosition.X = 0;
            //ca.InnerPlotPosition.Y = 0;

            // Height and width are in percentage (%)
            //ca.InnerPlotPosition.Height = 80;
            //ca.InnerPlotPosition.Width = 80;

            //create series as a line chart type
            Series s = new Series()
            {
                ChartType = SeriesChartType.Line,
                LegendText = "Fitness",
                Color = Color.Red
            };

            //add series and chart area to the chart
            chart1.ChartAreas.Add(ca);
            chart1.Series.Add(s);

        }

        /// <summary>
        /// Load fitness functions and selection types in to comboboxes
        /// </summary>
        private void LoadComboBoxes()
        {
            //Selection Type
            foreach (var item in Enum.GetValues(typeof(SelectionType)))
            {
                cbSelection.Items.Add(item);
            }
            cbSelection.SelectedIndex = 0; //select the first entry as default value

            //Fitness Function
            foreach (var item in Enum.GetValues(typeof(FitnessFunction)))
            {
                cbFitness.Items.Add(item);
            }
            cbFitness.SelectedIndex = 0; //select the first entry as default value
        }

        /// <summary>
        /// Enable or disable all text boxes and the run button
        /// </summary>
        /// <param name="enable">enable or disable</param>
        public void EnableControls(bool enable)
        {
            tbPopulation.Enabled = enable;
            tbChromossomes.Enabled = enable;
            tbIterations.Enabled = enable;
            tbMin.Enabled = enable;
            tbMax.Enabled = enable;
            tbMutation.Enabled = enable;
            cbSelection.Enabled = enable;
            cbFitness.Enabled = enable;
            bRun.Enabled = enable;
        }

        /// <summary>
        /// Enable or disable stop button
        /// </summary>
        /// <param name="enable">enable or disable</param>
        public void EnableStopButton(bool enable)
        {
            bStop.Enabled = enable;
            pbAlgo.Visible = enable;
        }

        /// <summary>
        /// Refresh UI information
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="fitness"></param>
        public void UpdateResultsLabel(int iteration, double fitness)
        {
            tbFitness.Text = "" + fitness;
            tbCurrentIteration.Text = "" + iteration;
            int percent = (iteration * 100 / vIterations); 
            pbAlgo.Value = percent;
        }

        #endregion

        #region click events

        /// <summary>
        /// Click event for "RUN" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bRun_Click(object sender, EventArgs e)
        {
            //clear graph
            chart1.Series[0].Points.Clear();

            //disabling boxes and run button
            EnableControls(false);

            //enabling stop button
            EnableStopButton(true);

            //getting values from combo boxes and text boxes
            vPopulation = Int32.TryParse(tbPopulation.Text, out vPopulation) ? vPopulation : -1;
            vChromossomes = Int32.TryParse(tbChromossomes.Text, out vChromossomes) ? vChromossomes : -1;
            vIterations = Int32.TryParse(tbIterations.Text, out vIterations) ? vIterations : -1;
            vMin = Double.TryParse(tbMin.Text, out vMin) ? vMin : -1;
            vMax = Double.TryParse(tbMax.Text, out vMax) ? vMax : -1;
            vMutation = Double.TryParse(tbMutation.Text, out vMutation) ? vMutation : -1;
            vST = (SelectionType)Enum.Parse(typeof(SelectionType), cbSelection.SelectedItem.ToString());
            vFF = (FitnessFunction)Enum.Parse(typeof(FitnessFunction), cbFitness.SelectedItem.ToString());

            //checking for errors
            if (vPopulation <= 0 || vPopulation % 2 != 0) MessageBox.Show("Population must be higher than 0 and an even number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            else if (vChromossomes <= 0) MessageBox.Show("Chromossomes must be higher than 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            else if (vIterations <= 0) MessageBox.Show("Iterations must be higher than 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            else if (vMutation < 0 || vMutation > 1) MessageBox.Show("Mutation must be between 0 and 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            else RunAlgorithm();

        }

        /// <summary>
        /// Click event for "STOP" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bStop_Click(object sender, EventArgs e)
        {
            //cancel algorithm
            algoThread.Abort();

            //refresh boxes and button
            EnableControls(true);
            EnableStopButton(false);
        }

        #endregion
    }
}
