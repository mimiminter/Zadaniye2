using System.Data;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Data.SqlClient;
using z2.Models;

namespace z2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void Button_Click_Clients(object sender, RoutedEventArgs e)
    {
        menuwindow menuwindow = new menuwindow();
        menuwindow.Show();
        Close();
    }
    private void Button_Click_Sum(object sender, RoutedEventArgs e)
    {
        SumPrice sumPrice = new SumPrice();
        sumPrice.Show();
        Close();
    }
    private void Button_Click_auto(object sender, RoutedEventArgs e)
    {
        AutoWindow autoWindow = new AutoWindow();
        autoWindow.Show();
        Close();
    }
    private void Button_Click_Purshaches(object sender, RoutedEventArgs e)
    {
        PurchaseWindow purchaseWindow = new PurchaseWindow();
        purchaseWindow.Show();
        Close();
    }
 
   
   
}