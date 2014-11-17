using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App = Autodesk.AutoCAD.ApplicationServices;
using cad = Autodesk.AutoCAD.ApplicationServices.Application;
using Ed = Autodesk.AutoCAD.EditorInput;
using Win = Autodesk.Windows;


namespace test_Ribbon
{
    public class AddRibbons
    {
        private static String _command;


        public static void AddRibbon(String TabName,
                               String TabTitle, 
                               String PanelName, 
                               String buttonName,
                               String Command)
        {
            //TabName="ACAD_DLL_Ribbon";
            //TabTitle = "ACAD_DLL";
            //PanelName= "Плиты";
            //buttonName ="_button1";
            _command = Command;


            bool tabAdd = false;
            bool PanelAdd = false;

            Ed.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            // получаем указатель на ленту AutoCAD
            Win.RibbonControl rbCtrl = Win.ComponentManager.Ribbon;
            Win.RibbonTab rbTab = rbCtrl.FindTab(TabName);
            if (rbTab == null)
            {
                // создаем вкладку
                tabAdd = true;
                rbTab = new Win.RibbonTab();
                rbTab.Title = TabTitle;
                rbTab.Id = TabName;
            }

            Win.RibbonPanel rbPanel = rbTab.FindPanel(PanelName);
            Win.RibbonPanelSource rbPanelSource = new Win.RibbonPanelSource();

            if (rbPanel == null)
            {
                // создаем контейнер для элементов
                //Win.RibbonPanelSource rbPanelSource = new Win.RibbonPanelSource();
                PanelAdd = true;
                rbPanelSource.Title = PanelName;
                rbPanelSource.Id = PanelName;
                // добавляем в контейнер элементы управления
                //rbPanelSource.Items.Add(comboBox1);
                //rbPanelSource.Items.Add(new Win.RibbonSeparator());
                //rbPanelSource.Items.Add(button1);

                // создаем панель
                rbPanel = new Win.RibbonPanel();
                // добавляем на панель контейнер для элементов
                rbPanel.Source = rbPanelSource;
            }

            Win.RibbonButton button1 = (Win.RibbonButton)rbPanel.FindItem(buttonName);
            if (button1 == null)
            {
                // создаем кнопку
                button1 = new Win.RibbonButton();
                button1.Id = buttonName;
                button1.Text = buttonName;
                button1.ShowText = true;
                button1.Orientation = System.Windows.Controls.Orientation.Vertical;
                button1.Name = buttonName;
                button1.Description = buttonName + "\nКомманда: " + _command;

                // привязываем к кнопке обработчик нажатия
                button1.CommandHandler = new CommandHandler_Button1();

                rbPanelSource.Items.Add(button1);

                if (PanelAdd)
                {
                    // добавляем на вкладку панель
                    rbTab.Panels.Add(rbPanel);
                }

                
                
                if(tabAdd)
                {
                // добавляем на ленту вкладку
                    rbCtrl.Tabs.Add(rbTab);
                }

                // делаем созданную вкладку активной ("выбранной")
                rbTab.IsActive = true;
            }


        }



        // обработчик нажатия кнопки
        public class CommandHandler_Button1 : System.Windows.Input.ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object param)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                App.Document acDoc = cad.DocumentManager.MdiActiveDocument;
                Ed.Editor ed = cad.DocumentManager.MdiActiveDocument.Editor;
                //acDoc.SendStringToExecute("._circle 2,2,0 4 ", true, false, false);
                acDoc.SendStringToExecute(_command, true, false, false);

                //System.Windows.MessageBox.Show("Habr!");
            }
        }

    }
}
