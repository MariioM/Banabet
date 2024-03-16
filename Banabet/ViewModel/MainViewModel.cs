
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BanaBet.ViewModel;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    {

    }
    [ObservableProperty]
    float precioKgBanana = 1;
    //Del forms
    [ObservableProperty]
    float apuestaMensual = 500000;
    [ObservableProperty]
    float contador;
    [ObservableProperty]
    //Del forms
    DateTime fecha_ini = new DateTime(2024, 03, 14, 15, 15, 15);
    [ObservableProperty]
    TimeSpan diferencia = new TimeSpan();


    [RelayCommand]
    async Task Tap()
    {
        while (true)
        {
            DateTime ahora = DateTime.Now;
            Diferencia = ahora - Fecha_ini;
            await Task.Delay(1000);
        }
    }
    [RelayCommand]
    void Reinicio()
    {
        Fecha_ini = DateTime.Now;
        Contador = 0;
    }
    [RelayCommand]
    async Task ContadorDinero()
    {
        float ahorro = ApuestaMensual / 43200;
        while (true)
        {
            Contador += ahorro;
            await Task.Delay(1000);
        }
    }
}