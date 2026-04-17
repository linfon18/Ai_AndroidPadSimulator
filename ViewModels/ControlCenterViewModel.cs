using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media;

namespace AndroidPadSimulator.ViewModels;

public partial class ControlCenterViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isWifiOn = true;

    [ObservableProperty]
    private bool _isBluetoothOn = true;

    [ObservableProperty]
    private bool _isMobileDataOn = true;

    [ObservableProperty]
    private bool _isAirplaneModeOn = false;

    [ObservableProperty]
    private bool _isFlashlightOn = false;

    [ObservableProperty]
    private bool _isRotationOn = true;

    [ObservableProperty]
    private bool _isLocationOn = true;

    [ObservableProperty]
    private bool _isNfcOn = false;

    [ObservableProperty]
    private int _brightness = 80;

    [ObservableProperty]
    private int _volume = 60;

    public string WifiColor => IsWifiOn ? "#FF2196F3" : "#40FFFFFF";
    public string BluetoothColor => IsBluetoothOn ? "#FF2196F3" : "#40FFFFFF";
    public string MobileDataColor => IsMobileDataOn ? "#FF4CAF50" : "#40FFFFFF";
    public string AirplaneColor => IsAirplaneModeOn ? "#FFFF9800" : "#40FFFFFF";
    public string FlashlightColor => IsFlashlightOn ? "#FFFFEB3B" : "#40FFFFFF";
    public string RotationColor => IsRotationOn ? "#FF9C27B0" : "#40FFFFFF";
    public string LocationColor => IsLocationOn ? "#FFF44336" : "#40FFFFFF";
    public string NfcColor => IsNfcOn ? "#FF00BCD4" : "#40FFFFFF";

    partial void OnIsWifiOnChanged(bool value) => OnPropertyChanged(nameof(WifiColor));
    partial void OnIsBluetoothOnChanged(bool value) => OnPropertyChanged(nameof(BluetoothColor));
    partial void OnIsMobileDataOnChanged(bool value) => OnPropertyChanged(nameof(MobileDataColor));
    partial void OnIsAirplaneModeOnChanged(bool value) => OnPropertyChanged(nameof(AirplaneColor));
    partial void OnIsFlashlightOnChanged(bool value) => OnPropertyChanged(nameof(FlashlightColor));
    partial void OnIsRotationOnChanged(bool value) => OnPropertyChanged(nameof(RotationColor));
    partial void OnIsLocationOnChanged(bool value) => OnPropertyChanged(nameof(LocationColor));
    partial void OnIsNfcOnChanged(bool value) => OnPropertyChanged(nameof(NfcColor));

    [RelayCommand]
    private void ToggleWifi() => IsWifiOn = !IsWifiOn;

    [RelayCommand]
    private void ToggleBluetooth() => IsBluetoothOn = !IsBluetoothOn;

    [RelayCommand]
    private void ToggleMobileData() => IsMobileDataOn = !IsMobileDataOn;

    [RelayCommand]
    private void ToggleAirplaneMode() => IsAirplaneModeOn = !IsAirplaneModeOn;

    [RelayCommand]
    private void ToggleFlashlight() => IsFlashlightOn = !IsFlashlightOn;

    [RelayCommand]
    private void ToggleRotation() => IsRotationOn = !IsRotationOn;

    [RelayCommand]
    private void ToggleLocation() => IsLocationOn = !IsLocationOn;

    [RelayCommand]
    private void ToggleNfc() => IsNfcOn = !IsNfcOn;
}
