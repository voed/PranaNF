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



            BluetoothLEAdvertisementWatcher watcher = new() { ScanningMode = BluetoothLEScanningMode.Active};
            watcher.Received += Watcher_Received;
            watcher.ScanningMode = BluetoothLEScanningMode.Active;

            while (true)
            {
                Console.WriteLine("Starting BluetoothLEAdvertisementWatcher");
                watcher.Start();

                // Run until we have found some devices to connect to
                while (s_foundDevices.Count == 0)
                {
                    Thread.Sleep(10000);
                }
                
                Console.WriteLine("Stopping BluetoothLEAdvertisementWatcher");

                // We can't connect if watch running so stop it.
                watcher.Stop();

                Console.WriteLine($"Devices found {s_foundDevices.Count}");
                Console.WriteLine("Connecting and Reading Sensors");


                s_foundDevices.Clear();
            }
        

        // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }

        private static void Watcher_Received(BluetoothLEAdvertisementWatcher sender,
            BluetoothLEAdvertisementReceivedEventArgs args)
        {
            Guid gattService = new("0000baba-0000-1000-8000-00805f9b34fb");
            Guid gattChar = new("0000cccc-0000-1000-8000-00805f9b34fb");
            Console.WriteLine(
                $"Received advertisement address:{args.BluetoothAddress:X}/{args.BluetoothAddressType} Name:{args.Advertisement.LocalName}  Advert type:{args.AdvertisementType}  Services:{args.Advertisement.ServiceUuids.Length}");

            if (args.Advertisement.LocalName.Contains("PRANA"))
            {
                device = BluetoothLEDevice.FromBluetoothAddress(args.BluetoothAddress);
                var sr = device.GetGattServicesForUuid(gattService);
                Console.WriteLine($"{sr.Status} {sr.Services.Length}");
                if (sr.Status == GattCommunicationStatus.Success && sr.Services.Length > 0)
                {
                    
                    var srv = sr.Services[0];
                    var cr = srv.GetCharacteristicsForUuid(gattChar);
                    if (cr.Status == GattCommunicationStatus.Success && cr.Characteristics.Length > 0)
                    {
                        var chr = cr.Characteristics[0];
                        chr.ValueChanged += Chr_ValueChanged;
                        var result =
                            chr.WriteClientCharacteristicConfigurationDescriptorWithResult(
                                GattClientCharacteristicConfigurationDescriptorValue.Notify);
                        var dw = new DataWriter();
                        dw.WriteBytes(Prana.Command.ReadState);
                        chr.WriteValueWithResult(dw);
                    }
                }
            }
        }

        private static void Chr_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs valueChangedEventArgs)
        {
            var dr = DataReader.FromBuffer(valueChangedEventArgs.CharacteristicValue);
            var skip = new byte[9];
            dr.ReadBytes(skip);
            Console.WriteLine(dr.ReadInt16().ToString());
            
        }
    }
}
