
#region Using directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

#endregion

namespace USMarcLibrary
{
    /// <summary> Stores the information for a field in a MARC21 record ( <see cref="MarcRecord"/> )</summary>
    /// <remarks>Object created by Mark V Sullivan (2006) for University of Florida's Digital Library Center.</remarks>
    public class MarcField
    {
        private string _data;
        private readonly List<MarcSubfield> _subfields;

        #region Constructors

        /// <summary> Constructor for a new instance of the MARC_Field class </summary>
        public MarcField()
        {
            _subfields = new List<MarcSubfield>();

            Tag = -1;
            Indicator1 = ' ';
            Indicator2 = ' ';
        }

        /// <summary> Constructor for a new instance of the MARC_Field class </summary>
        /// <param name="tag">Tag for this data field</param>
        /// <param name="controlFieldValue">Value for this control field </param>
        public MarcField(int tag, string controlFieldValue)
        {
            _subfields = new List<MarcSubfield>();

            this.Tag = tag;
            _data = controlFieldValue;
            Indicator1 = ' ';
            Indicator2 = ' ';
        }

        /// <summary> Constructor for a new instance of the MARC_Field class </summary>
        /// <param name="tag">Tag for this data field</param>
        /// <param name="indicator1">First indicator</param>
        /// <param name="indicator2">Second indicator</param>
        public MarcField(int tag, char indicator1, char indicator2)
        {
            _subfields = new List<MarcSubfield>();

            this.Tag = tag;
            this.Indicator1 = indicator1;
            this.Indicator2 = indicator2;
        }

        /// <summary> Constructor for a new instance of the MARC_Field class </summary>
        /// <param name="tag">Tag for this data field</param>
        /// <param name="indicators">Indicators</param>
        /// <param name="controlFieldValue">Value for this control field</param>
        public MarcField(int tag, string indicators, string controlFieldValue)
        {
            _subfields = new List<MarcSubfield>();

            this.Tag = tag;
            _data = controlFieldValue;

            if (indicators.Length >= 2)
            {
                Indicator1 = indicators[0];
                Indicator2 = indicators[1];
            }
            else
            {
                if (indicators.Length == 0)
                {
                    Indicator1 = ' ';
                    Indicator2 = ' ';
                }
                if (indicators.Length == 1)
                {
                    Indicator1 = indicators[0];
                    Indicator2 = ' ';
                }
            }
        }

        #endregion

        #region Simple properties

        /// <summary> Gets or sets the data for this MARC XML field which does not exist in any subfield </summary>
        /// <remarks> This is generally used for the control fields at the beginning of the MARC record </remarks>
        public string ControlFieldValue
        {
            get
            {
                if (String.IsNullOrEmpty(_data))
                    return String.Empty;
                return _data;
            }
            set { _data = value; }
        }

        /// <summary> Gets or sets the tag for this data field </summary>
        public int Tag { get; set; }

        /// <summary> Gets or sets the first character of the indicator </summary>
        public char Indicator1 { get; set; }

        /// <summary> Gets or sets the second character of the indicator </summary>
        public char Indicator2 { get; set; }

        /// <summary> Gets or sets the complete indicator for this data field </summary>
        public string Indicators
        {
            get { return Indicator1.ToString() + Indicator2; }
            set
            {
                if (value.Length >= 2)
                {
                    Indicator1 = value[0];
                    Indicator2 = value[1];
                }
                else
                {
                    if (value.Length == 0)
                    {
                        Indicator1 = ' ';
                        Indicator2 = ' ';
                    }
                    if (value.Length == 1)
                    {
                        Indicator1 = value[0];
                        Indicator2 = ' ';
                    }
                }
            }
        }

        #endregion

        #region Methods and properties for working with subfields within this field

        /// <summary> Get the number of subfields in this data field </summary>
        public int SubfieldCount => _subfields.Count;

        /// <summary> Gets the collection of subfields in this data field </summary>
        public ReadOnlyCollection<MarcSubfield> Subfields => new ReadOnlyCollection<MarcSubfield>(_subfields);

        /// <summary> Gets the data from a particular subfield in this data field </summary>
        /// <param name="subfieldCode"> Code for the subfield in question </param>
        /// <returns>The value of the subfield, or an empty string </returns>
        /// <remarks> If there are multiple instances of this subfield, then they are returned 
        /// together with a '|' delimiter between them </remarks>
        public string this[char subfieldCode]
        {
            get
            {
                var returnValue = string.Empty;
                foreach (var subfield in _subfields)
                {
                    if (subfield.SubfieldCode == subfieldCode)
                    {
                        if (returnValue.Length == 0)
                            returnValue = subfield.Data;
                        else
                            returnValue = returnValue + "|" + subfield.Data;
                    }
                }
                return returnValue;
            }
        }

        /// <summary> Returns flag indicating if this data field has the indicated subfield </summary>
        /// <param name="subfieldCode">Code for the subfield in question</param>
        /// <returns>TRUE if the subfield exists, otherwise FALSE</returns>
        public bool has_Subfield(char subfieldCode)
        {
            return _subfields.Any(subfield => subfield.SubfieldCode == subfieldCode);
        }

        /// <summary> Adds a new subfield code to this MARC field </summary>
        /// <param name="subfieldCode"> Code for this subfield in the MARC record field </param>
        /// <param name="data"> Data stored for this subfield </param>
        public void Add_Subfield(char subfieldCode, string data)
        {
            _subfields.Add(new MarcSubfield(subfieldCode, data));
        }

        /// <summary> Adds a new subfield code to this MARC field or updates an existing subfield of the same code </summary>
        /// <param name="subfieldCode"> Code for this subfield in the MARC record field </param>
        /// <param name="data"> Data stored for this subfield </param>
        /// <remarks> This is used to replace a non-repeatable subfield with new data </remarks>
        public void Add_NonRepeatable_Subfield(char subfieldCode, string data)
        {
            // Look through existing subfields
            foreach (var subfield in _subfields)
            {
                if (subfield.SubfieldCode == subfieldCode)
                {
                    subfield.Data = data;
                    return;
                }
            }

            // Add this as a new subfield
            _subfields.Add(new MarcSubfield(subfieldCode, data));
        }

        /// <summary> Clears the list of all subfields in this field </summary>
        public void Clear_Subfields()
        {
            _subfields.Clear();
        }

        /// <summary> Gets the colleciton of subfields by subfield code </summary>
        /// <param name="subfieldCode">Code for this subfield in the MARC record field </param>
        /// <returns> Collection of subfields by subfield code </returns>
        public ReadOnlyCollection<MarcSubfield> Subfields_By_Code(char subfieldCode)
        {
            var returnValue = _subfields.Where(subfield => subfield.SubfieldCode == subfieldCode).ToList();
            return new ReadOnlyCollection<MarcSubfield>(returnValue);
        }

        /// <summary> Returns this data field as a simple string value </summary>
        /// <returns> Data field as a string </returns>
        public override string ToString()
        {
            // Build the return value
            var returnValue = new StringBuilder(Tag.ToString() + " " + Indicator1 + Indicator2 + " ");
            foreach (var thisSubfield in _subfields)
            {
                returnValue.Append(thisSubfield + " ");
            }
            return returnValue.ToString();
        }

        #endregion
    }
}
