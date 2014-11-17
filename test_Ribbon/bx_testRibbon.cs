using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App = Autodesk.AutoCAD.ApplicationServices;
using cad = Autodesk.AutoCAD.ApplicationServices.Application;
using Db = Autodesk.AutoCAD.DatabaseServices;
using Ed = Autodesk.AutoCAD.EditorInput;
using Gem = Autodesk.AutoCAD.Geometry;
using Rtm = Autodesk.AutoCAD.Runtime;
using Win = Autodesk.Windows;

// [assembly: Rtm.CommandClass(typeof(MyClassSerializer.Commands))]

namespace test_Ribbon
{
       
    public class Commands: Rtm.IExtensionApplication
    {

        public void Initialize()
        {
            //Ed.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("\ninitialization test start...");
            //testRibbon();
        }

        public void Terminate()
        {
            Console.WriteLine("finish!");
        }


        [Rtm.CommandMethod("bx_testRibbon")]
        public void testRibbon()
        {
            String TabName="ACAD_DLL_Ribbon";
            String TabTitle = "ACAD_DLL";
            String PanelName = "Плиты";
            String buttonName = "_button1";
            String _command = "._circle 2,2,0 4 ";
            ////acDoc.SendStringToExecute("._circle 2,2,0 4 ", true, false, false);
            AddRibbons.AddRibbon(TabName, TabTitle, PanelName, buttonName, _command);
        }


    }
}
