using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaintAdmin
{
    class KeyClass
    {
        public static void DontDelete(KeyEventArgs e, TextBox textBox, int CountSymbhols)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (textBox.SelectionStart <= CountSymbhols)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
