using Oracle.ManagedDataAccess.Client;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;


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

        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new InitialForm());
        }

        private void InitialForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None ;
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
            progressBar1.Value = 40;
            con3 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            con3.Open();

            System.Threading.Thread.Sleep(500);
            string sql = "select * from extract order by time";
            //Console.WriteLine(sql);
            OracleCommand cmd = new OracleCommand(sql, con3);
            //cmd.ExecuteNonQuery();
            //SqlCommand command = new SqlCommand("select * from extract", con);
            OracleDataReader r = cmd.ExecuteReader();
            count = 0;

            progressBar1.Value = 50;
            Thread.Sleep(500);
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

                //Console.WriteLine("time={0},cpu={1},mem_v={2},mem_p={3},disk_r={4},disk_w={5},net_i={6},net_o={7},log_user={8},log_time={9},machine_name={10}",time[i],cpu[i],mem_v[i],mem_p[i],disk_r[i],disk_w[i],net_i[i],net_o[i],log_user[i],log_time[i],machine_name[i]);
                count++;
            }


            con3.Close();

            double sum_all = 0; double sum_sq_all = 0;


            progressBar1.Value = 60;
            Thread.Sleep(500);
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

            progressBar1.Value = 70;
            Thread.Sleep(500);
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

            progressBar1.Value = 80;
            Thread.Sleep(500);

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
            progressBar1.Value = 90;
            Thread.Sleep(500);

            //------
            con2 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            con2.Open();
            string sql1 = "select n" + ssa + " from ftable where d=" + sse;
            Console.WriteLine(sql1);
            OracleCommand cmd1 = new OracleCommand(sql1, con2);
            OracleDataReader r1 = cmd1.ExecuteReader();
            double ftab = 0;
            while (r1.Read())
            {
                ftab = r1.GetDouble(0);

            }
            Console.WriteLine("ftab={0}\n", ftab);

            progressBar1.Value = 100;
            Thread.Sleep(500);
            if (f1 > ftab)
            {
                buttonCriticalDiff.Enabled = true;
                buttonCriticalDiff.BackColor = Color.FromArgb(0, 168, 252);
            }
            else if(f2>ftab)
            {
                button3Sigma.Enabled = true;
                button3Sigma.BackColor = Color.FromArgb(0, 168, 252);
            }
            else
                MessageBox.Show("Normal Traffic", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con2.Close();

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            con3 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            con3.Open();

            string sql = "delete from extract";
            OracleCommand cmd = new OracleCommand(sql, con3);
            OracleDataReader r = cmd.ExecuteReader();
            con3.Close();
            monitor frm = new monitor();
            frm.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        
        {
            progressBar3.Value = 20;
            Thread.Sleep(500);
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
            progressBar3.Value = 30;
            Thread.Sleep(500);

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
            progressBar3.Value = 40;
            Thread.Sleep(500);

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


            progressBar3.Value = 50;
            Thread.Sleep(500);
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

            progressBar3.Value = 60;
            Thread.Sleep(500);
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

            progressBar3.Value = 70;
            Thread.Sleep(500);

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


            progressBar3.Value = 100;
            Thread.Sleep(500);
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
            progressBar3.Value = 100;
            Thread.Sleep(500);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonCriticalDiff_Click_1(object sender, EventArgs e)
        {
            progressBar2.Value = 30;
                     
            con3 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            con3.Open();

            string sql3 = "select * from extract order by time";
            OracleCommand cmd3 = new OracleCommand(sql3, con3);
            OracleDataReader r = cmd3.ExecuteReader();
            count = 0;
            progressBar2.Value = 40;
            Thread.Sleep(400); 

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
            progressBar2.Value = 50;
            Thread.Sleep(400);
            con2 = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
            con2.Open();
            string sql2 = "select t from ttable where df=" + count;
            Console.WriteLine(sql2);
            OracleCommand cmd2 = new OracleCommand(sql2, con2);
            OracleDataReader r2 = cmd2.ExecuteReader();
            double t = 0;
            while (r2.Read())
            {
                t = r2.GetDouble(0);
            }
            
            con2.Close();
            progressBar2.Value = 60;
            Thread.Sleep(400);
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
            progressBar2.Value = 100;
            Thread.Sleep(400);
            if (Flag == 1)
            {
                MessageBox.Show("Consuption of One Resouce is Dependent on The Consumption of Other", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Consuption of One Resouce is not Dependent on The Consumption of Other", "IDS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }

        }

        private void buttonPCA_Click(object sender, EventArgs e)
        {

        }

        private void buttonChiSqTest_Click(object sender, EventArgs e)
        {

        }

        private void buttonCheckForVirus_Click(object sender, EventArgs e)
        {

        }
    }
 }

   









