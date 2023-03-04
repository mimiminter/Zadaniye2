using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Azure.Messaging;
using z2.Models;

namespace z2;

public partial class menuwindow : Window
{
    private PostgresContext _context;
    public menuwindow()
    {
        InitializeComponent();
        _context = new PostgresContext();
        Load();
    }

    private void Load()
    {
        Table1.Items = _context.Clients.ToList();
    }
    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (Table1.SelectedItem is Client selectedItem)
        {
            _context.Clients.Remove(selectedItem);
            _context.SaveChanges();
            Load();
        }
    }
    private void BtnInsert_Click(object sender, RoutedEventArgs e)
    {
        //int num = Convert.ToInt32(id_tb.Text);
        if (!Int32.TryParse(id_tb.Text, out Int32 Id) || !Int32.TryParse(passport_series_tb.Text, out Int32 PassportSeries) || !Int32.TryParse(passport_number_tb.Text, out Int32 PassportNumber))
        {
            return;
        }
        Client client1 = new()
        {
            Id = Id,
            Fname = fname_tb.Text,
            Mname = mname_tb.Text,
            Lname = lname_tb.Text,
            PassportSeries = PassportSeries,
            //PassportSeries = Convert.ToInt32(passport_series_tb.Text),
            PassportNumber = PassportNumber,
            //PassportNumber = Convert.ToInt32(passport_number_tb.Text),
            HomeAddress = adress_tb.Text,
            NumberPhone = number_tb.Text
        };
        _context.Clients.Add(client1);
        _context.SaveChanges();
        Load();
    }
    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (Table1.SelectedItem is Client selectedItem)
        {
            if (!Int32.TryParse(id_tb.Text, out Int32 Id) || !Int32.TryParse(passport_series_tb.Text, out Int32 PassportSeries) || !Int32.TryParse(passport_number_tb.Text, out Int32 PassportNumber))
            {
                return;
            }
            selectedItem.Id = Id;
            selectedItem.Fname = fname_tb.Text;
            selectedItem.Mname = mname_tb.Text;
            selectedItem.Lname = lname_tb.Text;
            selectedItem.PassportNumber = PassportNumber;
            selectedItem.PassportSeries = PassportSeries;
            selectedItem.HomeAddress = adress_tb.Text;
            selectedItem.NumberPhone = number_tb.Text;
            _context.SaveChanges();
            Load();
        }
    }
    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void Table1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Table1.SelectedItem is Client selectedItem)
        {
            id_tb.Text = selectedItem.Id.ToString();
            fname_tb.Text = selectedItem.Fname;
            mname_tb.Text = selectedItem.Mname;
            lname_tb.Text = selectedItem.Lname;
            passport_number_tb.Text = selectedItem.PassportNumber.ToString();
            passport_series_tb.Text = selectedItem.PassportSeries.ToString();
            adress_tb.Text = selectedItem.HomeAddress;
            number_tb.Text = selectedItem.NumberPhone;
        }
    }

    
}