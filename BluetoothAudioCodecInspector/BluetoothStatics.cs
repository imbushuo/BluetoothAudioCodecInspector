namespace BluetoothAudioCodecInspector
{
    public static class BluetoothStatics
    {
        /// <summary>
        /// BthA2DP ETW Provider ID. There are multiple, but this one emits A2DP Streaming event.
        /// </summary>
        public static Guid BthA2dpETWSessionProvider { get; } = Guid.Parse("8776ad1e-5022-4451-a566-f47e708b9075");

        /// <summary>
        /// BthA2DP ETW Provider Name.
        /// </summary>
        public const string BthA2dpETWProviderName = "Microsoft.Windows.Bluetooth.BthA2dp";

        /// <summary>
        /// BthA2DP Streaming Event Name.
        /// </summary>
        public const string BthA2dpStreaming = "A2dpStreaming";
    }
}
