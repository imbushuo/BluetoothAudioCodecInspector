namespace BluetoothAudioCodecInspector
{
    public class BluetoothA2DPCodec
    {
        public byte StandardCodecId { get; set; }
        public int VendorId { get; set; }
        public int VendorCodecId { get; set; }

        public BluetoothA2DPCodec(byte standardCodecId, int vendorId, int vendorCodecId)
        {
            StandardCodecId = standardCodecId;
            VendorId = vendorId;
            VendorCodecId = vendorCodecId;
        }

        public override string ToString()
        {
            // Standard codec
            switch (StandardCodecId)
            {
                case 0x00:
                    return "SBC";
                case 0x01:
                    return "MP3";
                case 0x02:
                    return "AAC";
                case 0x04:
                    return "ATRAC";
            }

            // Vendor codec
            if (StandardCodecId != 0xFF)
            {
                return $"Unknown Codec (Invalid Vendor): {StandardCodecId} {VendorId}:{VendorCodecId}";
            }

            // Credits: https://helgeklein.com/blog/how-to-check-which-bluetooth-a2dp-audio-codec-is-used-on-windows/
            switch (VendorId)
            {
                case 0x004F when VendorCodecId == 0x0001:
                    return "Qualcomm/CSR aptX";
                case 0x00D7 when VendorCodecId == 0x0024:
                    return "Qualcomm/CSR aptX HD";
                case 0x00D7 when VendorCodecId == 0x0002:
                case 0x000A when VendorCodecId == 0x0002:
                    return "Qualcomm/CSR aptX LL";
                case 0x000A when VendorCodecId == 0x0001:
                    return "Qualcomm/CSR FastStream";
                case 0x000A when VendorCodecId == 0x0104:
                    return "Qualcomm/CSR True Wireless Stereo v3, AAC";
                case 0x000A when VendorCodecId == 0x0105:
                    return "Qualcomm/CSR True Wireless Stereo v3, MP3";
                case 0x000A when VendorCodecId == 0x0106:
                    return "Qualcomm/CSR True Wireless Stereo v3, aptX";

                case 0x012d when VendorCodecId == 0xAA:
                    return "Sony LDAC";

                case 0x0075 when VendorCodecId == 0x0102:
                    return "Samsung HD";
                case 0x0075 when VendorCodecId == 0x0103:
                    return "Samsung Scalable Codec";

                case 0x053A when VendorCodecId == 0x484C:
                    return "Savitech LHDC";
            }

            // Vendor ID can be found at https://www.bluetooth.com/specifications/assigned-numbers/company-identifiers/
            return $"Unknown Codec: {StandardCodecId} {VendorId}:{VendorCodecId}";
        }
    }
}
