using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB;
using PicDB.ViewModels;

namespace Helper
{
    /*[ValueConversion(typeof(int), typeof(int))]
    public class CurrentPhotographerIndexConverter : DependencyObject, IValueConverter
    {
        public IPhotographerViewModel Example
        {
            get => GetValue(ExampleProperty).ToString();
            set => SetValue(ExampleProperty, value);
        }
        public static readonly DependencyProperty ExampleProperty =
            DependencyProperty.Register("Example", typeof(string), typeof(ExampleConverter), new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var listCount = 0;
            if (value != null) int.TryParse(value.ToString(), out listCount);
            if (listCount == 0)
            {
                //no items, make selected index – 1
                return -1;
            }
            //has items, make selected index the first one in the list
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }*/

    [ValueConversion(typeof(decimal), typeof(string))]
    public class DecimalZeroToEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal decVal = (decimal)value;
            return decVal != 0 ? decVal.ToString() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal.TryParse((string)value, out decimal decVal);
            return decVal;
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class IntZeroToEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intVal = (int)value;
            //return intVal != 0 ? intVal.ToString() : string.Empty;
            return intVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int.TryParse((string)value, out int intVal);
            return intVal;
        }
    }

    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PicToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            return FileInformation.LoadBitmapImage(path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MessageBoxEx
    {
        private static IntPtr _owner;
        private static HookProc _hookProc;
        private static IntPtr _hHook;

        public static MessageBoxResult Show(string text)
        {
            Initialize();
            return MessageBox.Show(text);
        }

        public static MessageBoxResult Show(string text, string caption)
        {
            Initialize();
            return MessageBox.Show(text, caption);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon, defResult);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult, MessageBoxOptions options)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon, defResult, options);
        }

        public static MessageBoxResult Show(Window owner, string text)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon, defResult);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult, MessageBoxOptions options)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon,
                                    defResult, options);
        }

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public delegate void TimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime);

        public const int WH_CALLWNDPROCRET = 12;

        public enum CbtHookAction : int
        {
            HCBT_MOVESIZE = 0,
            HCBT_MINMAX = 1,
            HCBT_QS = 2,
            HCBT_CREATEWND = 3,
            HCBT_DESTROYWND = 4,
            HCBT_ACTIVATE = 5,
            HCBT_CLICKSKIPPED = 6,
            HCBT_KEYSKIPPED = 7,
            HCBT_SYSCOMMAND = 8,
            HCBT_SETFOCUS = 9
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("User32.dll")]
        public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

        [DllImport("user32.dll")]
        public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        };

        static MessageBoxEx()
        {
            _hookProc = new HookProc(MessageBoxHookProc);
            _hHook = IntPtr.Zero;
        }

        private static void Initialize()
        {
            if (_hHook != IntPtr.Zero)
            {
                throw new NotSupportedException("multiple calls are not supported");
            }

#pragma warning disable CS0618 // Type or member is obsolete
            _hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, _hookProc, IntPtr.Zero, AppDomain.GetCurrentThreadId());
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(_hHook, nCode, wParam, lParam);
            }

            CWPRETSTRUCT msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            IntPtr hook = _hHook;

            if (msg.message == (int)CbtHookAction.HCBT_ACTIVATE)
            {
                try
                {
                    CenterWindow(msg.hwnd);
                }
                finally
                {
                    UnhookWindowsHookEx(_hHook);
                    _hHook = IntPtr.Zero;
                }
            }

            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static void CenterWindow(IntPtr hChildWnd)
        {
            Rectangle recChild = new Rectangle(0, 0, 0, 0);
            bool success = GetWindowRect(hChildWnd, ref recChild);

            int width = recChild.Width - recChild.X;
            int height = recChild.Height - recChild.Y;

            Rectangle recParent = new Rectangle(0, 0, 0, 0);
            success = GetWindowRect(_owner, ref recParent);

            System.Drawing.Point ptCenter = new System.Drawing.Point(0, 0);
            ptCenter.X = recParent.X + ((recParent.Width - recParent.X) / 2);
            ptCenter.Y = recParent.Y + ((recParent.Height - recParent.Y) / 2);


            System.Drawing.Point ptStart = new System.Drawing.Point(0, 0);
            ptStart.X = (ptCenter.X - (width / 2));
            ptStart.Y = (ptCenter.Y - (height / 2));

            ptStart.X = (ptStart.X < 0) ? 0 : ptStart.X;
            ptStart.Y = (ptStart.Y < 0) ? 0 : ptStart.Y;

            int result = MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width,
                                    height, false);
        }
    }
}
