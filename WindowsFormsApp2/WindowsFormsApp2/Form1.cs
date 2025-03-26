using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2.Models;
using WindowsFormsApp2.Services;
using System.Threading.Tasks;
using MetroFramework.Controls;
using System.Globalization;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using System.ComponentModel;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Dictionary<Button, Panel> _dic = new Dictionary<Button, Panel>();
        Setting _setting = new Setting();

        //タイマー作成
        Timer _timer1 = new Timer();
        Timer _timer2 = new Timer();        
        Timer _timerkeika1 = new Timer();
        Timer _timerkeika2 = new Timer();

        DateTime _sdt1 = DateTime.Now;
        DateTime _sdt2 = DateTime.Now;


        Timer _timerArchive = new Timer();
        private TimeSpan targetTime = new TimeSpan(2, 0, 0); // 2:00に実行
        private DateTime nextRunTime;

        //システムログを生成
        private BindingList<SystemLog> systemLogs = new BindingList<SystemLog>();
        //転送ログを生成
        private BindingList<TranceferLog> tranceferLogs = new BindingList<TranceferLog>();

        //コンストラクタ
        public Form1()
        {
            InitializeComponent();

            // ボタンとパネルの組み合わせを登録
            _dic.Add(this.button1, this.panel1);
            _dic.Add(this.button2, this.panel2);
            _dic.Add(this.button3, this.panel3);

            // ボタンのクリックイベント登録、パネルのサイズ初期化
            this.AddEvent();

            // 画面初期表示時はボタン1を選択
            this.SelectPanel(this.button1);

            //タイマーで実行する処理
            _timer1.Tick += new EventHandler(監視1);
            _timer2.Tick += new EventHandler(監視2);
            _timerkeika1.Tick += new EventHandler(転送1_経過時間);
            _timerkeika2.Tick += new EventHandler(転送2_経過時間);
            _timerArchive.Tick += new EventHandler(_timerArchive_Tick);

            //システムログ
            this.listSystemLog.DataSource = systemLogs;
            this.listSystemLog.Columns[0].Width = 120;
            this.listSystemLog.Columns[1].Width = 300;
            this.listSystemLog.Columns[0].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";
            //転送ログ
            this.listTranceferLog.DataSource = tranceferLogs;
            this.listTranceferLog.Columns[0].Width = 50;
            this.listTranceferLog.Columns[1].Width = 40;
            this.listTranceferLog.Columns[2].Width = 100;
            this.listTranceferLog.Columns[3].Width = 250;
            this.listTranceferLog.Columns[4].Width = 300;
            this.listTranceferLog.Columns[5].Width = 100;
            this.listTranceferLog.Columns[5].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";


        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            //設定を読込
            await this.GetSetting();

            //各種表示
            this.lbl転送1_状況.Text = "停止中";
            this.lbl転送2_状況.Text = "停止中";

            //SystemLogに記録
            await this.AddSystemLog("起動しました。");

            //転送ログを取得
            await this.GetTranceferLog(_setting.ログ_表示件数);

            //何秒か待って処理自動開始
            await this.自動開始処理();
            



        }




        //共通処理
        /// <summary>
        /// イベントハンドラ紐づけ
        /// </summary>
        private void AddEvent()
        {
            foreach (KeyValuePair<Button, Panel> pair in _dic)
            {
                Button button = pair.Key;
                button.Click += new System.EventHandler(this.Button_Click);
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
                button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;

                Panel panel = pair.Value;
                panel.Left = 240;
                panel.Top = 0;
                panel.Width = 950;
                panel.Height = 565;
            }
        }

        /// <summary>
        /// ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            SelectPanel(button);
        }

        /// <summary>
        /// 表示を切り替え
        /// </summary>
        /// <param name="selectedButton">選択したボタン</param>
        private void SelectPanel(Button selectedButton)
        {
            foreach (KeyValuePair<Button, Panel> pair in _dic)
            {
                Button button = pair.Key;
                Panel panel = pair.Value;

                if (button.Equals(selectedButton))
                {
                    // 選択時
                    button.BackColor = Color.FromArgb(128, 128, 255);
                    button.ForeColor = Color.White;
                    panel.Visible = true;
                }
                else
                {
                    // 選択されていない時
                    button.BackColor = Color.FromArgb(192, 192, 255);
                    button.ForeColor = Color.Black;
                    panel.Visible = false;


                }
            }
        }

        private async Task GetSetting()
        {
            _setting = await SettingService.GetSettingAsync();
            this.txt転送1_監視.Text = _setting.転送1_監視.ToString();
            this.txt転送1_転送.Text = _setting.転送1_転送.ToString();
            this.txt転送1_戻し.Text = _setting.転送1_戻し.ToString();
            this.txt転送1_間隔.Text = _setting.転送1_間隔.ToString();
            this.txt転送2_監視.Text = _setting.転送2_監視.ToString();
            this.txt転送2_転送.Text = _setting.転送2_転送.ToString();
            this.txt転送2_戻し.Text = _setting.転送2_戻し.ToString();
            this.txt転送2_間隔.Text = _setting.転送2_間隔.ToString();
            this.txtログ_表示件数.Text = _setting.ログ_表示件数.ToString();
            this.txtアーカイブ先.Text = _setting.アーカイブ先.ToString();
            this.txt経過日数.Text = _setting.経過日数.ToString() ;

            _timer1.Interval = _setting.転送1_間隔 * 1000;
            _timer2.Interval = _setting.転送2_間隔 * 1000;
            _timerkeika1.Interval = 1000;
            _timerkeika2.Interval = 1000;

        }

        private async Task 自動開始処理()
        {
            await this.AddSystemLog("自動開始処理を行います。");
            string countdown = "";
            for (int i = 5; i > 0; i--)
            {
                await Task.Delay(1000);
                countdown = countdown + i.ToString();
            }
            await Task.Delay(1000);
            this.tgl転送1_開始.Checked = true;
            this.tgl転送2_開始.Checked = true;
            ScheduleNextRun(); // 初回の実行タイミングを設定
        }




        //ホーム
        private async void tgl転送1_開始_CheckedChanged(object sender, EventArgs e)
        {
            var emsg = "";
            if (this.tgl転送1_開始.Checked)
            {
                //ディレクトリチェックダメならfalseに戻す
                if (!Directory.Exists(_setting.転送1_監視))
                {
                    emsg = emsg + "転送1の監視フォルダが存在しません。\r\n";
                }
                if (!Directory.Exists(_setting.転送1_転送))
                {
                    emsg = emsg + "転送1の転送先フォルダが存在しません。\r\n";
                }
                if (!Directory.Exists(_setting.転送1_戻し))
                {
                    emsg = emsg + "転送1のエラー転送先フォルダが存在しません。\r\n";
                }
                if (_setting.転送1_間隔 == 0)
                {
                    emsg = emsg + "転送1の監視インターバルを設定してください。\r\n";
                }

                if (emsg == "")
                {
                    _timer1.Start();
                    _sdt1 = DateTime.Now;
                    _timerkeika1.Start();

                    this.lbl転送1_状況.Text = "稼働中";
                    this.lbl転送1_開始時間.Text = $"開始時間 : {DateTime.Now.ToString()}";
                    this.lbl転送1_稼働時間.Text = "";
                    await this.AddSystemLog("転送1を開始しました。");
                }
                else
                {
                    _timer1.Stop();
                    _timerkeika1.Stop();
                    this.tgl転送1_開始.Checked = false;
                    this.lbl転送1_状況.Text = "停止中";
                    this.lbl転送1_開始時間.Text = "";
                    this.lbl転送1_稼働時間.Text = "";
                    MessageBox.Show(emsg);
                }
                
            }
            else
            {
                _timer1.Stop();
                _timerkeika1.Stop();
                this.lbl転送1_状況.Text = "停止中";
                this.lbl転送1_開始時間.Text = "";
                this.lbl転送1_稼働時間.Text = "";
                await this.AddSystemLog("転送1を停止しました。");

            }
        }
        private async void tgl転送2_開始_CheckedChanged(object sender, EventArgs e)
        {
            var emsg = "";
            if (this.tgl転送2_開始.Checked)
            {
                //ディレクトリチェックダメならfalseに戻す
                if (!Directory.Exists(_setting.転送2_監視))
                {
                    emsg = emsg + "転送2の監視フォルダが存在しません。\r\n";
                }
                if (!Directory.Exists(_setting.転送2_転送))
                {
                    emsg = emsg + "転送2の転送先フォルダが存在しません。\r\n";
                }
                if (!Directory.Exists(_setting.転送2_戻し))
                {
                    emsg = emsg + "転送2のエラー転送先フォルダが存在しません。\r\n";
                }
                if (_setting.転送2_間隔 == 0)
                {
                    emsg = emsg + "転送2の監視インターバルを設定してください。\r\n";
                }

                if (emsg == "")
                {
                    _timer2.Start();
                    _sdt2 = DateTime.Now;
                    _timerkeika2.Start();

                    this.lbl転送2_状況.Text = "稼働中";
                    this.lbl転送2_開始時間.Text = $"開始時間 : {DateTime.Now.ToString()}";
                    this.lbl転送2_稼働時間.Text = "";
                    await this.AddSystemLog("転送2を開始しました。");                    
                }
                else
                {
                    _timer2.Stop();
                    _timerkeika2.Stop();
                    this.tgl転送2_開始.Checked = false;
                    this.lbl転送2_状況.Text = "停止中";
                    this.lbl転送2_開始時間.Text = "";
                    this.lbl転送2_稼働時間.Text = "";
                    MessageBox.Show(emsg);
                }

            }
            else
            {
                _timer2.Stop();
                _timerkeika2.Stop();
                this.lbl転送2_状況.Text = "停止中";
                this.lbl転送2_開始時間.Text = "";
                this.lbl転送2_稼働時間.Text = "";
                await this.AddSystemLog("転送2を停止しました。");                
            }
        }

        private async void 監視1(object sender, EventArgs e)
        {
            try
            {
                String[] files = Directory.GetFiles(_setting.転送1_監視);
                foreach(var file in files)
                {
                    await this.転送処理1(file);
                }
            }
            catch
            {
                await this.AddSystemLog("転送1で例外が発生しました。");
            }
        }
        private async void 監視2(object sender, EventArgs e)
        {
            try
            {
                String[] files = Directory.GetFiles(_setting.転送2_監視);
                foreach (var file in files)
                {
                    await this.転送処理2(file);
                }
            }
            catch
            {
                await this.AddSystemLog("転送2で例外が発生しました。");
            }
        }


        private async void 転送1_経過時間(object sender, EventArgs e)
        {
            this.lbl転送1_稼働時間.Text = await this.GetElapsedTime(_sdt1);
        }
        private async void 転送2_経過時間(object sender, EventArgs e)
        {
            this.lbl転送2_稼働時間.Text = await this.GetElapsedTime(_sdt2);
        }
        private async Task<String> GetElapsedTime(DateTime sdt)
        {
            string result = "";
            await Task.Run(() => 
            {
                TimeSpan diff = DateTime.Now - sdt;
                result = $"経過時間 : {diff.Days}日 {diff.Hours}時間 {diff.Minutes}分 {diff.Seconds}秒";
            });
            return result;
        }




        private async Task 転送処理1(String file)
        {
            var fullPath = Path.GetFullPath(file);
            var directoryName = Path.GetDirectoryName(file);
            var fileName = Path.GetFileNameWithoutExtension(file);
            var extensionName = Path.GetExtension(file);

            //以下全部一緒
            //this.txtAPPログ.Text += "\r\n" + file;
            //this.txtAPPログ.Text += "\r\n" + fullPath;
            //this.txtAPPログ.Text += "\r\n" + directoryName+ @"\" + fileName + extensionName;
            
            DateTime dt;
            var emsg = "";
            var fNameArray = fileName.Split('_');
            decimal zyutyuuID;

            TranceferFileInfo tranceferFileInfo = new TranceferFileInfo();
            tranceferFileInfo.拡張子 = extensionName;
            tranceferFileInfo.元ファイルFullPath = fullPath;

            try
            {
                //pdf以外は戻す
                if (extensionName != ".pdf")
                {
                    //戻し転送処理
                    await this.転送1_戻し転送(fullPath, emsg);
                    return;
                }

                if (decimal.TryParse(fNameArray[0], out zyutyuuID))//
                {
                    //ここから処理
                    //インデックス取得
                    Index index = new Index();
                    index = await IndexService.GetIndexAsync(zyutyuuID);

                    int len = fNameArray.Length;
                    int tsPosition;

                    //タイムスタンプ位置を取得
                    for (tsPosition = 0; tsPosition < len - 1; tsPosition++)
                    {
                        var str = fNameArray[tsPosition];
                        if (str.Length >= 14)
                        {
                            if (DateTime.TryParseExact(fNameArray[tsPosition].Substring(0, 14), "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
                            {
                                break;
                            }
                        }
                    }

                    if (index.ID != 0)
                    {
                        //受注ファイル以外
                        if (zyutyuuID == 501 || zyutyuuID == 601 || zyutyuuID == 701 || zyutyuuID == 801 || zyutyuuID == 901)
                        {
                            if (len >= 5 && tsPosition == 4)//要素数5以上、タイムスタンプ位置4
                            {
                                var ninusi = fNameArray[2];
                                if (ninusi != "" && ninusi != "登録名称不明" && ninusi != "scan" && ninusi != "取込" && ninusi != "本社受信")
                                {
                                    var sdate = "";
                                    var edate = "";
                                    var comment = fNameArray[1];
                                    var commentArray = comment.Split('-');
                                    var datestr = commentArray[0];
                                    if (DateTime.TryParseExact(datestr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                                    {
                                        sdate = datestr;
                                        edate = datestr;
                                        if (commentArray.Length > 1)
                                        {
                                            comment = commentArray[1];
                                            if (commentArray.Length > 2)
                                            {
                                                for (int i = 2; i < commentArray.Length; i++)
                                                {
                                                    comment = comment + "-" + commentArray[i];
                                                }
                                            }
                                        }
                                    }
                                    var faxnumber = fNameArray[3];
                                    if (faxnumber == "" && index.FAX番号 != "")
                                    {
                                        faxnumber = index.FAX番号;
                                    }
                                    tranceferFileInfo.荷主名 = ninusi;
                                    tranceferFileInfo.FAX番号 = faxnumber;
                                    tranceferFileInfo.タイムスタンプ = fNameArray[4].Substring(0, 14);
                                    tranceferFileInfo.受注CD = fNameArray[0];
                                    tranceferFileInfo.開始日 = sdate;
                                    tranceferFileInfo.終了日 = edate;
                                    tranceferFileInfo.担当部署 = index.担当部署;
                                    tranceferFileInfo.備考 = comment;
                                    tranceferFileInfo.ページ = fNameArray[5];
                                }
                                else
                                {
                                    emsg = "仕分先不明";
                                }
                            }
                            else//スキャンされたファイル等、整頓FAXでファイル名が生成されていないファイル、
                            {
                                emsg = "ファイル名構成不正";
                            }
                        }
                        //通常受注ファイル
                        else
                        {
                            if (len >= 4 && tsPosition == 3)
                            {
                                var ninusi = fNameArray[1];
                                if (ninusi != "登録名称不明" && ninusi != "scan" && ninusi != "取込" && ninusi != "")
                                {
                                    if (index.名称_TF == "")
                                    {
                                        await this.転送1_未登録荷主追加(index.荷主ID, fNameArray[1]);
                                    }
                                }
                                var faxnumber = fNameArray[2];
                                if (faxnumber == "")
                                {
                                    if (index.FAX番号 != "")
                                    {
                                        faxnumber = index.FAX番号;
                                    }
                                }
                                else
                                {
                                    if (index.FAX番号 == "")
                                    {
                                        await this.転送1_未登録FAX番号追加(index.荷主ID, faxnumber);
                                    }
                                }
                                var page = "";
                                if (len - 1 > tsPosition)
                                {
                                    for (var i = tsPosition + 1; i < len; i++)
                                    {
                                        page += fNameArray[i];
                                    }
                                }
                                tranceferFileInfo.荷主名 = index.荷主名;
                                tranceferFileInfo.FAX番号 = faxnumber;
                                tranceferFileInfo.タイムスタンプ = fNameArray[3].Substring(0, 14);
                                tranceferFileInfo.受注CD = fNameArray[0];
                                tranceferFileInfo.開始日 = index.開始日;
                                tranceferFileInfo.終了日 = index.終了日;
                                tranceferFileInfo.担当部署 = index.担当部署;
                                tranceferFileInfo.備考 = index.備考;
                                tranceferFileInfo.ページ = page;
                            }
                            else
                            {
                                emsg = "ファイル名構成不正";
                            }
                        }
                    }
                    else
                    {
                        emsg = "Index取得失敗";
                    }
                }
                else
                {
                    emsg = "受注コード不正";
                }

                if (emsg != "")
                {
                    await this.転送1_戻し転送(fullPath, emsg);
                }
                else
                {
                    //システム移行用
                    await this.システム移行用増長ファイル作成(file);

                    //正常転送処理
                    await this.転送1_正常転送(tranceferFileInfo);
                }
            }
            catch
            {
                emsg = "例外発生";
                await this.転送1_戻し転送(fullPath, emsg);
            }
        }
        private async Task 転送1_正常転送(TranceferFileInfo tranceferFileInfo)
        {
            var log = new TranceferLog(0, "転送1", "正常", tranceferFileInfo.元ファイルFullPath, "", DateTime.Now);            
            await Task.Run(async() => 
            {

                var directoryTo = _setting.転送1_転送 + @"\" + tranceferFileInfo.荷主名 + @"\" + tranceferFileInfo.担当部署;
                if(tranceferFileInfo.担当部署 == "4.倉庫保管")
                {
                    directoryTo += @"\" + tranceferFileInfo.備考;
                }
                //20241011追加、ディレクトリに"yyyymm"を追加（開始月と終了月が違う場合202410-202411という書式にする）
                if(tranceferFileInfo.開始日 != "" && tranceferFileInfo.終了日 != "")
                {
                    directoryTo += @"\" + tranceferFileInfo.開始日.Substring(0, 6);
                    if (tranceferFileInfo.開始日.Substring(0, 6) != tranceferFileInfo.終了日.Substring(0, 6))
                    {
                        directoryTo += "-" + tranceferFileInfo.終了日.Substring(0, 6);
                    }
                }
                //20241011追加終了

                if (!Directory.Exists(directoryTo))
                {
                    Directory.CreateDirectory(directoryTo);
                }                
                var newFileName = tranceferFileInfo.荷主名 + "_" + tranceferFileInfo.FAX番号 + "_" + tranceferFileInfo.タイムスタンプ + "_" + 
                                    tranceferFileInfo.受注CD + "_" + tranceferFileInfo.開始日 + "_" + tranceferFileInfo.終了日 + "_" + 
                                    tranceferFileInfo.担当部署 + "_" + tranceferFileInfo.ページ;
                var newPath = directoryTo + @"\" + newFileName + tranceferFileInfo.拡張子;
                int i = 0;
                while (File.Exists(newPath))
                {
                    newPath = directoryTo + @"\" + newFileName + "(" + i.ToString() + ")" + tranceferFileInfo.拡張子;
                    i++;
                }

//ここからTherefore転送
                //var newTfDir = @"\\192.168.2.240\ThereforeDATA\";
                //var newTfPath = newTfDir + newFileName + tranceferFileInfo.拡張子;
                //i = 0;
                //while (File.Exists(newTfPath))
                //{
                //    newTfPath = newTfDir + newFileName + "(" + i.ToString() + ")" + tranceferFileInfo.拡張子;
                //    i++;
                //}
//ここまで

                try
                {
                    var faxdataRegistrationHistory = new FaxdataRegistrationHistory();
                    faxdataRegistrationHistory.ZyutyuuID = Convert.ToDecimal(tranceferFileInfo.受注CD);
                    faxdataRegistrationHistory.CreateDateTime = DateTime.Now;
                    faxdataRegistrationHistory.TempFullPath = newPath;
                    await FaxdataRegistrationHistoryService.InsertFaxdataAsync(faxdataRegistrationHistory);
                    
                    //await IndexService.UpdateFullPath(Convert.ToDecimal(tranceferFileInfo.受注CD), newPath);

                    FileInfo fileInfo = new FileInfo(tranceferFileInfo.元ファイルFullPath);                    
                    ////fileInfo.CopyTo(newTfPath);
                    fileInfo.MoveTo(newPath);
                    log.変更後Fullpath = newPath;        
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    log.結果 = "正常移動時エラー";                    
                }
            });
            await this.AddTranceferLog(log);

        }
        private async Task 転送1_戻し転送(string fullPath, string emsg)
        {
            var fileName = Path.GetFileNameWithoutExtension(fullPath);
            var extensionName = Path.GetExtension(fullPath);
            //this.txtAPPログ.Text += "\r\n" + file;
            //this.txtAPPログ.Text += "\r\n" + fullPath;
            //this.txtAPPログ.Text += "\r\n" + directoryName+ @"\" + fileName + extensionName;

            await Task.Run(async () =>
            {
                var log = new TranceferLog(0, "転送1", emsg, fullPath, "", DateTime.Now);
                var newPath = _setting.転送1_戻し + @"\" + emsg;
                var fNameArray = fileName.Split('_');
                for (var n = 1; n < fNameArray.Length; n++)
                {
                    if (n > 1)
                    {
                        newPath += "_";
                    }
                    newPath += fNameArray[n];
                }
                newPath += extensionName;
                int i = 0;
                while (File.Exists(newPath))
                {
                    newPath = _setting.転送1_戻し + @"\" + emsg + fileName + "(" + i.ToString() + ")" + extensionName;
                }
                try
                {
                    FileInfo fileInfo = new FileInfo(fullPath);
                    fileInfo.MoveTo(newPath);
                    await this.AddTranceferLog(log);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    log.結果 = "移動時エラー";
                    await this.AddTranceferLog(log);
                }
            });
        }
        private async Task 転送1_未登録荷主追加(int ninusiID,string ninusimei)
        {
            await NinusiInfoService.UpdateNinusimeiAsync(ninusiID, ninusimei);
        }
        private async Task 転送1_未登録FAX番号追加(int ninusiID, string faxnumber)
        {
            await NinusiInfoService.UpdateFaxNumberAsync(ninusiID, faxnumber);
        }
               
        private async Task システム移行用増長ファイル作成(string file)
        {
            await Task.Run(() =>
            {
                var fullPath = Path.GetFullPath(file);
                var fileName = Path.GetFileName(file);
                var newPath = @"\\192.168.2.240\fax受信\FAX受信トレイ\FAX転送2\" + fileName;
                try
                {
                    FileInfo fileInfo = new FileInfo(fullPath);
                    fileInfo.CopyTo(newPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }




        private async Task 転送処理2(String file)
        {
            await Task.Run(async() =>
            {
                var fullPath = Path.GetFullPath(file);
                var directoryName = Path.GetDirectoryName(file);
                var fileName = Path.GetFileNameWithoutExtension(file);
                var extensionName = Path.GetExtension(file);

                //以下全部一緒
                //this.txtAPPログ.Text += "\r\n" + file;
                //this.txtAPPログ.Text += "\r\n" + fullPath;
                //this.txtAPPログ.Text += "\r\n" + directoryName+ @"\" + fileName + extensionName;
                var log = new TranceferLog(0, "転送2", "", fullPath, "", DateTime.Now);
                var directryTo = _setting.転送2_転送;
                var newFileName = "";
                var emsg = "";
                try
                {
                    if (DateTime.TryParseExact(fileName, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                    {
                        newFileName = "scan__" + fileName;
                    }
                    else
                    {
                        newFileName = "取込__" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                    int i = 0;
                    var newFullPath = directryTo + @"\" + newFileName + extensionName;
                    while (File.Exists(newFullPath))
                    {
                        newFullPath = directryTo + @"\" + newFileName + "(" + i.ToString() + ")" + extensionName;
                        i++;
                    }
                    try
                    {
                        FileInfo fileInfo = new FileInfo(fullPath);
                        fileInfo.MoveTo(newFullPath);
                        emsg = "正常";
                        log.変更後Fullpath = newFullPath;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        emsg = "移動時エラー";
                    }
                }
                catch
                {
                    emsg = "例外発生";
                }
                log.結果 = emsg;
                await this.AddTranceferLog(log);
            });
        }











        // 実行処理をスケジュールするメソッド
        private async void ScheduleNextRun()
        {
            DateTime now = DateTime.Now;
            nextRunTime = now.Date + targetTime;

            // 次回実行時刻が現在の時間より過去の場合は翌日に設定
            if (nextRunTime <= now)
            {
                nextRunTime = nextRunTime.AddDays(1);
            }
            await this.AddSystemLog($"次回のアーカイブは{nextRunTime.ToString()}です");
            // タイマーのインターバルを次の実行時刻までに設定
            _timerArchive.Interval = (int)(nextRunTime - now).TotalMilliseconds;
            _timerArchive.Start();
        }

        // タイマーのTickイベントで処理を実行
        private async void _timerArchive_Tick(object sender, EventArgs e)
        {
            _timerArchive.Stop(); // タイマーを停止

            // 実行したい処理をここに記述
            await RunTaskAsync();

            // 次の日の指定時間に再スケジュール
            ScheduleNextRun();
        }

        // 実行する処理
        private async Task RunTaskAsync()
        {
            await this.AddSystemLog($"指定の時間になりました。アーカイブ処理を実行します。{DateTime.Now.ToString()}");
            var syoriCount = 0;
            var zanCount = 0;
            await Task.Run(() =>
            {
                //経過日数を過ぎたファイルをアーカイブ
                //MessageBox.Show("指定の時間になりました。タスクを実行します。");   
                var path = _setting.転送1_転送;
                var di = new DirectoryInfo(path);
                var files = di.GetFiles("*",SearchOption.AllDirectories);
                var cutDate = DateTime.Now.AddDays(-_setting.経過日数);
                DateTime edate;
                Console.WriteLine(cutDate);
                foreach (var f in files)
                {                    
                    //ﾂﾙﾏﾌｰｽﾞ_0253758771_20241007155039_12241018238_20241019_20241019_1.県外発送_624100713820241008202410081.県外発送1 (2) (2)(7)
                    var fnArray = f.Name.Split('_');
                    if (DateTime.TryParseExact(fnArray[5], "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None, out edate))
                    {                        
                        if (edate < cutDate)
                        {
                            Console.WriteLine(f.FullName);



                            syoriCount++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("終了日不明"+f.Name);
                    }
                }
                files = di.GetFiles("*", SearchOption.AllDirectories);
                zanCount = files.Length;
            });

            //請求処理済をアーカイブ
            //var archiveInfos = new List<ArchiveInfo>();
            //archiveInfos = await IndexService.GetArchiveListAsync();
            //if (archiveInfos != null)
            //{
            //    //ﾂﾙﾏﾌｰｽﾞ_0253758771_20241007155039_12241018238_20241019_20241019_1.県外発送_6241007138　　20241008202410081.県外発送1 (2) (2)(7)
            //    //0荷主名_1FAX番号_2タイムスタンプ_3受注ID_4開始日_5終了日_6担当区分_7備考_8金額_9ページ
            //    foreach (var archiveInfo in archiveInfos)
            //    {
            //        var dir = Path.GetDirectoryName(archiveInfo.FullPath);
            //        var fname = Path.GetFileNameWithoutExtension(archiveInfo.FullPath);
            //        var ext = Path.GetExtension(archiveInfo.FullPath);
            //        var fNameArry = fname.Split('_');
            //        fNameArry[7] = archiveInfo.Amount.ToString();
            //        var newFileName = "";
            //        for(int j = 0; j < 8; j++)
            //        {
            //            newFileName += "_" + fNameArry[j];
            //        }
            //        newFileName = newFileName + "_" + archiveInfo.Amount.ToString();
            //        newFileName = newFileName.Substring(2);

            //        var directoryTo = _setting.アーカイブ先 + @"\" + fNameArry[0] + @"\" + fNameArry[6];
            //        if (fNameArry[6] == "4.倉庫保管")
            //        {
            //            directoryTo += @"\" + fNameArry[7];
            //        }
            //        if (fNameArry[4] != "" && fNameArry[5] != "")
            //        {
            //            directoryTo += @"\" + fNameArry[4].Substring(0, 6);
            //            if (fNameArry[4].Substring(0, 6) != fNameArry[5].Substring(0, 6))
            //            {
            //                directoryTo += "-" + fNameArry[5].Substring(0, 6);
            //            }
            //        }
                    
            //        if (!Directory.Exists(directoryTo))
            //        {
            //            Directory.CreateDirectory(directoryTo);
            //        }

            //        var newPath = directoryTo + @"\" + newFileName + ext;
            //        int i = 0;
            //        while (File.Exists(newPath))
            //        {
            //            newPath = directoryTo + @"\" + newFileName + "(" + i.ToString() + ")" + ext;
            //            i++;
            //        }

            //        try
            //        {
            //            //await IndexService.UpdateFullPath(Convert.ToDecimal(tranceferFileInfo.受注CD), newPath);

            //            FileInfo fileInfo = new FileInfo(archiveInfo.FullPath);
            //            ////fileInfo.CopyTo(newTfPath);
            //            fileInfo.MoveTo(newPath);
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e);
            //        }



            //    }
            //}




            await this.AddSystemLog($"終了{DateTime.Now.ToString()} 処理{syoriCount.ToString()} 残{zanCount.ToString()}");
        }






    

    //ログ
        private async Task AddSystemLog(string log)
        {
            await Task.Run(() => 
            {
                var dt = DateTime.Now;
                if (listSystemLog.InvokeRequired)
                {
                    listSystemLog.Invoke(new Action(() => systemLogs.Add(new SystemLog(dt, log))));
                }
                else
                {
                    systemLogs.Add(new SystemLog(dt, log));
                }
            });
        }
        private async Task GetTranceferLog(int kennsuu)
        {
            tranceferLogs = await TranceferLogService.GetTranceferLogAsync(kennsuu);
            this.listTranceferLog.DataSource = tranceferLogs;
        }
        private async Task AddTranceferLog(TranceferLog tranceferLog)
        {
            await TranceferLogService.AddTranceferLogAsync(tranceferLog);
            tranceferLogs.Add(tranceferLog);            
        }
        private async void btnログ更新_Click(object sender, EventArgs e)
        {
            await this.GetTranceferLog(_setting.ログ_表示件数);
        }
        private async void listTranceferLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var result = "";
            await Task.Run(() =>
            {                
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    result = listTranceferLog[e.ColumnIndex, e.RowIndex].Value.ToString();
                }
            });
            this.txtFull表示.Text = result;
        }

        




        //設定
        /// <summary>
        /// フォルダのパスを取得
        /// </summary>
        private void btn転送1_参照_監視_Click(object sender, EventArgs e)
        {
            this.txt転送1_監視.Text = GetPath();
        }
        private void btn転送1_参照_転送_Click(object sender, EventArgs e)
        {
            this.txt転送1_転送.Text = GetPath();
        }
        private void btn転送1_参照_戻し_Click(object sender, EventArgs e)
        {
            this.txt転送1_戻し.Text = GetPath();
        }
        private void btn転送2_参照_監視_Click(object sender, EventArgs e)
        {
            this.txt転送2_監視.Text = GetPath();
        }
        private void btn転送2_参照_転送_Click(object sender, EventArgs e)
        {
            this.txt転送2_転送.Text = GetPath();
        }
        private void btn転送2_参照_戻し_Click(object sender, EventArgs e)
        {
            this.txt転送2_戻し.Text = GetPath();
        }
        private void btnアーカイブ先_参照_Click(object sender, EventArgs e)
        {
            this.txtアーカイブ先.Text = GetPath();
        }
        private String GetPath()
        {
            String path;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "フォルダをしていしてください。";
            dialog.SelectedPath = @"C:";
            dialog.ShowNewFolderButton = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }
            else
            {
                path = "";
            }
            dialog.Dispose();
            return path;
        }
        private async void btn設定保存_Click(object sender, EventArgs e)
        {
            //入力チェック(現在のセッティングと違うところがあるかどうか）
            Setting newSetting = new Setting();
            newSetting.転送1_監視 = this.txt転送1_監視.Text;
            newSetting.転送1_転送 = this.txt転送1_転送.Text;
            newSetting.転送1_戻し = this.txt転送1_戻し.Text;
            newSetting.転送1_間隔 = Convert.ToInt32(this.txt転送1_間隔.Text);
            newSetting.転送2_監視 = this.txt転送2_監視.Text;
            newSetting.転送2_転送 = this.txt転送2_転送.Text;
            newSetting.転送2_戻し = this.txt転送2_戻し.Text;
            newSetting.転送2_間隔 = Convert.ToInt32(this.txt転送2_間隔.Text);
            newSetting.ログ_表示件数 = Convert.ToInt32(this.txtログ_表示件数.Text);
            newSetting.アーカイブ先 = this.txtアーカイブ先.Text;
            newSetting.経過日数 = Convert.ToInt32(this.txt経過日数.Text);

            //違うところがあれば保存
            if (newSetting.転送1_監視 != _setting.転送1_監視 ||
                newSetting.転送1_転送 != _setting.転送1_転送 ||
                newSetting.転送1_戻し != _setting.転送1_戻し ||
                newSetting.転送1_間隔 != _setting.転送1_間隔 ||
                newSetting.転送2_監視 != _setting.転送2_監視 ||
                newSetting.転送2_転送 != _setting.転送2_転送 ||
                newSetting.転送2_戻し != _setting.転送2_戻し ||
                newSetting.転送2_間隔 != _setting.転送2_間隔 ||
                newSetting.ログ_表示件数 != _setting.ログ_表示件数 || 
                newSetting.アーカイブ先 != _setting.アーカイブ先 || 
                newSetting.経過日数 != _setting.経過日数)
            {
                await SettingService.UpdateSettingAsync(newSetting);
                await this.GetSetting();
                await this.AddSystemLog("設定を保存しました。");
                MessageBox.Show("設定を保存しました");
            }
        }





        private async void BtnTest_Click(object sender, EventArgs e)
        {
            await RunTaskAsync();
        }
    }










}
