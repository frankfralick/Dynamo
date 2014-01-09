﻿using System;
using Inventor;
using System.Collections.Generic;

using Point = Autodesk.DesignScript.Geometry.Point;

namespace DSInventorNodes.ModulePlacement
{	
	public class OccurrenceList
	{
        List<ComponentOccurrence> oOccList = new List<ComponentOccurrence>();
        ApprenticeServerDocument oTargetAssDoc;

		public OccurrenceList(ApprenticeServer appServ, ApprenticeServerDocument assDoc)
		{
            oTargetAssDoc = assDoc;
			ComponentOccurrences oAllOccs;
			oAllOccs = oTargetAssDoc.ComponentDefinition.Occurrences; 
			EvaluateOccurrences(oAllOccs);
		}

		public void EvaluateOccurrences(ComponentOccurrences oOccs)
		{
			ComponentOccurrences oOccCol = oOccs;
			for (int i = 0; i < oOccCol.Count; i++) {
				if (oOccCol[i+1].DefinitionDocumentType == DocumentTypeEnum.kAssemblyDocumentObject){
					oOccList.Add(oOccCol[i+1]);
					EvaluateOccurrences((ComponentOccurrences)oOccCol[i+1].SubOccurrences);
				}
				
				else if (oOccCol[i+1].DefinitionDocumentType != DocumentTypeEnum.kAssemblyDocumentObject) {
					oOccList.Add(oOccCol[i+1]);
				}
				
				else
                {
					continue;
				}
			}
		}

		public List<ComponentOccurrence> Items
		{
			get
            {
				return oOccList;
			}

		}

		public ApprenticeServerDocument TargetAssembly
		{
			get
            {
				return oTargetAssDoc;
			}
		}

		public void CloseTargetAssembly()
		{
				oTargetAssDoc.Close();	
		}
	}
}
