

namespace WindowsFormsApp2.Models
{
    public class Setting
    {
        public string 転送1_監視 { get; set; } = "";
        public string 転送1_転送 { get; set; } = "";
        public string 転送1_戻し { get; set; } = "";
        public int 転送1_間隔 { get; set; } = 0;
        public string 転送2_監視 { get; set; } = "";
        public string 転送2_転送 { get; set; } = "";
        public string 転送2_戻し { get; set; } = "";
        public int 転送2_間隔 { get; set; } = 0;
        public int ログ_表示件数 { get; set; } = 0;
        public string アーカイブ先 { get; set; } = "";
        public int 経過日数 { get; set; } = 0;


    }


}
