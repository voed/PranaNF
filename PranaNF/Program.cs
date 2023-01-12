using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.Advertisement;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;


namespace PranaNF
{
    public class Program
    {


        // Devices found by watcher
        private readonly static Hashtable s_foundDevices = new();

        // Devices to collect from. Added when connected
        private readonly static Hashtable s_dataDevices = new();
        private static BluetoothLEDevice device;

        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");

            BluetoothLEAdvertisementWatcher watcher = new();
            watcher.Received += Watcher_Received;
            watcher.ScanningMode = BluetoothLEScanningMode.Active;

            while (true)
            {
                Console.WriteLine("Starting BluetoothLEAdvertisementWatcher");
                watcher.Start();
                Thread.Sleep(30000);
                watcher.Stop();
            }
        

        // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }

        private static void Watcher_Received(BluetoothLEAdvertisementWatcher sender,
            BluetoothLEAdvertisementReceivedEventArgs args)
        {
            Console.WriteLine(
                $"Received advertisement address:{args.BluetoothAddress:X}/{args.BluetoothAddressType} Name:{args.Advertisement.LocalName}  Advert type:{args.AdvertisementType}  Services:{args.Advertisement.ServiceUuids.Length}");

            device = BluetoothLEDevice.FromBluetoothAddress(args.BluetoothAddress);
            device.GattServicesChanged += Device_GattServicesChanged;
            var sr = device.GetGattServices();
            Console.WriteLine($"Service count: {sr.Services.Length}");
        }

        private static void Device_GattServicesChanged(object sender, EventArgs e)
        {
            var device = (BluetoothLEDevice)sender;

            Console.WriteLine($"Services changed!");
        }
    }
}
