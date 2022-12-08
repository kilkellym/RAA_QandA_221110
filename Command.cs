#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Forms = System.Windows.Forms;

#endregion

namespace RAA_QandA_221110
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //string[] fileNames = new string[0];

            //using (Forms.OpenFileDialog ofd = new Forms.OpenFileDialog())
            //{
            //    ofd.InitialDirectory = "C:\\";
            //    ofd.Multiselect = true;
            //    ofd.Filter = "RFA files (*.rfa)|*.rfa";

            //    if(ofd.ShowDialog() == Forms.DialogResult.OK)
            //    {
            //        fileNames = ofd.FileNames;
            //    }
            //}

            Form1 curForm = new Form1();
            if(curForm.ShowDialog() == Forms.DialogResult.Cancel)
                return Result.Failed;

            List<string> fileNames = curForm.GetFilenamesFromForm();
            ForgeTypeId groupType = curForm.GetGroupType();
            ForgeTypeId specType = curForm.GetSpecType();
            string paramName = curForm.GetParamName();

            if (fileNames.Count() > 0)
            {
                foreach(string file in fileNames)
                {
                    Document familyDoc = app.OpenDocumentFile(file);
                    
                    if(familyDoc.IsFamilyDocument == true)
                    {
                        Transaction t = new Transaction(familyDoc);
                        t.Start("Add parameter");

                        FamilyManager fm = familyDoc.FamilyManager;
                        FamilyParameter newParam = fm.AddParameter(paramName, groupType, specType, false);
                        fm.SetFormula(newParam, "100*5");


                        t.Commit();
                        t.Dispose();

                        familyDoc.Save();
                        familyDoc.Close();
                    }
                }
            }
            
            return Result.Succeeded;
        }
    }
}
