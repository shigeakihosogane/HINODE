using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Services
{
    public class ThereforeService
    {
        private static String _connectionString;
        static ThereforeService()
        {
            _connectionString = DBConnectionCube.GetConnectionString();
        }


        //ログの登録
        public static void TranceferLogToTherefore()
        {

        }




    }
}
