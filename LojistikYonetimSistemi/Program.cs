using System;
using System.Windows.Forms;
using LojistikYonetimSistemi.Views;

namespace LojistikYonetimSistemi
{
    // Uygulamanýn baþlangýç noktasý.
    // Uygulama açýlýnca direkt LoginForm açýlýyor,
    // kullanýcý giriþ yapýnca MainForm devreye giriyor.
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Uygulama LoginForm ile baþlýyor
            Application.Run(new LoginForm());
        }
    }
}