using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace calc {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        dynamic num1 = (decimal)0, num2 = (decimal)0, mem = (decimal)0;
        string op = "";
        bool action = false;
        private dynamic calc(dynamic num1, dynamic num2, string op) {
            switch (op) {
                case "+":
                    num1 += num2;
                    display.Text = num1.ToString("G12");
                    break;
                case "-":
                    num1 -= num2;
                    display.Text = num1.ToString("G12");
                    break;
                case "⨯":
                    num1 *= num2;
                    display.Text = num1.ToString("G12");
                    break;
                case "/":
                    try {
                        num1 /= num2;
                    } catch (DivideByZeroException) {
                        MessageBox.Show("can't divide by zero");
                    }
                    display.Text = num1.ToString("G12");
                    break;
            }
            return num1;
        }
        private void mem_btns_recolor(string color) {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            btn_MR.Background = brush;
            btn_M_plus.Background = brush;
            btn_M_minus.Background = brush;
            btn_MC.Background = brush;
        }
        public MainWindow() {
            InitializeComponent();
        }
        private void btn_num_Click(object sender, RoutedEventArgs e) {
            var btn = (Button)sender;
            if (history.Text.Length == 0) {
                num2 = 0;
                op = "";
            }
            if (action || display.Text == "0") {
                display.Text = "";
                action = false;
            }
            if (display.Text.Length >= 16) {
                return;
            }
            display.Text += btn.Content;
            if (op == "") {
                try {
                    num1 = decimal.Parse(display.Text);
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num1 = double.Parse(display.Text);
                }
            } else {
                try {
                    num2 = decimal.Parse(display.Text);
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num2 = double.Parse(display.Text);
                }
            }
        }
        private void btn_dot_Click(object sender, RoutedEventArgs e) {
            if (!display.Text.Contains('.')) {
                display.Text += '.';
            }
        }
        private void btn_op_Click(object sender, RoutedEventArgs e) {
            var btn = (Button)sender;
            if (history.Text.Length == 0) {
                num2 = 0;
                op = (string)btn.Content;
                history.Text += num1.ToString("G12") + ' ' + op + ' ';
            } else if (num2 == 0) {
                op = (string)btn.Content;
                history.Text = history.Text.Remove(history.Text.Length - 2) + op + ' ';
            } else {
                try {
                    num1 = (decimal)num1;
                    num2 = (decimal)num2;
                    num1 = calc(num1, num2, op);
                } catch (OverflowException) {
                    num1 = (double)num1;
                    num2 = (double)num2;
                    num1 = calc(num1, num2, op);
                }
                op = (string)btn.Content;
                history.Text += num2.ToString("G12") + ' ' + op + ' ';
                num2 = 0;
            }
            action = true;
        }
        private void btn_equals_Click(object sender, RoutedEventArgs e) {
            if (display.Text != "0" && num2 == 0) {
                try {
                    num1 = (decimal)num1;
                    num2 = (decimal)num2;
                    num1 = calc(num1, num1, op);
                } catch (OverflowException) {
                    num1 = (double)num1;
                    num2 = (double)num2;
                    num1 = calc(num1, num1, op);
                }
            } else {
                try {
                    num1 = (decimal)num1;
                    num2 = (decimal)num2;
                    num1 = calc(num1, num2, op);
                } catch (OverflowException) {
                    num1 = (double)num1;
                    num2 = (double)num2;
                    num1 = calc(num1, num2, op);
                }
            }
            history.Text = "";
            action = true;
        }
        private void btn_back_Click(object sender, RoutedEventArgs e) {
            if (action) {
                return;
            }
            if (display.Text.Length > 0) {
                display.Text = display.Text.Remove(display.Text.Length - 1);
                if (display.Text.Length == 0) {
                    display.Text = "0";
                }
                if (op == "") {
                    try {
                        num1 = decimal.Parse(display.Text);
                        num2 = (decimal)0;
                    } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                        num1 = double.Parse(display.Text);
                        num2 = (double)0;
                    }
                } else {
                    try {
                        num2 = decimal.Parse(display.Text);
                        num1 = (decimal)num1;
                    } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                        num2 = double.Parse(display.Text);
                        num1 = (double)num1;
                    }
                }
            }
        }
        private void btn_ce_Click(object sender, RoutedEventArgs e) {
            display.Text = "0";
            if (op == "") {
                num1 = (decimal)0;
            } else {
                if (num1.GetType() == typeof(decimal)) {
                    num2 = (decimal)0;
                } else {
                    num2 = (double)0;
                }
            }
        }
        private void btn_c_Click(object sender, RoutedEventArgs e) {
            num1 = 0;
            num2 = 0;
            op = "";
            display.Text = "0";
            history.Text = "";
            action = false;
        }
        private void btn_sqrt_Click(object sender, RoutedEventArgs e) {
            if (double.Parse(display.Text) < 0) {
                MessageBox.Show("you can't get square root of a negative number");
                btn_reverse_Click(sender, e);
                return;
            }
            if (op == "") {
                try {
                    num1 = (decimal)Math.Sqrt(double.Parse(display.Text));
                } catch (OverflowException) {
                    num1 = Math.Sqrt(double.Parse(display.Text));
                }
                display.Text = num1.ToString("G12");
            } else {
                try {
                    num2 = (decimal)Math.Sqrt(double.Parse(display.Text));
                } catch (OverflowException) {
                    num2 = Math.Sqrt(double.Parse(display.Text));
                }
                display.Text = num2.ToString("G12");
            }
            action = true;
        }
        private void btn_reciproc_Click(object sender, RoutedEventArgs e) {
            if (display.Text == "0") {
                MessageBox.Show("can't divide by zero");
                return;
            }
            if (op == "") {
                try {
                    num1 = 1 / decimal.Parse(display.Text);
                    display.Text = num1.ToString("G12");
                    num2 = (decimal)num2;
                } catch(Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num1 = 1 / double.Parse(display.Text);
                    display.Text = num1.ToString("G12");
                    num2 = (double)num2;
                }
            } else {
                try {
                    num2 = 1 / decimal.Parse(display.Text);
                    display.Text = num2.ToString("G12");
                    num1 = (decimal)num1;
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num2 = 1 / double.Parse(display.Text);
                    display.Text = num2.ToString("G12");
                    num1 = (double)num1;
                }
            }
            action = true;
        }
        private void btn_percent_Click(object sender, RoutedEventArgs e) {
            if (history.Text.Length > 0 && history.Text[history.Text.Length - 2].ToString() != op) {
                while (history.Text[history.Text.Length - 1].ToString() != op) {
                    history.Text = history.Text.Remove(history.Text.Length - 1);
                }
            }
            num2 *= num1 / 100;
            display.Text = num2.ToString("G12");
        }
        private void btn_reverse_Click(object sender, RoutedEventArgs e) {
            if (op == "") {
                if (num1.GetType() == typeof(decimal)) {
                    num1 = decimal.Parse(display.Text) * (-1);
                } else {
                    num1 = double.Parse(display.Text) * (-1);
                }
                display.Text = num1.ToString("G12");
            } else {
                if (num1.GetType() == typeof(decimal)) {
                    num2 = decimal.Parse(display.Text) * (-1);
                } else {
                    num2 = double.Parse(display.Text) * (-1);
                }
                display.Text = num2.ToString("G12");
            }
        }
        private void btn_MS_Click(object sender, RoutedEventArgs e) {
            try {
                mem = decimal.Parse(display.Text);
            } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                mem = (double)mem;
                mem = double.Parse(display.Text);
            }
            if (mem != 0) {
                mem_btns_recolor("white");
            } else {
                mem_btns_recolor("f6f6f6");
            }
        }
        private void btn_MR_Click(object sender, RoutedEventArgs e) {
            display.Text = mem.ToString("G12");
            if (op == "") {
                num1 = mem;
            } else {
                num2 = mem;
            }
            action = true;
        }
        private void btn_MC_Click(object sender, RoutedEventArgs e) {
            mem = (decimal)0;
            mem_btns_recolor("#f6f6f6");
        }
        private void btn_M_plus_Click(object sender, RoutedEventArgs e) {
            try {
                mem = (decimal)mem;
                mem += decimal.Parse(display.Text);
            } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                mem = (double)mem;
                mem += double.Parse(display.Text);
            }
            if (mem == 0) {
                mem_btns_recolor("f6f6f6");
            }
        }
        private void btn_M_minus_Click(object sender, RoutedEventArgs e) {
            try {
                mem = (decimal)mem;
                mem -= decimal.Parse(display.Text);
            } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                mem = (double)mem;
                mem -= double.Parse(display.Text);
            }
            if (mem == 0) {
                mem_btns_recolor("#f6f6f6");
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            bool shift = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
            if (shift == true && e.Key == Key.D8) {
                btn_multiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                return;
            }

            switch (e.Key) {
                case Key.Back:
                    btn_back.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D1:
                    btn1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D2:
                    btn2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D3:
                    btn3.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D4:
                    btn4.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D5:
                    btn5.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D6:
                    btn6.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D7:
                    btn7.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D8:
                    btn8.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D9:
                    btn9.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D0:
                    btn0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.OemPlus:
                    btn_plus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.OemMinus:
                    btn_minus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.OemQuestion:
                    btn_divide.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.Multiply:
                    btn_multiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
            }
        }
    }
}
