namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 01 Challenge code goes here
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 01 Challenge", "FizzBuzz Challenge");
            Transaction t = new Transaction(doc);
            t.Start("FizzBuzz Challenge");

            //Declare a number variable (numVar) and set it to 250
            int numVar = 250;

            //Declare a starting elevation variable (startingElev) and set it to 0
            double startingElev = 0;

            //Declare a floor height variable (floorHt) and set it to 15
            double floorHt = 15;

            //Loop through the number 1 to the number variable (numVar = 250)
            //Create a loop that counts from 1 to 250
            for (int i = 1; i <= numVar; i++)
            {
                //Declare a level elevation (levelElev)
                double levelElev = startingElev + (floorHt * i);
                    
                //Create a new level in Revit for each iteration of the loop

                Level newLevel = Level.Create(doc, levelElev);

                //Create a new level with the name "Level" + the current number in the loop
                newLevel.Name = "Challenge Level " + i.ToString();


                // Filtered element collector for ViewFamilyType
                FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                collector1.OfClass(typeof(ViewFamilyType));

                //If the number is divisible by both 3 and 5 (15), create a sheet and name it "FIZZBUZZ_#"
                if (i % 15 == 0)
                {
                // Get a Title Block type
                FilteredElementCollector collector2 = new FilteredElementCollector(doc);
                collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
                collector2.WhereElementIsElementType();

                // Create a Sheet
                ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
                newSheet.Name = "FIZZBUZZ_" + i.ToString();
                newSheet.SheetNumber = "RAB_" + i.ToString();

                // Create a Floor Plan view
                ViewFamilyType floorPlanVFT = null;
                foreach (ViewFamilyType curVFT in collector1)
                {
                    if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                    {
                        floorPlanVFT = curVFT;
                    }
                }

                ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                newFloorPlan.Name = "FIZZBUZZ_" + i.ToString();


                // Create a Viewport
                // First Create a Point
                XYZ insPoint = new XYZ(1.25, 1, 0);

                Viewport newViewport = Viewport.Create(doc, newSheet.Id, newFloorPlan.Id, insPoint);
                }

               //If the number is divisible by 3, create a floor plan and name it "FIZZ_#"
                else if (i % 3 == 0)
                {
                    // Create a Floor Plan view
                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                        }
                    }

                    ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    newFloorPlan.Name = "FIZZ_" + i.ToString();

                }
                //If the number is divisible by 5, create a ceiling plan and name it "BUZZ_#"
                else if (i % 5 == 0)
                {
                    // Create a Ceiling Plan view
                    ViewFamilyType ceilingPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector1)
                    {
                        if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                        {
                            ceilingPlanVFT = curVFT;
                        }
                    }

                    ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
                    newCeilingPlan.Name = "BUZZ_" + i.ToString();
                }


            }




            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge01";
            string buttonTitle = "Module\r01";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module01,
                Properties.Resources.Module01,
                "Module 01 Challenge");

            return myButtonData.Data;
        }
    }

}
