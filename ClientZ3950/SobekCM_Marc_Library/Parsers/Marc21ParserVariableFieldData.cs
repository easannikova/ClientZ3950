#region License and Copyright

//          SobekCM MARC Library ( Version 1.2 )
//          
//          Copyright (2005-2012) Mark Sullivan. ( Mark.V.Sullivan@gmail.com )
//          
//          This file is part of SobekCM MARC Library.
//          
//          SobekCM MARC Library is free software: you can redistribute it and/or modify
//          it under the terms of the GNU Lesser Public License as published by
//          the Free Software Foundation, either version 3 of the License, or
//          (at your option) any later version.
//            
//          SobekCM MARC Library is distributed in the hope that it will be useful,
//          but WITHOUT ANY WARRANTY; without even the implied warranty of
//          MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//          GNU Lesser Public License for more details.
//            
//          You should have received a copy of the GNU Lesser Public License
//          along with SobekCM MARC Library.  If not, see <http://www.gnu.org/licenses/>.


#endregion

namespace SobekCM_Marc_Library.Parsers
{
    internal struct Marc21ParserVariableFieldData
    {
        /// <summary> Data from the field, pre-converted to Unicode encoding </summary>
        /// <remarks> This is only used while actually parsing the MARC21 record </remarks>
        public readonly string FieldData;

        /// <summary> Starting character position of the variable field relative to the Base address of data (Leader/12-16) of the record. </summary>
        public readonly short StartingPosition;

        /// <summary> Constructor for a new instance of the MARC21_Parser_Variable_Field_Data structure </summary>
        /// <param name="startingPositionValue"> Starting character position of the variable field relative to the Base address of data (Leader/12-16) of the record. </param>
        /// <param name="fieldDataValue"> Data from the field, pre-converted to Unicode encoding </param>
        public Marc21ParserVariableFieldData(short startingPositionValue, string fieldDataValue)
        {
            StartingPosition = startingPositionValue;
            FieldData = fieldDataValue;
        }
    }
}