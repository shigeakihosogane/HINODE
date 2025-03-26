using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WindowsFormsApp2.Models;
using WindowsFormsApp2.Services;
using PdfiumViewer;

namespace FileBrowser
{
    public partial class Form1 : Form
    {        
        List<ImageInfo> imageInfos = new List<ImageInfo>();
        ImageList imageList = new ImageList();


        public Form1()
        {
            InitializeComponent();
            

            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DirTreeNode driveNode = new DirTreeNode(@"ライブラリ1", @"F:\FAX受信トレイ\細金\FAX転送TEST\転送先");
            treeView1.Nodes.Add(driveNode);

            // +ボタンを表示させるために仮のノードを追加しておく
            // ※ここではドライブ配下にはなんらかのフォルダが存在する前提
            driveNode.Nodes.Add(new DirTreeNode());
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            await this.LoadImagesToListView();
        }





        //リストボックス
        private async Task LoadImagesToListView()
        {
            listView1.Items.Clear();

            ImageInfo filterstr = new ImageInfo();
            //テスト検索条件
            //filterstr.受注ID = 12241018238;
            filterstr.荷主名 = "ﾂﾙﾏﾌｰｽﾞ";
            //一致する画像インフォリストを取得
            imageInfos = await ImageService.GetImageDataFromDBAsync(filterstr);



            
            imageList.ImageSize = new Size(100, 100);
            listView1.LargeImageList = imageList;

            Console.WriteLine(imageInfos.Count + "件取得");

            int imageIndex = 0;
            try
            {
                foreach (var item in imageInfos)
                {
                    byte[] fileData = item.ImageData; ;  // PDFのバイナリデータを取得
                    if (fileData == null) continue;

                    Image thumbnail;
                    using (var pdfStream = new MemoryStream(fileData))
                    using (var pdfDocument = PdfDocument.Load(pdfStream))
                    {
                        // 最初のページをサムネイルとして取得
                        thumbnail = pdfDocument.Render(0, 100, 100, PdfRenderFlags.CorrectFromDpi);
                    }
                    imageList.Images.Add(thumbnail);

                    var listViewItem = new ListViewItem(item.ImageName)
                    {
                        ImageIndex = imageIndex
                    };
                    listView1.Items.Add(listViewItem);
                    imageIndex++;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message,ex.Source);
            }
        }





        //ピクチャーボックス
        private async void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string fileName = listView1.SelectedItems[0].Text;
                byte[] fileData = imageInfos.SingleOrDefault(imageInfos => imageInfos.ImageName == fileName).ImageData;

                if (fileData != null)
                {
                    using (var pdfStream = new MemoryStream(fileData))
                    using (var pdfDocument = PdfDocument.Load(pdfStream))
                    {
                        // PDFの最初のページを画像として取得
                        var pageImage = pdfDocument.Render(0, 100, 100, PdfRenderFlags.CorrectFromDpi);
                        pictureBox1.Image = pageImage;
                    }
                }
                else
                {
                    MessageBox.Show("PDFファイルが見つかりません。");
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }












        //ツリービュー

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // 展開するノードを取得
            DirTreeNode dirNode = (DirTreeNode)e.Node;

            // 展開ノード配下のノードをクリア
            dirNode.Nodes.Clear();

            // 展開するノード配下のフォルダ数を繰り返し
            foreach (string directoryPath in Directory.GetDirectories(dirNode.DirectoryPath))
            {
                // フォルダ情報を取得
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

                // OSのシステムフォルダはここで除外する
                if ((directoryInfo.Attributes & FileAttributes.System) != FileAttributes.System)
                {
                    // 展開するノード配下にサブフォルダのノードを追加
                    DirTreeNode subDirNode = new DirTreeNode(directoryInfo.Name, directoryPath);
                    dirNode.Nodes.Add(subDirNode);

                    // サブフォルダ配下にさらにフォルダがある場合
                    if (Directory.GetDirectories(directoryPath).Length > 0)
                    {
                        // +ボタンを表示させるために仮のノードを追加しておく
                        subDirNode.Nodes.Add(new DirTreeNode());
                    }
                }
            }
        }









        private void button2_Click(object sender, EventArgs e)
        {
            var path = @"F:\FAX受信トレイ\細金\FAX転送TEST\ﾏﾂｲﾌｰｽﾞ_0253758771_20241007155039_6241007138_20241008_20241008_1.県外発送__1 (2).pdf";

            using (var pdfDocument = PdfDocument.Load(path))
            {
                var image = pdfDocument.Render(0, 200, 200, true);
                pictureBox1.Image = image;
            }            
            
            
            
            //byte[] bytes = File.ReadAllBytes(path);





        }



    }
}
