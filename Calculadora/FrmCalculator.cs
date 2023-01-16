using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora 
{
    public partial class FrmCalculator : Form 
    {
        bool mover = false;
        Point posicao_inicial;

        Double result = 0;
        string operation = string.Empty;
        string fstNum, secNum;
        bool enterValue = false;
        public FrmCalculator() 
        {
            InitializeComponent();
        }

        private void BtnMathOperation_Click(object sender, EventArgs e) 
        {
            if (result != 0) btnResultado.PerformClick();
            else result = Double.Parse(txtDisplay1.Text);

            CustomButton button = (CustomButton)sender;
            operation = button.Text;
            enterValue = true;
            if(txtDisplay1.Text !="0") 
            {
                txtDisplay2.Text = fstNum = $"{result}{operation}";
                txtDisplay1.Text = string.Empty;
            }
        }

        private void btnResultado_Click(object sender, EventArgs e) 
        {
            secNum = txtDisplay1.Text;
            txtDisplay2.Text = $"{txtDisplay2.Text} {txtDisplay1.Text}=";
            if(txtDisplay1.Text != string.Empty) 
            {
                if (txtDisplay1.Text == "0") txtDisplay2.Text = string.Empty;
                switch(operation)
                {
                    case "+":
                        txtDisplay1.Text = (result + Double.Parse(txtDisplay1.Text)).ToString();
                        rtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {txtDisplay1.Text}\n");
                        break;
                    case "-":
                        txtDisplay1.Text = (result - Double.Parse(txtDisplay1.Text)).ToString();
                        rtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {txtDisplay1.Text}\n");
                        break;
                    case "x":
                        txtDisplay1.Text = (result * Double.Parse(txtDisplay1.Text)).ToString();
                        rtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {txtDisplay1.Text}\n");
                        break;
                    case "÷":
                        txtDisplay1.Text = (result / Double.Parse(txtDisplay1.Text)).ToString();
                        rtBoxDisplayHistory.AppendText($"{fstNum}{secNum} = {txtDisplay1.Text}\n");
                        break;
                    default: txtDisplay2.Text = $"{txtDisplay1.Text}= ";
                        break;
                }

                result = Double.Parse(txtDisplay1.Text);
                operation = string.Empty;
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            pnlHistory.Height = (pnlHistory.Height == 5)? pnlHistory.Height = 345 : 5;
        }

        private void BtnClearHistory_Click(object sender, EventArgs e) 
        {
            rtBoxDisplayHistory.Clear();
            if (rtBoxDisplayHistory.Text == string.Empty)
                rtBoxDisplayHistory.Text = "Ainda não há histórico";
        }

        private void btnApagar_Click(object sender, EventArgs e) 
        {
            if (txtDisplay1.Text.Length > 0)
                txtDisplay1.Text = txtDisplay1.Text.Remove(txtDisplay1.Text.Length - 1, 1);
            if (txtDisplay1.Text == string.Empty) txtDisplay1.Text = "0";
        }

        private void btnC_Click(object sender, EventArgs e) 
        {
            txtDisplay1.Text = "0";
            txtDisplay2.Text = string.Empty;
            result = 0;
        }

        private void btnExit_Click(object sender, EventArgs e) 
        {
            Application.Exit();
        }

        private void BtnOperations_Click(object sender, EventArgs e) 
        {
            CustomButton button = (CustomButton)sender;
            operation = button.Text;
            switch(operation) 
            {
                case "%":
                    txtDisplay2.Text = $"%({txtDisplay1.Text})";
                    txtDisplay1.Text = Convert.ToString(Convert.ToDouble(txtDisplay1.Text)/Convert.ToDouble(100));
                    break;
            }
            rtBoxDisplayHistory.AppendText($"{txtDisplay2.Text}{txtDisplay1.Text}\n");
        }

        private void BtnMaximized_Click(object sender, EventArgs e) 
        {
            //this.WindowState = FormWindowState.Maximized;
        }

        private void BtnMinimized_Click(object sender, EventArgs e) 
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            mover = true;
            posicao_inicial = new Point(e.X, e.Y);
        }
        private void pnlTitle_MouseUp(object sender, MouseEventArgs e) 
        {
            mover = false;
        }
        private void pnlTitle_MouseMove(object sender, MouseEventArgs e) 
        {
            if (mover) 
            {
                Point novo = PointToScreen(e.Location);
                Location = new Point(novo.X - posicao_inicial.X, novo.Y - posicao_inicial.Y);
            }
        }

        private void BtnNum_Click(object sender, EventArgs e) 
        {
            if (txtDisplay1.Text == "0" || enterValue) txtDisplay1.Text = string.Empty;

            enterValue = false;
            CustomButton button =(CustomButton)sender;
            if (button.Text == ".") 
            {
                if (!txtDisplay1.Text.Contains("."))
                    txtDisplay1.Text = txtDisplay1.Text + button.Text;
            }
            else txtDisplay1.Text = txtDisplay1.Text + button.Text;
        }
    }
}
