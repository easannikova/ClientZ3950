
namespace ZClient.Library.USMarc.Parsers
{
    internal struct ParserVariableFieldData
    {
        /// <summary> Data from the field, pre-converted to Unicode encoding </summary>
        /// <remarks> This is only used while actually parsing the MARC21 record </remarks>
        public readonly string FieldData;

        /// <summary> Starting character position of the variable field relative to the Base address of data (Leader/12-16) of the record. </summary>
        public readonly short StartingPosition;

        /// <summary> Constructor for a new instance of the MARC21_Parser_Variable_Field_Data structure </summary>
        /// <param name="startingPositionValue"> Starting character position of the variable field relative to the Base address of data (Leader/12-16) of the record. </param>
        /// <param name="fieldDataValue"> Data from the field, pre-converted to Unicode encoding </param>
        public ParserVariableFieldData(short startingPositionValue, string fieldDataValue)
        {
            StartingPosition = startingPositionValue;
            FieldData = fieldDataValue;
        }
    }
}