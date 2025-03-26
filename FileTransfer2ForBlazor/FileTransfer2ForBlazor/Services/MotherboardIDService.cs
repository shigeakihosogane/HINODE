using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer2ForBlazor.Services
{
    public class MotherboardIDService
    {
        public string GetMotherboardSerialNumber()
        {
            string serialNumber = string.Empty;

            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                {
                    serialNumber = obj["SerialNumber"]?.ToString();
                    break; // 最初の結果を取得
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"シリアルナンバー取得中にエラーが発生しました: {ex.Message}");
            }

            return serialNumber;
        }
    }
}
