using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HandyControl.Controls;
using HandyControl.Data;
using WpfFreeSqlBuilder.ViewModal;
using Window = System.Windows.Window;

namespace WpfFreeSqlBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModal Modal { get; }
        public MainWindow(MainViewModal modal)
        {
            Modal = modal;
            InitializeComponent();
        }

    }
}
