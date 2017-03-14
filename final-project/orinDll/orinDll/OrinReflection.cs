using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace orinDll
{
    public class OrinReflection
    {
        public string Message { get; set; }
        public void showMessage()
        {
            MessageBox.Show(Message);
        }
    }
}
