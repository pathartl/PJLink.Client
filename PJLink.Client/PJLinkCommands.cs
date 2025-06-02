using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PJLink.Client;

/// <summary>
/// Contains constants and enums for PJLink protocol commands.
/// This implementation supports PJLink Class 1 devices only.
/// For more information about PJLink Class 1 specification, visit https://pjlink.jbmia.or.jp/english/
/// </summary>
public static class PJLinkCommands
{
    /// <summary>
    /// The command prefix for PJLink Class 1 commands.
    /// All Class 1 commands must be prefixed with "%1".
    /// </summary>
    public const string CommandPrefix = "%1";
    
    /// <summary>
    /// Contains constants for power control commands.
    /// </summary>
    public static class Power
    {
        /// <summary>
        /// The power control command.
        /// </summary>
        public const string Command = "POWR";

        /// <summary>
        /// The query parameter for getting power status.
        /// </summary>
        public const string QueryParameter = "?";

        /// <summary>
        /// The parameter for powering on the projector.
        /// </summary>
        public const string PowerOn = "1";

        /// <summary>
        /// The parameter for powering off the projector.
        /// </summary>
        public const string PowerOff = "0";
    }

    /// <summary>
    /// Contains constants and enums for input control commands.
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// The input control command.
        /// </summary>
        public const string Command = "INPT";

        /// <summary>
        /// The query parameter for getting current input.
        /// </summary>
        public const string QueryParameter = "?";

        /// <summary>
        /// Represents the available input sources on the projector according to PJLink specification.
        /// </summary>
        public enum Source
        {
            /// <summary>RGB input 1 (D-SUB)</summary>
            [Display(Name = "VGA")]
            RGB_DSub = 11,
            /// <summary>RGB input 2 (5 BNC)</summary>
            [Display(Name = "RGB BNC")]
            RGB_BNC = 12,
            /// <summary>RGB input 3 (DVI-I Analog)</summary>
            [Display(Name = "DVI-I Analog")]
            RGB_DviAnalog = 13,
            /// <summary>RGB input 4 (SCART)</summary>
            [Display(Name = "RGB SCART")]
            RGB_Scart = 14,
            /// <summary>RGB input 5 (M1-DA Analog)</summary>
            [Display(Name = "M1-DA Analog")]
            RGB_M1DA = 15,
            /// <summary>RGB input 6</summary>
            RGB_6 = 16,
            /// <summary>RGB input 7</summary>
            RGB_7 = 17,
            /// <summary>RGB input 8</summary>
            RGB_8 = 18,
            /// <summary>RGB input 9</summary>
            RGB_9 = 19,

            /// <summary>Video input 1 (Composite RCA)</summary>
            [Display(Name = "Composite")]
            Video_Composite = 21,
            /// <summary>Video input 2 (Component 3 RCA)</summary>
            [Display(Name = "Component")]
            Video_Component = 22,
            /// <summary>Video input 3 (Component BNC)</summary>
            [Display(Name = "Component BNC")]
            Video_ComponentBNC = 23,
            /// <summary>Video input 4 (S-Video)</summary>
            [Display(Name = "S-Video")]
            Video_SVideo = 24,
            /// <summary>Video input 5 (D Terminal)</summary>
            [Display(Name = "D Terminal")]
            Video_DTerminal = 25,
            /// <summary>Video input 6 (SCART)</summary>
            [Display(Name = "SCART")]
            Video_Scart = 26,
            /// <summary>Video input 7</summary>
            Video_7 = 27,
            /// <summary>Video input 8</summary>
            Video_8 = 28,
            /// <summary>Video input 9</summary>
            Video_9 = 29,

            /// <summary>Digital input 1 (DVI-I Digital)</summary>
            [Display(Name = "DVI-I Digital")]
            Digital_DviDigital = 31,
            /// <summary>Digital input 2 (DVI-D)</summary>
            [Display(Name = "DVI-D")]
            Digital_DviD = 32,
            /// <summary>Digital input 3 (HDMI)</summary>
            [Display(Name = "HDMI")]
            Digital_Hdmi = 33,
            /// <summary>Digital input 4 (SDI)</summary>
            [Display(Name = "SDI")]
            Digital_Sdi = 34,
            /// <summary>Digital input 5 (iLink/FireWire)</summary>
            [Display(Name = "iLink/FireWire")]
            Digital_ILink = 35,
            /// <summary>Digital input 6 (M1-DA Digital)</summary>
            [Display(Name = "M1-DA Digital")]
            Digital_M1DA = 36,
            /// <summary>Digital input 7 (M1-D)</summary>
            [Display(Name = "M1-D")]
            Digital_M1D = 37,
            /// <summary>Digital input 8 (DisplayPort)</summary>
            [Display(Name = "DisplayPort")]
            Digital_DisplayPort = 38,
            /// <summary>Digital input 9 (Wireless HDMI)</summary>
            [Display(Name = "Wireless HDMI")]
            Digital_WirelessHdmi = 39,

            /// <summary>Storage input 1 (USB Type A)</summary>
            [Display(Name = "USB Type A")]
            Storage_Usb = 41,
            /// <summary>Storage input 2 (PC Card Type II)</summary>
            [Display(Name = "PC Card Type II")]
            Storage_PcCard = 42,
            /// <summary>Storage input 3 (CompactFlash)</summary>
            [Display(Name = "CompactFlash")]
            Storage_CompactFlash = 43,
            /// <summary>Storage input 4 (SD Card)</summary>
            [Display(Name = "SD Card")]
            Storage_SdCard = 44,
            /// <summary>Storage input 5</summary>
            Storage_5 = 45,
            /// <summary>Storage input 6</summary>
            Storage_6 = 46,
            /// <summary>Storage input 7</summary>
            Storage_7 = 47,
            /// <summary>Storage input 8</summary>
            Storage_8 = 48,
            /// <summary>Storage input 9</summary>
            Storage_9 = 49,

            /// <summary>Network input 1 (Wired LAN - RJ-45)</summary>
            [Display(Name = "Wired LAN")]
            Network_Wired = 51,
            /// <summary>Network input 2 (Wireless LAN)</summary>
            [Display(Name = "Wireless LAN")]
            Network_Wireless = 52,
            /// <summary>Network input 3 (USB Type B)</summary>
            [Display(Name = "USB Type B")]
            Network_UsbB = 53,
            /// <summary>Network input 4 (Wireless USB)</summary>
            [Display(Name = "Wireless USB")]
            Network_WirelessUsb = 54,
            /// <summary>Network input 5 (Bluetooth)</summary>
            [Display(Name = "Bluetooth")]
            Network_Bluetooth = 55,
            /// <summary>Network input 6</summary>
            Network_6 = 56,
            /// <summary>Network input 7</summary>
            Network_7 = 57,
            /// <summary>Network input 8</summary>
            Network_8 = 58,
            /// <summary>Network input 9</summary>
            Network_9 = 59
        }
    }

    /// <summary>
    /// Contains constants and enums for audio/video mute control commands.
    /// </summary>
    public static class AudioVideoMute
    {
        /// <summary>
        /// The audio/video mute control command.
        /// </summary>
        public const string Command = "AVMT";

        /// <summary>
        /// The query parameter for getting mute status.
        /// </summary>
        public const string QueryParameter = "?";
        
        /// <summary>
        /// Represents the available mute states.
        /// </summary>
        public enum MuteState
        {
            /// <summary>Video mute off</summary>
            [Display(Name = "Video Mute Off")]
            VideoMuteOff = 10,
            /// <summary>Video mute on</summary>
            [Display(Name = "Video Mute On")]
            VideoMuteOn = 11,
            /// <summary>Audio mute off</summary>
            [Display(Name = "Audio Mute Off")]
            AudioMuteOff = 20,
            /// <summary>Audio mute on</summary>
            [Display(Name = "Audio Mute On")]
            AudioMuteOn = 21,
            /// <summary>Audio and video mute off</summary>
            [Display(Name = "AV Mute Off")]
            AudioVideoMuteOff = 30,
            /// <summary>Audio and video mute on</summary>
            [Display(Name = "AV Mute On")]
            AudioVideoMuteOn = 31
        }
    }

    /// <summary>
    /// Contains constants for error status commands.
    /// </summary>
    public static class ErrorStatus
    {
        /// <summary>
        /// The error status command.
        /// </summary>
        public const string Command = "ERST";

        /// <summary>
        /// The query parameter for getting error status.
        /// </summary>
        public const string QueryParameter = "?";
    }

    /// <summary>
    /// Contains constants for lamp status commands.
    /// </summary>
    public static class LampStatus
    {
        /// <summary>
        /// The lamp status command.
        /// </summary>
        public const string Command = "LAMP";

        /// <summary>
        /// The query parameter for getting lamp status.
        /// </summary>
        public const string QueryParameter = "?";
    }

    /// <summary>
    /// Contains constants for input list commands.
    /// </summary>
    public static class InputList
    {
        /// <summary>
        /// The input list command.
        /// </summary>
        public const string Command = "INST";

        /// <summary>
        /// The query parameter for getting input list.
        /// </summary>
        public const string QueryParameter = "?";
    }

    /// <summary>
    /// Contains constants for projector name commands.
    /// </summary>
    public static class ProjectorName
    {
        /// <summary>
        /// The projector name command.
        /// </summary>
        public const string Command = "NAME";

        /// <summary>
        /// The query parameter for getting projector name.
        /// </summary>
        public const string QueryParameter = "?";
    }

    /// <summary>
    /// Contains constants for manufacturer name commands.
    /// </summary>
    public static class ManufacturerName
    {
        /// <summary>
        /// The manufacturer name command.
        /// </summary>
        public const string Command = "INF1";

        /// <summary>
        /// The query parameter for getting manufacturer name.
        /// </summary>
        public const string QueryParameter = "?";
    }

    /// <summary>
    /// Contains constants for product name commands.
    /// </summary>
    public static class ProductName
    {
        /// <summary>
        /// The product name command.
        /// </summary>
        public const string Command = "INF2";

        /// <summary>
        /// The query parameter for getting product name.
        /// </summary>
        public const string QueryParameter = "?";
    }

    /// <summary>
    /// Contains constants for other information commands.
    /// </summary>
    public static class OtherInfo
    {
        /// <summary>
        /// The other information command.
        /// </summary>
        public const string Command = "INFO";

        /// <summary>
        /// The query parameter for getting other information.
        /// </summary>
        public const string QueryParameter = "?";
    }

    /// <summary>
    /// Contains constants for class information commands.
    /// </summary>
    public static class ClassInfo
    {
        /// <summary>
        /// The class information command.
        /// </summary>
        public const string Command = "CLSS";

        /// <summary>
        /// The query parameter for getting class information.
        /// </summary>
        public const string QueryParameter = "?";
    }

    /// <summary>
    /// Contains constants for error response codes.
    /// </summary>
    public static class ErrorResponses
    {
        /// <summary>
        /// Undefined command error.
        /// </summary>
        public const string UndefinedCommand = "ERR1";

        /// <summary>
        /// Out of parameter error.
        /// </summary>
        public const string OutOfParameter = "ERR2";

        /// <summary>
        /// Unavailable time error.
        /// </summary>
        public const string UnavailableTime = "ERR3";

        /// <summary>
        /// Projector/Display failure error.
        /// </summary>
        public const string ProjectorFailure = "ERR4";

        /// <summary>
        /// Authentication error.
        /// </summary>
        public const string AuthenticationError = "ERRA";
    }
} 