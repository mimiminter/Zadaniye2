using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using z2.Models;

namespace z2;

public partial class PurchaseWindow : Window
{
    private PostgresContext _context;
    public PurchaseWindow()
    {
        InitializeComponent();
        _context = new PostgresContext();
        Load();
    }
    private void Load()
    {
        Table1.Items = _context.Purchases.
            Include(q=>q.IdClientNavigation).
            Include(q=> q.IdPaymentTypeNavigation).
            Include(q=> q.IdProductNavigation).ThenInclude(q => q.IdMachineMarkNavigation).
            Include(q => q.IdProductNavigation).ThenInclude(q =>q.IdModelCarNavigation).ToList();
        comboModel1.Items = _context.PaymentTypes.ToList();
        comboModel1.SelectedIndex = 0;
        comboModel.Items = _context.MachineMarks.ToList();
        comboModel.SelectedIndex = 0;
    }
    private void Table1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Table1.SelectedItem is Purchase selectedItem)
        {
            id_tb.Text = selectedItem.Id.ToString();
            car_tb.Text = selectedItem.IdProduct.ToString();
            client_tb.Text = selectedItem.IdClient.ToString();
            data_tb.Text = selectedItem.Data.ToString();
            payment_tb.Text = selectedItem.IdPaymentType.ToString();
        }
    }
    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
    private void BtnSearch_Click(object sender, RoutedEventArgs e)
    {
        
        if (comboModel.SelectedIndex != -1)
        {
            Table1.Items = _context.Purchases.ToList()
                .Where(q => q.IdProductNavigation.IdMachineMark == ((MachineMark) comboModel.SelectedItem).Id);
        }
    }
    private void BtnSearch1_Click(object sender, RoutedEventArgs e)
    {
        
        if (comboModel1.SelectedIndex != -1)
        {
            Table1.Items = _context.Purchases.ToList()
                .Where(q => q.IdPaymentType == ((PaymentType) comboModel1.SelectedItem).Id);
        }
    }
    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (Table1.SelectedItem is Purchase selectedItem)
        {
            if (!Int32.TryParse(id_tb.Text, out Int32 Id) || !Int32.TryParse(car_tb.Text, out Int32 IdProduct )
                                                          || !Int32.TryParse(client_tb.Text, out Int32 IdClient)
                                                          || !Int32.TryParse(payment_tb.Text, out Int32 IdPaymentType)
                                                          || !DateOnly.TryParse(data_tb.Text, out  DateOnly data))
                                                      
            {
                return;
            }
            selectedItem.Id = Id;
            selectedItem.IdClient = IdClient;
            selectedItem.IdProduct = IdProduct;
            selectedItem.Data = data;
            selectedItem.IdPaymentType = IdPaymentType;
            _context.SaveChanges();
            Load();
        }
    }
    private void BtnInsert_Click(object sender, RoutedEventArgs e)
    {
        if (!Int32.TryParse(id_tb.Text, out Int32 Id) || !Int32.TryParse(car_tb.Text, out Int32 IdProduct )
                                                      || !Int32.TryParse(client_tb.Text, out Int32 IdClient)
                                                      || !Int32.TryParse(payment_tb.Text, out Int32 IdPaymentType)
                                                      || !DateOnly.TryParse(data_tb.Text, out  DateOnly data))
                                                      
        {
            return;
        }
        Purchase purchase1 = new()
        {
            Id = Id,
            IdProduct = IdProduct,
            IdClient= IdClient,
            IdPaymentType = IdPaymentType,
            Data = data
        };
        _context.Purchases.Add(purchase1);
        _context.SaveChanges();
        Load();
    }
    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (Table1.SelectedItem is Purchase selectedItem)
        {
            _context.Purchases.Remove(selectedItem);
            _context.SaveChanges();
            Load();
        }
    }
}