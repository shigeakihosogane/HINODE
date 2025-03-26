using MetroFramework.Controls;
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

namespace ThereFileInport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.metroTextBox1.Text = @"E:\CanonExportData\配送依頼FAX\Documents";
            this.metroTextBox2.Text = @"E:\Output";
            this.metroTextBox3.Text = @"E:\ExceptionOutput";



        }






        private void btnGo_Click(object sender, EventArgs e)
        {
            var emsg = "";
            if(!Directory.Exists(this.metroTextBox1.Text))
            {
                emsg += "Inputを指定してください。\r\n";
            }
            if (!Directory.Exists(this.metroTextBox2.Text))
            {
                emsg += "Outputを指定してください。\r\n";
            }
            if (!Directory.Exists(this.metroTextBox3.Text))
            {
                emsg += "ExceptionOutputを指定してください。\r\n";
            }

            if(emsg != "")
            {
                MessageBox.Show(emsg);
            }
            else
            {
                this.ConvertThereFileToIwdt(metroTextBox1.Text, metroTextBox2.Text, metroTextBox3.Text);
            }
        }


        private void ConvertThereFileToIwdt(string inputFolder,string outputFolder,string ExceptionOutputFolder)
        {
            DirectoryInfo di = new DirectoryInfo(inputFolder);
            DirectoryInfo[] directoryInfos = di.GetDirectories("*",SearchOption.AllDirectories);


            //MessageBox.Show(directoryInfos.Length.ToString()); 

            //foreach(var directoryInfo in directoryInfos)
            //{

            //}









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
        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.metroTextBox1.Text = GetPath(); 
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.metroTextBox2.Text = GetPath();

        }
        private void metroButton3_Click(object sender, EventArgs e)
        {
            this.metroTextBox3.Text = GetPath();

        }
    }
}
