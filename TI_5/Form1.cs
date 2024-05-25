using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TI_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool checks() {
            //проверка что бы все поля были заполнены
            if (inp_H.Text.Length == 0 || inp_K.Text.Length == 0 || inp_P.Text.Length == 0 ||
                inp_Q.Text.Length == 0 || inp_X.Text.Length == 0 || openFileDialog1.FileName=="" || openFileDialog2.FileName == "") {
                label8.Text = "Choose file";
                label8.Visible = true;
                return true;
            }
            //проверка условий
            try
            {
                var q = BigInteger.Parse(inp_Q.Text);
                var p = BigInteger.Parse(inp_P.Text);
                var h = BigInteger.Parse(inp_H.Text);
                var x = BigInteger.Parse(inp_X.Text);
                var k = BigInteger.Parse(inp_K.Text);
                var m = BigInteger.Parse(inp_M.Text);
                var y = BigInteger.Parse(inp_Y.Text);
                ValueChecker.checkQ(q);
                ValueChecker.checkP(p, q);
                ValueChecker.checkH(q, p, h);
                ValueChecker.checkInterval(BigInteger.Zero, q, x);
                ValueChecker.checkInterval(BigInteger.One, q - BigInteger.One, k);
            }
            catch (Exception) {
                label8.Text = "Wrong Data";
                label8.Visible = true;
                return true;
            }
            label8.Visible = false;
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //заполняются все необходимые поля для подписи
            Signer signer = new Signer(new BigInteger(Convert.ToInt32(inp_Q.Text)),
                new BigInteger(Convert.ToInt32(inp_P.Text)),
                new BigInteger(Convert.ToInt32(inp_H.Text)),
                new BigInteger(Convert.ToInt32(inp_K.Text)),
                new BigInteger(Convert.ToInt32(inp_M.Text)),
                openFileDialog1.FileName,
                openFileDialog2.FileName
            );
            BigInteger[] arr = null;

            //проверка на некоректные данные
            if(!checks())

                //подписывает файл
                arr = signer.ensign(new BigInteger(Convert.ToInt32(inp_X.Text)));

            //вывод результата
            if (arr != null) {
                textBox2.Text = "";
                BigInteger hash = arr[0];
                BigInteger r = arr[1];
                BigInteger s = arr[2];
                BigInteger y = arr[3];
                inp_Y.Text = y.ToString();
                StringBuilder str = new StringBuilder();
                textBox1.Text = ($"Hash: {hash.ToString()}\r\nR: {r.ToString()}\r\nS: {s.ToString()}\r\nY: {y.ToString()}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //заполняются все необходимые поля для подписи
            Signer signer = new Signer(new BigInteger(Convert.ToInt32(inp_Q.Text)),
                new BigInteger(Convert.ToInt32(inp_P.Text)),
                new BigInteger(Convert.ToInt32(inp_H.Text)),
                new BigInteger(Convert.ToInt32(inp_K.Text)),
                new BigInteger(Convert.ToInt32(inp_M.Text)),
                openFileDialog1.FileName,
                openFileDialog2.FileName
            );
            BigInteger[] arr = null;

            //проверка на некоректные данные
            if (!checks())

                //проверка подписи
                arr = signer.design(Convert.ToInt32(inp_Y.Text));

            //вывод результата
            if (arr != null){
                BigInteger hash = arr[0];
                BigInteger r = arr[1];
                BigInteger s = arr[2];
                BigInteger w = arr[3];
                BigInteger u1 = arr[4];
                BigInteger u2 = arr[5];
                BigInteger v = arr[6];
                StringBuilder str = new StringBuilder();
                if (v == r) {
                    textBox2.Text = "V == R(True)";
                }
                else {
                    textBox2.Text = "V != R(False)";
                }
                textBox1.Text = ($"Hash: {hash.ToString()}\r\nR: {r.ToString()}\r\nS: {s.ToString()}\r\nW: {w.ToString()}\r\n" +
                    $"U1: {u1.ToString()}\r\nU2: {u2.ToString()}\r\nV: {v.ToString()}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }
    }
}
