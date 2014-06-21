using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SimpleInjector;
using SimpleInjector.Extensions;
using Container = SimpleInjector.Container;
using Inventor;

namespace InventorServices.Persistence
{
    /// <summary>
    /// This class holds static references that the application needs.  
    /// </summary>
    public class PersistenceManager
    {   //TODO Probably git rid of all this static junk.  It doesn't seem like a good idea.
        private static ApprenticeServerComponentClass apprenticeServer;

        private static Inventor.Application invApp;
        private static Container container;
        private static AssemblyDocument assDoc;
        private static PartDocument partDoc;

        public static AssemblyDocument ActiveAssemblyDoc
        {
            get
            {
                //TODO: This is not good.  The convention in the DynamoInventor is that if we don't have an active
                //assembly document, we set this to null.  This is just for RTC demo.  Change this back.
                if (assDoc == null && ActiveDocument != null && ActiveDocument.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    assDoc = (AssemblyDocument)ActiveDocument;
                }
                else if (ActiveDocument == null)
                {
                    assDoc = (AssemblyDocument)InventorApplication.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject);
                }
                return assDoc;
            }
            set { assDoc = value; }
        }

        public static DrawingDocument ActiveDrawingDoc { get; set; }

        public static PartDocument ActivePartDoc
        {
            get
            {
                //TODO: This is not good.  The convention in the DynamoInventor is that if we don't have an active
                //part document, we set this to null.  This is just for RTC demo.  Change this back.
                if (partDoc == null && ActiveDocument.DocumentType == DocumentTypeEnum.kPartDocumentObject)
                {
                    partDoc = (PartDocument)ActiveDocument;
                }
                return partDoc;
            }
            set { partDoc = value; }
        }

        public static Document ActiveDocument
        {
            get { return InventorApplication.ActiveDocument; }
        }

        public static Inventor.Application InventorApplication
        {
            get
            {
                if (invApp == null)
                {
                    try
                    {
                        invApp = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
                    }
                    catch
                    {
                        Type invAppType =  System.Type.GetTypeFromProgID("Inventor.Application");
                        invApp = (Inventor.Application)System.Activator.CreateInstance(invAppType);
                        invApp.Visible = true;
                    }
                }
                return invApp;
            }
            set { invApp = value; }
        }

        public static ApprenticeServerComponent ActiveApprenticeServer
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
                
        //This is the name of the storage for Dynamo in Inventor files.
        //Not currently using this.
        private static string dynamoStorageName = "Dynamo";

        public static string DynamoStorageName
        {
            get { return dynamoStorageName; }
        }

        //TODO: This was changed temporarily for RTC demo for running in Revit session.
        public static Container IoC 
        {
            get 
            {
                if (container == null)
                {
                    LetThereBeIoC();
                }
                return container; }
            set 
            { 
                container = value;
            }
        }

        public static void LetThereBeIoC()
        {
            container = new Container();
            IoC = container;
            IoC.Register<IModuleBinder, ModuleReferenceKeyBinder>(Lifestyle.Transient);
            IoC.Register<IObjectBinder, ReferenceKeyBinder>(Lifestyle.Transient);
            IoC.Register<IBindableObject, ModuleObject>(Lifestyle.Transient);
            IoC.Register<IContextData, ModuleContextArray>(Lifestyle.Transient);
            IoC.Register<ISerializableModuleIdManager, ModuleIdManager>(Lifestyle.Transient);
            IoC.Register<ISerializableIdManager, ObjectIdManager>(Lifestyle.Transient);

            //Batch register is not possible for ISerializableId<T> for all T because it must
            //have more than one constructor.
            IoC.Register<ISerializableId<List<Tuple<string, int, int, byte[]>>>>(() => new ModuleId());
            IoC.Register<ISerializableId<byte[]>>(() => new ObjectId());
            IoC.Register<IContextManager, ModuleContextManager>(Lifestyle.Transient);
        }

        public static void ResetOnDocumentActivate(_Document activeDoc)
        {
            try
            {
                if (activeDoc.DocumentType == DocumentTypeEnum.kAssemblyDocumentObject)
                {
                    PersistenceManager.ActiveAssemblyDoc = (AssemblyDocument)activeDoc;
                    ReferenceManager.KeyManager = PersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager;
                }

                else if (activeDoc.DocumentType == DocumentTypeEnum.kDrawingDocumentObject)
                {
                    PersistenceManager.ActiveDrawingDoc = (DrawingDocument)activeDoc;
                    ReferenceManager.KeyManager = PersistenceManager.ActiveDrawingDoc.ReferenceKeyManager;
                }

                else if (activeDoc.DocumentType == DocumentTypeEnum.kPartDocumentObject)
                {
                    PersistenceManager.ActivePartDoc = (PartDocument)activeDoc;
                    ReferenceManager.KeyManager = PersistenceManager.ActivePartDoc.ReferenceKeyManager;
                }

                else
                {
                    ResetOnDocumentDeactivate();
                }
            }
            catch (Exception e)
            {
                throw;
            } 
        }

        public static void ResetOnDocumentDeactivate()
        {
            ActiveAssemblyDoc = null;
            ActiveDrawingDoc = null;
            ActivePartDoc = null;
        }
    }
}
