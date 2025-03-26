using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using MetroFramework.Forms; // 追加

namespace FileTransfer2
{
    public partial class Form1 : MetroForm/* ←変更 */
    {
        string connectionString = "User ID=hinode;Password=Hkanri8739;Initial Catalog=HINODEDB;Data Source=hinode-server.database.windows.net;TrustServerCertificate=True";
        
        
        public Form1()
        {
            InitializeComponent();

        }

        private int duration = 5;
        private void Form1_Load(object sender, EventArgs e)
        {
            //ロード時
            txt転送1監視フォルダ.Text = @"\\Thereforesv\fax受信\FAX受信トレイ\細金\FAX転送";           
            txt転送1間隔.Text = "1";
            txt転送1稼働時間.Text = "";
            txt転送1転送先.Text = @"\\Thereforesv\fax受信\FAX受信トレイ\細金\FAX保管";
            txt転送1エラー時転送先.Text = "";

            txt転送2監視フォルダ.Text = @"\\Thereforesv\fax受信\FAX受信トレイ\細金\FAX転送";
            txt転送2間隔.Text = "1";
            txt転送2稼働時間.Text = "";
            txt転送2転送先.Text = @"\\Thereforesv\fax受信\FAX受信トレイ\細金\FAX保管";
            txt転送2エラー時転送先.Text = "";



            Run(false);

            if (Directory.Exists(txt転送1監視フォルダ.Text) && txt転送1間隔.Text != string.Empty)
            {
                //カウントダウン後監視開始
                timer1 = new Timer();
                timer1.Tick += new EventHandler(Countdown);
                timer1.Interval = 1000;
                timer1.Start();
            }
        }

        private void Countdown(object sender, EventArgs e)
        {
            if (duration == 0) {
                timer1.Stop();
                Run(true);
            }
            else if (duration > 0) {
                duration--;
                lbl状況.Text = duration.ToString();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (lbl状況.Text == "稼働中")
            {
                Run(false);
            }
            else
            {
                Run(true);
            }
        }

        //スタートストップの切替
        private void Run(Boolean run)
        {
            if (run)
            {
                //入力チェック
                if (!Directory.Exists(txt転送1監視フォルダ.Text))
                {
                    MessageBox.Show("監視フォルダを指定してください。");
                    return;
                }

                //if (textBox2.Text != string.Empty)
                //{
                //    MessageBox.Show("監視インターバルを指定してくさださい。");
                //    return;
                //}

                if (!Directory.Exists(txt転送1転送先.Text))
                {
                    MessageBox.Show("転送先フォルダを指定してください。");
                    return;
                }

                lbl状況.Text = "稼働中";
                txt転送1監視フォルダ.Enabled = false;
                txt転送1間隔.Enabled = false;
                txt転送1転送先.Enabled = false;
                btnRun.Text = "Stop";

                timer2 = new Timer();
                timer2.Tick += new EventHandler(MainOperation);
                timer2.Interval = Convert.ToInt32(txt転送1間隔.Text) * 1000;
                timer2.Start();
            }
            else
            {
                lbl状況.Text = "停止中";
                txt転送1監視フォルダ.Enabled = true;
                txt転送1間隔.Enabled = true;
                txt転送1転送先.Enabled = true;
                btnRun.Text = "Go";

                timer2.Stop();
            }

        }




        //メイン処理
        private void MainOperation(object sender, EventArgs e)
        {
            //ファイル名取得
            lbl表示.Text = DateTime.Now.ToString();
            string fileList = "";
            var files = Directory.GetFiles(txt転送1監視フォルダ.Text);

            foreach (var file in files)//ファイル毎処理
            {
                bool transFlg = false;
                string fname = Path.GetFileName(file);
                string fexten = Path.GetExtension(file);

                var fnameAry = fname.Split('_');
                var firstStr = fnameAry[0];
                decimal zyutyuuID;

                if (decimal.TryParse(firstStr, out zyutyuuID))//ファイル名の一番目が数値かどうか
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string sql = @"SELECT dbo.T_TF_D_Index.開始日, dbo.T_TF_D_Index.終了日, dbo.T_TF_D_Index.担当部署, dbo.T_TF_D_Index.備考, dbo.T_TF_D_Index.荷主ID, dbo.T_TF_D_Index.荷主名, dbo.T_TF_M_NinusiInfo.名称_HND, dbo.T_TF_M_NinusiInfo.名称_TF, dbo.T_TF_M_NinusiInfo.FAX番号 
                                            FROM dbo.T_TF_D_Index LEFT OUTER JOIN dbo.T_TF_M_NinusiInfo ON dbo.T_TF_D_Index.荷主ID = dbo.T_TF_M_NinusiInfo.荷主ID 
                                            WHERE (dbo.T_TF_D_Index.受注ID = @ZyutyuuID)";

                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@ZyutyuuID",zyutyuuID);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {  
                                    while (reader.Read())
                                    {
                                        Index index = new Index();
                                        if (reader.GetDateTime(0) != null)
                                        {
                                            index.sdate = reader.GetDateTime(0).ToString("yyyy/MM/dd");
                                        }
                                        if (reader.GetDateTime(1) != null)
                                        {
                                            index.edate = reader.GetDateTime(1).ToString("yyyy/MM/dd");
                                        }
                                        index.busyo = reader.GetString(2);
                                        index.bikou = reader.GetString(3);
                                        index.ninusiID = reader.GetInt32(4).ToString();
                                        index.ninusimeiHND = reader.GetString(5);
                                        index.ninusimeiTF = reader.GetString(6);
                                        index.FAXbanngou = reader.GetString(7);



                                        string filename;
                                        if (zyutyuuID == 501 || zyutyuuID == 601 || zyutyuuID == 701 || zyutyuuID == 801 || zyutyuuID == 901)
                                        {
                                            //受注以外FAX
                                        }
                                        else
                                        {
                                            //受注FAX
                                            //ファイル名作成
                                            filename = index.ninusimeiHND;
                                            filename += "_" + index.FAXbanngou;
                                            filename += "_" + fnameAry[3];
                                            filename += "_" + fnameAry[0];
                                            filename += "_" + index.sdate;
                                            filename += "_" + index.edate;
                                            filename += "_" + index.busyo;
                                            filename += "_" + index.bikou;
                                            filename += "_";//ページ                                            

                                            //転送先ディレクトリチェック、荷主名、担当部署（なければ作成）（あればファイル名チェック）

                                            string dirName = txt転送1転送先.Text;//転送先ディレクトリ
                                            dirName = dirName + @"\" + index.ninusimeiHND + @"\" + index.busyo;
                                            string fullPath = dirName + @"\" + filename + fexten;
                                            if (!Directory.Exists(dirName))
                                            {
                                                Directory.CreateDirectory(dirName);
                                            }
                                            else
                                            {
                                                int i = 1;
                                                while (File.Exists(fullPath))
                                                {
                                                    fullPath = dirName + @"\" + filename + "(" + i.ToString() + ")" + fexten;
                                                    i++;
                                                }

                                            }
                                            fname = Path.GetFileName(fullPath);
                                        }
                                    } 
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("接続失敗");
                        Console.WriteLine(ex.Message);
                    }

                    transFlg = true;
                }
                else
                {
                    //ファイル名の最初が数値以外（エラー）
                }








                fileList += fname + transFlg.ToString() + "\n";
            }
            lblFileList.Text = fileList;


            //ファイルのチェック
            //
            //



            //データ取得



            //ファイル名作成



            //CSV作成


            //転送先ディレクトリ確認作成

            //転送先に同ファイル名確認、

            //転送

        }


        private void Trance1()
        {

        }
        private void Trance2()
        {

        }

        private void chk転送1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chk転送2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }








    public class Index
    {
        public string sdate { get; set; } = "";
        public string edate { get; set; } = "";
        public string busyo { get; set; } = "";
        public string bikou { get; set; } = "";
        public string ninusiID { get; set; } = "";
        public string ninusimeiHND { get; set; } = "";
        public string ninusimeiTF { get; set; } = "";
        public string FAXbanngou { get; set; } = "";
    }

}









