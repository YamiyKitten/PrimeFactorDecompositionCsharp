using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FMA1
{
    public partial class F_1 : Form
    {
        public F_1()
        {
            InitializeComponent();
        }

        private void F_1_Load(object sender, EventArgs e)
        {
            kekka1.ReadOnly = true;
            kekka2.ReadOnly = true;
            this.Text = "素数じゃないなら素因数分解しちゃうぞ";
        }

        public void button1_Click(object sender, EventArgs e)
        {
            bool _end = false;
            do
            {
                string _s = Convert.ToString(textBox1.Text);

                Regex _r = new Regex(@"[^0-9]");
                _s = _r.Replace(_s, "");
                _s = Regex.Replace(_s, @"\s", "");
                if (_s == "")
                {
                    _s = "0";
                }

                int _n;
                if (Int32.TryParse(_s, out _n))
                {
                    _n = int.Parse(_s);
                }
                else
                {
                    kekka1.Text = Convert.ToString("<!>値が大きすぎます！");
                    kekka2.Text = Convert.ToString("");
                    _end = true;
                    break;
                }

                bool _p = Pri(_n);

                if (_p == true)
                {
                    kekka1.Text = Convert.ToString(_n + "は素数です。");
                    kekka2.Text = Convert.ToString("");
                    _end = true;
                    break;
                }
                else
                {
                    string __s = Pfact(_n);
                    kekka1.Text = Convert.ToString(_n + "は素数ではありません。");
                    kekka2.Text = Convert.ToString(_n + "=" + __s);
                    _end = true;
                    break;
                }
            } while (_end == false);
        }



        //素数か判別するプログラム
        static bool Pri(int _nn)
        {
            //0,1,2,偶数を排除
            if (_nn < 2) { return false; }
            else if (_nn == 2) { return true; }
            else if (_nn % 2 == 0) { return false; };

            //平方根をとる。
            double Sq_nn = Math.Sqrt(_nn);
            for (int _i = 3; _i <= Sq_nn; _i += 1)
            {
                if (_nn % _i == 0)
                {
                    //割り切れるので排除
                    return false;
                }
            }
            return true;
        }
        static string Pfact(int _nnn)
        {
            //変数の設定
            int Pfac = 1;
            var __i = new List<int>();
            var __j = new List<int>();
            int __k = 0;
            bool prime = false;
            bool prime2 = false;
            bool end = false;
            if (_nnn == 0) { return "0"; }
            else if (_nnn == 1) { return "1"; }

            while (end == false)
            {
                //割る数が素数であるようにする。
                while (prime == false)
                {
                    Pfac++;
                    prime = Pri(Pfac);
                }

                
                prime2 = true;
                //素数で割れるか確かめて割れるなら割る。

                while (prime2 == true)
                {
                    if (_nnn % Pfac == 0)
                    {                        
                        _nnn /= Pfac;
                        __k += 1;
                    }
                    else
                    {
                        if (__k > 0)
                        {
                            __i.Add(Pfac);
                            __j.Add(__k);
                            __k = 0;
                        }
                        prime2 = false;
                    }
                }
                bool prime3 = false;
                //割れなくなったら1を除外して素数かどうか確認する

                prime3 = Pri(_nnn);
                if (prime3 == false)
                {
                    if (_nnn == 1)
                    {
                        end = true;
                    }
                    else
                    {
                        prime = false;
                    }

                }
                else
                {
                    __i.Add(_nnn);
                    __j.Add(1);
                    end = true;
                }
            }

            string str = "";
            for (int __l = 0; __l <= __i.Count - 1; __l += 1)
            {
                if (__j[__l] != 1)
                {
                    str += "(" + __i[__l] + "^" + __j[__l] + ")×";
                }
                else
                {
                    str += __i[__l] + "×";
                }
            }
            str = str.Remove(str.Length - 1);
            return str;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
