
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.DirectoryServices.AccountManagement;
using System.Management;
using System.Linq;
using System.Diagnostics;


namespace SystemMonitor
{
    /// <summary>
    /// Summary description for monitor.
    /// </summary>
    public class monitor : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelCpu;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelMemP;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelMemV;
		private System.Windows.Forms.Label labelDiskR;
		private System.Windows.Forms.Label labelDiskW;
		private System.Windows.Forms.Label labelNetO;
		private System.Windows.Forms.Label labelNetI;
		private System.Windows.Forms.Label labelNames;
		private System.Windows.Forms.TextBox textBoxProcessor;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labelModel;

		private SystemMonitor.DataBar dataBarCPU;
		private SystemMonitor.DataBar dataBarMemP;
		private SystemMonitor.DataBar dataBarMemV;
		private SystemMonitor.DataChart dataChartDiskR;
		private SystemMonitor.DataChart dataChartDiskW;
		private SystemMonitor.DataChart dataChartNetI;
		private SystemMonitor.DataChart dataChartNetO;
		private SystemMonitor.DataChart dataChartCPU;
		private SystemMonitor.DataChart dataChartMem;

		private ArrayList  _listDiskR = new ArrayList();
		private ArrayList  _listDiskW = new ArrayList();
		private ArrayList  _listNetI = new ArrayList();
		private ArrayList  _listNetO = new ArrayList();

		private ArrayList  _listCPU = new ArrayList();
		private ArrayList  _listMem = new ArrayList();

		private SystemData sd = new SystemData();
		private Size _sizeOrg;
        private Size _size;
        double cpu, mem_v,mem_p,disk_r,disk_w,net_i,net_o;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;                                            
        public OracleConnection con;
        private int count=0;
        Stopwatch stopWatch = new Stopwatch();
        
        public int _custom_runtime;
        public double _percentrun=0;
        public int _seconds;
       
        public int _percent;

        public InitialForm ParentFormObject { get;set; } // to access parent form object

        public monitor()
		{
            
            InitializeComponent();
            
            
            _size = Size;
			_sizeOrg = Size;

			labelModel.Text = sd.QueryComputerSystem("manufacturer") +", " + sd.QueryComputerSystem("model");
			textBoxProcessor.Text = "Processor: "+sd.QueryEnvironment("%PROCESSOR_IDENTIFIER%");
			labelNames.Text = "User: " +sd.QueryComputerSystem("username");  //sd.LogicalDisk();
            con = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");

            
            // Get the elapsed time as a TimeSpan value.
            
            UpdateData();

            con.Open();
            timer.Interval = 10000;
            
            timer.Start();
            
            
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(monitor));
            this.labelNetI = new System.Windows.Forms.Label();
            this.labelNetO = new System.Windows.Forms.Label();
            this.labelDiskW = new System.Windows.Forms.Label();
            this.labelDiskR = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelMemV = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelMemP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCpu = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelNames = new System.Windows.Forms.Label();
            this.textBoxProcessor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dataChartMem = new SystemMonitor.DataChart();
            this.dataChartCPU = new SystemMonitor.DataChart();
            this.dataChartNetI = new SystemMonitor.DataChart();
            this.dataChartNetO = new SystemMonitor.DataChart();
            this.dataChartDiskW = new SystemMonitor.DataChart();
            this.dataChartDiskR = new SystemMonitor.DataChart();
            this.dataBarMemV = new SystemMonitor.DataBar();
            this.dataBarMemP = new SystemMonitor.DataBar();
            this.dataBarCPU = new SystemMonitor.DataBar();
            this.SuspendLayout();
            // 
            // labelNetI
            // 
            this.labelNetI.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.labelNetI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelNetI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelNetI.Location = new System.Drawing.Point(366, 437);
            this.labelNetI.Name = "labelNetI";
            this.labelNetI.Size = new System.Drawing.Size(160, 16);
            this.labelNetI.TabIndex = 18;
            this.labelNetI.Text = "Net I";
            // 
            // labelNetO
            // 
            this.labelNetO.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.labelNetO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelNetO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelNetO.Location = new System.Drawing.Point(366, 407);
            this.labelNetO.Name = "labelNetO";
            this.labelNetO.Size = new System.Drawing.Size(160, 16);
            this.labelNetO.TabIndex = 17;
            this.labelNetO.Text = "Net O";
            // 
            // labelDiskW
            // 
            this.labelDiskW.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.labelDiskW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelDiskW.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDiskW.Location = new System.Drawing.Point(366, 377);
            this.labelDiskW.Name = "labelDiskW";
            this.labelDiskW.Size = new System.Drawing.Size(160, 16);
            this.labelDiskW.TabIndex = 16;
            this.labelDiskW.Text = "Disk W";
            // 
            // labelDiskR
            // 
            this.labelDiskR.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.labelDiskR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelDiskR.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDiskR.Location = new System.Drawing.Point(366, 347);
            this.labelDiskR.Name = "labelDiskR";
            this.labelDiskR.Size = new System.Drawing.Size(160, 16);
            this.labelDiskR.TabIndex = 15;
            this.labelDiskR.Text = "Disk R";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(11, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Mem V";
            // 
            // labelMemV
            // 
            this.labelMemV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelMemV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelMemV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMemV.Location = new System.Drawing.Point(182, 113);
            this.labelMemV.Name = "labelMemV";
            this.labelMemV.Size = new System.Drawing.Size(158, 16);
            this.labelMemV.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(11, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Mem P";
            // 
            // labelMemP
            // 
            this.labelMemP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelMemP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelMemP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMemP.Location = new System.Drawing.Point(182, 137);
            this.labelMemP.Name = "labelMemP";
            this.labelMemP.Size = new System.Drawing.Size(158, 16);
            this.labelMemP.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(366, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "CPU";
            // 
            // labelCpu
            // 
            this.labelCpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelCpu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelCpu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCpu.Location = new System.Drawing.Point(516, 139);
            this.labelCpu.Name = "labelCpu";
            this.labelCpu.Size = new System.Drawing.Size(184, 16);
            this.labelCpu.TabIndex = 10;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(366, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 18);
            this.label3.TabIndex = 26;
            this.label3.Text = "CPU Usage History";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(12, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 24);
            this.label5.TabIndex = 28;
            this.label5.Text = "Memory Usage History";
            // 
            // labelModel
            // 
            this.labelModel.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.labelModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelModel.Location = new System.Drawing.Point(10, 20);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(708, 16);
            this.labelModel.TabIndex = 30;
            this.labelModel.Text = "Motherboard:";
            this.labelModel.Click += new System.EventHandler(this.labelModel_Click);
            // 
            // labelNames
            // 
            this.labelNames.BackColor = System.Drawing.Color.Transparent;
            this.labelNames.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.labelNames.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.labelNames.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelNames.Location = new System.Drawing.Point(11, 56);
            this.labelNames.Name = "labelNames";
            this.labelNames.Size = new System.Drawing.Size(328, 16);
            this.labelNames.TabIndex = 33;
            this.labelNames.Text = "User:";
            // 
            // textBoxProcessor
            // 
            this.textBoxProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.textBoxProcessor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxProcessor.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textBoxProcessor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.textBoxProcessor.Location = new System.Drawing.Point(11, 39);
            this.textBoxProcessor.Multiline = true;
            this.textBoxProcessor.Name = "textBoxProcessor";
            this.textBoxProcessor.ReadOnly = true;
            this.textBoxProcessor.Size = new System.Drawing.Size(208, 20);
            this.textBoxProcessor.TabIndex = 35;
            this.textBoxProcessor.Text = "Processor:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Courier New", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(8, 323);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 14);
            this.label6.TabIndex = 38;
            this.label6.Text = "HDD S.No";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Courier New", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(119, 323);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(257, 20);
            this.label7.TabIndex = 39;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Courier New", 9F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(127, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Courier New", 9F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(8, 347);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 15);
            this.label10.TabIndex = 40;
            this.label10.Text = "Processor ID";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Courier New", 9F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(8, 369);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 15);
            this.label11.TabIndex = 42;
            this.label11.Text = "Current Clock Speed";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Courier New", 9F);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(127, 369);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 15);
            this.label12.TabIndex = 43;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Courier New", 9F);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(49, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(594, 20);
            this.label13.TabIndex = 44;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Courier New", 9F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(12, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 20);
            this.label14.TabIndex = 45;
            this.label14.Text = "OS:";
            // 
            // dataChartMem
            // 
            this.dataChartMem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataChartMem.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.dataChartMem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataChartMem.ChartType = SystemMonitor.ChartType.Line;
            this.dataChartMem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataChartMem.ForeColor = System.Drawing.Color.Transparent;
            this.dataChartMem.GridColor = System.Drawing.Color.Gray;
            this.dataChartMem.GridPixels = 15;
            this.dataChartMem.InitialHeight = 100;
            this.dataChartMem.LineColor = System.Drawing.Color.Red;
            this.dataChartMem.Location = new System.Drawing.Point(11, 204);
            this.dataChartMem.Name = "dataChartMem";
            this.dataChartMem.Size = new System.Drawing.Size(329, 107);
            this.dataChartMem.TabIndex = 29;
            // 
            // dataChartCPU
            // 
            this.dataChartCPU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataChartCPU.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.dataChartCPU.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataChartCPU.ChartType = SystemMonitor.ChartType.Line;
            this.dataChartCPU.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataChartCPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.dataChartCPU.ForeColor = System.Drawing.Color.Transparent;
            this.dataChartCPU.GridColor = System.Drawing.Color.Gray;
            this.dataChartCPU.GridPixels = 15;
            this.dataChartCPU.InitialHeight = 100;
            this.dataChartCPU.LineColor = System.Drawing.Color.Red;
            this.dataChartCPU.Location = new System.Drawing.Point(369, 204);
            this.dataChartCPU.Name = "dataChartCPU";
            this.dataChartCPU.Size = new System.Drawing.Size(331, 107);
            this.dataChartCPU.TabIndex = 27;
            // 
            // dataChartNetI
            // 
            this.dataChartNetI.BackColor = System.Drawing.Color.Transparent;
            this.dataChartNetI.ChartType = SystemMonitor.ChartType.Stick;
            this.dataChartNetI.GridColor = System.Drawing.Color.Yellow;
            this.dataChartNetI.GridPixels = 0;
            this.dataChartNetI.InitialHeight = 1000;
            this.dataChartNetI.LineColor = System.Drawing.Color.Red;
            this.dataChartNetI.Location = new System.Drawing.Point(542, 429);
            this.dataChartNetI.Name = "dataChartNetI";
            this.dataChartNetI.Size = new System.Drawing.Size(160, 24);
            this.dataChartNetI.TabIndex = 25;
            // 
            // dataChartNetO
            // 
            this.dataChartNetO.BackColor = System.Drawing.Color.Transparent;
            this.dataChartNetO.ChartType = SystemMonitor.ChartType.Stick;
            this.dataChartNetO.GridColor = System.Drawing.Color.Yellow;
            this.dataChartNetO.GridPixels = 0;
            this.dataChartNetO.InitialHeight = 1000;
            this.dataChartNetO.LineColor = System.Drawing.Color.Red;
            this.dataChartNetO.Location = new System.Drawing.Point(542, 399);
            this.dataChartNetO.Name = "dataChartNetO";
            this.dataChartNetO.Size = new System.Drawing.Size(160, 24);
            this.dataChartNetO.TabIndex = 24;
            // 
            // dataChartDiskW
            // 
            this.dataChartDiskW.BackColor = System.Drawing.Color.Transparent;
            this.dataChartDiskW.ChartType = SystemMonitor.ChartType.Stick;
            this.dataChartDiskW.GridColor = System.Drawing.Color.Gold;
            this.dataChartDiskW.GridPixels = 0;
            this.dataChartDiskW.InitialHeight = 100000;
            this.dataChartDiskW.LineColor = System.Drawing.Color.Red;
            this.dataChartDiskW.Location = new System.Drawing.Point(542, 369);
            this.dataChartDiskW.Name = "dataChartDiskW";
            this.dataChartDiskW.Size = new System.Drawing.Size(160, 24);
            this.dataChartDiskW.TabIndex = 23;
            // 
            // dataChartDiskR
            // 
            this.dataChartDiskR.BackColor = System.Drawing.Color.Transparent;
            this.dataChartDiskR.ChartType = SystemMonitor.ChartType.Stick;
            this.dataChartDiskR.GridColor = System.Drawing.Color.Gold;
            this.dataChartDiskR.GridPixels = 0;
            this.dataChartDiskR.InitialHeight = 100000;
            this.dataChartDiskR.LineColor = System.Drawing.Color.Red;
            this.dataChartDiskR.Location = new System.Drawing.Point(542, 339);
            this.dataChartDiskR.Name = "dataChartDiskR";
            this.dataChartDiskR.Size = new System.Drawing.Size(160, 24);
            this.dataChartDiskR.TabIndex = 22;
            // 
            // dataBarMemV
            // 
            this.dataBarMemV.BackColor = System.Drawing.Color.Transparent;
            this.dataBarMemV.BarColor = System.Drawing.Color.Red;
            this.dataBarMemV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.dataBarMemV.Location = new System.Drawing.Point(78, 113);
            this.dataBarMemV.Name = "dataBarMemV";
            this.dataBarMemV.Size = new System.Drawing.Size(96, 16);
            this.dataBarMemV.TabIndex = 21;
            this.dataBarMemV.Value = 0;
            // 
            // dataBarMemP
            // 
            this.dataBarMemP.BackColor = System.Drawing.Color.Transparent;
            this.dataBarMemP.BarColor = System.Drawing.Color.Red;
            this.dataBarMemP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.dataBarMemP.Location = new System.Drawing.Point(78, 137);
            this.dataBarMemP.Name = "dataBarMemP";
            this.dataBarMemP.Size = new System.Drawing.Size(96, 16);
            this.dataBarMemP.TabIndex = 20;
            this.dataBarMemP.Value = 0;
            // 
            // dataBarCPU
            // 
            this.dataBarCPU.BackColor = System.Drawing.Color.Transparent;
            this.dataBarCPU.BarColor = System.Drawing.Color.Red;
            this.dataBarCPU.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.dataBarCPU.Location = new System.Drawing.Point(414, 137);
            this.dataBarCPU.Name = "dataBarCPU";
            this.dataBarCPU.Size = new System.Drawing.Size(96, 16);
            this.dataBarCPU.TabIndex = 19;
            this.dataBarCPU.Value = 0;
            // 
            // monitor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(732, 468);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxProcessor);
            this.Controls.Add(this.labelNames);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.dataChartMem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataChartCPU);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataChartNetI);
            this.Controls.Add(this.dataChartNetO);
            this.Controls.Add(this.dataChartDiskW);
            this.Controls.Add(this.dataChartDiskR);
            this.Controls.Add(this.dataBarMemV);
            this.Controls.Add(this.dataBarMemP);
            this.Controls.Add(this.dataBarCPU);
            this.Controls.Add(this.labelNetI);
            this.Controls.Add(this.labelNetO);
            this.Controls.Add(this.labelDiskW);
            this.Controls.Add(this.labelDiskR);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelMemV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelMemP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCpu);
            this.Font = new System.Drawing.Font("Courier New", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(400, 0);
            this.Name = "monitor";
            this.Opacity = 0.9D;
            this.Text = "scan & monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			timer.Stop();
            con.Dispose();
            con.Close();
            

        }

        
        private void timer_Tick(object sender, System.EventArgs e)
		{
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes,ts.Seconds,ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.Write("min elapsed={0}", ts.Minutes);
            
            string logged_time = (GetLastLoginToMachine(Environment.MachineName, Environment.UserName)).ToString();
            UpdateData();
            //Console.WriteLine(logged_time);
            _custom_runtime = 2;         // allocate a time in minutes for the process to run.
            _seconds = ts.Seconds + ts.Minutes * 60;

            _percentrun = ((double)_seconds / ((double)_custom_runtime * 60)) * 100;
            Console.WriteLine("percent run={0} seconds={1}", _percentrun, _seconds);
            
            ParentFormObject.UpdateProgress(_percentrun);

            stopWatch.Start();
            
            if (ts.Minutes>= _custom_runtime)
            {
                stopWatch.Stop();
                timer.Stop();
                con.Dispose();
                con.Close();
                //i.Show();
                Dispose();
            }
            else
            {
                string sql = "insert into extract values(to_char(sysdate,'DD/MM/YY:HH:MI:SSAM')," + cpu + "," + mem_v + "," + mem_p + "," + disk_r + "," + disk_w + "," + net_i + "," + net_o + "," + "'" + count + "'" + "," + "'" + logged_time + "','" + Environment.MachineName + "'" + ")";
                Console.WriteLine(sql);
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Console.WriteLine("UserName: {0} Environment.MachineName={1}",Environment.UserName,Environment.MachineName);
            }
           
        }
        

       
        private void labelModel_Click(object sender, EventArgs e)
        {

        }
        

        public static DateTime? GetLastLoginToMachine(string machineName, string userName)
        {
            PrincipalContext pc = new PrincipalContext(ContextType.Machine, machineName);
            UserPrincipal uc = UserPrincipal.FindByIdentity(pc, userName);
            return uc.LastLogon;

        }

        private void UpdateData()
		{
			string s = sd.GetProcessorData();
			labelCpu.Text = s;
			double d = double.Parse(s.Substring(0, s.IndexOf("%")));
			dataBarCPU.Value = (int)d;

			dataChartCPU.UpdateChart(d);
            cpu = (int)d;

			s = sd.GetMemoryVData();
			labelMemV.Text = s;
			d = double.Parse(s.Substring(0, s.IndexOf("%")));
			dataBarMemV.Value = (int)d;
			dataChartMem.UpdateChart(d);
            mem_v = (int)d;

			s = sd.GetMemoryPData();
			labelMemP.Text = s;
			d=double.Parse(s.Substring(0, s.IndexOf("%")));
            dataBarMemP.Value = (int)d;
            mem_p = (int)d;

            d = sd.GetDiskData(SystemData.DiskData.Read);
			labelDiskR.Text = "Disk R (" + sd.FormatBytes(d) + "/s)";
			dataChartDiskR.UpdateChart(d);
            disk_r = (int)(d / 1024);

			d = sd.GetDiskData(SystemData.DiskData.Write); 
			labelDiskW.Text = "Disk W (" + sd.FormatBytes(d) + "/s)";
			dataChartDiskW.UpdateChart(d);
            disk_w = (int)(d / 1024);

			d = sd.GetNetData(SystemData.NetData.Received);
			labelNetI.Text = "Net I (" + sd.FormatBytes(d) + "/s)";
			dataChartNetI.UpdateChart(d);
            net_i = (int)(d / 1024);

			d = sd.GetNetData(SystemData.NetData.Sent);
			labelNetO.Text = "Net O (" + sd.FormatBytes(d) + "/s)";
			dataChartNetO.UpdateChart(d);
            net_o = (int)(d / 1024);

            label12.Text = HardwareInfo.GetCPUCurrentClockSpeed().ToString();

            count = 0;
            ManagementObjectSearcher usersSearcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_UserAccount");
            ManagementObjectCollection users = usersSearcher.Get();

            var localUsers = users.Cast<ManagementObject>().Where(
                u => (bool)u["LocalAccount"] == true &&
                     (bool)u["Disabled"] == false &&
                     (bool)u["Lockout"] == false &&
                     int.Parse(u["SIDType"].ToString()) == 1 &&
                     u["Name"].ToString() != "HomeGroupUser$");

            foreach (ManagementObject user in users)
            {
                Console.WriteLine("Name: " + user["Name"].ToString());
                count++;
            }
        }
       


		protected override void OnResize(EventArgs e)
		{
			if (0==_sizeOrg.Width || 0== _sizeOrg.Height) return;

			if (Size.Width != _size.Width && Size.Width > _sizeOrg.Width)		
			{
				int xChange = Size.Width - _size.Width;	  // Client window

				// Adjust three bars
				int newWidth = dataBarCPU.Size.Width +xChange;
				int labelStart = labelCpu.Location.X + xChange;

				dataBarCPU.Size = new Size(newWidth, dataBarCPU.Size.Height);
				labelCpu.Location = new Point(labelStart, labelCpu.Location.Y);

				dataBarMemV.Size = new Size(newWidth, dataBarCPU.Size.Height);
				labelMemV.Location = new Point(labelStart, labelMemV.Location.Y);

				dataBarMemP.Size = new Size(newWidth, dataBarCPU.Size.Height);
				labelMemP.Location = new Point(labelStart, labelMemP.Location.Y);
					
				// Resize four charts
				int margin = 8;
				Rectangle rt = this.ClientRectangle;

				newWidth = (rt.Width - 3*margin)/2;
				labelStart = newWidth +2*margin;

				dataChartDiskR.Size = new Size(newWidth, dataChartDiskR.Size.Height);
				labelDiskW.Location = new Point(labelStart, labelDiskW.Location.Y);
				dataChartDiskW.Location = new Point(labelStart, dataChartDiskW.Location.Y);
				dataChartDiskW.Size = new Size(newWidth, dataChartDiskW.Size.Height);

				dataChartNetI.Size = new Size(newWidth, dataChartNetI.Size.Height);
				labelNetO.Location = new Point(labelStart, labelNetO.Location.Y);
				dataChartNetO.Location = new Point(labelStart, dataChartNetO.Location.Y);
				dataChartNetO.Size = new Size(newWidth, dataChartNetO.Size.Height);

				
//				dataChartCPU.Size = new Size(rt.Width - 2*margin, dataChartCPU.Size.Height);
//				dataChartMem.Size = new Size(rt.Width - 2*margin, dataChartMem.Size.Height);

				_size = Size;
			}

			Size = new Size(Size.Width, _sizeOrg.Height);
		}

        public void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(600, 100);
            label7 .Text = HardwareInfo.GetHDDSerialNo();
            label8 .Text = HardwareInfo.GetProcessorId();
            label12.Text= HardwareInfo.GetCPUCurrentClockSpeed().ToString() ;
            label13.Text= HardwareInfo.GetOSInformation(); 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            timer.Stop();
            con.Dispose();
            con.Close();
            //i.Show();
            Dispose();
           
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
