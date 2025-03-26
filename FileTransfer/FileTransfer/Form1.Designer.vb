<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.lblステータス = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.btnテスト = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UserControl2 = New FileTransfer.UserControl2()
        Me.UserControl1 = New FileTransfer.UserControl1()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.処理DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.結果DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.変更前ファイル名DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.変更後ファイル名DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.日時DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TTFDFileTransferLogBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.HINODEDBDataSet = New FileTransfer.HINODEDBDataSet()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.T_TF_D_FileTransferLogTableAdapter = New FileTransfer.HINODEDBDataSetTableAdapters.T_TF_D_FileTransferLogTableAdapter()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TTFDFileTransferLogBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HINODEDBDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblステータス
        '
        Me.lblステータス.AutoSize = True
        Me.lblステータス.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblステータス.Location = New System.Drawing.Point(40, 31)
        Me.lblステータス.Name = "lblステータス"
        Me.lblステータス.Size = New System.Drawing.Size(164, 48)
        Me.lblステータス.TabIndex = 6
        Me.lblステータス.Text = "停止中"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.Button1.Location = New System.Drawing.Point(764, 31)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(83, 19)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "開始"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(743, 34)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 22
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CheckBox2)
        Me.Panel1.Controls.Add(Me.btnテスト)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.lblステータス)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(986, 107)
        Me.Panel1.TabIndex = 23
        '
        'CheckBox2
        '
        Me.CheckBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(866, 74)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(72, 16)
        Me.CheckBox2.TabIndex = 26
        Me.CheckBox2.Text = "自動更新"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'btnテスト
        '
        Me.btnテスト.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnテスト.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.btnテスト.Location = New System.Drawing.Point(636, 31)
        Me.btnテスト.Name = "btnテスト"
        Me.btnテスト.Size = New System.Drawing.Size(83, 19)
        Me.btnテスト.TabIndex = 25
        Me.btnテスト.Text = "TEST"
        Me.btnテスト.UseVisualStyleBackColor = True
        Me.btnテスト.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.Button3.Location = New System.Drawing.Point(866, 31)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(83, 19)
        Me.Button3.TabIndex = 24
        Me.Button3.Text = "設定"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.Button2.Location = New System.Drawing.Point(764, 72)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(83, 19)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "ログ更新"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UserControl2)
        Me.Panel2.Controls.Add(Me.UserControl1)
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 107)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(986, 406)
        Me.Panel2.TabIndex = 24
        '
        'UserControl2
        '
        Me.UserControl2.BackColor = System.Drawing.Color.OrangeRed
        Me.UserControl2.Location = New System.Drawing.Point(12, 20)
        Me.UserControl2.Name = "UserControl2"
        Me.UserControl2.Size = New System.Drawing.Size(15, 362)
        Me.UserControl2.TabIndex = 1
        '
        'UserControl1
        '
        Me.UserControl1.BackColor = System.Drawing.SystemColors.Highlight
        Me.UserControl1.Location = New System.Drawing.Point(958, 20)
        Me.UserControl1.Name = "UserControl1"
        Me.UserControl1.Size = New System.Drawing.Size(16, 362)
        Me.UserControl1.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.処理DataGridViewTextBoxColumn, Me.結果DataGridViewTextBoxColumn, Me.変更前ファイル名DataGridViewTextBoxColumn, Me.変更後ファイル名DataGridViewTextBoxColumn, Me.日時DataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.TTFDFileTransferLogBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(5, 5)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 30
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(976, 366)
        Me.DataGridView1.TabIndex = 2
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.Width = 60
        '
        '処理DataGridViewTextBoxColumn
        '
        Me.処理DataGridViewTextBoxColumn.DataPropertyName = "処理"
        Me.処理DataGridViewTextBoxColumn.HeaderText = "処理"
        Me.処理DataGridViewTextBoxColumn.Name = "処理DataGridViewTextBoxColumn"
        Me.処理DataGridViewTextBoxColumn.ReadOnly = True
        Me.処理DataGridViewTextBoxColumn.Width = 60
        '
        '結果DataGridViewTextBoxColumn
        '
        Me.結果DataGridViewTextBoxColumn.DataPropertyName = "結果"
        Me.結果DataGridViewTextBoxColumn.HeaderText = "結果"
        Me.結果DataGridViewTextBoxColumn.Name = "結果DataGridViewTextBoxColumn"
        Me.結果DataGridViewTextBoxColumn.ReadOnly = True
        Me.結果DataGridViewTextBoxColumn.Width = 60
        '
        '変更前ファイル名DataGridViewTextBoxColumn
        '
        Me.変更前ファイル名DataGridViewTextBoxColumn.DataPropertyName = "変更前ファイル名"
        Me.変更前ファイル名DataGridViewTextBoxColumn.HeaderText = "変更前ファイル名"
        Me.変更前ファイル名DataGridViewTextBoxColumn.Name = "変更前ファイル名DataGridViewTextBoxColumn"
        Me.変更前ファイル名DataGridViewTextBoxColumn.ReadOnly = True
        Me.変更前ファイル名DataGridViewTextBoxColumn.Width = 250
        '
        '変更後ファイル名DataGridViewTextBoxColumn
        '
        Me.変更後ファイル名DataGridViewTextBoxColumn.DataPropertyName = "変更後ファイル名"
        Me.変更後ファイル名DataGridViewTextBoxColumn.HeaderText = "変更後ファイル名"
        Me.変更後ファイル名DataGridViewTextBoxColumn.Name = "変更後ファイル名DataGridViewTextBoxColumn"
        Me.変更後ファイル名DataGridViewTextBoxColumn.ReadOnly = True
        Me.変更後ファイル名DataGridViewTextBoxColumn.Width = 400
        '
        '日時DataGridViewTextBoxColumn
        '
        Me.日時DataGridViewTextBoxColumn.DataPropertyName = "日時"
        Me.日時DataGridViewTextBoxColumn.HeaderText = "日時"
        Me.日時DataGridViewTextBoxColumn.Name = "日時DataGridViewTextBoxColumn"
        Me.日時DataGridViewTextBoxColumn.ReadOnly = True
        '
        'TTFDFileTransferLogBindingSource
        '
        Me.TTFDFileTransferLogBindingSource.DataMember = "T_TF_D_FileTransferLog"
        Me.TTFDFileTransferLogBindingSource.DataSource = Me.HINODEDBDataSet
        '
        'HINODEDBDataSet
        '
        Me.HINODEDBDataSet.DataSetName = "HINODEDBDataSet"
        Me.HINODEDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(5, 375)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(976, 19)
        Me.TextBox1.TabIndex = 3
        '
        'T_TF_D_FileTransferLogTableAdapter
        '
        Me.T_TF_D_FileTransferLogTableAdapter.ClearBeforeFill = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(986, 513)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "FileTransfer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TTFDFileTransferLogBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HINODEDBDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblステータス As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents UserControl2 As UserControl2
    Friend WithEvents UserControl1 As UserControl1
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents HINODEDBDataSet As HINODEDBDataSet
    Friend WithEvents TTFDFileTransferLogBindingSource As BindingSource
    Friend WithEvents T_TF_D_FileTransferLogTableAdapter As HINODEDBDataSetTableAdapters.T_TF_D_FileTransferLogTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 処理DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 結果DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 変更前ファイル名DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 変更後ファイル名DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 日時DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents btnテスト As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CheckBox2 As CheckBox
End Class
