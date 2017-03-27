using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Security.AccessControl;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;

namespace RecursiveSearchCS
{
    /// <summary>
    /// Summary description for Form1
    /// </summary>
    public partial class Form1 : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.TextBox txtFile;
        internal System.Windows.Forms.Label lblFile;
        internal System.Windows.Forms.Label lblDirectory;
        internal System.Windows.Forms.ListBox lstFilesFound;
        internal System.Windows.Forms.ComboBox cboDirectory;
        

        /// <summary>
        /// Required designer variable
        /// </summary>


        public Form1()
        {
            // 
            // Required for Windows Form Designer support
            // 
            InitializeComponent();

            // 
            // TODO: Add any constructor code after InitializeComponent call.
            // 
        }


        

        #region Windows Form Designer generated code
        
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            lstFilesFound.Items.Clear();
            txtFile.Enabled = false;
            cboDirectory.Enabled = false;
            btnSearch.Text = "Searching...";
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            DirSearch(cboDirectory.Text);
            btnSearch.Text = "Search";
            this.Cursor = Cursors.Default;
            txtFile.Enabled = true;
            cboDirectory.Enabled = true;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.Location = new Point(700, 100);
            
            cboDirectory.Items.Clear();
            foreach (string s in Directory.GetLogicalDrives())
            {
                cboDirectory.Items.Add(s);
            }
            cboDirectory.Text = "E:\\";
        }

        void DirSearch(string sDir)
        {
            
            try
            {
                
                foreach (string d in Directory.GetDirectories(sDir))
                {                   
                    foreach (string f in Directory.GetFiles(d, txtFile.Text))
                    {
                        lstFilesFound.Items.Add(f);                       
                    }
                    DirSearch(d);
                        
                }
            }
            catch (System.UnauthorizedAccessException excpt)
            {
                Console.WriteLine(excpt.Message);
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelExist.Text = "";
            labelNew.Text = "";

            // for calculating new hash
            String file="";
            try
            {
                file = lstFilesFound.SelectedItem.ToString();
            }catch(Exception)
            {
                labelNew.Text = "Please Search Again";
            }
            
            //String filename = file.Substring(file.LastIndexOf('\\') + 1, (file.Length - file.LastIndexOf('\\') - 1));
            //Console.WriteLine("\nfilename is -- {0} length={1}\n", file, file.Length);

            String md5 = CreateMD5(file);
            labelNew.Text = Regex.Replace(md5,@"[^\u0000-\u007F]", string.Empty);
            //Console.WriteLine("md5=" + md5);
            //Console.WriteLine("New {0}\nOld {1}", Regex.Replace(labelNew.Text, @"[^\u0000-\u007F]", string.Empty), labelExist.Text);

            //for retriving old hash from database
            //Console.WriteLine("\n******The filename is {0}\n\n",file);
            try
            {
                OracleConnection con = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id =system; Password =system");
                con.Open();
            
            string sql = "select md5 from md5_hash where filename='"+file.ToLower().Trim().Replace(" ", String.Empty)+"'";
            Console.WriteLine("****sql is {0}",sql);
            
            OracleCommand cmd = new OracleCommand(sql, con);
            OracleDataReader r = cmd.ExecuteReader();


            while (r.Read())
            {
                labelExist.Text = "";
                String retrive= r["MD5"].ToString();
                    Console.WriteLine("length returned "+retrive.Length);
                    if (retrive.Length > 0)
                        labelExist.Text = retrive;
                    else
                        labelExist.Text = "Not Found";
            }
            
            }
            catch (Exception exp)
            {
                Console.WriteLine("exception :" + exp);
            }
            //con.Close();

            //check if both are same
            if (labelExist.Text.ToString().Equals(labelNew.Text.ToString()))
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = ids.Properties.Resources.correct;
            }
            else
            {
                Console.WriteLine("New {0}\nOld {1}",labelNew.Text, labelExist.Text);
                pictureBox1.Visible = true;
                pictureBox1.Image = ids.Properties.Resources.warning;
            }

            //Console.WriteLine("\nMd5 is -- {0}\n", md5);
        }
        public static String CreateMD5(string filename)
        {
            try
            {
                using (var md5 = MD5.Create())
            {
                
                    using (var stream = File.OpenRead(filename))
                    {
                        return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "‌​").ToLower();
                    }
                }


            }
            catch (Exception)
            {
                return "Please use the search and select a file from the above list";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
#endregion