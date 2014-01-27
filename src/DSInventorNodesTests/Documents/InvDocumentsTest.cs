using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Inventor;
using DSInventorNodes;
using DSInventorNodes.API;
using InventorServices.Persistence;


namespace DSInventorNodesTests.Documents
{
    [TestFixture]
    public class InvDocumentsTest
    {
        [Test]
        public void Add_ValidArgs()
        {
            InvDocuments docs = InvDocuments.ByInvDocuments();
            string assemblyTemplateFile = @"C:\Users\Public\Documents\Autodesk\Inventor 2013\Templates\Standard.iam";
            InvDocument doc = docs.Add(InvDocumentTypeEnum.kAssemblyDocumentObject, assemblyTemplateFile, true);
            Assert.NotNull(doc);        
        }
    }
}
