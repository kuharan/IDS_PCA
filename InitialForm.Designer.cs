namespace SystemMonitor
{
    partial class InitialForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.buttonScanAndMon = new System.Windows.Forms.Button();
            this.buttonAnova = new System.Windows.Forms.Button();
            this.buttonCriticalDiff = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonCheckForVirus = new System.Windows.Forms.Button();
            this.button3Sigma = new System.Windows.Forms.Button();
            this.buttonChiSqTest = new System.Windows.Forms.Button();
            this.buttonPCA = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.progressBar4 = new System.Windows.Forms.ProgressBar();
            this.progressBar5 = new System.Windows.Forms.ProgressBar();
            this.progressBar6 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // buttonScanAndMon
            // 
            this.buttonScanAndMon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(252)))));
            this.buttonScanAndMon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonScanAndMon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonScanAndMon.FlatAppearance.BorderSize = 0;
            this.buttonScanAndMon.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.buttonScanAndMon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonScanAndMon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonScanAndMon.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScanAndMon.ForeColor = System.Drawing.Color.Black;
            this.buttonScanAndMon.Image = ((System.Drawing.Image)(resources.GetObject("buttonScanAndMon.Image")));
            this.buttonScanAndMon.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonScanAndMon.Location = new System.Drawing.Point(24, 12);
            this.buttonScanAndMon.Name = "buttonScanAndMon";
            this.buttonScanAndMon.Size = new System.Drawing.Size(132, 110);
            this.buttonScanAndMon.TabIndex = 15;
            this.buttonScanAndMon.Text = "Scan and Monitor";
            this.buttonScanAndMon.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonScanAndMon.UseVisualStyleBackColor = false;
            this.buttonScanAndMon.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAnova
            // 
            this.buttonAnova.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(252)))));
            this.buttonAnova.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAnova.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAnova.FlatAppearance.BorderSize = 0;
            this.buttonAnova.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.buttonAnova.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonAnova.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnova.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAnova.ForeColor = System.Drawing.Color.Black;
            this.buttonAnova.Image = ((System.Drawing.Image)(resources.GetObject("buttonAnova.Image")));
            this.buttonAnova.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAnova.Location = new System.Drawing.Point(162, 12);
            this.buttonAnova.Name = "buttonAnova";
            this.buttonAnova.Size = new System.Drawing.Size(132, 110);
            this.buttonAnova.TabIndex = 16;
            this.buttonAnova.Text = "Anova";
            this.buttonAnova.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAnova.UseVisualStyleBackColor = false;
            this.buttonAnova.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonCriticalDiff
            // 
            this.buttonCriticalDiff.BackColor = System.Drawing.Color.Gray;
            this.buttonCriticalDiff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCriticalDiff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCriticalDiff.Enabled = false;
            this.buttonCriticalDiff.FlatAppearance.BorderSize = 0;
            this.buttonCriticalDiff.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.buttonCriticalDiff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonCriticalDiff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCriticalDiff.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCriticalDiff.ForeColor = System.Drawing.Color.Black;
            this.buttonCriticalDiff.Image = ((System.Drawing.Image)(resources.GetObject("buttonCriticalDiff.Image")));
            this.buttonCriticalDiff.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonCriticalDiff.Location = new System.Drawing.Point(300, 12);
            this.buttonCriticalDiff.Name = "buttonCriticalDiff";
            this.buttonCriticalDiff.Size = new System.Drawing.Size(130, 110);
            this.buttonCriticalDiff.TabIndex = 17;
            this.buttonCriticalDiff.Text = "Critical Diff";
            this.buttonCriticalDiff.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCriticalDiff.UseVisualStyleBackColor = false;
            this.buttonCriticalDiff.Click += new System.EventHandler(this.buttonCriticalDiff_Click_1);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(252)))));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.Color.Black;
            this.buttonClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonClose.Image")));
            this.buttonClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonClose.Location = new System.Drawing.Point(437, 239);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(132, 110);
            this.buttonClose.TabIndex = 18;
            this.buttonClose.Text = "Close";
            this.buttonClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // buttonCheckForVirus
            // 
            this.buttonCheckForVirus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(252)))));
            this.buttonCheckForVirus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCheckForVirus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCheckForVirus.FlatAppearance.BorderSize = 0;
            this.buttonCheckForVirus.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.buttonCheckForVirus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonCheckForVirus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCheckForVirus.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCheckForVirus.ForeColor = System.Drawing.Color.Black;
            this.buttonCheckForVirus.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckForVirus.Image")));
            this.buttonCheckForVirus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonCheckForVirus.Location = new System.Drawing.Point(24, 239);
            this.buttonCheckForVirus.Name = "buttonCheckForVirus";
            this.buttonCheckForVirus.Size = new System.Drawing.Size(132, 110);
            this.buttonCheckForVirus.TabIndex = 19;
            this.buttonCheckForVirus.Text = "Check For known Viruses";
            this.buttonCheckForVirus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCheckForVirus.UseVisualStyleBackColor = false;
            this.buttonCheckForVirus.Click += new System.EventHandler(this.buttonCheckForVirus_Click);
            // 
            // button3Sigma
            // 
            this.button3Sigma.BackColor = System.Drawing.Color.Gray;
            this.button3Sigma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3Sigma.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3Sigma.Enabled = false;
            this.button3Sigma.FlatAppearance.BorderSize = 0;
            this.button3Sigma.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.button3Sigma.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button3Sigma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3Sigma.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3Sigma.ForeColor = System.Drawing.Color.Black;
            this.button3Sigma.Image = ((System.Drawing.Image)(resources.GetObject("button3Sigma.Image")));
            this.button3Sigma.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3Sigma.Location = new System.Drawing.Point(436, 12);
            this.button3Sigma.Name = "button3Sigma";
            this.button3Sigma.Size = new System.Drawing.Size(130, 110);
            this.button3Sigma.TabIndex = 20;
            this.button3Sigma.Text = "3 Sigma";
            this.button3Sigma.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3Sigma.UseVisualStyleBackColor = false;
            this.button3Sigma.Click += new System.EventHandler(this.button8_Click);
            // 
            // buttonChiSqTest
            // 
            this.buttonChiSqTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(252)))));
            this.buttonChiSqTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonChiSqTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonChiSqTest.FlatAppearance.BorderSize = 0;
            this.buttonChiSqTest.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.buttonChiSqTest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonChiSqTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChiSqTest.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonChiSqTest.ForeColor = System.Drawing.Color.Black;
            this.buttonChiSqTest.Image = ((System.Drawing.Image)(resources.GetObject("buttonChiSqTest.Image")));
            this.buttonChiSqTest.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonChiSqTest.Location = new System.Drawing.Point(162, 239);
            this.buttonChiSqTest.Name = "buttonChiSqTest";
            this.buttonChiSqTest.Size = new System.Drawing.Size(130, 110);
            this.buttonChiSqTest.TabIndex = 21;
            this.buttonChiSqTest.Text = "Chi Square Test";
            this.buttonChiSqTest.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonChiSqTest.UseVisualStyleBackColor = false;
            this.buttonChiSqTest.Click += new System.EventHandler(this.buttonChiSqTest_Click);
            // 
            // buttonPCA
            // 
            this.buttonPCA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(252)))));
            this.buttonPCA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonPCA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPCA.FlatAppearance.BorderSize = 0;
            this.buttonPCA.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.buttonPCA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonPCA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPCA.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPCA.ForeColor = System.Drawing.Color.Black;
            this.buttonPCA.Image = ((System.Drawing.Image)(resources.GetObject("buttonPCA.Image")));
            this.buttonPCA.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonPCA.Location = new System.Drawing.Point(299, 239);
            this.buttonPCA.Name = "buttonPCA";
            this.buttonPCA.Size = new System.Drawing.Size(130, 110);
            this.buttonPCA.TabIndex = 22;
            this.buttonPCA.Text = "PCA";
            this.buttonPCA.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonPCA.UseVisualStyleBackColor = false;
            this.buttonPCA.Click += new System.EventHandler(this.buttonPCA_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar1.Location = new System.Drawing.Point(162, 128);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(131, 25);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 23;
            this.progressBar1.UseWaitCursor = true;
            // 
            // progressBar2
            // 
            this.progressBar2.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar2.Location = new System.Drawing.Point(300, 128);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(131, 25);
            this.progressBar2.TabIndex = 24;
            // 
            // progressBar3
            // 
            this.progressBar3.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar3.Location = new System.Drawing.Point(435, 128);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(131, 25);
            this.progressBar3.TabIndex = 25;
            // 
            // progressBar4
            // 
            this.progressBar4.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar4.Location = new System.Drawing.Point(163, 208);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(131, 25);
            this.progressBar4.TabIndex = 26;
            // 
            // progressBar5
            // 
            this.progressBar5.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar5.Location = new System.Drawing.Point(299, 208);
            this.progressBar5.Name = "progressBar5";
            this.progressBar5.Size = new System.Drawing.Size(131, 25);
            this.progressBar5.TabIndex = 27;
            // 
            // progressBar6
            // 
            this.progressBar6.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar6.Location = new System.Drawing.Point(435, 208);
            this.progressBar6.Name = "progressBar6";
            this.progressBar6.Size = new System.Drawing.Size(131, 25);
            this.progressBar6.TabIndex = 28;
            // 
            // InitialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(590, 367);
            this.Controls.Add(this.progressBar6);
            this.Controls.Add(this.progressBar5);
            this.Controls.Add(this.progressBar4);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonPCA);
            this.Controls.Add(this.buttonChiSqTest);
            this.Controls.Add(this.button3Sigma);
            this.Controls.Add(this.buttonCheckForVirus);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonCriticalDiff);
            this.Controls.Add(this.buttonAnova);
            this.Controls.Add(this.buttonScanAndMon);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InitialForm";
            this.Opacity = 0.9D;
            this.Text = "Intrusion Detection System";
            this.Load += new System.EventHandler(this.InitialForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.Timer timer1;
        
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button buttonScanAndMon;
        private System.Windows.Forms.Button buttonAnova;
        private System.Windows.Forms.Button buttonCriticalDiff;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCheckForVirus;
        private System.Windows.Forms.Button button3Sigma;
        private System.Windows.Forms.Button buttonChiSqTest;
        private System.Windows.Forms.Button buttonPCA;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.ProgressBar progressBar4;
        private System.Windows.Forms.ProgressBar progressBar5;
        private System.Windows.Forms.ProgressBar progressBar6;
    }
}