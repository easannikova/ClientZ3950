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

using SobekCM_Marc_Library;

namespace SobekCMMarcLibrary
{
    /// <summary> Holds the data about a single subfield in a <see cref="MarcField"/>. <br /> <br /> </summary>
    public class MarcSubfield
    {
        /// <summary> Constructor for a new instance the MARC_Subfield class </summary>
		/// <param name="subfieldCode"> Code for this subfield in the MARC record </param>
		/// <param name="data"> Data stored for this subfield </param>
        public MarcSubfield(char subfieldCode, string data)
		{
			// Save the parameters
            this.SubfieldCode = subfieldCode;
            this.Data = data;
		}

		/// <summary> Gets the MARC subfield code associated with this data  </summary>
        public char SubfieldCode
        {
            get;  private set;
        }

		/// <summary> Gets the data associated with this MARC subfield  </summary>
        public string Data
        {
            get;   set; 
        }

        /// <summary> Returns this MARC Subfield as a string </summary>
        /// <returns> Subfield in format '|x data'.</returns>
        public override string ToString()
        {
            return "|" + SubfieldCode + " " + Data;
        }
    }
}
