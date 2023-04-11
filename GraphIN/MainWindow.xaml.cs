using LiveCharts.Configurations;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using GraphIN.ViewModels;

namespace GraphIN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainVM _mainVM;
        public MainWindow()
        {
            InitializeComponent();


            _mainVM = new MainVM();
            DataContext = _mainVM;
            //Texty.Text = ExcelToArrayConverter.ConvertColumnToArray("C:\\Users\\MI\\Desktop\\данные.xlsx", 6)[5];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainVM.SwitchDrawing();
        }
    }

    
}