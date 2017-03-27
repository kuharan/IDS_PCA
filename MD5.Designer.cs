namespace RecursiveSearchCS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblDirectory = new System.Windows.Forms.Label();
            this.lstFilesFound = new System.Windows.Forms.ListBox();
            this.cboDirectory = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelNew = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.labelExist = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Location = new System.Drawing.Point(11, 129);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(141, 66);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.txtFile.Location = new System.Drawing.Point(11, 39);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(139, 21);
            this.txtFile.TabIndex = 4;
            this.txtFile.Text = "*.txt";
            // 
            // lblFile
            // 
            this.lblFile.Location = new System.Drawing.Point(8, 18);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(168, 18);
            this.lblFile.TabIndex = 5;
            this.lblFile.Text = "Search for files containing:";
            // 
            // lblDirectory
            // 
            this.lblDirectory.Location = new System.Drawing.Point(8, 70);
            this.lblDirectory.Name = "lblDirectory";
            this.lblDirectory.Size = new System.Drawing.Size(140, 27);
            this.lblDirectory.TabIndex = 3;
            this.lblDirectory.Text = "Look In:";
            // 
            // lstFilesFound
            // 
            this.lstFilesFound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lstFilesFound.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstFilesFound.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstFilesFound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.lstFilesFound.ItemHeight = 16;
            this.lstFilesFound.Location = new System.Drawing.Point(177, 79);
            this.lstFilesFound.Name = "lstFilesFound";
            this.lstFilesFound.Size = new System.Drawing.Size(432, 256);
            this.lstFilesFound.TabIndex = 1;
            // 
            // cboDirectory
            // 
            this.cboDirectory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cboDirectory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDirectory.DropDownWidth = 112;
            this.cboDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDirectory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.cboDirectory.Location = new System.Drawing.Point(9, 100);
            this.cboDirectory.Name = "cboDirectory";
            this.cboDirectory.Size = new System.Drawing.Size(141, 23);
            this.cboDirectory.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(11, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 64);
            this.button1.TabIndex = 6;
            this.button1.Text = "MD5";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelNew
            // 
            this.labelNew.AutoSize = true;
            this.labelNew.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNew.Location = new System.Drawing.Point(174, 375);
            this.labelNew.Name = "labelNew";
            this.labelNew.Size = new System.Drawing.Size(38, 18);
            this.labelNew.TabIndex = 7;
            this.labelNew.Text = "new";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(252)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(11, 271);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 64);
            this.button2.TabIndex = 8;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelExist
            // 
            this.labelExist.AutoSize = true;
            this.labelExist.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExist.Location = new System.Drawing.Point(174, 393);
            this.labelExist.Name = "labelExist";
            this.labelExist.Size = new System.Drawing.Size(38, 18);
            this.labelExist.TabIndex = 9;
            this.labelExist.Text = "old";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ids.Properties.Resources.correct;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(513, 346);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(174, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Displayed Result (Ignoring Files > 50 MB)\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(114, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 18);
            this.label2.TabIndex = 13;
            this.label2.Text = "Old:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(114, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "New:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(625, 442);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelExist);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelNew);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblDirectory);
            this.Controls.Add(this.lstFilesFound);
            this.Controls.Add(this.cboDirectory);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(252)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelNew;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelExist;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
#endregion
