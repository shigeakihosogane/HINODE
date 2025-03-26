namespace FileTransfer2
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl状況 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnRun = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txt転送1稼働時間 = new System.Windows.Forms.TextBox();
            this.chk転送1 = new System.Windows.Forms.CheckBox();
            this.chk転送2 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt転送2稼働時間 = new System.Windows.Forms.TextBox();
            this.lbl表示 = new System.Windows.Forms.Label();
            this.metroCheckBox1 = new MetroFramework.Controls.MetroCheckBox();
            this.metroTrackBar1 = new MetroFramework.Controls.MetroTrackBar();
            this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
            this.metroDateTime1 = new MetroFramework.Controls.MetroDateTime();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.htmlPanel1 = new MetroFramework.Drawing.Html.HtmlPanel();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.metroListView1 = new MetroFramework.Controls.MetroListView();
            this.metroToggle1 = new MetroFramework.Controls.MetroToggle();
            this.SuspendLayout();
            // 
            // lbl状況
            // 
            this.lbl状況.Font = new System.Drawing.Font("Meiryo UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl状況.Location = new System.Drawing.Point(639, 101);
            this.lbl状況.Name = "lbl状況";
            this.lbl状況.Size = new System.Drawing.Size(400, 50);
            this.lbl状況.TabIndex = 4;
            this.lbl状況.Text = "停止中";
            this.lbl状況.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(480, 18);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(181, 50);
            this.btnRun.TabIndex = 6;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(203, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "稼働時間";
            // 
            // txt転送1稼働時間
            // 
            this.txt転送1稼働時間.Location = new System.Drawing.Point(205, 129);
            this.txt転送1稼働時間.Name = "txt転送1稼働時間";
            this.txt転送1稼働時間.Size = new System.Drawing.Size(164, 19);
            this.txt転送1稼働時間.TabIndex = 13;
            // 
            // chk転送1
            // 
            this.chk転送1.AutoSize = true;
            this.chk転送1.Location = new System.Drawing.Point(76, 129);
            this.chk転送1.Name = "chk転送1";
            this.chk転送1.Size = new System.Drawing.Size(54, 16);
            this.chk転送1.TabIndex = 15;
            this.chk転送1.Text = "転送1";
            this.chk転送1.UseVisualStyleBackColor = true;
            this.chk転送1.CheckedChanged += new System.EventHandler(this.chk転送1_CheckedChanged);
            // 
            // chk転送2
            // 
            this.chk転送2.AutoSize = true;
            this.chk転送2.Location = new System.Drawing.Point(76, 186);
            this.chk転送2.Name = "chk転送2";
            this.chk転送2.Size = new System.Drawing.Size(54, 16);
            this.chk転送2.TabIndex = 26;
            this.chk転送2.Text = "転送2";
            this.chk転送2.UseVisualStyleBackColor = true;
            this.chk転送2.CheckedChanged += new System.EventHandler(this.chk転送2_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(203, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "稼働時間";
            // 
            // txt転送2稼働時間
            // 
            this.txt転送2稼働時間.Location = new System.Drawing.Point(205, 183);
            this.txt転送2稼働時間.Name = "txt転送2稼働時間";
            this.txt転送2稼働時間.Size = new System.Drawing.Size(164, 19);
            this.txt転送2稼働時間.TabIndex = 24;
            // 
            // lbl表示
            // 
            this.lbl表示.Location = new System.Drawing.Point(45, 506);
            this.lbl表示.Name = "lbl表示";
            this.lbl表示.Size = new System.Drawing.Size(830, 81);
            this.lbl表示.TabIndex = 27;
            this.lbl表示.Text = "表示";
            // 
            // metroCheckBox1
            // 
            this.metroCheckBox1.AutoSize = true;
            this.metroCheckBox1.Location = new System.Drawing.Point(76, 262);
            this.metroCheckBox1.Name = "metroCheckBox1";
            this.metroCheckBox1.Size = new System.Drawing.Size(114, 15);
            this.metroCheckBox1.TabIndex = 28;
            this.metroCheckBox1.Text = "metroCheckBox1";
            this.metroCheckBox1.UseSelectable = true;
            // 
            // metroTrackBar1
            // 
            this.metroTrackBar1.BackColor = System.Drawing.Color.Transparent;
            this.metroTrackBar1.Location = new System.Drawing.Point(76, 330);
            this.metroTrackBar1.Name = "metroTrackBar1";
            this.metroTrackBar1.Size = new System.Drawing.Size(75, 23);
            this.metroTrackBar1.TabIndex = 29;
            this.metroTrackBar1.Text = "metroTrackBar1";
            // 
            // metroRadioButton1
            // 
            this.metroRadioButton1.AutoSize = true;
            this.metroRadioButton1.Location = new System.Drawing.Point(186, 338);
            this.metroRadioButton1.Name = "metroRadioButton1";
            this.metroRadioButton1.Size = new System.Drawing.Size(127, 15);
            this.metroRadioButton1.TabIndex = 30;
            this.metroRadioButton1.Text = "metroRadioButton1";
            this.metroRadioButton1.UseSelectable = true;
            // 
            // metroDateTime1
            // 
            this.metroDateTime1.Location = new System.Drawing.Point(407, 331);
            this.metroDateTime1.MinimumSize = new System.Drawing.Size(0, 29);
            this.metroDateTime1.Name = "metroDateTime1";
            this.metroDateTime1.Size = new System.Drawing.Size(200, 29);
            this.metroDateTime1.TabIndex = 31;
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Location = new System.Drawing.Point(673, 338);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(121, 29);
            this.metroComboBox1.TabIndex = 32;
            this.metroComboBox1.UseSelectable = true;
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.AutoScrollMinSize = new System.Drawing.Size(574, 18);
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel1.Location = new System.Drawing.Point(252, 416);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(574, 128);
            this.htmlPanel1.TabIndex = 33;
            this.htmlPanel1.Text = "htmlPanel1";
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Location = new System.Drawing.Point(839, 293);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.Size = new System.Drawing.Size(200, 193);
            this.metroTabControl1.TabIndex = 34;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(407, 262);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(254, 23);
            this.metroTile1.TabIndex = 35;
            this.metroTile1.Text = "metroTile1";
            this.metroTile1.UseSelectable = true;
            // 
            // metroListView1
            // 
            this.metroListView1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.metroListView1.FullRowSelect = true;
            this.metroListView1.Location = new System.Drawing.Point(705, 186);
            this.metroListView1.Name = "metroListView1";
            this.metroListView1.OwnerDraw = true;
            this.metroListView1.Size = new System.Drawing.Size(219, 97);
            this.metroListView1.TabIndex = 36;
            this.metroListView1.UseCompatibleStateImageBehavior = false;
            this.metroListView1.UseSelectable = true;
            // 
            // metroToggle1
            // 
            this.metroToggle1.AutoSize = true;
            this.metroToggle1.Location = new System.Drawing.Point(71, 391);
            this.metroToggle1.Name = "metroToggle1";
            this.metroToggle1.Size = new System.Drawing.Size(80, 16);
            this.metroToggle1.TabIndex = 37;
            this.metroToggle1.Text = "Off";
            this.metroToggle1.UseSelectable = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 689);
            this.Controls.Add(this.metroToggle1);
            this.Controls.Add(this.metroListView1);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.htmlPanel1);
            this.Controls.Add(this.metroComboBox1);
            this.Controls.Add(this.metroDateTime1);
            this.Controls.Add(this.metroRadioButton1);
            this.Controls.Add(this.metroTrackBar1);
            this.Controls.Add(this.metroCheckBox1);
            this.Controls.Add(this.lbl表示);
            this.Controls.Add(this.chk転送2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt転送2稼働時間);
            this.Controls.Add(this.chk転送1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt転送1稼働時間);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lbl状況);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl状況;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt転送1稼働時間;
        private System.Windows.Forms.CheckBox chk転送1;
        private System.Windows.Forms.CheckBox chk転送2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt転送2稼働時間;
        private System.Windows.Forms.Label lbl表示;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox1;
        private MetroFramework.Controls.MetroTrackBar metroTrackBar1;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
        private MetroFramework.Controls.MetroDateTime metroDateTime1;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private MetroFramework.Drawing.Html.HtmlPanel htmlPanel1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroListView metroListView1;
        private MetroFramework.Controls.MetroToggle metroToggle1;
    }
}

