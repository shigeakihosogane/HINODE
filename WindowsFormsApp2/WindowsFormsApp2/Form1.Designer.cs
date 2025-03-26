namespace WindowsFormsApp2
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listSystemLog = new System.Windows.Forms.DataGridView();
            this.lbl転送1_状況 = new System.Windows.Forms.Label();
            this.lbl転送2_状況 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tgl転送2_開始 = new MetroFramework.Controls.MetroToggle();
            this.tgl転送1_開始 = new MetroFramework.Controls.MetroToggle();
            this.lbl転送2_稼働時間 = new System.Windows.Forms.Label();
            this.lbl転送2_開始時間 = new System.Windows.Forms.Label();
            this.lbl転送1_稼働時間 = new System.Windows.Forms.Label();
            this.lbl転送1_開始時間 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtFull表示 = new System.Windows.Forms.TextBox();
            this.btnログ更新 = new System.Windows.Forms.Button();
            this.listTranceferLog = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn転送2_参照_戻し = new System.Windows.Forms.Button();
            this.btn転送2_参照_転送 = new System.Windows.Forms.Button();
            this.btn転送2_参照_監視 = new System.Windows.Forms.Button();
            this.btn転送1_参照_戻し = new System.Windows.Forms.Button();
            this.btn転送1_参照_転送 = new System.Windows.Forms.Button();
            this.btn転送1_参照_監視 = new System.Windows.Forms.Button();
            this.txtログ_表示件数 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txt転送2_間隔 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txt転送2_戻し = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt転送2_転送 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt転送2_監視 = new System.Windows.Forms.TextBox();
            this.btn設定保存 = new System.Windows.Forms.Button();
            this.txt転送1_間隔 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txt転送1_戻し = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt転送1_転送 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt転送1_監視 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt経過日数 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnアーカイブ先_参照 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtアーカイブ先 = new System.Windows.Forms.TextBox();
            this.BtnTest = new MetroFramework.Controls.MetroButton();
            this.panelMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listSystemLog)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listTranceferLog)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panelMenu.Controls.Add(this.button3);
            this.panelMenu.Controls.Add(this.button2);
            this.panelMenu.Controls.Add(this.button1);
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(240, 565);
            this.panelMenu.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(0, 272);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(240, 40);
            this.button3.TabIndex = 2;
            this.button3.Text = "設定";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(0, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(240, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "ログ";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(0, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "ホーム";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.BtnTest);
            this.panel1.Controls.Add(this.listSystemLog);
            this.panel1.Controls.Add(this.lbl転送1_状況);
            this.panel1.Controls.Add(this.lbl転送2_状況);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tgl転送2_開始);
            this.panel1.Controls.Add(this.tgl転送1_開始);
            this.panel1.Controls.Add(this.lbl転送2_稼働時間);
            this.panel1.Controls.Add(this.lbl転送2_開始時間);
            this.panel1.Controls.Add(this.lbl転送1_稼働時間);
            this.panel1.Controls.Add(this.lbl転送1_開始時間);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(240, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 560);
            this.panel1.TabIndex = 1;
            // 
            // listSystemLog
            // 
            this.listSystemLog.AllowUserToAddRows = false;
            this.listSystemLog.AllowUserToDeleteRows = false;
            this.listSystemLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSystemLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listSystemLog.Location = new System.Drawing.Point(140, 396);
            this.listSystemLog.Name = "listSystemLog";
            this.listSystemLog.ReadOnly = true;
            this.listSystemLog.RowTemplate.Height = 21;
            this.listSystemLog.Size = new System.Drawing.Size(672, 150);
            this.listSystemLog.TabIndex = 17;
            // 
            // lbl転送1_状況
            // 
            this.lbl転送1_状況.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl転送1_状況.Location = new System.Drawing.Point(275, 127);
            this.lbl転送1_状況.Name = "lbl転送1_状況";
            this.lbl転送1_状況.Size = new System.Drawing.Size(117, 29);
            this.lbl転送1_状況.TabIndex = 15;
            this.lbl転送1_状況.Text = "転送1";
            // 
            // lbl転送2_状況
            // 
            this.lbl転送2_状況.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl転送2_状況.Location = new System.Drawing.Point(275, 265);
            this.lbl転送2_状況.Name = "lbl転送2_状況";
            this.lbl転送2_状況.Size = new System.Drawing.Size(117, 29);
            this.lbl転送2_状況.TabIndex = 14;
            this.lbl転送2_状況.Text = "転送2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(65, 396);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "ログ";
            // 
            // tgl転送2_開始
            // 
            this.tgl転送2_開始.AutoSize = true;
            this.tgl転送2_開始.Location = new System.Drawing.Point(140, 261);
            this.tgl転送2_開始.Name = "tgl転送2_開始";
            this.tgl転送2_開始.Size = new System.Drawing.Size(80, 16);
            this.tgl転送2_開始.TabIndex = 11;
            this.tgl転送2_開始.Text = "Off";
            this.tgl転送2_開始.UseSelectable = true;
            this.tgl転送2_開始.CheckedChanged += new System.EventHandler(this.tgl転送2_開始_CheckedChanged);
            // 
            // tgl転送1_開始
            // 
            this.tgl転送1_開始.AutoSize = true;
            this.tgl転送1_開始.Location = new System.Drawing.Point(140, 123);
            this.tgl転送1_開始.Name = "tgl転送1_開始";
            this.tgl転送1_開始.Size = new System.Drawing.Size(80, 16);
            this.tgl転送1_開始.TabIndex = 10;
            this.tgl転送1_開始.Text = "Off";
            this.tgl転送1_開始.UseSelectable = true;
            this.tgl転送1_開始.CheckedChanged += new System.EventHandler(this.tgl転送1_開始_CheckedChanged);
            // 
            // lbl転送2_稼働時間
            // 
            this.lbl転送2_稼働時間.AutoSize = true;
            this.lbl転送2_稼働時間.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl転送2_稼働時間.Location = new System.Drawing.Point(468, 311);
            this.lbl転送2_稼働時間.Name = "lbl転送2_稼働時間";
            this.lbl転送2_稼働時間.Size = new System.Drawing.Size(55, 15);
            this.lbl転送2_稼働時間.TabIndex = 9;
            this.lbl転送2_稼働時間.Text = "稼働時間";
            // 
            // lbl転送2_開始時間
            // 
            this.lbl転送2_開始時間.AutoSize = true;
            this.lbl転送2_開始時間.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl転送2_開始時間.Location = new System.Drawing.Point(468, 265);
            this.lbl転送2_開始時間.Name = "lbl転送2_開始時間";
            this.lbl転送2_開始時間.Size = new System.Drawing.Size(55, 15);
            this.lbl転送2_開始時間.TabIndex = 8;
            this.lbl転送2_開始時間.Text = "開始時間";
            // 
            // lbl転送1_稼働時間
            // 
            this.lbl転送1_稼働時間.AutoSize = true;
            this.lbl転送1_稼働時間.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl転送1_稼働時間.Location = new System.Drawing.Point(468, 168);
            this.lbl転送1_稼働時間.Name = "lbl転送1_稼働時間";
            this.lbl転送1_稼働時間.Size = new System.Drawing.Size(55, 15);
            this.lbl転送1_稼働時間.TabIndex = 7;
            this.lbl転送1_稼働時間.Text = "稼働時間";
            // 
            // lbl転送1_開始時間
            // 
            this.lbl転送1_開始時間.AutoSize = true;
            this.lbl転送1_開始時間.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl転送1_開始時間.Location = new System.Drawing.Point(468, 127);
            this.lbl転送1_開始時間.Name = "lbl転送1_開始時間";
            this.lbl転送1_開始時間.Size = new System.Drawing.Size(55, 15);
            this.lbl転送1_開始時間.TabIndex = 6;
            this.lbl転送1_開始時間.Text = "開始時間";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(138, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "取込スキャン転送";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(138, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "FAX保管転送";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "転送2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "転送1";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.txtFull表示);
            this.panel2.Controls.Add(this.btnログ更新);
            this.panel2.Controls.Add(this.listTranceferLog);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(1111, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(40, 560);
            this.panel2.TabIndex = 2;
            // 
            // txtFull表示
            // 
            this.txtFull表示.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFull表示.Enabled = false;
            this.txtFull表示.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFull表示.Location = new System.Drawing.Point(23, 526);
            this.txtFull表示.Name = "txtFull表示";
            this.txtFull表示.Size = new System.Drawing.Size(0, 23);
            this.txtFull表示.TabIndex = 12;
            // 
            // btnログ更新
            // 
            this.btnログ更新.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnログ更新.Location = new System.Drawing.Point(784, 37);
            this.btnログ更新.Name = "btnログ更新";
            this.btnログ更新.Size = new System.Drawing.Size(75, 23);
            this.btnログ更新.TabIndex = 3;
            this.btnログ更新.Text = "ログ更新";
            this.btnログ更新.UseVisualStyleBackColor = true;
            this.btnログ更新.Click += new System.EventHandler(this.btnログ更新_Click);
            // 
            // listTranceferLog
            // 
            this.listTranceferLog.AllowUserToAddRows = false;
            this.listTranceferLog.AllowUserToDeleteRows = false;
            this.listTranceferLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listTranceferLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listTranceferLog.Location = new System.Drawing.Point(23, 67);
            this.listTranceferLog.Name = "listTranceferLog";
            this.listTranceferLog.ReadOnly = true;
            this.listTranceferLog.RowTemplate.Height = 21;
            this.listTranceferLog.Size = new System.Drawing.Size(0, 439);
            this.listTranceferLog.TabIndex = 2;
            this.listTranceferLog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listTranceferLog_CellClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(21, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "転送ログ";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Info;
            this.panel3.Controls.Add(this.btnアーカイブ先_参照);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtアーカイブ先);
            this.panel3.Controls.Add(this.txt経過日数);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.btn転送2_参照_戻し);
            this.panel3.Controls.Add(this.btn転送2_参照_転送);
            this.panel3.Controls.Add(this.btn転送2_参照_監視);
            this.panel3.Controls.Add(this.btn転送1_参照_戻し);
            this.panel3.Controls.Add(this.btn転送1_参照_転送);
            this.panel3.Controls.Add(this.btn転送1_参照_監視);
            this.panel3.Controls.Add(this.txtログ_表示件数);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.txt転送2_間隔);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.txt転送2_戻し);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.txt転送2_転送);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.txt転送2_監視);
            this.panel3.Controls.Add(this.btn設定保存);
            this.panel3.Controls.Add(this.txt転送1_間隔);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.txt転送1_戻し);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.txt転送1_転送);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.txt転送1_監視);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Location = new System.Drawing.Point(1157, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(24, 560);
            this.panel3.TabIndex = 2;
            // 
            // btn転送2_参照_戻し
            // 
            this.btn転送2_参照_戻し.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn転送2_参照_戻し.Location = new System.Drawing.Point(696, 242);
            this.btn転送2_参照_戻し.Name = "btn転送2_参照_戻し";
            this.btn転送2_参照_戻し.Size = new System.Drawing.Size(42, 19);
            this.btn転送2_参照_戻し.TabIndex = 27;
            this.btn転送2_参照_戻し.Text = "参照";
            this.btn転送2_参照_戻し.UseVisualStyleBackColor = true;
            this.btn転送2_参照_戻し.Click += new System.EventHandler(this.btn転送2_参照_戻し_Click);
            // 
            // btn転送2_参照_転送
            // 
            this.btn転送2_参照_転送.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn転送2_参照_転送.Location = new System.Drawing.Point(696, 211);
            this.btn転送2_参照_転送.Name = "btn転送2_参照_転送";
            this.btn転送2_参照_転送.Size = new System.Drawing.Size(42, 19);
            this.btn転送2_参照_転送.TabIndex = 26;
            this.btn転送2_参照_転送.Text = "参照";
            this.btn転送2_参照_転送.UseVisualStyleBackColor = true;
            this.btn転送2_参照_転送.Click += new System.EventHandler(this.btn転送2_参照_転送_Click);
            // 
            // btn転送2_参照_監視
            // 
            this.btn転送2_参照_監視.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn転送2_参照_監視.Location = new System.Drawing.Point(696, 183);
            this.btn転送2_参照_監視.Name = "btn転送2_参照_監視";
            this.btn転送2_参照_監視.Size = new System.Drawing.Size(42, 19);
            this.btn転送2_参照_監視.TabIndex = 25;
            this.btn転送2_参照_監視.Text = "参照";
            this.btn転送2_参照_監視.UseVisualStyleBackColor = true;
            this.btn転送2_参照_監視.Click += new System.EventHandler(this.btn転送2_参照_監視_Click);
            // 
            // btn転送1_参照_戻し
            // 
            this.btn転送1_参照_戻し.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn転送1_参照_戻し.Location = new System.Drawing.Point(696, 102);
            this.btn転送1_参照_戻し.Name = "btn転送1_参照_戻し";
            this.btn転送1_参照_戻し.Size = new System.Drawing.Size(42, 19);
            this.btn転送1_参照_戻し.TabIndex = 24;
            this.btn転送1_参照_戻し.Text = "参照";
            this.btn転送1_参照_戻し.UseVisualStyleBackColor = true;
            this.btn転送1_参照_戻し.Click += new System.EventHandler(this.btn転送1_参照_戻し_Click);
            // 
            // btn転送1_参照_転送
            // 
            this.btn転送1_参照_転送.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn転送1_参照_転送.Location = new System.Drawing.Point(696, 75);
            this.btn転送1_参照_転送.Name = "btn転送1_参照_転送";
            this.btn転送1_参照_転送.Size = new System.Drawing.Size(42, 19);
            this.btn転送1_参照_転送.TabIndex = 23;
            this.btn転送1_参照_転送.Text = "参照";
            this.btn転送1_参照_転送.UseVisualStyleBackColor = true;
            this.btn転送1_参照_転送.Click += new System.EventHandler(this.btn転送1_参照_転送_Click);
            // 
            // btn転送1_参照_監視
            // 
            this.btn転送1_参照_監視.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn転送1_参照_監視.Location = new System.Drawing.Point(696, 46);
            this.btn転送1_参照_監視.Name = "btn転送1_参照_監視";
            this.btn転送1_参照_監視.Size = new System.Drawing.Size(42, 19);
            this.btn転送1_参照_監視.TabIndex = 22;
            this.btn転送1_参照_監視.Text = "参照";
            this.btn転送1_参照_監視.UseVisualStyleBackColor = true;
            this.btn転送1_参照_監視.Click += new System.EventHandler(this.btn転送1_参照_監視_Click);
            // 
            // txtログ_表示件数
            // 
            this.txtログ_表示件数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtログ_表示件数.Location = new System.Drawing.Point(316, 326);
            this.txtログ_表示件数.Name = "txtログ_表示件数";
            this.txtログ_表示件数.Size = new System.Drawing.Size(42, 23);
            this.txtログ_表示件数.TabIndex = 21;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label24.Location = new System.Drawing.Point(155, 329);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(55, 15);
            this.label24.TabIndex = 20;
            this.label24.Text = "表示件数";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label23.Location = new System.Drawing.Point(52, 306);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(25, 15);
            this.label23.TabIndex = 19;
            this.label23.Text = "ログ";
            // 
            // txt転送2_間隔
            // 
            this.txt転送2_間隔.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt転送2_間隔.Location = new System.Drawing.Point(316, 268);
            this.txt転送2_間隔.Name = "txt転送2_間隔";
            this.txt転送2_間隔.Size = new System.Drawing.Size(42, 23);
            this.txt転送2_間隔.TabIndex = 18;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.Location = new System.Drawing.Point(156, 271);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 15);
            this.label19.TabIndex = 17;
            this.label19.Text = "間隔(秒)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.Location = new System.Drawing.Point(155, 244);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(114, 15);
            this.label20.TabIndex = 16;
            this.label20.Text = "エラー時戻し先フォルダ";
            // 
            // txt転送2_戻し
            // 
            this.txt転送2_戻し.Enabled = false;
            this.txt転送2_戻し.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt転送2_戻し.Location = new System.Drawing.Point(316, 239);
            this.txt転送2_戻し.Name = "txt転送2_戻し";
            this.txt転送2_戻し.Size = new System.Drawing.Size(371, 23);
            this.txt転送2_戻し.TabIndex = 15;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label21.Location = new System.Drawing.Point(155, 213);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(126, 15);
            this.label21.TabIndex = 14;
            this.label21.Text = "成功時通常保管フォルダ";
            // 
            // txt転送2_転送
            // 
            this.txt転送2_転送.Enabled = false;
            this.txt転送2_転送.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt転送2_転送.Location = new System.Drawing.Point(316, 209);
            this.txt転送2_転送.Name = "txt転送2_転送";
            this.txt転送2_転送.Size = new System.Drawing.Size(371, 23);
            this.txt転送2_転送.TabIndex = 13;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label22.Location = new System.Drawing.Point(156, 183);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 15);
            this.label22.TabIndex = 12;
            this.label22.Text = "監視フォルダ";
            // 
            // txt転送2_監視
            // 
            this.txt転送2_監視.Enabled = false;
            this.txt転送2_監視.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt転送2_監視.Location = new System.Drawing.Point(316, 180);
            this.txt転送2_監視.Name = "txt転送2_監視";
            this.txt転送2_監視.Size = new System.Drawing.Size(371, 23);
            this.txt転送2_監視.TabIndex = 11;
            // 
            // btn設定保存
            // 
            this.btn設定保存.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn設定保存.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn設定保存.Location = new System.Drawing.Point(-130, 504);
            this.btn設定保存.Name = "btn設定保存";
            this.btn設定保存.Size = new System.Drawing.Size(75, 23);
            this.btn設定保存.TabIndex = 10;
            this.btn設定保存.Text = "保存";
            this.btn設定保存.UseVisualStyleBackColor = true;
            this.btn設定保存.Click += new System.EventHandler(this.btn設定保存_Click);
            // 
            // txt転送1_間隔
            // 
            this.txt転送1_間隔.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt転送1_間隔.Location = new System.Drawing.Point(316, 130);
            this.txt転送1_間隔.Name = "txt転送1_間隔";
            this.txt転送1_間隔.Size = new System.Drawing.Size(42, 23);
            this.txt転送1_間隔.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(155, 133);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 15);
            this.label18.TabIndex = 8;
            this.label18.Text = "間隔(秒)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.Location = new System.Drawing.Point(155, 106);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(114, 15);
            this.label17.TabIndex = 7;
            this.label17.Text = "エラー時戻し先フォルダ";
            // 
            // txt転送1_戻し
            // 
            this.txt転送1_戻し.Enabled = false;
            this.txt転送1_戻し.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt転送1_戻し.Location = new System.Drawing.Point(316, 101);
            this.txt転送1_戻し.Name = "txt転送1_戻し";
            this.txt転送1_戻し.Size = new System.Drawing.Size(371, 23);
            this.txt転送1_戻し.TabIndex = 6;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.Location = new System.Drawing.Point(155, 77);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(126, 15);
            this.label16.TabIndex = 5;
            this.label16.Text = "成功時通常保管フォルダ";
            // 
            // txt転送1_転送
            // 
            this.txt転送1_転送.Enabled = false;
            this.txt転送1_転送.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt転送1_転送.Location = new System.Drawing.Point(316, 72);
            this.txt転送1_転送.Name = "txt転送1_転送";
            this.txt転送1_転送.Size = new System.Drawing.Size(371, 23);
            this.txt転送1_転送.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.Location = new System.Drawing.Point(156, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 15);
            this.label15.TabIndex = 3;
            this.label15.Text = "監視フォルダ";
            // 
            // txt転送1_監視
            // 
            this.txt転送1_監視.Enabled = false;
            this.txt転送1_監視.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt転送1_監視.Location = new System.Drawing.Point(316, 43);
            this.txt転送1_監視.Name = "txt転送1_監視";
            this.txt転送1_監視.Size = new System.Drawing.Size(371, 23);
            this.txt転送1_監視.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(52, 160);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 15);
            this.label14.TabIndex = 1;
            this.label14.Text = "転送2";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(52, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 15);
            this.label13.TabIndex = 0;
            this.label13.Text = "転送1";
            // 
            // txt経過日数
            // 
            this.txt経過日数.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt経過日数.Location = new System.Drawing.Point(316, 459);
            this.txt経過日数.Name = "txt経過日数";
            this.txt経過日数.Size = new System.Drawing.Size(42, 23);
            this.txt経過日数.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(155, 462);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "〇〇日経過後";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(52, 411);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 28;
            this.label7.Text = "アーカイブ";
            // 
            // btnアーカイブ先_参照
            // 
            this.btnアーカイブ先_参照.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnアーカイブ先_参照.Location = new System.Drawing.Point(696, 433);
            this.btnアーカイブ先_参照.Name = "btnアーカイブ先_参照";
            this.btnアーカイブ先_参照.Size = new System.Drawing.Size(42, 19);
            this.btnアーカイブ先_参照.TabIndex = 33;
            this.btnアーカイブ先_参照.Text = "参照";
            this.btnアーカイブ先_参照.UseVisualStyleBackColor = true;
            this.btnアーカイブ先_参照.Click += new System.EventHandler(this.btnアーカイブ先_参照_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(155, 435);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "アーカイブ先フォルダ";
            // 
            // txtアーカイブ先
            // 
            this.txtアーカイブ先.Enabled = false;
            this.txtアーカイブ先.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtアーカイブ先.Location = new System.Drawing.Point(316, 430);
            this.txtアーカイブ先.Name = "txtアーカイブ先";
            this.txtアーカイブ先.Size = new System.Drawing.Size(371, 23);
            this.txtアーカイブ先.TabIndex = 31;
            // 
            // BtnTest
            // 
            this.BtnTest.Location = new System.Drawing.Point(674, 151);
            this.BtnTest.Name = "BtnTest";
            this.BtnTest.Size = new System.Drawing.Size(75, 23);
            this.BtnTest.TabIndex = 18;
            this.BtnTest.Text = "TEST";
            this.BtnTest.UseSelectable = true;
            this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 561);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelMenu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listSystemLog)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listTranceferLog)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl転送2_稼働時間;
        private System.Windows.Forms.Label lbl転送2_開始時間;
        private System.Windows.Forms.Label lbl転送1_稼働時間;
        private System.Windows.Forms.Label lbl転送1_開始時間;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt転送1_戻し;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt転送1_転送;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt転送1_監視;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt転送1_間隔;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtログ_表示件数;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt転送2_間隔;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt転送2_戻し;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt転送2_転送;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt転送2_監視;
        private System.Windows.Forms.Button btn設定保存;
        private MetroFramework.Controls.MetroToggle tgl転送2_開始;
        private MetroFramework.Controls.MetroToggle tgl転送1_開始;
        private System.Windows.Forms.Button btn転送2_参照_戻し;
        private System.Windows.Forms.Button btn転送2_参照_転送;
        private System.Windows.Forms.Button btn転送2_参照_監視;
        private System.Windows.Forms.Button btn転送1_参照_戻し;
        private System.Windows.Forms.Button btn転送1_参照_転送;
        private System.Windows.Forms.Button btn転送1_参照_監視;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl転送1_状況;
        private System.Windows.Forms.Label lbl転送2_状況;
        private System.Windows.Forms.DataGridView listSystemLog;
        private System.Windows.Forms.DataGridView listTranceferLog;
        private System.Windows.Forms.Button btnログ更新;
        private System.Windows.Forms.TextBox txtFull表示;
        private System.Windows.Forms.TextBox txt経過日数;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnアーカイブ先_参照;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtアーカイブ先;
        private MetroFramework.Controls.MetroButton BtnTest;
    }
}

