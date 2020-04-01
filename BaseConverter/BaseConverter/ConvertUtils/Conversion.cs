using System.ComponentModel;

namespace BaseConverter.ConvertUtils
{
    internal enum Conversion
    {
        [Description("Unary To Binary")]
        UnaryToBinary,

        [Description("Unary To Decimal")]
        UnaryToDecimal,

        [Description("Unary To Hexadecimal")]
        UnaryToHexadecimal,

        [Description("Binary To Unary")]
        BinaryToUnary,

        [Description("Binary To Decimal")]
        BinaryToDecimal,

        [Description("Binary To Hexadecimal")]
        BinaryToHexadecimal,

        [Description("Decimal To Unary")]
        DecimalToUnary,

        [Description("Decimal To Binary")]
        DecimalToBinary,

        [Description("Decimal To Hexadecimal")]
        DecimalToHexadecimal,

        [Description("Hexadecimal To Unary")]
        HexadecimalToUnary,

        [Description("Hexadecimal To Binary")]
        HexadecimalToBinary,

        [Description("Hexadecimal To Decimal")]
        HexadecimalToDecimal
    }
}