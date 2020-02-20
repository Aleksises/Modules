using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ShowGreetings()
        {
            if (textBox1.TextLength > 0)
            {
                var greetings = string.Format("Hello, {0}", textBox1.Text);
                MessageBox.Show(greetings, "Greetings", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("You did not provide a username!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            ShowGreetings();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ShowGreetings();
            }
        }
    }
}
