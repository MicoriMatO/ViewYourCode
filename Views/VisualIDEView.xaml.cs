using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using VPMSerialezator.Models;
using VPMSerialezator.Models.Prefabs;
using VPMSerialezator.Models.TestPreFabs;
using ViewYourCode.Controllers;
using VPMSerialezator;

namespace ViewYourCode
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Envoriment.VisualPrimitivEnvoriment VisualPrimitivEnvoriment;
        public List<BasePreFabsModel> preFabsList;
        public VPMSerialezator.SerializatiorVPM vPM;


        public MainWindow()
        {
            VisualPrimitivEnvoriment = new Envoriment.VisualPrimitivEnvoriment();
            preFabsList = new List<BasePreFabsModel>();




            InitializeComponent();

            EditGrid.MouseMove += VisualPrimitivEnvoriment.Grid_MouseMove;
            EditGrid.MouseLeftButtonUp += VisualPrimitivEnvoriment.mouseLeftButtonUp;


            PreFabsList.ItemsSource = preFabsList;

            preFabsList.Add(new TestUnit());
        }

        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {


            //var temp = e.Data.GetData(DataFormats.FileDrop);//сделать через D&D
            //string idPrefab = preFabsList[PreFabsList.SelectedIndex].PreFabsId;//КОСТЫЛЁЧЕК

            VisualPrimitivEnvoriment.CreatePuzzl(ref EditGrid, preFabsList[PreFabsList.SelectedIndex]);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FileWriter fileWriter = new FileWriter();
            Build_Click(sender, e);

            fileWriter.WriteToSkript(vPM.VPMmodel);
        }
        private void Build_Click(object sender, RoutedEventArgs e)
        {
            List<BasePreFabsModel> OutList = new List<BasePreFabsModel>();
            vPM = new VPMSerialezator.SerializatiorVPM();

            foreach (var item in EditGrid.Children)
            {
                Panel tempItem = item as Panel;

                OutList.Add(tempItem.Resources["model"] as BasePreFabsModel);
            }
            vPM.SerializateIntoVPM(OutList);
        }

        private void PreFabsList_Grab(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            int indexSelectItem = listBox.SelectedIndex;

            if (indexSelectItem >= 0)
            { 
                DragDrop.DoDragDrop(listBox, (PreFabsList.Items[indexSelectItem] as BasePreFabsModel).PreFabsId, DragDropEffects.Move);
            }      
        }
    }
}
