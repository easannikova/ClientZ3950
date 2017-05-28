
namespace USMarcLibrary.Z3950
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