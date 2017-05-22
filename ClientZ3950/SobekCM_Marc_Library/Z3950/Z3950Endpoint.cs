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

using System;

namespace SobekCM_Marc_Library.Z3950
{
    /// <summary> Class stores information regarding a Z39.50 endpoint associated with this user </summary>
    public class Z3950Endpoint
    {
        /// <summary> Arbitrary name associated with this Z39.50 endpoint by the user</summary>
        public string Name { get; set; }

        /// <summary> Port for the connection to the Z39.50 endpoint </summary>
        public uint Port { get; set; }

        /// <summary> Name of the database within the Z39.50 endpoint </summary>
        public string DatabaseName { get; set; }

        /// <summary> URI / URL for the connection to the Z39.50 endpoint </summary>
        public string Uri { get; set; }

        /// <summary> Username for the connection to the endpoint, if one is needed </summary>
        public string Username { get; set; }

        /// <summary> Password for the connection to the endpoint, if one is needed </summary>
        public string Password { get; set; }

        /// <summary> Flag indicates if the password should be saved for this connection to the user's 
        /// personal settings </summary>
        public bool SavePasswordFlag { get; set; }

        /// <summary> Constructor for a new instance of the Z39.50 endpoint object </summary>
        /// <param name="name"> Arbitrary name associated with this Z39.50 endpoint by the user </param>
        /// <param name="uri"> URI / URL for the connection to the Z39.50 endpoint </param>
        /// <param name="port"> Port for the connection to the Z39.50 endpoint </param>
        /// <param name="databaseName"> Name of the database within the Z39.50 endpoint  </param>
        public Z3950Endpoint(string name, string uri, uint port, string databaseName)
        {
            this.Name = name;
            this.Uri = uri;
            this.Port = port;
            this.DatabaseName = databaseName;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.SavePasswordFlag = false;
        }

        /// <summary> Constructor for a new instance of the Z39.50 endpoint object </summary>
        public Z3950Endpoint()
        {
            this.Name = string.Empty;
            this.Uri = string.Empty;
            this.Port = 0;
            this.DatabaseName = string.Empty;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.SavePasswordFlag = false;
        }

        /// <summary> Constructor for a new instance of the Z39.50 endpoint object </summary>
        /// <param name="name"> Arbitrary name associated with this Z39.50 endpoint by the user </param>
        /// <param name="uri"> URI / URL for the connection to the Z39.50 endpoint </param>
        /// <param name="port"> Port for the connection to the Z39.50 endpoint </param>
        /// <param name="databaseName"> Name of the database within the Z39.50 endpoint  </param>
        /// <param name="username"> Username for the connection to the endpoint, if one is needed </param>
        public Z3950Endpoint(string name, string uri, uint port, string databaseName, string username)
        {
            this.Name = name;
            this.Uri = uri;
            this.Port = port;
            this.DatabaseName = databaseName;
            this.Username = username;
            this.Password = string.Empty;
            this.SavePasswordFlag = false;
        }

        /// <summary> Create a copy of this object </summary>
        /// <returns> Copy of this object with all the same data </returns>
        public Z3950Endpoint Copy()
        {
            var copyPoint = new Z3950Endpoint(Name, Uri, Port, DatabaseName, Username)
            {
                Password = Password,
                SavePasswordFlag = SavePasswordFlag
            };
            return copyPoint;
        }
    }
}