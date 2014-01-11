using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;

using Inventor;


namespace InventorServices.Persistence
{
    /// <summary>
    /// This class holds static references that the application needs.  
    /// </summary>
    /// 
    //TODO: This should probably not be public, but would need to be marked as friend to DSInventorNodes.
    public class DocumentManager
    {
        public static Inventor.Application InventorApplication { get; set; }

        public static AssemblyDocument ActiveAssemblyDoc { get; set; }

        public ApprenticeServerComponent ActiveApprenticeServer
        {
            get
            {
                if (apprenticeServer == null)
                {
                    apprenticeServer = new ApprenticeServerComponentClass();
                }
                return apprenticeServer;
            }

            set { value = apprenticeServer; }
        }

        //This is the name of the storage for Dynamo object bindings.
        private static string dynamoStorageName = "Dynamo";
        public static string DynamoStorageName
        {
            get { return dynamoStorageName; }
        }
    }
}
