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
            //テキストボックス、ウィンドウタイトルの設定
            kekka1.ReadOnly = true;
            kekka2.ReadOnly = true;
            this.Text = "素数じゃないなら素因数分解しちゃうぞ";
        }

        public void button1_Click(object sender, EventArgs e)
        {
            
            bool _end = false;
            //_endがfalseの間処理され続ける
            do
            {
                //textBox1から入力された値をstring(文字列)として取得し_sに代入
                string _s = Convert.ToString(textBox1.Text);

                //余分な文字(数字以外)を文字列から消去
                Regex _r = new Regex(@"[^0-9]");
                _s = _r.Replace(_s, "");
                _s = Regex.Replace(_s, @"\s", "");
                //もし文字列が何もなくなったら_sに0を代入
                if (_s == "")
                {
                    _s = "0";
                }

               
                int _n;
                //数字のみになった文字列_sをint(数値)として_nに代入した時、
                //32bitの最大値(2,147,483,647)を越えてないか確認
                //越えてなかったらそのまま_nに数値として代入
                //越えていたらelse以下の処理(値が大きすぎるとエラーを返し処理終了)
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

                //_nを素数か判定する(97行目~)
                bool _p = Pri(_n);

                //素数ならtrue
                //素数じゃないならfalse(else以下)
                if (_p == true)
                {
                    //入力された数値が素数であることをkekka1(テキストボックス)
                    //に表示して処理終了
                    kekka1.Text = Convert.ToString(_n + "は素数です");
                    kekka2.Text = Convert.ToString("");
                    _end = true;
                    break;
                }
                else
                {
                    //_nを素因数分解して式をstringとして__sに代入(128行目～)
                    string __s = Pfact(_n);
                    //kekka1に_nが素数ではないことを
                    //kekka2に_nを素因数分解した式を表示して処理終了
                    kekka1.Text = Convert.ToString(_n + "は素数ではありません");
                    kekka2.Text = Convert.ToString(_n + "=" + __s);
                    _end = true;
                    break;
                }
            } while (_end == false);
        }



        //素数か判別するプログラム
        //Pri(変数)で_nnに変数の値を代入
        //Pri(x),x=2⇒_nn=2
        static bool Pri(int _nn)
        {
            //2未満なら素数ではないのでfalseを返して処理終了
            if (_nn < 2) { return false; }
            //2なら素数なのでtrueを返して処理終了
            else if (_nn == 2) { return true; }
            //偶数なら素数ではないのでfalseを返して処理終了(%はmod記号)
            else if (_nn % 2 == 0) { return false; };

            //平方根をとる
            double Sq_nn = Math.Sqrt(_nn);
            //ある数Nの平方根以下の素数で割れなければNは素数であることを利用して
            //_nnを割る動作をSq_nn(入力された数値の平方根)まで繰り返す
            for (int _i = 3; _i <= Sq_nn; _i += 1)
            {
                if (_nn % _i == 0)
                {
                    //割り切れたため素数ではないのでfalseを返して処理終了
                    return false;
                }
            }
            //割り切れなかったため素数なのでtrueを返して処理終了
            return true;
        }

        //素因数分解するプログラム
        //Pfact(変数)で_nnnに変数の値を代入
        //Pfact(x),x=2⇒_nnn=2
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
            //_nnnが0だったらそのまま0
            //_nnnが1だったらそのまま1を帰して処理終了
            if (_nnn == 0) { return "0"; }
            else if (_nnn == 1) { return "1"; }

            //endがfalseの間実行され続ける
            while (end == false)
            {

                //割る数が素数であるようにする
                while (prime == false)
                {
                    //Pfacの値を1ずつ増加させて素数ならこの処理を抜け出す
                    Pfac++;
                    prime = Pri(Pfac);
                }

                
                prime2 = true;
                //素数で割れるか確かめて割れるなら割る
                //prime2がtrueの間処理され続ける
                while (prime2 == true)
                {
                    
                    if (_nnn % Pfac == 0)
                    {                        
                        //_nnnをPfac(素数)で割り切れるなら割って
                        //__k(割った回数)を加算して処理続行
                        _nnn /= Pfac;
                        __k += 1;
                    }
                    else
                    {
                        //_nnnがPfacで割り切れなくなった場合

                        //1回でも割っているならばlist(__i)に割った素数を
                        //list(__j)に割った回数を保存して
                        //__k(割った回数)を0にリセットして処理終了
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
                //割れなくなったらその時点の_nnnが素数かどうか確認する
                prime3 = Pri(_nnn);

                //素数じゃなかったらif以下
                //素数だったらelse以下の処理に移動
                if (prime3 == false)
                {
                    //素数じゃなかった場合_nnnが1ならばもう割る数がないので
                    //endをtrueにして処理終了して次へ
                    if (_nnn == 1)
                    {
                        end = true;
                    }
                    else
                    {
                        //1じゃなかったらprimeをfalseにして
                        //148行目に移動して割る数を探す処理に戻る
                        prime = false;
                    }

                }
                else
                {
                    //素数だった場合list(__i)の最後尾に今の値_nnnを
                    //list(__j)の最後尾に1を保存して処理終了し次へ
                    __i.Add(_nnn);
                    __j.Add(1);
                    end = true;
                }
            }

            //strに""(null)をstring(文字列)として代入
            string str = "";


            //素因数分解した後の式を作成
            for (int __l = 0; __l <= __i.Count - 1; __l += 1)
            {
                //list(__j)(割った回数)の__l番目の値が1かどうか
                if (__j[__l] != 1)
                {
                    //1じゃなかったらstrに__l番目の"(__i(割った素数)^__j(割った回数))×"
                    //の文字列を追加
                    str += "(" + __i[__l] + "^" + __j[__l] + ")×";
                }
                else
                {
                    //1だった場合は1乗なので"割った素数×"をstrに追加
                    str += __i[__l] + "×";
                }
            }
            //追加し終わったら最後に"×"が余分につくので文字列から最後の文字を消去
            str = str.Remove(str.Length - 1);

            //最終的な文字列を変数__sに返す(84行目)
            return str;
        }
    }
}
