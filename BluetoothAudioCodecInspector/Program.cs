using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace BluetoothAudioCodecInspector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!TraceEventSession.IsElevated() ?? false)
            {
                Console.WriteLine("ERROR: This program requires elevated privilege.");
                return;
            }

            Console.WriteLine("Note: this utility may have delay depending on the Bluetooth chipset used, A2DP streaming event may not be presented in real time.");
            Console.WriteLine("Format information may appear during audio playback session or at the end of the audio playback session.");
            Console.WriteLine("This is a Windows Bluetooth Audio Stack limitation.");
            Console.WriteLine();

            using (var bthA2dpSession = new TraceEventSession("BthA2DpInspectorSession", TraceEventSessionOptions.Create))
            {
                bthA2dpSession.StopOnDispose = true;
                Console.CancelKeyPress += (_, e) => 
                { 
                    bthA2dpSession.Dispose();
                    Environment.Exit(0);
                };
                AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
                {
                    bthA2dpSession.Dispose();
                };

                 bthA2dpSession.Source.Dynamic.AddCallbackForProviderEvent(BluetoothStatics.BthA2dpETWProviderName, BluetoothStatics.BthA2dpStreaming, e =>
                {
                    // Field #3 is A2dpStandardCodecId, #4 is Vendor ID, #5 is Vendor Codec ID
                    var codec = new BluetoothA2DPCodec((byte) e.PayloadValue(3), (int) e.PayloadValue(4), (int) e.PayloadValue(5));
                    Console.WriteLine($"A2DP Streaming event. Codec: {codec}");
                });

                bthA2dpSession.EnableProvider(BluetoothStatics.BthA2dpETWSessionProvider, TraceEventLevel.Verbose, 0);
                bthA2dpSession.Source.Process();
            }
        }
    }
}
