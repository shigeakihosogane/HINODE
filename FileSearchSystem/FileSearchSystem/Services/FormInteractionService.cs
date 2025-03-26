using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchSystem.Services
{
    public class FormInteractionService
    {
        public Action<int, int>? ResizeForm; // フォームサイズ変更のデリゲート

        public void ChangeFormSize(int width, int height)
        {
            ResizeForm?.Invoke(width, height);
        }
    }
}
