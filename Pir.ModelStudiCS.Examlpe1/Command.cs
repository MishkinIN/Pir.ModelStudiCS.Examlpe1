using ApplicationServices = HostMgd.ApplicationServices;
using DatabaseServices = Teigha.DatabaseServices;
using EditorInput = HostMgd.EditorInput;
using Runtime = Teigha.Runtime;
using mdsUnitsLib;
using System;

namespace Pir.Mechanics {
    /// <summary>
    /// Представляет коменды и операции по работе с базой данных чертежа.
    /// </summary>
    public class Command  {
     /// <summary>
     /// Создает новый экземпляр класса Command.
     /// </summary>
        public Command() {
         }

         #region UI commands
 
        /// <summary>
        /// Выводит в окно свойств выбранные элементы.
        /// </summary>
        [Runtime.CommandMethod("PIRELPROP", Runtime.CommandFlags.Redraw)]
        public void ShowElementProperties() {
            try {
               {
                    DatabaseServices.ObjectId[] objectIds = SelectElementsExecute();
                    foreach (var id in objectIds) {
                        LogElementName(id);
                    }
                }
            }
            catch (Exception ex) {
                LogExeption(ex);
            }
        }



        #endregion
        #region 


        #region private procedures
        private void LogElementName(DatabaseServices.ObjectId objectId) {
            ApplicationServices.Document doc = ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (!(doc is null)) {
                DatabaseServices.Database db = doc.Database;
                if (objectId.IsValid) {
                    using (DatabaseServices.Transaction tr = db.TransactionManager.StartTransaction()) {
                        DatabaseServices.Entity entity = (DatabaseServices.Entity)tr.GetObject(objectId, DatabaseServices.OpenMode.ForRead);
                        if (entity.AcadObject is mdsUnitsLib.IMDSParametricEnt mdsParametric) { // Check that selected object have COM interface IMDSParametricEnt
                            IElement element = mdsParametric.Element; // Get COM interface IElement
                            string elName = element.Name; // Read from COM interface
                            Log($"Selected element: ObjectClass={objectId.ObjectClass}, IElement.Name={elName}.");
                        }
                    }
                }
            }
        }

        private static DatabaseServices.ObjectId[] SelectElementsExecute() {
            ApplicationServices.Document doc = ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            EditorInput.Editor editor = doc?.Editor;
            if (!(editor is null)) {
                try {
                    var options = new EditorInput.PromptSelectionOptions() {
                        MessageForAdding = "Select Model Studio CS primitive.",
                        AllowDuplicates = true,
                        RejectPaperspaceViewport = true,
                    };
                    EditorInput.PromptSelectionResult res;
                    do {
                        res = editor.GetSelection(options);

                    } while (res.Status == EditorInput.PromptStatus.Error);
                    if (res.Status == EditorInput.PromptStatus.OK) {
                        EditorInput.SelectionSet selection = res.Value;
                        if (selection.Count < 1) {
                            editor.WriteMessage ("Nothing selected.");
                        }
                        else {
                            DatabaseServices.ObjectId[] objectIds = selection.GetObjectIds();
                            return objectIds;
                        }
                    }
                }
                catch (System.Exception ex) {
                    editor?.WriteMessage(ex.Message);
                }
            }
            return Array.Empty<DatabaseServices.ObjectId>();
        }
        #endregion
       
        #endregion

        /// <summary>
        /// Out ex.Message to command line.
        /// </summary>
        private static void LogExeption(Exception ex) {
            Log(ex?.Message);
        }
        /// <summary>
        /// Out message to command line.
        /// </summary>
        private static void Log(string message) {
            ApplicationServices.Document doc = ApplicationServices.Application.DocumentManager?.MdiActiveDocument;
            EditorInput.Editor editor = doc?.Editor;
            editor?.WriteMessage(message ?? string.Empty);
        }    }
}
