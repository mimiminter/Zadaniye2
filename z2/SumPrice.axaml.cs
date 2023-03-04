using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using z2.Models;

namespace z2;

public partial class SumPrice : Window
{
    private PostgresContext _context;

    public SumPrice()
    {
        InitializeComponent();
        _context = new PostgresContext();
    }
    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
    private void BtnSum_Click(object sender, RoutedEventArgs e)
    {
        Table1.Items = _context.Database.SqlQuery<decimal>($"select sum(product.price) from purchase,product where purchase.id_product = product.id").ToList();
    }
}