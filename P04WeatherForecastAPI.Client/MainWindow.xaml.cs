using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using P04WeatherForecastAPI.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace P04WeatherForecastAPI.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(MainViewModel mainViewModel)
		{
			InitializeComponent();
			DataContext = mainViewModel;
		}
    }
}