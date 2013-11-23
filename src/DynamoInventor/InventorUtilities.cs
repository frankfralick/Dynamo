using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVOI = Microsoft.VisualStudio.OLE.Interop;
using System.Runtime.InteropServices;
using Inventor;

using System.Windows.Forms;

namespace DynamoInventor
{
    class InventorUtilities
    {
        public static bool TryBindReferenceKey<T>(byte[] key, out T e)
        //where T :  ComponentOccurrence //how can this be constrained and work all the time
        //It is so convenient to Element as a common base in Revit.
        {
            if (InventorSettings.KeyManager == null)
            {
                //TODO Set these once, elsewhere.
                InventorSettings.ActiveAssemblyDoc = (AssemblyDocument)InventorSettings.InventorApplication.ActiveDocument;
                InventorSettings.KeyManager = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager;
            }

            try
            {
                //Whoever is responsible for Inventor API documentation is failing at their job.
                //object outType = typeof(T);
                object outType = null;
                int keyContext = InventorSettings.ActiveAssemblyDoc.ReferenceKeyManager.CreateKeyContext();
                T invObject = (T)InventorSettings.KeyManager.BindKeyToObject(ref key, keyContext, out outType);
                e = invObject;
                return invObject != null;
            }
            catch
            {
                //Can't set e to null because it might not be nullable, using default(T) instead.
                e = default(T);
                return false;
            }
        }

        //Enum flags for STGM
        [Flags]
        public enum STGM : int
        {
            DIRECT = 0x00000000,
            TRANSACTED = 0x00010000,
            SIMPLE = 0x08000000,
            READ = 0x00000000,
            WRITE = 0x00000001,
            READWRITE = 0x00000002,
            SHARE_DENY_NONE = 0x00000040,
            SHARE_DENY_READ = 0x00000030,
            SHARE_DENY_WRITE = 0x00000020,
            SHARE_EXCLUSIVE = 0x00000010,
            PRIORITY = 0x00040000,
            DELETEONRELEASE = 0x04000000,
            NOSCRATCH = 0x00100000,
            CREATE = 0x00001000,
            CONVERT = 0x00020000,
            FAILIFTHERE = 0x00000000,
            NOSNAPSHOT = 0x00200000,
            DIRECT_SWMR = 0x00400000,
        }

        public static bool CreatePrivateStorageAndStream(Document pDoc, string StorageName, string StreamName, string data)
        {
            try
            {
                //Create/get storage. "true" means if create 
                //if it does not exists.
                MVOI.IStorage pStg = (MVOI.IStorage)pDoc.GetPrivateStorage(StorageName, true);
                if (pStg == null)
                {
                    return false;
                }

                //Create stream in the storage
                MVOI.IStream pStream = null;
                pStg.CreateStream(StreamName, (uint)
                   (STGM.DIRECT | STGM.CREATE |
                  STGM.READWRITE | STGM.SHARE_EXCLUSIVE),
                    0, 0, out pStream);

                if (pStream == null)
                {
                    return false;
                }

                byte[] byteVsize = System.BitConverter.GetBytes(data.Length);

                byte[] byteVData = Encoding.Default.GetBytes(data);

                uint dummy;

                //Convert string to byte and store it to the stream
                pStream.Write(byteVsize, (uint)(sizeof(int)), out dummy);
                pStream.Write(byteVData, (uint)(byteVData.Length), out dummy);

                //Save the data          
                pStream.Commit((uint)(MVOI.STGC.STGC_OVERWRITE | MVOI.STGC.STGC_DEFAULT));

                //Don't forget to commit changes also in storage
                pStg.Commit((uint)(MVOI.STGC.STGC_DEFAULT | MVOI.STGC.STGC_OVERWRITE));

                //Force document to be dirty thus
                //the change can be saved when document 
                //is saved.
                pDoc.Dirty = true;
                pDoc.Save();

                Marshal.ReleaseComObject(pStg);

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //Read the storge and stream
        public static bool ReadPrivateStorageAndStream(Document pDoc, string StorageName, string StreamName, out string outDataStr)
        {
            outDataStr = "";
            try
            {
                //Get the storge. "false" means do not create 
                //if it does not exist
                MVOI.IStorage pStg = (MVOI.IStorage)pDoc.GetPrivateStorage(StorageName, false);

                if (pStg == null)
                {
                    return false;
                }

                //Open stream to read
                MVOI.IStream pStream = null;
                pStg.OpenStream(StreamName, IntPtr.Zero, (uint)(STGM.DIRECT | STGM.READWRITE | STGM.SHARE_EXCLUSIVE), 0, out pStream);

                if (pStream == null)
                {
                    return false;
                }

                byte[] byteVsize = new byte[16];
                uint intSize = sizeof(int);

                //Read the stream
                uint dummy;
                pStream.Read(byteVsize, (uint)intSize, out dummy);
                int lSize = System.BitConverter.ToInt16(byteVsize, 0);

                byte[] outDataByte = new byte[8192];
                pStream.Read(outDataByte, (uint)lSize, out dummy);

                //Convert byte to string
                outDataStr = Encoding.Default.GetString(outDataByte, 0, lSize);

                Marshal.ReleaseComObject(pStg);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
