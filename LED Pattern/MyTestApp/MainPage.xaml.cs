using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<int> _pinList = new List<int>();
        List<GpioPin> _openPins = new List<GpioPin>();
        DispatcherTimer _timer = new DispatcherTimer();
        int _timerSpan = 500;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _pinList.Add(24);
            _pinList.Add(25);
            _pinList.Add(26);

            InitializePins();

            _timer.Interval = TimeSpan.FromMilliseconds(_timerSpan);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, object e)
        {
            _timer.Stop();

            SetPinsState(GpioPinValue.High);
            SetPinsState(GpioPinValue.Low);

            _timer.Start();
        }

        private void SetPinsState(GpioPinValue state)
        {
            foreach (var pin in _openPins)
            {
                pin.Write(state);
                pin.SetDriveMode(GpioPinDriveMode.Output);

                var ts = TimeSpan.FromMilliseconds(_timerSpan + 500);
                var dt = DateTime.Now;

                while (DateTime.Now < dt.AddSeconds(ts.Seconds))
                {

                }
            }
        }

        private void InitializePins()
        {
            foreach (var pin in _pinList)
            {
                var gpio = GpioController.GetDefault();
                var p = gpio.OpenPin(pin);

                _openPins.Add(p);

            }
        }
    }
}
