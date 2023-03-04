using System;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Microsoft.EntityFrameworkCore;
using z2.Models;

namespace z2;

public partial class AutoWindow : Window
{
    private PostgresContext _context;
    public AutoWindow()
    {
        InitializeComponent();
        _context = new PostgresContext();
        Load();
    }
    private void Load()
    
    {
        Table1.Items = _context.Products.
            Include(q=>q.IdMachineMarkNavigation).
            Include(p => p.IdColorNavigation).
            Include(p => p.IdModelCarNavigation).
            Include(p => p.IdManufacturerCountryNavigation)
            .ToList();
        comboModel.Items = _context.ModelCars.ToList();
        comboModel.SelectedIndex = 0;
        comboMark.Items = _context.MachineMarks.ToList();
        comboMark.SelectedIndex = 0;
        comboModel1.Items = _context.ModelCars.ToList();
        comboModel1.SelectedIndex = 0;
    }

    private void SC(object sender, SelectionChangedEventArgs e)
    {
        
    }
    private void BtnSearch1_Click(object sender, RoutedEventArgs e)
    {
        if (comboModel.SelectedIndex != -1)
        {
            Table1.Items = _context.Products.ToList()
                .Where(q => q.IdModelCar == ((ModelCar)comboModel.SelectedItem).Id);
        }
    }
    private void BtnSearch2_Click(object sender, RoutedEventArgs e)
    {
        int selectedModel = ((ModelCar)comboModel1.SelectedItem).Id;
        int selectedMark = ((MachineMark)comboMark.SelectedItem).Id;
        
        var countAuto = _context.Products.ToList().
            Where(q => q.IdModelCar == selectedModel && q.IdMachineMark == selectedMark).FirstOrDefault().Quantity;
        //var countAuto1 = _context.Products.ToList().
           // Where(q => q.IdMachineMark == selectedMark).First().Quantity;
        if (countAuto > 0)
        {
            count_tb.Text = "car exist";
        }
        else
        {
            count_tb.Text = "car not exist";
        }
    }
    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (Table1.SelectedItem is Product selectedItem)
        {
            if (!Int32.TryParse(id_tb.Text, out Int32 Id) || !Int32.TryParse(counry_man_tb.Text, out Int32 IdManufacturerCountry )
                                                          || !Int32.TryParse(machine_mark_tb.Text, out Int32 IdMachineMark)
                                                          || !Int32.TryParse(model_tb.Text, out Int32 IdModel)
                                                          || !Int32.TryParse(color_tb.Text, out Int32 IdColor)
                                                          || !Int32.TryParse(quant_tb.Text, out Int32 Quantity)
                                                          || !Decimal.TryParse(price_tb.Text, out Decimal Price))
            {
                return;
            }
            selectedItem.Id = Id;
            selectedItem.IdManufacturerCountry = IdManufacturerCountry;
            selectedItem.IdMachineMark = IdMachineMark;
            selectedItem.IdModelCar = IdModel;
            selectedItem.IdColor = IdColor;
            selectedItem.Quantity = Quantity;
            selectedItem.Price = Price;
            _context.SaveChanges();
            Load();
        }
    }
    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (Table1.SelectedItem is Product selectedItem)
        {
            _context.Products.Remove(selectedItem);
            _context.SaveChanges();
            Load();
        }
    }
    private void BtnInsert_Click(object sender, RoutedEventArgs e)
    {
        if (!Int32.TryParse(id_tb.Text, out Int32 Id) || !Int32.TryParse(counry_man_tb.Text, out Int32 IdManufacturerCountry )
                                                      || !Int32.TryParse(machine_mark_tb.Text, out Int32 IdMachineMark)
                                                      || !Int32.TryParse(model_tb.Text, out Int32 IdModel)
                                                      || !Int32.TryParse(color_tb.Text, out Int32 IdColor)
                                                      || !Int32.TryParse(quant_tb.Text, out Int32 Quantity)
                                                      || !Decimal.TryParse(price_tb.Text, out Decimal Price))
        {
            return;
        }
        Product product1 = new()
        {
            Id = Id,
            IdManufacturerCountry = IdManufacturerCountry,
            IdMachineMark= IdMachineMark,
            IdModelCar = IdModel,
            IdColor= IdColor,
            //PassportSeries = Convert.ToInt32(passport_series_tb.Text),
            Quantity = Quantity,
            //PassportNumber = Convert.ToInt32(passport_number_tb.Text),
            Price = Price
        };
        _context.Products.Add(product1);
        _context.SaveChanges();
        Load();
    }
    private void Table1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Table1.SelectedItem is Product selectedItem)
        {
            id_tb.Text = selectedItem.Id.ToString();
            counry_man_tb.Text = selectedItem.IdManufacturerCountry.ToString();
            machine_mark_tb.Text = selectedItem.IdMachineMark.ToString();
            model_tb.Text = selectedItem.IdModelCar.ToString();
            color_tb.Text = selectedItem.IdColor.ToString();
            quant_tb.Text = selectedItem.Quantity.ToString();
            price_tb.Text = selectedItem.Price.ToString();
        }
    }
    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
}