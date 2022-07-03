using System;

public class FGL
{
	public FGL()
	{
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

        private void num(Button btn) {
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
    }
}
