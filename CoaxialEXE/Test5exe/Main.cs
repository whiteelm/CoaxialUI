using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Test5exe
{
    public partial class Main : Form
    {
        [DllImport(@"Coaxial.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "COAXIAL")]
        private static extern void Coaxial(ref double er, ref double a, ref double b, ref double dC,
            ref double dL, ref double dZ0, int mod);
        public Main()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Calculate(1);
            ZoLabel.Font = new Font(ZoLabel.Font, FontStyle.Underline);
            dLab.Font = new Font(dLab.Font, FontStyle.Regular);
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Calculate(2);
            dLab.Font = new Font(dLab.Font, FontStyle.Underline);
            ZoLabel.Font = new Font(ZoLabel.Font, FontStyle.Regular);
        }

        private void Calculate(int mod)
        {
            double dC = 0;
            double dL = 0;
            try
            {
                var dZ0 = Converter(textBox1);
                var b = Converter(textBox2);
                var a = Converter(textBox3);
                var er = Converter(textBox4);
                
                Coaxial(ref er, ref a, ref b, ref dC, ref dL, ref dZ0, mod);

                textBox1.Text = NumberRound(dZ0);
                textBox2.Text = NumberRound(b);
                textBox3.Text = NumberRound(a);
                textBox4.Text = NumberRound(er);
                textBox5.Text = NumberRound(dC);
                textBox6.Text = NumberRound(dL);
                textBox7.Text = NumberRound(er);
            }
            catch
            {
                MessageBox.Show(@"Ошибка в заполнении", @"Внимание!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static double Converter(Control textBox)
        {
            var replace = textBox.Text.Replace(".", ",");
            return Convert.ToDouble(replace);
        }

        private static string NumberRound(double num)
        {
            return Math.Round(num, 4).ToString(CultureInfo.CurrentCulture);
        }
    }
}
