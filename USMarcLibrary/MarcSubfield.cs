
namespace USMarcLibrary
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
