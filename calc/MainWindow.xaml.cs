using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace calc {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void BtnNumClick(object sender, RoutedEventArgs e) {
            var btn = (Button)sender;
            FGL.Num(btn, Display, History);
        }
        private void BtnDotClick(object sender, RoutedEventArgs e) {
            FGL.Dot(Display);
        }
        private void BtnOpClick(object sender, RoutedEventArgs e) {
            var btn = (Button)sender;
            FGL.Operation(btn, Display, History);
        } 
        private void BtnEqualClick(object sender, RoutedEventArgs e) {
            FGL.Equal(Display, History);
        } 
        private void BtnBackClick(object sender, RoutedEventArgs e) {
            FGL.Back(Display);
        } 
        private void BtnClearEntryClick(object sender, RoutedEventArgs e) {
            FGL.ClearEntry(Display);
        } 
        private void BtnClearClick(object sender, RoutedEventArgs e) {
            FGL.Clear(Display, History);
        } 
        private void BtnSqrtClick(object sender, RoutedEventArgs e) {
            FGL.Sqrt(Display, History);
        } 
        private void BtnReciprocClick(object sender, RoutedEventArgs e) {
            FGL.Reciproc(Display);
        }
        private void BtnPercentClick(object sender, RoutedEventArgs e) {
            FGL.Percent(Display, History);
        }
        private void BtnReverseClick(object sender, RoutedEventArgs e) {
            FGL.Reverse(Display, History);
        } 
        private void BtnMemoryStoreClick(object sender, RoutedEventArgs e) {
            FGL.MemoryStore(Display, BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
        }
        private void BtnMemoryRestoreClick(object sender, RoutedEventArgs e) {
            FGL.MemoryRestore(Display);
        } 
        private void BtnMemoryClearClick(object sender, RoutedEventArgs e) {
            FGL.MemoryClear(BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
        }
        private void BtnMemoryPlusClick(object sender, RoutedEventArgs e) {
            FGL.MemoryPlus(Display, BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
        } 
        private void BtnMemoryMinusClick(object sender, RoutedEventArgs e) {
            FGL.MemoryMinus(Display, BtnMemoryRestore, BtnMemoryPlus, BtnMemoryMinus, BtnMemoryClear);
        } 
        private void WindowKeyDown(object sender, KeyEventArgs e) {
            bool shift = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
            if (shift == true && e.Key == Key.D8) {
                BtnMultiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                return;
            }

            switch (e.Key) {
                case Key.Back:
                    BtnBack.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D1:
                    Btn1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D2:
                    Btn2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D3:
                    Btn3.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D4:
                    Btn4.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D5:
                    Btn5.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D6:
                    Btn6.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D7:
                    Btn7.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D8:
                    Btn8.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D9:
                    Btn9.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.D0:
                    Btn0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.OemPlus:
                    BtnPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.OemMinus:
                    BtnMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.OemQuestion:
                    BtnDivide.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.Multiply:
                    BtnMultiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
                case Key.Enter:
                    BtnEqual.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    return;
            }
        }
    }
}
