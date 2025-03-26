Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
'Imports Microsoft.SqlServer.Server

Public Class Form1

    'DBの接続文字列
    Dim connectionkey As String = "FileTransfer.My.MySettings.HINODEDBConnectionString"
    Dim settings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(connectionkey)
    Dim cnstr As String = settings.ConnectionString


    '転送1
    'Fax受信⇒ThereforDATA　（Therefor上にアップロードするリネーム処理）
    Dim kannsi1 As String = ""
    Dim saki1 As String = ""
    Dim moto1 As String = ""

    '転送1
    'scam⇒Fax受信　（スキャン時のリネーム処理）（取込ファイルのファイル名調整）
    Dim kannsi2 As String = ""
    Dim saki2 As String = ""
    Dim moto2 As String = ""

    '監視インターバル
    Dim interval As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: このコード行はデータを 'HINODEDBDataSet.T_TF_D_FileTransferLog' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
        Me.T_TF_D_FileTransferLogTableAdapter.Fill(Me.HINODEDBDataSet.T_TF_D_FileTransferLog)

        SettingClass.設定読込()
        DataGridSetting()
        Me.UserControl2.TextBox1.Text = cnstr

        Me.UserControl1.Visible = False
        Me.UserControl2.Visible = False

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If lblステータス.Text = "稼働中" Then
            MsgBox("稼働中は閉じることができません！")
            e.Cancel = True
        Else
            SettingClass.設定保存()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click 'ON/OFF切り替え

        If Me.CheckBox1.Checked = False Then

            'If Me.UserControl1.TextBox1.Text <> "" And Me.UserControl1.TextBox2.Text <> "" And Me.UserControl1.TextBox3.Text <> "" And Me.UserControl1.TextBox4.Text <> "" And Me.UserControl1.TextBox5.Text <> "" And Me.UserControl1.TextBox6.Text <> "" And Me.UserControl1.TextBox7.Text <> "" Then
            If Directory.Exists(Me.UserControl1.TextBox1.Text) And Directory.Exists(Me.UserControl1.TextBox2.Text) And Directory.Exists(Me.UserControl1.TextBox3.Text) And Directory.Exists(Me.UserControl1.TextBox4.Text) And Directory.Exists(Me.UserControl1.TextBox5.Text) And Directory.Exists(Me.UserControl1.TextBox6.Text) Then

                If IsNumeric(Me.UserControl1.TextBox7.Text) And IsNumeric(Me.UserControl1.TextBox8.Text) Then

                    Me.CheckBox1.Checked = True
                    MsgBox("転送を開始しました。")
                    Console.WriteLine("転送開始")

                Else
                    MsgBox("監視インターバルとログ取得件数を正しく入力してください。")
                End If
            Else
                MsgBox("ファルダ選択エラー")
            End If

        Else
            Dim result As DialogResult = MessageBox.Show("FAX転送を停止しますか？", "確認メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
            If result = DialogResult.OK Then
                Dim inputText As String
                inputText = InputBox("パスワードを入力してください。", "確認メッセージ", "", Me.Left + 300, Me.Top + 200)
                If inputText = "Hinode8739" Then

                    Me.CheckBox1.Checked = False
                    MsgBox("転送を停止しました。")
                    Console.WriteLine("転送停止")
                Else
                    MsgBox("パスワードが違います！")
                End If

            End If


        End If
        ONOFF切り替え()

    End Sub

    Private Sub ONOFF切り替え()

        If Me.CheckBox1.Checked = True Then

            lblステータス.Text = "稼働中"
            Button1.Text = "停止"
            kannsi1 = Me.UserControl1.TextBox1.Text
            saki1 = Me.UserControl1.TextBox2.Text
            moto1 = Me.UserControl1.TextBox3.Text
            kannsi2 = Me.UserControl1.TextBox4.Text
            saki2 = Me.UserControl1.TextBox5.Text
            moto2 = Me.UserControl1.TextBox6.Text
            Try
                interval = CInt(Me.UserControl1.TextBox7.Text) * 1000
            Catch ex As System.Exception
                interval = 1000
            End Try
            Me.UserControl1.TextBox1.ReadOnly = True
            Me.UserControl1.TextBox2.ReadOnly = True
            Me.UserControl1.TextBox3.ReadOnly = True
            Me.UserControl1.TextBox4.ReadOnly = True
            Me.UserControl1.TextBox5.ReadOnly = True
            Me.UserControl1.TextBox6.ReadOnly = True
            Me.UserControl1.TextBox7.ReadOnly = True
            Me.UserControl1.TextBox8.ReadOnly = True
            Me.UserControl2.TextBox1.ReadOnly = True
            Me.UserControl1.Visible = False
            Me.UserControl2.Visible = False

            Timer1.Interval = interval
            Timer1.Enabled = True

        Else
            lblステータス.Text = "停止中"
            Button1.Text = "開始"
            Me.UserControl1.TextBox1.ReadOnly = False
            Me.UserControl1.TextBox2.ReadOnly = False
            Me.UserControl1.TextBox3.ReadOnly = False
            Me.UserControl1.TextBox4.ReadOnly = False
            Me.UserControl1.TextBox5.ReadOnly = False
            Me.UserControl1.TextBox6.ReadOnly = False
            Me.UserControl1.TextBox7.ReadOnly = False
            Me.UserControl1.TextBox8.ReadOnly = False
            Me.UserControl2.TextBox1.ReadOnly = False

            Timer1.Enabled = False

        End If

    End Sub

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Await Task.Run(Sub()
                           '転送1
                           Dim di1 As New System.IO.DirectoryInfo(kannsi1)
                           Dim files1 As System.IO.FileInfo() = di1.GetFiles("*", System.IO.SearchOption.AllDirectories)

                           '転送2
                           Dim di2 As New System.IO.DirectoryInfo(kannsi2)
                           Dim files2 As System.IO.FileInfo() = di2.GetFiles("*", System.IO.SearchOption.AllDirectories)

                           System.Threading.Thread.Sleep(300)

                           For Each f1 As System.IO.FileInfo In files1

                               Dim kakutyousi As String = Path.GetExtension(f1.FullName) '拡張子
                               If kakutyousi = ".tmp" Or kakutyousi = ".db" Then
                               Else
                                   Test用転送(f1.FullName)
                                   転送処理1(f1.FullName)
                                   Console.WriteLine(f1.FullName)
                               End If
                           Next

                           For Each f2 As System.IO.FileInfo In files2
                               Dim kakutyousi As String = Path.GetExtension(f2.FullName) '拡張子
                               If kakutyousi = ".tmp" Or kakutyousi = ".db" Then
                               Else
                                   'Test用転送(f2.FullName)
                                   転送処理2(f2.FullName)
                                   Console.WriteLine(f2.FullName)
                               End If
                           Next
                       End Sub)
        If CheckBox2.Checked = True Then
            Dim c As Integer
            Try
                c = CInt(UserControl1.TextBox8.Text)
            Catch ex As System.Exception
                c = 100
            End Try
            ログ取得(c)
        End If

    End Sub

    Private Sub Test用転送(ByVal s As String)

        Dim fnwx As String = Path.GetFileNameWithoutExtension(s) 'ファイル名
        Dim fnex As String = Path.GetExtension(s) '拡張子 

        Dim fi As New FileInfo(s)
        Dim fn As String = Path.GetFileName(s)
        Dim i As Integer
        Dim copyToDir As String = "\\192.168.2.240\fax受信\FAX受信トレイ\FAX転送2\"
        Dim copyTo As String = copyToDir & fnwx & fnex
        If File.Exists(s) Then

            While File.Exists(copyTo)
                copyTo = copyToDir & fnwx & "(" & i & ")" & fnex
                i = i + 1
            End While

            Dim copyFile As FileInfo = fi.CopyTo(copyTo)

        End If

    End Sub


    Private Sub 転送処理1(ByVal s As String)



        'Fax受信　⇒　ThereforDATA

        Dim fname As String = Path.GetFileNameWithoutExtension(s) 'ファイル名
        Dim kakutyousi As String = Path.GetExtension(s) '拡張子

        Dim fnarray As Array = Split(fname, "_")

        Dim flg As Boolean = True
        Dim errorstr As String = ""


        If IsNumeric(fnarray(0)) Then '-数値

            Dim zyutyuuCD As Decimal = CDec(fnarray(0))

            Dim sqlstr As String = "Select dbo.T_TF_D_Index.受注ID, dbo.T_TF_D_Index.開始日, dbo.T_TF_D_Index.終了日, dbo.T_TF_D_Index.担当部署, dbo.T_TF_D_Index.備考, dbo.T_TF_D_Index.荷主ID, dbo.T_TF_M_NinusiInfo.名称_HND, dbo.T_TF_M_NinusiInfo.名称_TF, dbo.T_TF_M_NinusiInfo.FAX番号 "
            sqlstr &= "FROM dbo.T_TF_D_Index LEFT OUTER JOIN dbo.T_TF_M_NinusiInfo On dbo.T_TF_D_Index.荷主ID = dbo.T_TF_M_NinusiInfo.荷主ID "
            sqlstr &= "WHERE (dbo.T_TF_D_Index.受注ID = " & zyutyuuCD & ")"

            Dim cn As SqlClient.SqlConnection
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            Dim sdate As String = ""
            Dim edate As String = ""
            Dim busyo As String = ""
            Dim bikou As String = ""
            Dim ninusiID As Integer = 0
            Dim ninusimeiHND As String = ""
            Dim ninusimeiTF As String = ""
            Dim FAXbanngou As String = ""

            cn = New SqlClient.SqlConnection(cnstr)

            cmd = cn.CreateCommand
            cmd.CommandText = sqlstr

            cn.Open()
            'Console.WriteLine(cn.State)

            reader = cmd.ExecuteReader()

            If reader.HasRows = True Then '---レコードあり

                Try

                    Do While reader.Read()
                        If reader("開始日") IsNot DBNull.Value Then
                            sdate = Format(reader("開始日"), "yyyyMMdd")
                            'sdate = reader("開始日").ToString("yyyyMMdd")
                        End If
                        If reader("終了日") IsNot DBNull.Value Then
                            edate = Format(reader("終了日"), "yyyyMMdd")
                            'edate = reader("終了日").ToString("yyyyMMdd")
                        End If
                        busyo = reader("担当部署")
                        bikou = reader("備考")
                        ninusiID = reader("荷主ID")
                        ninusimeiHND = reader("名称_HND")
                        ninusimeiTF = reader("名称_TF")
                        FAXbanngou = reader("FAX番号")
                    Loop

                Catch ex As System.Exception '------------------------すべての例外
                    System.Console.WriteLine(ex.Message)
                End Try


                reader.Close()
                cmd.Dispose()
                cn.Close()
                cn.Dispose()

                Dim yousosuu As Integer = fnarray.Length
                Dim TSiti As Integer
                Dim dt As Date
                For TSiti = 0 To yousosuu - 1
                    If DateTime.TryParseExact(Strings.Left(fnarray(TSiti), 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, dt) = True Then
                        Exit For
                    Else
                        'MsgBox(i & " " & fnarray(i))
                    End If
                Next

                If zyutyuuCD = 501 Or zyutyuuCD = 601 Or zyutyuuCD = 701 Or zyutyuuCD = 801 Or zyutyuuCD = 901 Then '------------受注以外の処理（受注CD三桁以下）

                    If yousosuu >= 5 And TSiti = 4 Then '配列の要素数チェック----要素数5以上、6以下、タイムスタンプ位置4

                        Dim str As String = fnarray(1)
                        Dim strarray As Array = Split(str, "-")
                        Dim datestr As String = strarray(0)

                        If Date.TryParseExact(datestr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, dt) = True Then '送信時に入力する文字列が日付の場合
                            sdate = datestr
                            edate = datestr
                            If strarray.Length > 1 Then
                                bikou = strarray(1)
                                If strarray.Length > 2 Then
                                    Dim i As Integer
                                    For i = 2 To strarray.Length - 1
                                        bikou &= "-" & strarray(i)
                                    Next
                                End If
                            End If
                        Else '-----------------------------------------------------------送信時に入力する文字列が日付以外の場合
                            bikou = str
                        End If

                        If fnarray(2) = "登録名称不明" Or fnarray(2) = "scan" Or fnarray(2) = "取込" Or fnarray(2) = "本社受信" Or fnarray(2) = "" Then '荷主名
                            If ninusimeiTF = "" Then
                                fname = ninusimeiHND
                            Else
                                fname = ninusimeiTF
                            End If
                        Else
                            fname = fnarray(2)
                        End If

                        If fnarray(3) = "" Then '---------------------------------------FAX番号
                            If FAXbanngou = "" Then
                                fname &= "_" & ""
                            Else
                                fname &= "_" & FAXbanngou
                            End If
                        Else
                            fname &= "_" & fnarray(3)
                        End If

                        fname &= "_" & Strings.Left(fnarray(4), 14) '-------------------タイムスタンプ
                        fname &= "_" & fnarray(0) '-------------------------------------受注CD
                        fname &= "_" & sdate '------------------------------------------開始日
                        fname &= "_" & edate '------------------------------------------終了日
                        fname &= "_" & busyo '------------------------------------------担当部署
                        fname &= "_" & bikou '------------------------------------------備考

                        Dim pstr As String = ""
                        If yousosuu - 1 > TSiti Then
                            Dim i As Integer
                            For i = TSiti + 1 To yousosuu - 1
                                pstr &= fnarray(i)
                            Next
                        Else
                            pstr = "1"
                        End If
                        fname &= "_" & pstr '-------------------------------------------ページ
                        fname &= kakutyousi '-------------------------------------------拡張子

                        '-----------------------------------------------------------------------------------------ファイルのコピー処理
                        ファイル転送("転送1", "正常", s, saki1 & "\" & fname)

                    Else
                        flg = False
                        errorstr = "ファイル名不正"
                    End If

                Else '-----------------------------------------------------------------------------------------------------------------通常受注処理

                    If yousosuu >= 4 And TSiti = 3 Then '配列の要素数チェック----要素数4以上、5以下、タイムスタンプ位置3

                        fname = ninusimeiHND '受注のFAXについては荷主名を強制的に上書きする

                        If fnarray(1) = "登録名称不明" Or fnarray(1) = "scan" Or fnarray(1) = "取込" Or fnarray(1) = "" Then '荷主名
                        Else
                            If ninusimeiTF = "" Then
                                荷主名保存(ninusiID, fnarray(1))
                            End If
                        End If

                        If fnarray(2) = "" Then '---------------------------------------FAX番号
                            If FAXbanngou = "" Then
                                fname &= "_" & ""
                            Else
                                fname &= "_" & FAXbanngou
                            End If
                        Else
                            fname &= "_" & fnarray(2)
                            If FAXbanngou = "" Then
                                FAX番号保存(ninusiID, fnarray(2))
                            End If
                        End If

                        fname &= "_" & Strings.Left(fnarray(3), 14) '-------------------タイムスタンプ
                        fname &= "_" & fnarray(0) '-------------------------------------受注CD
                        fname &= "_" & sdate '------------------------------------------開始日
                        fname &= "_" & edate '------------------------------------------終了日
                        fname &= "_" & busyo '------------------------------------------担当部署
                        fname &= "_" & bikou '------------------------------------------備考

                        Dim pstr As String = ""
                        If yousosuu - 1 > TSiti Then
                            Dim i As Integer
                            For i = TSiti + 1 To yousosuu - 1
                                pstr &= fnarray(i)
                            Next
                        Else
                            pstr = "1"
                        End If
                        fname &= "_" & pstr '-------------------------------------------ページ
                        fname &= kakutyousi '-------------------------------------------拡張子


                        'ここからテスト(新システム向け転送処理）
                        Dim copyTo As String = "\\192.168.2.240\fax受信\FAX受信トレイ\受注FAX保管\" & fname
                        'Console.WriteLine(copyTo)
                        受注FAX保管(s, copyTo)

                        '-----------------------------------------------------------------------------------------ファイルのコピー処理
                        ファイル転送("転送1", "正常", s, saki1 & "\" & fname)

                    Else
                        flg = False
                        errorstr = "ファイル名不正"
                    End If

                End If

            Else '受注CD適合なし
                flg = False
                errorstr = "受注CD該当なし"
            End If

        Else '数値以外
            flg = False
            errorstr = "受注CD不正"
        End If


        If flg = False Then '---------------------------------------------------------------------------------------------------エラー時処理

            Dim ac As Integer = fnarray.Length
            Dim i As Integer

            Try

                If fnarray(0) = "501" Or fnarray(0) = "601" Or fnarray(0) = "701" Or fnarray(0) = "801" Or fnarray(0) = "901" Then

                    fname = errorstr
                    fname &= fnarray(2)
                    If ac > 3 Then
                        For i = 4 To ac
                            If i < ac Then
                                fname &= "_" & fnarray(i)
                            End If
                        Next
                        fname &= kakutyousi
                    End If

                Else

                    fname = errorstr
                    fname &= fnarray(1)
                    If ac > 2 Then
                        For i = 2 To ac
                            If i < ac Then
                                fname &= "_" & fnarray(i)
                            End If
                        Next
                        fname &= kakutyousi
                    End If

                End If

            Catch ex As System.Exception
                System.Console.WriteLine(ex.Message)
            End Try

            ファイル転送("転送1", errorstr, s, moto1 & "\" & fname)

        End If


    End Sub

    Private Sub 転送処理2(ByVal s As String)

        'scan ⇒　FAX受信
        Dim maxcount As Integer = 60
        Dim i As Integer
        Dim fname As String = Path.GetFileNameWithoutExtension(s)
        Dim kakutyousi As String = Path.GetExtension(s)
        Dim dt As Date

        If DateTime.TryParseExact(fname, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, dt) = True Then

            For i = 0 To maxcount
                fname = "scan__" & fname & "_" & i & kakutyousi
                If System.IO.File.Exists(saki2 & "\" & fname) = False Then
                    Exit For
                End If
            Next i

        Else

            For i = 0 To maxcount
                fname = "取込__" & Format(Now(), "yyyyMMddHHmmss") & "_" & i & kakutyousi
                'fname = "取込__" & DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & i & kakutyousi
                If System.IO.File.Exists(saki2 & "\" & fname) = False Then
                    Exit For
                End If
            Next i

        End If

        ファイル転送("転送2", "正常", s, saki2 & "\" & fname)

    End Sub

    Private Function ファイル転送(ByVal syori As String, ByVal kekka As String, ByVal a As String, ByVal b As String) As Boolean

        Try '-----------------------------------------------------------------------------------------ファイルのコピー処理
            If FileOpenCheck(a) = True Then
                'Console.WriteLine(b)
                System.IO.File.Copy(a, b)
            End If

            Try '-----------------------------------------------------------------------------------------ファイルの削除処理
                If FileOpenCheck(a) = True Then
                    System.IO.File.Delete(a)
                End If
            Catch ex As System.UnauthorizedAccessException '------削除ファイルへのアクセスが拒否された場合
                System.Console.WriteLine(ex.Message)
            Catch ex As System.Exception '------------------------すべての例外
                System.Console.WriteLine(ex.Message)
            End Try

            ログ保存(syori, kekka, Path.GetFileName(a), Path.GetFileName(b))
            ファイル転送 = True

            'Catch ex As System.IO.FileNotFoundException '---------コピーする元ファイルがない場合
            'System.Console.WriteLine(ex.Message)
            'Catch ex As System.IO.IOException '-------------------コピー先のファイルがすでに存在している場合
            'System.Console.WriteLine(ex.Message)
            'Catch ex As System.UnauthorizedAccessException '------コピー先のファイルへのアクセスが拒否された場合
            'System.Console.WriteLine(ex.Message)
        Catch ex As System.Exception '------------------------すべての例外
            System.Console.WriteLine(ex.Message)
            ファイル転送 = False
        End Try

    End Function

    Private Sub 受注FAX保管(ByVal copyFrom As String, ByVal copyTo As String)

        Dim fnarray As Array = Split(Path.GetFileName(copyTo), "_")
        Dim dirName0 As String = Path.GetDirectoryName(copyTo)
        Dim dirName1 As String = fnarray(0)
        Dim dirName2 As String = fnarray(6)
        Dim dirName3 As String = fnarray(7)
        Dim filName As String = Path.GetFileNameWithoutExtension(copyTo)
        Dim extName As String = Path.GetExtension(copyTo)
        Dim fullPath As String

        '転送先ディレクトリ存在チェック
        Dim dirNameTo As String = dirName0 & "\" & dirName1 & "\" & dirName2
        If dirName2 = "4.倉庫保管" And dirName3 <> "" Then
            dirNameTo = dirNameTo & "\" & dirName3
        End If

        'Console.WriteLine("保存ディレクトリ: " & dirNameTo)

        fullPath = dirNameTo & "\" & filName & extName
        If Directory.Exists(dirNameTo) = False Then
            Directory.CreateDirectory(dirNameTo)
        Else
            'ファイルの存在チェック（重複の場合ファイル名に(i)を付与）
            Dim i As Integer = 0
            While File.Exists(fullPath)
                i = i + 1
                fullPath = dirNameTo & "\" & filName & "(" & i & ")" & extName
            End While
        End If

        'Console.WriteLine("フルパス: " & fullPath)
        'Console.WriteLine("コピー元: " & copyFrom)

        Try
            File.Copy(copyFrom, fullPath)
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try

    End Sub






    Private Sub 荷主名保存(ByVal ninusiID As Integer, ByVal ninusimeiTF As String)

        Dim sqlstr As String = "UPDATE T_TF_M_NinusiInfo SET 名称_TF = '" & ninusimeiTF & "' WHERE 荷主ID =" & ninusiID

        Dim cn As SqlClient.SqlConnection
        Dim cmd As SqlClient.SqlCommand

        cn = New SqlClient.SqlConnection(cnstr)
        cn.Open()

        cmd = cn.CreateCommand
        cmd.CommandText = sqlstr
        cmd.ExecuteNonQuery()

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Private Sub FAX番号保存(ByVal ninusiID As Integer, ByVal FAXbanngou As String)

        Dim sqlstr As String = "UPDATE T_TF_M_NinusiInfo SET FAX番号 = '" & FAXbanngou & "' WHERE 荷主ID =" & ninusiID

        Dim cn As SqlClient.SqlConnection
        Dim cmd As SqlClient.SqlCommand

        cn = New SqlClient.SqlConnection(cnstr)
        cn.Open()

        cmd = cn.CreateCommand
        cmd.CommandText = sqlstr
        cmd.ExecuteNonQuery()

        cmd.Dispose()
        cn.Close()
        cn.Dispose()

    End Sub

    Private Sub ログ保存(ByVal syori As String, ByVal kekka As String, ByVal oname As String, ByVal nname As String)

        Dim dt As Date = DateTime.Now
        Dim sqlstr As String = "INSERT INTO T_TF_D_FileTransferLog (処理, 結果, 変更前ファイル名, 変更後ファイル名, 日時) values ('" & syori & "', '" & kekka & "', '" & oname & "', '" & nname & "', '" & dt & "')"

        Dim cn As SqlClient.SqlConnection
        Dim cmd As SqlClient.SqlCommand

        cn = New SqlClient.SqlConnection(cnstr)
        cn.Open()

        cmd = cn.CreateCommand
        cmd.CommandText = sqlstr
        cmd.ExecuteNonQuery()

        cmd.Dispose()
        cn.Close()
        cn.Dispose()


    End Sub

    Private Sub ログ取得(c As Integer)

        Dim cn As New SqlClient.SqlConnection(cnstr)
        cn.Open()

        Dim da = New SqlDataAdapter("SELECT TOP (" & c & ") ID, 処理, 結果, 変更前ファイル名, 変更後ファイル名, 日時 FROM dbo.T_TF_D_FileTransferLog ORDER BY ID DESC", cn)

        Dim ds As New DataSet()

        da.Fill(ds)

        DataGridView1.DataSource = ds.Tables(0)

        cn.Close()
        cn.Dispose()

    End Sub

    Private Function FileOpenCheck(fpath As String) As Boolean

        Dim st As Stream = Nothing
        Dim maxcount As Integer = 60
        Dim i As Integer
        Dim c As Integer

        For i = 0 To maxcount
            Try
                st = File.Open(fpath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                If Not st Is Nothing Then
                    'Console.WriteLine(c)
                    Exit For
                End If
            Catch ex As Exception
                System.Threading.Thread.Sleep(1000)
                c += 1
            End Try
        Next i

        If Not st Is Nothing Then
            st.Close()
            FileOpenCheck = True
        Else
            ' タイムアウト
            FileOpenCheck = False
        End If

    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim c As Integer
        Try
            c = CInt(UserControl1.TextBox8.Text)
        Catch ex As System.Exception
            c = 100
        End Try
        ログ取得(c)
        TextBox1.Text = ""
        Me.UserControl1.Visible = False
        Me.UserControl2.Visible = False
        Me.UserControl2.Dock = DockStyle.Fill
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If CheckBox1.Checked = False Then
            Me.UserControl1.Visible = True
            Me.UserControl2.Visible = False
            Me.UserControl1.Dock = DockStyle.Fill
        Else
            MsgBox("稼働中は設定変更できません。")
        End If
    End Sub

    Private Sub DataGridSetting()

        DataGridView1.Sort(DataGridView1.Columns(0), System.ComponentModel.ListSortDirection.Descending)

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        TextBox1.Text = DataGridView1.CurrentCell.Value
        CheckBox2.Checked = False
    End Sub

    Public Function 接続テスト(s As String) As Boolean

        Try
            Dim cn As New SqlConnection
            Using cn

                cn.ConnectionString = s
                cn.Open()

                MessageBox.Show(cn.State.ToString, "DB接続テスト")
                接続テスト = cn.State

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "接続テスト【例外発生】")
            接続テスト = False
        End Try

    End Function

    Private Sub btnテスト_Click(sender As Object, e As EventArgs) Handles btnテスト.Click



    End Sub

End Class








Public Class SettingItemClass
    Public kannsi1 As String
    Public saki1 As String
    Public moto1 As String
    Public kannsi2 As String
    Public saki2 As String
    Public moto2 As String
    Public interval As String
    Public logc As String
End Class

Class SettingClass

    Public Shared Sub 設定保存()

        Dim strPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        strPath = System.IO.Path.GetDirectoryName(strPath)
        strPath = strPath & "\setting.xml"

        Dim obj As New SettingItemClass()
        obj.kannsi1 = Form1.UserControl1.TextBox1.Text
        obj.saki1 = Form1.UserControl1.TextBox2.Text
        obj.moto1 = Form1.UserControl1.TextBox3.Text
        obj.kannsi2 = Form1.UserControl1.TextBox4.Text
        obj.saki2 = Form1.UserControl1.TextBox5.Text
        obj.moto2 = Form1.UserControl1.TextBox6.Text
        obj.interval = Form1.UserControl1.TextBox7.Text
        obj.logc = Form1.UserControl1.TextBox8.Text

        Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(SettingItemClass))
        Dim sw As New System.IO.StreamWriter(strPath, False, New System.Text.UTF8Encoding(False))
        serializer.Serialize(sw, obj)

        sw.Close()

    End Sub

    Public Shared Sub 設定読込()

        Try
            Dim strPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
            strPath = System.IO.Path.GetDirectoryName(strPath)
            strPath = strPath & "\setting.xml"

            Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(SettingItemClass))
            Dim sr As New System.IO.StreamReader(strPath, New System.Text.UTF8Encoding(False))
            Dim obj As SettingItemClass = DirectCast(serializer.Deserialize(sr), SettingItemClass)

            Form1.UserControl1.TextBox1.Text = obj.kannsi1
            Form1.UserControl1.TextBox2.Text = obj.saki1
            Form1.UserControl1.TextBox3.Text = obj.moto1
            Form1.UserControl1.TextBox4.Text = obj.kannsi2
            Form1.UserControl1.TextBox5.Text = obj.saki2
            Form1.UserControl1.TextBox6.Text = obj.moto2
            Form1.UserControl1.TextBox7.Text = obj.interval
            Form1.UserControl1.TextBox8.Text = obj.logc

            sr.Close()

        Catch ex As System.Exception '------------------------すべての例外
            System.Console.WriteLine(ex.Message)
        End Try

    End Sub


End Class
