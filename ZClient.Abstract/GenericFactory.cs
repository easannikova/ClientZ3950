using System;
using System.Configuration;

namespace ZClient.Abstract
{
    internal class GenericFactory
    {
        public static object Create(string typeName, object[] args)
        {
            if (string.IsNullOrEmpty(typeName)){
                string message 
                    = "\n\nZoom.Net was unable to create an implementation "
                    + "class that implements\n"
                    + "a given interface as it couldn't find the name of the "
                    + "class you want to\n"
                    + "use. This is usually because the Zoom.Net.Factory "
                    + "section is missing\n"
                    + "from the application configuration.\n\n";
                throw new ConfigurationErrorsException(message);
            }

            var type = Type.GetType(typeName);
            if (type == null){
                string message 
                    = "\n\n Zoom.Net was unable to create the implementation "
                    + "class: \n'" + typeName + "'.\n"
                    + "This is usually because the implementation assembly "
                    + "is not acessible for \nthe executable binary. \n\n";
                throw new ConfigurationErrorsException(message);
            }
            var created = Activator.CreateInstance(type, args);
            return created;
        }
    }
}
