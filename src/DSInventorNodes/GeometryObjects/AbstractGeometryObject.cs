using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Inventor;
using Autodesk.DesignScript.Interfaces;

using InventorServices.Persistence;
using DSInventorNodes.Containers;
using Dynamo.Models;

using System.Windows.Forms;


namespace DSInventorNodes.GeometryObjects
{
    [Browsable(false)]
    public abstract class AbstractGeometryObject 
    {
        private int _runCount;

        protected Inventor.AssemblyDocument AssemblyDocument
        {
            get { return InventorPersistenceManager.ActiveAssemblyDoc; }
        }

        public static Stack<ComponentOccurrencesContainer> ComponentOccurrencesContainers
            = new Stack<ComponentOccurrencesContainer>(new[] { new ComponentOccurrencesContainer() });

        //private List<List<byte[]>> elements
        //{
        //    get
        //    {
        //        return ComponentOccurrencesContainers.Peek()[GUID];
        //    }
        //}

        public List<byte[]> compOccKeys = new List<byte[]>();
        public List<byte[]> ComponentOccurrenceKeys
        {
            get
            { 
                return compOccKeys;
            }
            set { value = compOccKeys; }
        }

        //public IEnumerable<byte[]> AllComponentOccurrenceKeys
        //{
        //    get
        //    {
        //        return elements.SelectMany(x => x);
        //    }
        //}

        //protected override void OnEvaluate()
        //{
        //    base.OnEvaluate();
        //    _runCount++;
        //}

        /// <summary>
        /// Custom save data for your Element. 
        /// </summary>
        /// <param name="xmlDoc">The XmlDocument representing the whole workspace containing this Element.</param>
        /// <param name="nodeElement">The XmlElement representing this Element.</param>
        /// <param name="context">Why is this being called?</param>
        //protected override void SaveNode(XmlDocument xmlDoc, XmlElement nodeElement, SaveContext context)
        //{
        //    //if (InventorSettings.ActiveAssemblyDoc == null)
        //    if (DocumentManager.ActiveAssemblyDoc == null)
        //    {
        //        //InventorSettings.ActiveAssemblyDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
        //        DocumentManager.ActiveAssemblyDoc = (AssemblyDocument)DocumentManager.InventorApplication.ActiveDocument;
        //    }

        //    //If KeyContext hasn't been set ever, what does that mean? Fix this.
        //    //if (InventorSettings.KeyContextArray == null)
        //    if (ReferenceManager.KeyContextArray == null)
        //    {
        //        //if (InventorSettings.KeyContext == null)
        //        if (ReferenceManager.KeyContext == null)
        //        {
        //            //InventorSettings.KeyContext = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
        //            ReferenceManager.KeyContext = DocumentManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
        //        }
        //        byte[] keyContextArray = new byte[] { };
        //        //InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.SaveContextToArray((int)InventorSettings.KeyContext, ref keyContextArray);
        //        //InventorSettings.KeyContextArray = keyContextArray;
        //        DocumentManager.ActiveAssemblyDoc.ReferenceKeyManager.SaveContextToArray((int)ReferenceManager.KeyContext, ref keyContextArray);
        //        ReferenceManager.KeyContextArray = keyContextArray;
        //    }
        //    try
        //    {
        //        var nodeType = Type.GetType(nodeElement.Name);
        //        //var invNodeType = typeof(InventorTransactionNode);
        //        var objectsKeysList = xmlDoc.CreateElement("objects");
        //        nodeElement.AppendChild(objectsKeysList);
        //        foreach (var key in this.ComponentOccurrenceKeys)
        //        {
        //            var objectKey = xmlDoc.CreateElement("object");
        //            objectsKeysList.AppendChild(objectKey);
        //            string keyString = Convert.ToBase64String(key);
        //            //string contextString = Convert.ToBase64String(InventorSettings.KeyContextArray);
        //            string contextString = Convert.ToBase64String(ReferenceManager.KeyContextArray);
        //            objectKey.SetAttribute("context", contextString);
        //            objectKey.SetAttribute("key", keyString);
        //        }
                
        //    }

        //    catch (Exception y)
        //    {
        //        System.Windows.Forms.MessageBox.Show(y.ToString());
        //    }  
        //}

        /// <summary>
        /// Custom data for your Element.
        /// SaveNode() in order to write the data when saved.
        /// </summary>
        /// <param name="nodeElement">The XmlNode representing this Element.</param>
        //protected override void LoadNode(XmlNode nodeElement)
        //{
        //    //if (InventorSettings.ActiveAssemblyDoc == null)
        //    if (DocumentManager.ActiveAssemblyDoc == null)
        //    {
        //        //InventorSettings.ActiveAssemblyDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
        //        DocumentManager.ActiveAssemblyDoc = (AssemblyDocument)DocumentManager.InventorApplication.ActiveDocument;
        //    }
      
        //    if (nodeElement.HasChildNodes)
        //    {
        //        foreach (XmlNode objectsNode in nodeElement.ChildNodes)
        //        {
        //            if (objectsNode.Name == "objects")
        //            {
        //                if (objectsNode.HasChildNodes)
        //                {
        //                    foreach (XmlNode objectNode in objectsNode.ChildNodes)
        //                    {
        //                        string contextString = objectNode.Attributes["context"].Value;
        //                        string keyString = objectNode.Attributes["key"].Value;
        //                        byte[] context = Convert.FromBase64String(contextString);
        //                        byte[] key = Convert.FromBase64String(keyString);
        //                        //InventorSettings.KeyContextArray = context;
        //                        ReferenceManager.KeyContextArray = context;
        //                        this.ComponentOccurrenceKeys.Add(key);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        protected void VerifyContextSettings()
        {
            //if (InventorSettings.ActiveAssemblyDoc == null)
            if (InventorPersistenceManager.ActiveAssemblyDoc == null)
            {
                //InventorSettings.ActiveAssemblyDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
                InventorPersistenceManager.ActiveAssemblyDoc = (AssemblyDocument)InventorPersistenceManager.InventorApplication.ActiveDocument;
            }

            //if (InventorSettings.KeyContext == null)
            if (ReferenceManager.KeyContext == null)
            {
                //InventorSettings.KeyContext = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                ReferenceManager.KeyContext = InventorPersistenceManager.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
            }
            
        }
    }
}
