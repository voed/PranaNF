namespace PranaNF
{
    public class Prana
    {
        public static class Command
        {
            public static readonly byte[] Stop = { 0xBE, 0xEF, 0x04, 0x01 };
            public static readonly byte[] Brightness = { 0xBE, 0xEF, 0x04, 0x02 };
            public static readonly byte[] Heating = { 0xBE, 0xEF, 0x04, 0x05 };
            public static readonly byte[] EnableNightMode = { 0xBE, 0xEF, 0x04, 0x06 };
            public static readonly byte[] EnableHighSpeed = { 0xBE, 0xEF, 0x04, 0x07 };
            public static readonly byte[] FlowLock = { 0xBE, 0xEF, 0x04, 0x09 };
            public static readonly byte[] FlowOutOff = { 0xBE, 0xEF, 0x04, 0x10 };
            public static readonly byte[] SpeedOutUp = { 0xBE, 0xEF, 0x04, 0x11 };
            public static readonly byte[] SpeedOutDown = { 0xBE, 0xEF, 0x04, 0x12 };
            public static readonly byte[] WinterMode = { 0xBE, 0xEF, 0x04, 0x16 };
            public static readonly byte[] SpeedDown = { 0xBE, 0xEF, 0x04, 0x0B };
            public static readonly byte[] SpeedUp = { 0xBE, 0xEF, 0x04, 0x0C };
            public static readonly byte[] SpeedInUp = { 0xBE, 0xEF, 0x04, 0x0E };
            public static readonly byte[] SpeedInDown = { 0xBE, 0xEF, 0x04, 0x0F };
            public static readonly byte[] FLowInOff = { 0xBE, 0xEF, 0x04, 0x0D };

            public static readonly byte[] ReadState = { 0xBE, 0xEF, 0x05, 0x01, 0x00, 0x00, 0x00, 0x00, 0x5A };
            public static readonly byte[] ReadDetails = { 0xBE, 0xEF, 0x05, 0x02, 0x00, 0x00, 0x00, 0x00, 0x5A };
        }
    }
}