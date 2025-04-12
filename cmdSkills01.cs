namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 01 Skills code goes here
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 01 Skills", "Got Here!");
            // create a floor level 
            Transaction t = new Transaction(doc);
            t.Start("I'm doing something in Revit");

            Level newLevel = Level.Create(doc, 10);
            newLevel.Name = "My New Level";


            // Filtered element collector for ViewFamilyType
            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            collector1.OfClass(typeof(ViewFamilyType));

            ViewFamilyType floorPlanVFT = null;
            foreach (ViewFamilyType curVFT in collector1)
            {
                if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                {
                    floorPlanVFT = curVFT;
                }
            }

            // Create a Floor Plan view
            ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
            newFloorPlan.Name = "My New Floor Plan";


            ViewFamilyType ceilingPlanVFT = null;
            foreach (ViewFamilyType curVFT in collector1)
            {
                if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                {
                    ceilingPlanVFT = curVFT;
                }
            }
            // Create a Ceiling Plan view
            ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
            newCeilingPlan.Name = "My New Ceiling Plan";

            // Get a Title Block type
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
            collector2.WhereElementIsElementType();


            // Create a Sheet
            ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
            newSheet.Name = "My New Sheet";
            newSheet.SheetNumber = "SMC-MP101";

            // Create a Viewport
            // First Create a Point
            XYZ insPoint = new XYZ();
            XYZ insPoint2 = new XYZ(1, 1.0, 0);

            Viewport newViewport = Viewport.Create(doc, newSheet.Id, newFloorPlan.Id, insPoint2);



            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
    }

}
