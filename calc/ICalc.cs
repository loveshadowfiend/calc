using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace calc {
    public interface ICalc {
        private static dynamic num1 = (decimal)0, num2 = (decimal)0, mem = (decimal)0;
        private static string op = "";
        private static bool action = false;
        private static void MemBtnsRecolor(string color, Button BtnMemoryRestore, Button BtnMemoryPlus, Button BtnMemoryMinus, Button BtnMemoryClear) {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            BtnMemoryRestore.Background = brush;
            BtnMemoryPlus.Background = brush;
            BtnMemoryMinus.Background = brush;
            BtnMemoryClear.Background = brush;
        }
        public static dynamic Calc(dynamic num1, dynamic num2, string op) {
            switch (op) {
                case "+":
                    num1 += num2;
                    break;
                case "-":
                    num1 -= num2;
                    break;
                case "⨯":
                    num1 *= num2;
                    break;
                case "/":
                    try {
                        num1 /= num2;
                    } catch (DivideByZeroException) {
                        MessageBox.Show("can't divide by zero");
                    }
                    break;
            }
            return num1;
        }
        public static void Num(Button btn, TextBox Display, TextBox History) {
            if (History.Text.Length == 0) {
                num2 = 0;
                op = "";
            }
            if (action || Display.Text == "0") {
                Display.Text = "";
                action = false;
            }
            if (Display.Text.Length >= 16) {
                return;
            }
            Display.Text += btn.Content;
            if (op == "") {
                try {
                    num1 = decimal.Parse(Display.Text);
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num1 = double.Parse(Display.Text);
                }
            } else {
                try {
                    num2 = decimal.Parse(Display.Text);
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num2 = double.Parse(Display.Text);
                }
            }
        }
        public static void Dot(TextBox Display) {
            if (!Display.Text.Contains('.') && Display.Text.Length <= 16) {
                Display.Text += '.';
            }
        }
        public static void Operation(Button btn, TextBox Display, TextBox History) {
            if (History.Text.Length == 0) {
                num2 = 0;
                op = (string)btn.Content;
                History.Text += num1.ToString("G12") + ' ' + op + ' ';
            } else if (num2 == 0) {
                op = (string)btn.Content;
                History.Text = History.Text.Remove(History.Text.Length - 2) + op + ' ';
            } else {
                try {
                    num1 = (decimal)num1;
                    num2 = (decimal)num2;
                    num1 = Calc(num1, num2, op);
                    Display.Text = num1.ToString("G12");
                } catch (OverflowException) {
                    num1 = (double)num1;
                    num2 = (double)num2;
                    num1 = Calc(num1, num2, op);
                    Display.Text = num1.ToString("G12");
                }
                op = (string)btn.Content;
                History.Text += num2.ToString("G12") + ' ' + op + ' ';
                num2 = 0;
            }
            action = true;
        }
        public static void Equal(TextBox Display, TextBox History) {
            if (Display.Text != "0" && num2 == 0) {
                try {
                    num1 = (decimal)num1;
                    num2 = (decimal)num2;
                    num1 = Calc(num1, num1, op);
                    Display.Text = num1.ToString("G12");

                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num1 = (double)num1;
                    num2 = (double)num2;
                    num1 = Calc(num1, num1, op);
                    Display.Text = num1.ToString("G12");
                }
            } else {
                try {
                    num1 = (decimal)num1;
                    num2 = (decimal)num2;
                    num1 = Calc(num1, num2, op);
                    Display.Text = num1.ToString("G12");
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num1 = (double)num1;
                    num2 = (double)num2;
                    num1 = Calc(num1, num2, op);
                    Display.Text = num1.ToString("G12");
                }
            }
            History.Text = "";
            action = true;
        }
        public static void Back(TextBox Display) {
            if (action) {
                return;
            }
            if (Display.Text.Length > 0) {
                Display.Text = Display.Text.Remove(Display.Text.Length - 1);
                if (Display.Text.Length == 0) {
                    Display.Text = "0";
                }
                if (op == "") {
                    try {
                        num1 = decimal.Parse(Display.Text);
                        num2 = (decimal)0;
                    } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                        num1 = double.Parse(Display.Text);
                        num2 = (double)0;
                    }
                } else {
                    try {
                        num2 = decimal.Parse(Display.Text);
                        num1 = (decimal)num1;
                    } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                        num2 = double.Parse(Display.Text);
                        num1 = (double)num1;
                    }
                }
            }
        }
        public static void ClearEntry(TextBox Display) {
            Display.Text = "0";
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
        public static void Clear(TextBox Display, TextBox History) {
            num1 = 0;
            num2 = 0;
            op = "";
            Display.Text = "0";
            History.Text = "";
            action = false;
        }
        public static void Sqrt(TextBox Display, TextBox History) {
            if (double.Parse(Display.Text) < 0) {
                MessageBox.Show("you can't get square root of a negative number");
                Reverse(Display, History);
                return;
            }
            if (op == "") {
                try {
                    num1 = (decimal)Math.Sqrt(double.Parse(Display.Text));
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num1 = Math.Sqrt(double.Parse(Display.Text));
                }
                Display.Text = num1.ToString("G12");
            } else {
                try {
                    num2 = (decimal)Math.Sqrt(double.Parse(Display.Text));
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num2 = Math.Sqrt(double.Parse(Display.Text));
                }
                Display.Text = num2.ToString("G12");
            }
            action = true;
        }
        public static void Reciproc(TextBox Display) {
            if (Display.Text == "0") {
                MessageBox.Show("can't divide by zero");
                return;
            }
            if (op == "") {
                try {
                    num1 = 1 / decimal.Parse(Display.Text);
                    Display.Text = num1.ToString("G12");
                    num2 = (decimal)num2;
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num1 = 1 / double.Parse(Display.Text);
                    Display.Text = num1.ToString("G12");
                    num2 = (double)num2;
                }
            } else {
                try {
                    num2 = 1 / decimal.Parse(Display.Text);
                    Display.Text = num2.ToString("G12");
                    num1 = (decimal)num1;
                } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                    num2 = 1 / double.Parse(Display.Text);
                    Display.Text = num2.ToString("G12");
                    num1 = (double)num1;
                }
            }
            action = true;
        }
        public static void Percent(TextBox Display, TextBox History) {
            if (History.Text.Length > 0 && History.Text[History.Text.Length - 2].ToString() != op) {
                while (History.Text[History.Text.Length - 1].ToString() != op) {
                    History.Text = History.Text.Remove(History.Text.Length - 1);
                }
            }
            num2 *= num1 / 100;
            Display.Text = num2.ToString("G12");
        }
        public static void Reverse(TextBox Display, TextBox History) {
            if (op == "") {
                if (num1.GetType() == typeof(decimal)) {
                    num1 = decimal.Parse(Display.Text) * (-1);
                } else {
                    num1 = double.Parse(Display.Text) * (-1);
                }
                Display.Text = num1.ToString("G12");
            } else {
                if (num1.GetType() == typeof(decimal)) {
                    num2 = decimal.Parse(Display.Text) * (-1);
                } else {
                    num2 = double.Parse(Display.Text) * (-1);
                }
                Display.Text = num2.ToString("G12");
            }
        }
        public static void MemoryStore(TextBox Display, Button BtnMemoryRestore, Button BtnMemoryPlus, Button BtnMemoryMinus, Button BtnMemoryClear) {
            try {
                mem = decimal.Parse(Display.Text);
            } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                mem = (double)mem;
                mem = double.Parse(Display.Text);
            }
            if (mem != 0) {
                MemBtnsRecolor("white", BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
            } else {
                MemBtnsRecolor("f6f6f6", BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
            }
        }
        public static void MemoryRestore(TextBox Display) {
            Display.Text = mem.ToString("G12");
            if (op == "") {
                num1 = mem;
            } else {
                num2 = mem;
            }
            action = true;
        }
        public static void MemoryClear(Button BtnMemoryRestore, Button BtnMemoryPlus, Button BtnMemoryMinus, Button BtnMemoryClear) {
            mem = (decimal)0;
            MemBtnsRecolor("#f6f6f6", BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
        }
        public static void MemoryPlus(TextBox Display, Button BtnMemoryRestore, Button BtnMemoryPlus, Button BtnMemoryMinus, Button BtnMemoryClear) {
            try {
                mem = (decimal)mem;
                mem += decimal.Parse(Display.Text);
            } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                mem = (double)mem;
                mem += double.Parse(Display.Text);
            }
            if (mem == 0) {
                MemBtnsRecolor("#f6f6f6", BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
            }
        }
        public static void MemoryMinus(TextBox Display, Button BtnMemoryRestore, Button BtnMemoryPlus, Button BtnMemoryMinus, Button BtnMemoryClear) {
            try {
                mem = (decimal)mem;
                mem -= decimal.Parse(Display.Text);
            } catch (Exception ex) when (ex is OverflowException || ex is FormatException) {
                mem = (double)mem;
                mem -= double.Parse(Display.Text);
            }
            if (mem == 0) {
                MemBtnsRecolor("#f6f6f6", BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
            }
        }
    }
}
