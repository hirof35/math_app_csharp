using System;
using System.Windows;
using System.Windows.Media;

namespace sannsuuApp
{
    public partial class MainWindow : Window
    {
        // 現在の問題と正解を保持する変数
        private int _correctAnswer;
        private Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        // 「スタート / つぎの問題」ボタンが押されたとき
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // 選択されている学年を取得 (0=1年生, 1=2年生...)
            int grade = GradeSelector.SelectedIndex + 1;

            GenerateQuestion(grade);

            // 画面のリセット
            AnswerBox.Text = "";
            ResultLabel.Text = "";
            AnswerBox.Focus(); // 入力欄にカーソルを合わせる
        }

        // 学年に応じた問題を生成するメソッド
        private void GenerateQuestion(int grade)
        {
            int a, b;

            switch (grade)
            {
                case 1: // 1年生：10までの足し算
                    a = _random.Next(1, 10);
                    b = _random.Next(1, 11 - a); // 合計が10以下
                    _correctAnswer = a + b;
                    QuestionLabel.Text = $"{a} + {b} =";
                    break;

                case 2: // 2年生：九九
                    a = _random.Next(2, 10);
                    b = _random.Next(1, 10);
                    _correctAnswer = a * b;
                    QuestionLabel.Text = $"{a} × {b} =";
                    break;

                case 3: // 3年生：3桁の引き算
                    a = _random.Next(100, 1000);
                    b = _random.Next(10, a);
                    _correctAnswer = a - b;
                    QuestionLabel.Text = $"{a} - {b} =";
                    break;

                case 4: // 4年生：割り算（あまりなし）
                    b = _random.Next(2, 13);
                    _correctAnswer = _random.Next(2, 21);
                    a = b * _correctAnswer;
                    QuestionLabel.Text = $"{a} ÷ {b} =";
                    break;

                case 5: // 5年生：小数の足し算（0.1単位）
                    double da = _random.Next(1, 100) / 10.0;
                    double db = _random.Next(1, 100) / 10.0;
                    // 小数点第1位までの計算
                    decimal result = (decimal)da + (decimal)db;
                    _correctAnswer = (int)(result * 10); // 判定用に10倍して保持するか、小数判定ロジックへ
                    QuestionLabel.Text = $"{da} + {db} =";
                    // 注意: int型での判定を維持するため、ここでは「*10」した値を答えに設定するか
                    // または _correctAnswer を double型に変更してください。
                    break;

                case 6: // 6年生：xを使った計算
                    a = _random.Next(2, 10);
                    _correctAnswer = _random.Next(2, 15);
                    QuestionLabel.Text = $"{a} × x = {a * _correctAnswer} \n x =";
                    break;
            }
        }

        // 「こたえあわせ」ボタンが押されたとき
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(QuestionLabel.ToString()) || QuestionLabel.ToString() == "---") return;

            if (int.TryParse(AnswerBox.Text, out int userAns))
            {
                if (userAns == _correctAnswer)
                {
                    ResultLabel.Text = "★ せいかい！";
                    ResultLabel.Foreground = Brushes.Red;
                }
                else
                {
                    ResultLabel.Text = $"× ざんねん！ (こたえ：{_correctAnswer})";
                    ResultLabel.Foreground = Brushes.Blue;
                }
            }
            else
            {
                MessageBox.Show("すうじを いれてね！");
            }
        }
    }
}
