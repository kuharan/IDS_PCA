using Oracle.ManagedDataAccess.Client;
using RecursiveSearchCS;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Security.Permissions;
using MySql.Data.MySqlClient;



namespace SystemMonitor
{
    public partial class InitialForm : Form
    {
        public OracleConnection con3;
        public OracleConnection con2;
        private int count;
        private double mse;
        
        public InitialForm()
        {
            InitializeComponent();
            
        }
        public void UpdateProgress(double j)
        {
            // public method to invoke from the child form
            double _percentrun, _percent;
            _percentrun = j;
            if (_percentrun > 100){
                _percentrun = 100;
            }
            progressBar7.Value = (int)_percentrun;
            _percent = (int)(((double)progressBar7.Value / (double)progressBar7.Maximum) * 100);

            progressBar7.CreateGraphics().DrawString(_percent.ToString() + "%",
               new Font("Courier New", (float)10, FontStyle.Bold),
               Brushes.Black,
               new PointF(progressBar7.Width / 2 - 10, progressBar7.Height / 2 - 7)); //calling method to update the progressbar
        }


        static void Main()
        {
            //Application.EnableVisualStyles();
            Application.Run(new InitialForm());
            
        }

        private void InitialForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None ;
            this.Location = new Point(0, 100);
            //DateTime time = DateTime.Now;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void _Run()
        {
            string[] args = { "C:", "E:" };  //System.Environment.GetCommandLineArgs();

            // If a directory is not specified, exit program.
            if (args.Length != 2)
            {
                // Display the proper way to call the program.
                Console.WriteLine("Usage: Watcher.exe (directory)");
                //Console.ReadKey();
                return;
            }
            Console.WriteLine("\n############################" + args[1]);

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args[1];
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Watch all files.
            watcher.Filter = "*.txt";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
            while (Console.Read() != 'q') ;

        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            monitor frm = new monitor();
            frm.Show();
            this.Hide();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            
            con3 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            con3.Open();
            
            string sql = "select * from extract order by time";
            OracleCommand cmd = new OracleCommand(sql, con3);
            OracleDataReader r = cmd.ExecuteReader();
            count = 0;
           
            string[] time = new string[2000]; string[] cpu = new string[2000]; string[] mem_v = new string[2000]; string[] mem_p = new string[2000]; string[] disk_r = new string[2000]; string[] disk_w = new string[2000]; string[] net_i = new string[2000]; string[] net_o = new string[2000]; string[] log_user = new string[2000]; string[] log_time = new string[2000]; string[] machine_name = new string[2000];
            while (r.Read())
            {
                time[count] = r["TIME"].ToString();
                cpu[count] = r["CPU"].ToString();
                mem_v[count] = r["MEM_V"].ToString();
                mem_p[count] = r["MEM_P"].ToString();
                disk_r[count] = r["DISK_R"].ToString();
                disk_w[count] = r["DISK_W"].ToString();
                net_i[count] = r["NET_I"].ToString();
                net_o[count] = r["NET_O"].ToString();
                log_user[count] = r["LOGGED_USER"].ToString();
                log_time[count] = r["LOGGED_TIME"].ToString();
                machine_name[count] = r["MACHINE_NAME"].ToString();
                count++;
            }
           
            con3.Close();

            //calculating means 1

            double mean_cpu1, mean_mem_v1, mean_mem_p1, mean_disk_r1, mean_disk_w1, mean_net_i1, mean_net_o1, mean_log_user1;
            mean_cpu1 = mean_net_i1 = mean_mem_v1 = mean_mem_p1 = mean_disk_r1 = mean_disk_w1 = mean_net_o1 = mean_log_user1 = 0;

            
            for (int temp = 0; temp < count/2; temp++)
            {
                mean_cpu1 += double.Parse(cpu[temp]) / count/2;
                mean_mem_v1 += double.Parse(mem_v[temp]) / count/2;
                mean_mem_p1 += double.Parse(mem_p[temp]) / count/2;
                mean_disk_r1 += double.Parse(disk_r[temp]) / count/2;
                mean_disk_w1 += double.Parse(disk_w[temp]) / count/2;
                mean_net_i1 += double.Parse(net_i[temp]) / count/2;
                mean_net_o1 += double.Parse(net_o[temp]) / count/2;
                mean_log_user1 += double.Parse(log_user[temp]) / count/2;
            }
            Console.WriteLine("mean_cpu1={0}",mean_cpu1);
            
            double xcpu1, xmem_p1,xmem_v1,xdisk_r1,xdisk_w1,xnet_i1,xnet_o1,xlog_user1;
            xcpu1 = xmem_p1 = xmem_v1 = xdisk_r1 = xdisk_w1 = xnet_i1 = xnet_o1 = xlog_user1 = 0;

            for (int i = 0; i < count/2; i++)
            {
                xcpu1 += Math.Pow((double.Parse(cpu[i]) - mean_cpu1),2)/(count/2); 
                xmem_p1 += Math.Pow((double.Parse(mem_p[i]) - mean_mem_p1), 2) / (count/2);
                xmem_v1 += Math.Pow((double.Parse(mem_v[i]) - mean_mem_v1), 2) / (count/2);
                xdisk_r1 += Math.Pow((double.Parse(disk_r[i]) - mean_disk_r1), 2) / (count/2);
                xdisk_w1 += Math.Pow((double.Parse(disk_w[i]) - mean_disk_w1), 2) / (count/2);
                xnet_i1 += Math.Pow((double.Parse(net_i[i]) - mean_net_i1), 2) / (count/2);
                xnet_o1 += Math.Pow((double.Parse(net_o[i]) - mean_net_o1), 2) / (count/2);
                xlog_user1 += Math.Pow((double.Parse(log_user[i]) - mean_log_user1), 2) / (count/2);

            }


            double sigma_cpu1, sigma_mem_p1, sigma_mem_v1, sigma_disk_r1, sigma_disk_w1, sigma_net_i1, sigma_net_o1, sigma_log_user1;
            sigma_cpu1 = sigma_mem_p1 = sigma_mem_v1 = sigma_disk_r1 = sigma_disk_w1 = sigma_net_i1 = sigma_net_o1 = sigma_log_user1 = 0;

            sigma_cpu1 = Math.Sqrt(xcpu1);
            sigma_mem_p1 = Math.Sqrt(xmem_p1);
            sigma_mem_v1 = Math.Sqrt(xmem_v1);
            sigma_disk_r1 = Math.Sqrt(xdisk_r1);
            sigma_disk_w1 = Math.Sqrt(xdisk_w1);
            sigma_net_i1 = Math.Sqrt(xnet_i1);
            sigma_net_o1 = Math.Sqrt(xnet_o1);
            sigma_log_user1 = Math.Sqrt(xlog_user1);
            Console.WriteLine("\nsigma_cpu1 ={0}\nsigma_mem_p1 ={1}\nsigma_mem_v1 ={2}\nsigma_disk_r1 ={3}\nsigma_disk_w1 ={4}\nsigma_net_i1 ={5}\nsigma_net_o1 ={6}\nsigma_log_user1 ={7}\n", sigma_cpu1, sigma_mem_p1, sigma_mem_v1, sigma_disk_r1, sigma_disk_w1, sigma_net_i1, sigma_net_o1, sigma_log_user1);

            

            //-calculating means 2

            double mean_cpu2, mean_mem_v2, mean_mem_p2, mean_disk_r2, mean_disk_w2, mean_net_i2, mean_net_o2, mean_log_user2;
            mean_cpu2 = mean_net_i2 = mean_mem_v2 = mean_mem_p2 = mean_disk_r2 = mean_disk_w2 = mean_net_o2 = mean_log_user2 = 0;


            for (int temp = (count/2)+1; temp < count; temp++)
            {
                mean_cpu2 += double.Parse(cpu[temp]) / (count-(count / 2));
                mean_mem_v2 += double.Parse(mem_v[temp]) / (count - (count / 2));
                mean_mem_p2 += double.Parse(mem_p[temp]) / (count - (count / 2));
                mean_disk_r2 += double.Parse(disk_r[temp]) / (count - (count / 2));
                mean_disk_w2 += double.Parse(disk_w[temp]) / (count - (count / 2));
                mean_net_i2 += double.Parse(net_i[temp]) / (count - (count / 2));
                mean_net_o2 += double.Parse(net_o[temp]) / (count - (count / 2));
                mean_log_user2 += double.Parse(log_user[temp]) / (count - (count / 2));
            }
            Console.WriteLine("mean_cpu2={0}", mean_cpu2);
           

            double xcpu2, xmem_p2, xmem_v2, xdisk_r2, xdisk_w2, xnet_i2, xnet_o2, xlog_user2;
            xcpu2 = xmem_p2 = xmem_v2 = xdisk_r2 = xdisk_w2 = xnet_i2 = xnet_o2 = xlog_user2 = 0;

            for (int i = (count / 2) + 1; i < count; i++)
            {
                xcpu2 += Math.Pow((double.Parse(cpu[i]) - mean_cpu2), 2) / (count - (count / 2));
                xmem_p2 += Math.Pow((double.Parse(mem_p[i]) - mean_mem_p2), 2) / (count - (count / 2));
                xmem_v2 += Math.Pow((double.Parse(mem_v[i]) - mean_mem_v2), 2) / (count - (count / 2));
                xdisk_r2 += Math.Pow((double.Parse(disk_r[i]) - mean_disk_r2), 2) / (count - (count / 2));
                xdisk_w2 += Math.Pow((double.Parse(disk_w[i]) - mean_disk_w2), 2) / (count - (count / 2));
                xnet_i2 += Math.Pow((double.Parse(net_i[i]) - mean_net_i2), 2) / (count - (count / 2));
                xnet_o2 += Math.Pow((double.Parse(net_o[i]) - mean_net_o2), 2) / (count - (count / 2));
                xlog_user2 += Math.Pow((double.Parse(log_user[i]) - mean_log_user2), 2) / (count - (count / 2));

            }

           

            double sigma_cpu2, sigma_mem_p2, sigma_mem_v2, sigma_disk_r2, sigma_disk_w2, sigma_net_i2, sigma_net_o2, sigma_log_user2;
            sigma_cpu2 = sigma_mem_p2 = sigma_mem_v2 = sigma_disk_r2 = sigma_disk_w2 = sigma_net_i2 = sigma_net_o2 = sigma_log_user2 = 0;

            sigma_cpu2 = Math.Sqrt(xcpu2);
            sigma_mem_p2 = Math.Sqrt(xmem_p2);
            sigma_mem_v2 = Math.Sqrt(xmem_v2);
            sigma_disk_r2 = Math.Sqrt(xdisk_r2);
            sigma_disk_w2 = Math.Sqrt(xdisk_w2);
            sigma_net_i2 = Math.Sqrt(xnet_i2);
            sigma_net_o2 = Math.Sqrt(xnet_o2);
            sigma_log_user2 = Math.Sqrt(xlog_user2);
            Console.WriteLine("\nsigma_cpu2 ={0}\nsigma_mem_p2 ={2}\nsigma_mem_v2 ={2}\nsigma_disk_r2 ={3}\nsigma_disk_w2 ={4}\nsigma_net_i2 ={5}\nsigma_net_o2 ={6}\nsigma_log_user2 ={7}\n", sigma_cpu2, sigma_mem_p2, sigma_mem_v2, sigma_disk_r2, sigma_disk_w2, sigma_net_i2, sigma_net_o2, sigma_log_user2);

            

            if ((sigma_cpu1 >= sigma_cpu2) && (sigma_mem_p1 >= sigma_mem_p2) && (sigma_mem_v1 >= sigma_mem_v2) && (sigma_disk_r1 >= sigma_disk_r2) && (sigma_disk_w1 >= sigma_disk_w2) && (sigma_net_i1 >= sigma_net_i2) && (sigma_net_o1 >= sigma_net_o2) && (sigma_log_user1 >= sigma_log_user2))
            {
                MessageBox.Show("Normal Traffic", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((sigma_cpu1 <= sigma_cpu2) && (sigma_mem_p1 <= sigma_mem_p2) && (sigma_mem_v1 <= sigma_mem_v2) && (sigma_disk_r1 <= sigma_disk_r2) && (sigma_disk_w1 <= sigma_disk_w2) && (sigma_net_i1 <= sigma_net_i2) && (sigma_net_o1 <= sigma_net_o2) && (sigma_log_user1 <= sigma_log_user2))
            {
                MessageBox.Show("Abnormal Traffic", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if ((2 * sigma_cpu1 <= sigma_cpu2) && (2 * sigma_mem_p1 <= sigma_mem_p2) && (2 * sigma_mem_v1 <= sigma_mem_v2) && (2 * sigma_disk_r1 <= sigma_disk_r2) && (2 * sigma_disk_w1 <= sigma_disk_w2) && (2 * sigma_net_i1 <= sigma_net_i2) && (2 * sigma_net_o1 <= sigma_net_o2) && (2 * sigma_log_user1 <= sigma_log_user2))
                {
                    MessageBox.Show("Moderately Suspicious Traffic ", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if ((3 * sigma_cpu1 <= sigma_cpu2) && (3 * sigma_mem_p1 <= sigma_mem_p2) && (3 * sigma_mem_v1 <= sigma_mem_v2) && (3 * sigma_disk_r1 <= sigma_disk_r2) && (3 * sigma_disk_w1 <= sigma_disk_w2) && (3 * sigma_net_i1 <= sigma_net_i2) && (3 * sigma_net_o1 <= sigma_net_o2) && (3 * sigma_log_user1 <= sigma_log_user2))
                    {
                        MessageBox.Show("Highly Suspicious Traffic ", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                }
                else MessageBox.Show("Normal Traffic", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            
        }

        private void FlatButton7_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonCriticalDiff_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            int value = 20;
            Updateprogressbar1(value);

            // con3 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            //con3.Open();

            //Console.WriteLine(sql);
            // OracleCommand cmd = new OracleCommand(sql, con3);
            //cmd.ExecuteNonQuery();
            //SqlCommand command = new SqlCommand("select * from extract", con);
            //OracleDataReader r = cmd.ExecuteReader();

            //---MySQL---
            string sql = "select * from sys.sys_mon";
            MySqlConnection MyConn2 = new MySqlConnection("datasource=localhost;port=3306;username=root;password=system");
            MyConn2.Open();
            MySqlCommand MyCommand2 = new MySqlCommand(sql, MyConn2);
            MySqlDataReader dataReader = MyCommand2.ExecuteReader();
            

            count = 0;

            value = 40;
            Updateprogressbar1(value);

            string[] time = new string[2000]; string[] cpu = new string[2000]; string[] mem_v = new string[2000]; string[] mem_p = new string[2000]; string[] disk_r = new string[2000]; string[] disk_w = new string[2000]; string[] net_i = new string[2000]; string[] net_o = new string[2000]; string[] log_user = new string[2000]; string[] log_time = new string[2000]; string[] machine_name = new string[2000];
            while (dataReader.Read())
            {
                time[count] = dataReader["TIME"].ToString();
                cpu[count] = dataReader["CPU"].ToString();
                mem_v[count] = dataReader["MEM_V"].ToString();
                mem_p[count] = dataReader["MEM_P"].ToString();
                disk_r[count] = dataReader["DISK_R"].ToString();
                disk_w[count] = dataReader["DISK_W"].ToString();
                net_i[count] = dataReader["NET_I"].ToString();
                net_o[count] = dataReader["NET_O"].ToString();
                log_user[count] = dataReader["LOG_USER"].ToString();
                log_time[count] = dataReader["LOG_TIME"].ToString();
                machine_name[count] = dataReader["MACHINE_NAME"].ToString();

                //Console.WriteLine("time={0},cpu={1},mem_v={2},mem_p={3},disk_r={4},disk_w={5},net_i={6},net_o={7},log_user={8},log_time={9},machine_name={10}",time[i],cpu[i],mem_v[i],mem_p[i],disk_r[i],disk_w[i],net_i[i],net_o[i],log_user[i],log_time[i],machine_name[i]);
                count++;
            }

            dataReader.Close();
            MyConn2.Close();

            //con3.Close();

            double sum_all = 0; double sum_sq_all = 0;

            value = 50;
            Updateprogressbar1(value);
            for (int temp = 0; temp < count; temp++)
            {
                sum_all += double.Parse(cpu[temp])
                        + double.Parse(mem_v[temp])
                        + double.Parse(mem_p[temp])
                        + double.Parse(disk_r[temp])
                        + double.Parse(disk_w[temp])
                        + double.Parse(net_i[temp])
                        + double.Parse(net_o[temp])
                        + double.Parse(log_user[temp]);

                sum_sq_all += Math.Pow(double.Parse(cpu[temp]), 2)
                    + Math.Pow(double.Parse(mem_v[temp]), 2)
                    + Math.Pow(double.Parse(mem_p[temp]), 2)
                    + Math.Pow(double.Parse(disk_r[temp]), 2)
                    + Math.Pow(double.Parse(disk_w[temp]), 2)
                    + Math.Pow(double.Parse(net_i[temp]), 2)
                    + Math.Pow(double.Parse(net_o[temp]), 2)
                    + Math.Pow(double.Parse(log_user[temp]), 2);
            }

            int res = 8;
            double cf = Math.Pow(sum_all, 2) / (res * count);
            double ss = sum_sq_all - cf;
            value = 60;
            Updateprogressbar1(value);

            Console.WriteLine("\nsum_all={0}\nsum of sqaures of all={1}\ncf={2}\ntotal square sum={3}", sum_all, sum_sq_all, cf, ss);

            double[] t = new double[2000];
            double ss_resource = 0;

            for (int temp = 0; temp < count; temp++)
            {
                t[temp] += double.Parse(cpu[temp])
                        + double.Parse(mem_v[temp])
                        + double.Parse(mem_p[temp])
                        + double.Parse(disk_r[temp])
                        + double.Parse(disk_w[temp])
                        + double.Parse(net_i[temp])
                        + double.Parse(net_o[temp])
                        + double.Parse(log_user[temp]);
                ss_resource += Math.Pow(t[temp], 2);
            }
            Console.WriteLine("ss_res before={0}", ss_resource);
            int h = res;
            ss_resource = (ss_resource / h) - cf;
            Console.WriteLine("ss_res={0}", ss_resource);
            int k = count;
            double sum_cpu, sum_mem_v, sum_mem_p, sum_disk_r, sum_disk_w, sum_net_i, sum_net_o, sum_log_user;
            sum_cpu = sum_net_i = sum_mem_v = sum_mem_p = sum_disk_r = sum_disk_w = sum_net_o = sum_log_user = 0;
            
            value = 70;
            Updateprogressbar1(value);


            //calculating means.
            for (int temp = 0; temp < count; temp++)
            {
                sum_cpu += double.Parse(cpu[temp]);
                sum_mem_v += double.Parse(mem_v[temp]);
                sum_mem_p += double.Parse(mem_p[temp]);
                sum_disk_r += double.Parse(disk_r[temp]);
                sum_disk_w += double.Parse(disk_w[temp]);
                sum_net_i += double.Parse(net_i[temp]);
                sum_net_o += double.Parse(net_o[temp]);
                sum_log_user += double.Parse(log_user[temp]);
            }

            double ss_intervals = (Math.Pow(sum_cpu, 2)
                                + Math.Pow(sum_mem_v, 2)
                                + Math.Pow(sum_mem_p, 2)
                                + Math.Pow(sum_disk_r, 2)
                                + Math.Pow(sum_disk_w, 2)
                                + Math.Pow(sum_net_i, 2)
                                + Math.Pow(sum_net_o, 2)
                                + Math.Pow(sum_log_user, 2)) / k - cf;
            Console.WriteLine("\nss interval ={0}\n", ss_intervals);
            double ss_error = ss - (ss_resource + ss_intervals);
            double total_ss = (h * k) - 1;
            double ssa = h - 1;
            double ssb = k - 1;
            double sse = total_ss - (ssa + ssb);
            double msa = ss_resource / ssa;
            double msb = ss_intervals / ssb;
            mse = ss_error / sse;
            double f1 = msa / mse;
            double f2 = msb / mse;
            Console.WriteLine("f1={0}\n,ssa={1}\n,sse={2}\n,f2={3}\n,ssb={4}", f1, ssa, sse, f2, ssb);

            value = 80;
            Updateprogressbar1(value);


            //------Oracle
            //con2 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            //con2.Open();
            string sql1 = "select n" + ssa + " from ftable where d=" + sse;
            Console.WriteLine(sql1);
            //OracleCommand cmd1 = new OracleCommand(sql1, con2);
            //OracleDataReader r1 = cmd1.ExecuteReader();
            double ftab = 0;
            /*while (r1.Read())
            {
                ftab = r1.GetDouble(0);

            }*/

            Console.WriteLine("ftab={0}\n", ftab);
            
            
            if (f1 > ftab)
            {
                buttonCriticalDiff.BackColor = Color.FromArgb(0, 168, 252);
                buttonCriticalDiff.Enabled = true;
                
            }
            if(f2>ftab)
            {
                button3Sigma.BackColor = Color.FromArgb(0, 168, 252);
                button3Sigma.Enabled = true;
            }
            else
                MessageBox.Show("Normal Traffic", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //con2.Close();
            

            value = 100;
            Updateprogressbar1(value);

        }

        private void Updateprogressbar1(int value)
        {
            progressBar1.Value = value;
            progressBar1.ForeColor = Color.FromArgb(0, 168, 252);
            int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.Refresh();
            progressBar1.CreateGraphics().DrawString(percent.ToString() + "%",
               new Font("Courier New", (float)10, FontStyle.Bold),
               Brushes.Black,
               new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));

            Thread.Sleep(200);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //-----Oracle ------
            //con3 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            //con3.Open();

            //string sql = "delete from extract";
            //OracleCommand cmd = new OracleCommand(sql, con3);
            //OracleDataReader r = cmd.ExecuteReader();
            //con3.Close();

            //------ MySQL ------

            try
            {
                MySqlConnection MyConn1 = new MySqlConnection("datasource=localhost;port=3306;username=root;password=system");
                string DeleteQuery = "delete from sys.sys_mon";
                MySqlCommand MyCommand1 = new MySqlCommand(DeleteQuery, MyConn1);
                MySqlDataReader MyReader1;
                MyConn1.Open();
                MyReader1 = MyCommand1.ExecuteReader();
                Console.WriteLine("Data Deleted from table sys_mon");
                while (MyReader1.Read())
                {
                }
                MyConn1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            monitor frm = new monitor();
            frm.ParentFormObject = this; // pass the parent form object 
            frm.Show();
            //this.Hide();
            
                        
        }

        private void button8_Click(object sender, EventArgs e)
        
        {
            int value = 30;
            Updateprogressbar3(value);

            //con3 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            //con3.Open();
            
              
            

            //string sql = "select * from extract order by time";




            //OracleCommand cmd = new OracleCommand(sql, con3);
            //OracleDataReader r = cmd.ExecuteReader();


            count = 0;


            string[] time = new string[2000]; string[] cpu = new string[2000]; string[] mem_v = new string[2000]; string[] mem_p = new string[2000]; string[] disk_r = new string[2000]; string[] disk_w = new string[2000]; string[] net_i = new string[2000]; string[] net_o = new string[2000]; string[] log_user = new string[2000]; string[] log_time = new string[2000]; string[] machine_name = new string[2000];
            /*while (r.Read())
            {
                time[count] = r["TIME"].ToString();
                cpu[count] = r["CPU"].ToString();
                mem_v[count] = r["MEM_V"].ToString();
                mem_p[count] = r["MEM_P"].ToString();
                disk_r[count] = r["DISK_R"].ToString();
                disk_w[count] = r["DISK_W"].ToString();
                net_i[count] = r["NET_I"].ToString();
                net_o[count] = r["NET_O"].ToString();
                log_user[count] = r["LOGGED_USER"].ToString();
                log_time[count] = r["LOGGED_TIME"].ToString();
                machine_name[count] = r["MACHINE_NAME"].ToString();
                count++;
            }*/

            //con3.Close();
            value = 40;
            Updateprogressbar3(value);

            //calculating means 1

            double mean_cpu1, mean_mem_v1, mean_mem_p1, mean_disk_r1, mean_disk_w1, mean_net_i1, mean_net_o1, mean_log_user1;
            mean_cpu1 = mean_net_i1 = mean_mem_v1 = mean_mem_p1 = mean_disk_r1 = mean_disk_w1 = mean_net_o1 = mean_log_user1 = 0;


            for (int temp = 0; temp < count / 2; temp++)
            {
                mean_cpu1 += double.Parse(cpu[temp]) / count / 2;
                mean_mem_v1 += double.Parse(mem_v[temp]) / count / 2;
                mean_mem_p1 += double.Parse(mem_p[temp]) / count / 2;
                mean_disk_r1 += double.Parse(disk_r[temp]) / count / 2;
                mean_disk_w1 += double.Parse(disk_w[temp]) / count / 2;
                mean_net_i1 += double.Parse(net_i[temp]) / count / 2;
                mean_net_o1 += double.Parse(net_o[temp]) / count / 2;
                mean_log_user1 += double.Parse(log_user[temp]) / count / 2;
            }
            Console.WriteLine("mean_cpu1={0}", mean_cpu1);

            double xcpu1, xmem_p1, xmem_v1, xdisk_r1, xdisk_w1, xnet_i1, xnet_o1, xlog_user1;
            xcpu1 = xmem_p1 = xmem_v1 = xdisk_r1 = xdisk_w1 = xnet_i1 = xnet_o1 = xlog_user1 = 0;

            for (int i = 0; i < count / 2; i++)
            {
                xcpu1 += Math.Pow((double.Parse(cpu[i]) - mean_cpu1), 2) / (count / 2);
                xmem_p1 += Math.Pow((double.Parse(mem_p[i]) - mean_mem_p1), 2) / (count / 2);
                xmem_v1 += Math.Pow((double.Parse(mem_v[i]) - mean_mem_v1), 2) / (count / 2);
                xdisk_r1 += Math.Pow((double.Parse(disk_r[i]) - mean_disk_r1), 2) / (count / 2);
                xdisk_w1 += Math.Pow((double.Parse(disk_w[i]) - mean_disk_w1), 2) / (count / 2);
                xnet_i1 += Math.Pow((double.Parse(net_i[i]) - mean_net_i1), 2) / (count / 2);
                xnet_o1 += Math.Pow((double.Parse(net_o[i]) - mean_net_o1), 2) / (count / 2);
                xlog_user1 += Math.Pow((double.Parse(log_user[i]) - mean_log_user1), 2) / (count / 2);

            }
            value = 50;
            Updateprogressbar3(value);

            double sigma_cpu1, sigma_mem_p1, sigma_mem_v1, sigma_disk_r1, sigma_disk_w1, sigma_net_i1, sigma_net_o1, sigma_log_user1;
            sigma_cpu1 = sigma_mem_p1 = sigma_mem_v1 = sigma_disk_r1 = sigma_disk_w1 = sigma_net_i1 = sigma_net_o1 = sigma_log_user1 = 0;

            sigma_cpu1 = Math.Sqrt(xcpu1);
            sigma_mem_p1 = Math.Sqrt(xmem_p1);
            sigma_mem_v1 = Math.Sqrt(xmem_v1);
            sigma_disk_r1 = Math.Sqrt(xdisk_r1);
            sigma_disk_w1 = Math.Sqrt(xdisk_w1);
            sigma_net_i1 = Math.Sqrt(xnet_i1);
            sigma_net_o1 = Math.Sqrt(xnet_o1);
            sigma_log_user1 = Math.Sqrt(xlog_user1);
            Console.WriteLine("\nsigma_cpu1 ={0}\nsigma_mem_p1 ={1}\nsigma_mem_v1 ={2}\nsigma_disk_r1 ={3}\nsigma_disk_w1 ={4}\nsigma_net_i1 ={5}\nsigma_net_o1 ={6}\nsigma_log_user1 ={7}\n", sigma_cpu1, sigma_mem_p1, sigma_mem_v1, sigma_disk_r1, sigma_disk_w1, sigma_net_i1, sigma_net_o1, sigma_log_user1);


            value = 60;
            Updateprogressbar3(value);
            //-calculating means 2

            double mean_cpu2, mean_mem_v2, mean_mem_p2, mean_disk_r2, mean_disk_w2, mean_net_i2, mean_net_o2, mean_log_user2;
            mean_cpu2 = mean_net_i2 = mean_mem_v2 = mean_mem_p2 = mean_disk_r2 = mean_disk_w2 = mean_net_o2 = mean_log_user2 = 0;


            for (int temp = (count / 2) + 1; temp < count; temp++)
            {
                mean_cpu2 += double.Parse(cpu[temp]) / (count - (count / 2));
                mean_mem_v2 += double.Parse(mem_v[temp]) / (count - (count / 2));
                mean_mem_p2 += double.Parse(mem_p[temp]) / (count - (count / 2));
                mean_disk_r2 += double.Parse(disk_r[temp]) / (count - (count / 2));
                mean_disk_w2 += double.Parse(disk_w[temp]) / (count - (count / 2));
                mean_net_i2 += double.Parse(net_i[temp]) / (count - (count / 2));
                mean_net_o2 += double.Parse(net_o[temp]) / (count - (count / 2));
                mean_log_user2 += double.Parse(log_user[temp]) / (count - (count / 2));
            }
            Console.WriteLine("mean_cpu2={0}", mean_cpu2);

            value = 70;
            Updateprogressbar3(value);

            double xcpu2, xmem_p2, xmem_v2, xdisk_r2, xdisk_w2, xnet_i2, xnet_o2, xlog_user2;
            xcpu2 = xmem_p2 = xmem_v2 = xdisk_r2 = xdisk_w2 = xnet_i2 = xnet_o2 = xlog_user2 = 0;

            for (int i = (count / 2) + 1; i < count; i++)
            {
                xcpu2 += Math.Pow((double.Parse(cpu[i]) - mean_cpu2), 2) / (count - (count / 2));
                xmem_p2 += Math.Pow((double.Parse(mem_p[i]) - mean_mem_p2), 2) / (count - (count / 2));
                xmem_v2 += Math.Pow((double.Parse(mem_v[i]) - mean_mem_v2), 2) / (count - (count / 2));
                xdisk_r2 += Math.Pow((double.Parse(disk_r[i]) - mean_disk_r2), 2) / (count - (count / 2));
                xdisk_w2 += Math.Pow((double.Parse(disk_w[i]) - mean_disk_w2), 2) / (count - (count / 2));
                xnet_i2 += Math.Pow((double.Parse(net_i[i]) - mean_net_i2), 2) / (count - (count / 2));
                xnet_o2 += Math.Pow((double.Parse(net_o[i]) - mean_net_o2), 2) / (count - (count / 2));
                xlog_user2 += Math.Pow((double.Parse(log_user[i]) - mean_log_user2), 2) / (count - (count / 2));

            }

            value = 80;
            Updateprogressbar3(value);

            double sigma_cpu2, sigma_mem_p2, sigma_mem_v2, sigma_disk_r2, sigma_disk_w2, sigma_net_i2, sigma_net_o2, sigma_log_user2;
            sigma_cpu2 = sigma_mem_p2 = sigma_mem_v2 = sigma_disk_r2 = sigma_disk_w2 = sigma_net_i2 = sigma_net_o2 = sigma_log_user2 = 0;

            sigma_cpu2 = Math.Sqrt(xcpu2);
            sigma_mem_p2 = Math.Sqrt(xmem_p2);
            sigma_mem_v2 = Math.Sqrt(xmem_v2);
            sigma_disk_r2 = Math.Sqrt(xdisk_r2);
            sigma_disk_w2 = Math.Sqrt(xdisk_w2);
            sigma_net_i2 = Math.Sqrt(xnet_i2);
            sigma_net_o2 = Math.Sqrt(xnet_o2);
            sigma_log_user2 = Math.Sqrt(xlog_user2);
            Console.WriteLine("\nsigma_cpu2 ={0}\nsigma_mem_p2 ={2}\nsigma_mem_v2 ={2}\nsigma_disk_r2 ={3}\nsigma_disk_w2 ={4}\nsigma_net_i2 ={5}\nsigma_net_o2 ={6}\nsigma_log_user2 ={7}\n", sigma_cpu2, sigma_mem_p2, sigma_mem_v2, sigma_disk_r2, sigma_disk_w2, sigma_net_i2, sigma_net_o2, sigma_log_user2);


            value = 100;
            Updateprogressbar3(value);

            if ((sigma_cpu1 >= sigma_cpu2) && (sigma_mem_p1 >= sigma_mem_p2) && (sigma_mem_v1 >= sigma_mem_v2) && (sigma_disk_r1 >= sigma_disk_r2) && (sigma_disk_w1 >= sigma_disk_w2) && (sigma_net_i1 >= sigma_net_i2) && (sigma_net_o1 >= sigma_net_o2) && (sigma_log_user1 >= sigma_log_user2))
            {
                MessageBox.Show("Normal Traffic", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if ((sigma_cpu1 <= sigma_cpu2) && (sigma_mem_p1 <= sigma_mem_p2) && (sigma_mem_v1 <= sigma_mem_v2) && (sigma_disk_r1 <= sigma_disk_r2) && (sigma_disk_w1 <= sigma_disk_w2) && (sigma_net_i1 <= sigma_net_i2) && (sigma_net_o1 <= sigma_net_o2) && (sigma_log_user1 <= sigma_log_user2))
            {
                MessageBox.Show("--Abnormal Traffic--", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if ((2 * sigma_cpu1 <= sigma_cpu2) && (2 * sigma_mem_p1 <= sigma_mem_p2) && (2 * sigma_mem_v1 <= sigma_mem_v2) && (2 * sigma_disk_r1 <= sigma_disk_r2) && (2 * sigma_disk_w1 <= sigma_disk_w2) && (2 * sigma_net_i1 <= sigma_net_i2) && (2 * sigma_net_o1 <= sigma_net_o2) && (2 * sigma_log_user1 <= sigma_log_user2))
                {
                    MessageBox.Show("--Moderately Suspicious Traffic-- ", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if ((3 * sigma_cpu1 <= sigma_cpu2) && (3 * sigma_mem_p1 <= sigma_mem_p2) && (3 * sigma_mem_v1 <= sigma_mem_v2) && (3 * sigma_disk_r1 <= sigma_disk_r2) && (3 * sigma_disk_w1 <= sigma_disk_w2) && (3 * sigma_net_i1 <= sigma_net_i2) && (3 * sigma_net_o1 <= sigma_net_o2) && (3 * sigma_log_user1 <= sigma_log_user2))
                {
                        MessageBox.Show("--Highly Suspicious Traffic-- ", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
             }
            else MessageBox.Show("--Normal Traffic--", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
           
        }

        private void Updateprogressbar3(int value)
        {
            progressBar3.Value = value; 
            progressBar3.ForeColor = Color.FromArgb(0, 168, 252);
            int percent = (int)(((double)progressBar3.Value / (double)progressBar3.Maximum) * 100);
            progressBar3.Refresh();
            progressBar3.CreateGraphics().DrawString(percent.ToString() + "%",
               new Font("Courier New", (float)10, FontStyle.Bold),
               Brushes.Black,
               new PointF(progressBar3.Width / 2 - 10, progressBar3.Height / 2 - 7));
            Thread.Sleep(200);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonCriticalDiff_Click_1(object sender, EventArgs e)
        {
            int value = 30;
            Updateprogressbar2(value);
            //con3 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            //con3.Open();

            //string sql3 = "select * from extract order by time";
            //OracleCommand cmd3 = new OracleCommand(sql3, con3);
            //OracleDataReader r = cmd3.ExecuteReader();
            count = 0;
            value = 40;
            Updateprogressbar2(value);

            string[] time = new string[2000]; string[] cpu = new string[2000]; string[] mem_v = new string[2000]; string[] mem_p = new string[2000]; string[] disk_r = new string[2000]; string[] disk_w = new string[2000]; string[] net_i = new string[2000]; string[] net_o = new string[2000]; string[] log_user = new string[2000]; string[] log_time = new string[2000]; string[] machine_name = new string[2000];
            /*while (r.Read())
            {
                time[count] = r["TIME"].ToString();
                cpu[count] = r["CPU"].ToString();
                mem_v[count] = r["MEM_V"].ToString();
                mem_p[count] = r["MEM_P"].ToString();
                disk_r[count] = r["DISK_R"].ToString();
                disk_w[count] = r["DISK_W"].ToString();
                net_i[count] = r["NET_I"].ToString();
                net_o[count] = r["NET_O"].ToString();
                log_user[count] = r["LOGGED_USER"].ToString();
                log_time[count] = r["LOGGED_TIME"].ToString();
                machine_name[count] = r["MACHINE_NAME"].ToString();
                count++;
            }

            con3.Close();
            */
            value = 50;
            Updateprogressbar2(value);

            //con2 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            //con2.Open();
            string sql2 = "select t from ttable where df=" + count;
            Console.WriteLine(sql2);
            //OracleCommand cmd2 = new OracleCommand(sql2, con2);
            //OracleDataReader r2 = cmd2.ExecuteReader();
            double t = 0;
            /*while (r2.Read())
            {
                t = r2.GetDouble(0);
            }*/
            
            //con2.Close();
            value = 70;
            Updateprogressbar2(value);
            double cd = Math.Sqrt(mse) * Math.Sqrt(2 * count) * t;
            Console.WriteLine("t value={0}\nCD={1}", t,cd);
            int Flag = 0;
            for (int temp = 0; temp < count; temp++)
            {
                if(((double.Parse(cpu [temp]) - double.Parse(mem_p [temp])) > cd) || ((double.Parse(cpu [temp]) - double.Parse(log_user [temp])) > cd) || ((double.Parse(cpu [temp]) - double.Parse(disk_w [temp])) > cd) || ((double.Parse(disk_w [temp]) - double.Parse(log_user [temp])) > cd) || ((double.Parse(disk_w [temp]) - double.Parse(mem_p [temp])) > cd) || ((double.Parse(log_user [temp]) - double.Parse(mem_p[temp])) > cd))
                {
                    Flag = 1;
                }

            }
            value = 100;
            Updateprogressbar2(value);
            if (Flag == 1)
            {
                MessageBox.Show("Consuption of One Resouce is Dependent on The Consumption of Other", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Consuption of One Resouce is not Dependent on The Consumption of Other", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }

        }

        private void Updateprogressbar2(int value)
        {
            progressBar2.Value = value;
            progressBar2.ForeColor = Color.FromArgb(0, 168, 252);
            int percent = (int)(((double)progressBar2.Value / (double)progressBar2.Maximum) * 100);
            progressBar2.Refresh();
            progressBar2.CreateGraphics().DrawString(percent.ToString() + "%",
               new Font("Courier New", (float)10, FontStyle.Bold),
               Brushes.Black,
               new PointF(progressBar2.Width / 2 - 10, progressBar2.Height / 2 - 7));
            Thread.Sleep(200);
        }

        private void buttonPCA_Click(object sender, EventArgs e)
        {

        }

        private void buttonChiSqTest_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            f.Show();
            
        }

        

        private void buttonCheckForVirus_Click(object sender, EventArgs e)
        {
            
            
        }

        
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        
    }
 }











